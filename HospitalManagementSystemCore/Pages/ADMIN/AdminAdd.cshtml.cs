using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagementSystem.Pages.ADMIN
{
    public class AdminAddModel : PageModel
    {
        private readonly ILogger<AdminAddModel> _logger;
        private readonly HosDB HDB;
        public string sid { get; set; }

        public string mname { get; set; }
        public AdminAddModel(ILogger<AdminAddModel> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        public void OnGet()
        {
            sid = HttpContext.Session.GetString("sid");
            mname = HDB.getStaffFnameByID(sid);
        }

            public IActionResult OnPost()
        {
            string selected = HttpContext.Request.Form["selectedItem"];
            if (string.IsNullOrEmpty(selected))
            {
                return Page();
            }
            else
            {
                List<object> list = new List<object>();
                if (selected == "doctor")
                {
                    string fName = HttpContext.Request.Form["first-name"];
                    string lName = HttpContext.Request.Form["last-name"];
                    string staffId = HttpContext.Request.Form["staff-id"];
                    string nationalId = HttpContext.Request.Form["national-id"];
                    int deptNumber = int.Parse(HttpContext.Request.Form["dept-number"]);
                    int age = int.Parse(HttpContext.Request.Form["age"]);
                    string gender = HttpContext.Request.Form["gender"];
                    char g;
                    if (gender == "male")
                    {
                        g = 'M';
                    }
                    else
                    {
                        g = 'F';
                    }
                    string street = HttpContext.Request.Form["street"];
                    string city = HttpContext.Request.Form["city"];
                    string governorate = HttpContext.Request.Form["governorate"];
                    string phone = HttpContext.Request.Form["phone"];
                    string password = HttpContext.Request.Form["password"];
                    string email = HttpContext.Request.Form["staff-email"];
                    list.Add(fName);
                    list.Add(lName);
                    list.Add(staffId);
                    list.Add(nationalId);
                    list.Add(deptNumber);
                    list.Add(age);
                    list.Add(g);
                    list.Add(street);
                    list.Add(city);
                    list.Add(governorate);
                    list.Add(phone);
                    list.Add(password);
                    list.Add(email);
                    HDB.addMedicalStaff(selected, list);
                }
                else if(selected == "nurse")
                {
                    string fName = HttpContext.Request.Form["first-name"];
                    string lName = HttpContext.Request.Form["last-name"];
                    string staffId = HttpContext.Request.Form["staff-id"];
                    string nationalId = HttpContext.Request.Form["national-id"];
                    int deptNumber = int.Parse(HttpContext.Request.Form["dept-number"]);
                    int age = int.Parse(HttpContext.Request.Form["age"]);
                    string gender = HttpContext.Request.Form["gender"];
                    char g;
                    if (gender == "male")
                    {
                        g = 'M';
                    }
                    else
                    {
                        g = 'F';
                    }
                    string street = HttpContext.Request.Form["street"];
                    string city = HttpContext.Request.Form["city"];
                    string governorate = HttpContext.Request.Form["governorate"];
                    string phone = HttpContext.Request.Form["phone"];
                    string password = HttpContext.Request.Form["password"];
                    string email = HttpContext.Request.Form["staff-email"];
                    list.Add(fName);
                    list.Add(lName);
                    list.Add(staffId);
                    list.Add(nationalId);
                    list.Add(deptNumber);
                    list.Add(age);
                    list.Add(g);
                    list.Add(phone);
                    list.Add(street);
                    list.Add(city);
                    list.Add(governorate);
                    list.Add(password);
                    list.Add(email);
                    HDB.addMedicalStaff(selected, list);
                }
                else if(selected == "pharmacist")
                {
                    string fName = HttpContext.Request.Form["first-name"];
                    string lName = HttpContext.Request.Form["last-name"];
                    string staffId = HttpContext.Request.Form["staff-id"];
                    string nationalId = HttpContext.Request.Form["national-id"];
                    int age = int.Parse(HttpContext.Request.Form["age"]);
                    string gender = HttpContext.Request.Form["gender"];
                    char g;
                    if (gender == "male")
                    {
                        g = 'M';
                    }
                    else
                    {
                        g = 'F';
                    }
                    string street = HttpContext.Request.Form["street"];
                    string city = HttpContext.Request.Form["city"];
                    string governorate = HttpContext.Request.Form["governorate"];
                    string phone = HttpContext.Request.Form["phone"];
                    string password = HttpContext.Request.Form["password"];
                    string email = HttpContext.Request.Form["staff-email"];
                    list.Add(fName);
                    list.Add(lName);
                    list.Add(staffId);
                    list.Add(nationalId);
                    list.Add(age);
                    list.Add(g);
                    list.Add(phone);
                    list.Add(street);
                    list.Add(city);
                    list.Add(governorate);
                    list.Add(password);
                    list.Add(email);
                    HDB.addMedicalStaff(selected, list);
                }
                else if (selected == "labtechnician")
                {
                    string fName = HttpContext.Request.Form["first-name"];
                    string lName = HttpContext.Request.Form["last-name"];
                    string staffId = HttpContext.Request.Form["staff-id"];
                    string nationalId = HttpContext.Request.Form["national-id"];
                    int age = int.Parse(HttpContext.Request.Form["age"]);
                    string gender = HttpContext.Request.Form["gender"];
                    char g;
                    if (gender == "male")
                    {
                        g = 'M';
                    }
                    else
                    {
                        g = 'F';
                    }
                    string street = HttpContext.Request.Form["street"];
                    string city = HttpContext.Request.Form["city"];
                    string governorate = HttpContext.Request.Form["governorate"];
                    string phone = HttpContext.Request.Form["phone"];
                    string password = HttpContext.Request.Form["password"];
                    string email = HttpContext.Request.Form["staff-email"];
                    list.Add(fName);
                    list.Add(lName);
                    list.Add(staffId);
                    list.Add(nationalId);
                    list.Add(age);
                    list.Add(g);
                    list.Add(phone);
                    list.Add(street);
                    list.Add(city);
                    list.Add(governorate);
                    list.Add(password);
                    list.Add(email);
                    HDB.addMedicalStaff(selected, list);
                }
                else if(selected == "NURSE_SERVE_AT")
                {
                    string room_id = HttpContext.Request.Form["roomid"];
                    string nurse_id = HttpContext.Request.Form["nurseid"];
                    string day = HttpContext.Request.Form["day"];
                    string start_time = HttpContext.Request.Form["start-time"];
                    string end_time = HttpContext.Request.Form["end-time"];
                    list.Add (room_id);
                    list.Add (nurse_id);
                    list.Add (day);
                    list.Add (start_time);
                    list.Add (end_time);
                    HDB.addMedicalStaff(selected, list);

                }
                else if (selected == "DOCTOR_WORK_AT")
                {
                    string room_id = HttpContext.Request.Form["roomid"];
                    string doctor_id = HttpContext.Request.Form["doctorid"];
                    string day = HttpContext.Request.Form["day"];
                    string start_time = HttpContext.Request.Form["start-time"];
                    string end_time = HttpContext.Request.Form["end-time"];
                    list.Add(room_id);
                    list.Add(doctor_id);
                    list.Add(day);
                    list.Add(start_time);
                    list.Add(end_time);
                    HDB.addMedicalStaff(selected, list);

                }
                else
                {
                    string Name = HttpContext.Request.Form["name"];
                    int deptNumber = int.Parse(HttpContext.Request.Form["dept-number"]);
                    string specialty = HttpContext.Request.Form["specialty"];
                    list.Add (Name);
                    list.Add (deptNumber);
                    list.Add (specialty);
                    HDB.addMedicalStaff(selected, list);

                }
            }
            return Page();

        }
    }
}
