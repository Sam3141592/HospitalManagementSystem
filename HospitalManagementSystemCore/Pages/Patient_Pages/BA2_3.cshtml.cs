using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Data;

namespace HospitalManagementSystem.Pages.Patient_Pages
{
    
    public class BA2_3Model : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string deptName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string docName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string date { get; set; }
        public string pid { get; set; }
        public string pname { get; set; }
        private readonly HosDB HDB;
        private readonly ILogger<BA2_3Model> _logger;
        public TimeSpan doctorStartingHours { get; set; }
        public TimeSpan doctorEndHours { get; set; }
        public string JSONWorkingDays { get; set; }

        public DataTable AppointmentsStartingHours { get; set; }

        public List<TimeSpan> OngoingAppoitmentsSchedules { get; set; }

        public List<TimeSpan> AvailabeTimeSpan { get; set; }

        public BA2_3Model(ILogger<BA2_3Model> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        private string GetDayOfWeek(string dateString)
        {
            DateTime date;
            if (DateTime.TryParseExact(dateString, "MM-dd-yyyy", null, System.Globalization.DateTimeStyles.None, out date))
            {
                return date.ToString("dddd");
            }
            else
            {
                return "Invalid Date";
            }
        }


        public static string ConvertDateFormat(string date)
        {
            DateTime parsedDate;
            if (DateTime.TryParseExact(date, "MM-dd-yyyy", null, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                return parsedDate.ToString("yyyy-MM-dd");
            }
            else
            {
                return "Invalid Date";
            }
        }

        public List<TimeSpan> ConvertDataTableToList(DataTable dataTable)
        {
            List<TimeSpan> timeSpans = dataTable.AsEnumerable()
                .Select(row => row.Field<TimeSpan>("Time_of_Appointment"))
                .ToList();

            return timeSpans;
        }

        public static List<TimeSpan> GetAvailableAppointments(TimeSpan startTime, TimeSpan endTime, List<TimeSpan> ongoingAppointments)
        {
            List<TimeSpan> availableAppointments = new List<TimeSpan>();
            int appointmentDuration = 30;
            TimeSpan currentTime = startTime;
            while (currentTime < endTime)
            {
                // Check if the current time is not within any ongoing appointments
                bool isAvailable = true;
                foreach (TimeSpan appointment in ongoingAppointments)
                {
                    TimeSpan appointmentEnd = appointment.Add(TimeSpan.FromMinutes(appointmentDuration));
                    if (currentTime >= appointment && currentTime < appointmentEnd)
                    {
                        isAvailable = false;
                        break;
                    }
                }
                if (isAvailable)
                {
                    availableAppointments.Add(currentTime);
                }
                currentTime = currentTime.Add(TimeSpan.FromMinutes(appointmentDuration));
            }
            return availableAppointments;
        }

        public void OnGet()
        {
            pid =  HttpContext.Session.GetString("pid");
            doctorStartingHours = HDB.GetDoctorWorkingStartHours(docName,GetDayOfWeek(date));
            doctorEndHours = HDB.GetDoctorWorkingEndHours(docName, GetDayOfWeek(date));
            AppointmentsStartingHours = HDB.GetAppointmentsStartingHours(docName,ConvertDateFormat(date));
            OngoingAppoitmentsSchedules = ConvertDataTableToList(AppointmentsStartingHours);
            AvailabeTimeSpan = GetAvailableAppointments(doctorStartingHours, doctorEndHours,OngoingAppoitmentsSchedules);
        }
        public IActionResult OnPost()
        {
            string piid = HttpContext.Session.GetString("pid");
            String span = HttpContext.Request.Form["select"];
            String type = HttpContext.Request.Form["selectType"];
            String notes = HttpContext.Request.Form["message"];
            HDB.addAppointment(piid,span,type,notes,docName,date);
            return RedirectToPage("BA2");
        }
    }
}
