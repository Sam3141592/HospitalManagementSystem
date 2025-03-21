using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using System.Data;

namespace HospitalManagementSystem.Pages.Manager_Pages
{
    public class ManagerAppointmentRequestsModel : PageModel
    {
        private readonly HosDB HDB;
        private readonly ILogger<ManagerAppointmentRequestsModel> _logger;
        public ManagerAppointmentRequestsModel(ILogger<ManagerAppointmentRequestsModel> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        public string msg { get; set; }
        [BindProperty(SupportsGet = true)]
        [ViewData]
        public string sid { get; set; }

        public string mname { get; set; }


        public DataTable appts { get; set; }

        public DataTable apptsToday { get; set; }
        public string deptName { get; set; }

        public string apptIDToAdd { get; set; } 
        public string diagnosis { get; set; }
        public bool isTest { get; set; }

        public string testName { get; set; }
        public IActionResult OnPostMarkAppointmenr()
        {
            //if (bool.TryParse(HttpContext.Request.Form["testNeeded"], out bool testNeededValue))
            //{
            //    isTest = testNeededValue;
            //}
            diagnosis = HttpContext.Request.Form["diagnosis"];
            apptIDToAdd = HttpContext.Request.Form["apptid"];
            testName = HttpContext.Request.Form["testname"];
             isTest = HttpContext.Request.Form["testNeeded"].ToString() == "on";

            // Use the isTest variable as needed in further logic
            // Use the isTest variable as needed in further logic

            if (isTest)
            {

                string newV = HDB.addVisit(apptIDToAdd, diagnosis);
                HDB.addTest(newV, testName);

            }
            else
            {
                string newV = HDB.addVisit(apptIDToAdd, diagnosis);

            }

            return RedirectToPage("/DOCTOR/ManagerAppointmentRequests");
        }
        public void OnGet()
        {
            string today = DateTime.Today.ToString("yyyy-MM-dd");
            sid = HttpContext.Session.GetString("sid");
            mname = HDB.getStaffFnameByID(sid);
            appts = HDB.getApptsDoc(sid);
            deptName = HDB.getDeptName(sid);
            apptsToday = HDB.getApptsToday(sid, today);

        }
    }
}
