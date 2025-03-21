using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;

namespace HospitalManagementSystem.Pages.Admin
{
    public class AdminEditModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public string profession { get; set; }
        [BindProperty(SupportsGet = true)]
        public string ID { get; set; }
        public Dictionary<String, Tuple<List<String>, List<String>>> professions { get; set; }
        private readonly ILogger<IndexModel> _logger;
        private readonly HosDB db;
        public DataTable Datatable { get; set; }
        public string sid { get; set; }
        public string mname { get; set; }
        
         public AdminEditModel(ILogger<IndexModel> logger, HosDB db)
        {
            
            _logger = logger;
            this.db = db;
            professions = new Dictionary<String, Tuple<List<String>, List<String>>>()
            {
                {
                    "Doctor",
                    new Tuple<List<String>, List<String>>(
                    new List<String>(){"First Name","Last Name","Staff ID","National ID","Department Number","Age","Gender","Street","City","Governorate","Phone Number","Password","Staff Email"},
                    new List<String>(){"first-name","last-name","staff-id","national-id","department-num","age","gender","street","city","governorate","phone-number", "password","staff-email" }
                )
            },
                {
                    "Nurse",
                    new Tuple<List<String>, List < String >>(
                    new List<String>(){"First Name","Last Name","Staff ID","National ID","Department Number","Age","Gender", "Phone Number", "Street","City","Governorate","Password","Staff Email"},
                    new List<String>(){"first-name","last-name","staff-id","national-id","department-num","age","gender", "phone-number", "street","city","governorate", "password" ,"staff-email" }
                )
            },
                {
                    "Pharmacist",
                    new Tuple<List<String>, List < String >>(
                    new List<String>(){"First Name","Last Name","Staff ID","National ID","Age","Gender", "Phone Number", "Street","City","Governorate","Password","Staff Email"},
                    new List<String>(){"first-name","last-name","staff-id","national-id","age","gender", "phone-number", "street","city","governorate","password","staff-email" }
                )
            },
                {
                    "LabTechnician",
                    new Tuple<List<String>, List < String >>(
                    new List<String>(){"First Name","Last Name","Staff ID","National ID","Age","Gender", "Phone Number","Street","City","Governorate","Password","Staff Email"},
                    new List<String>(){"first-name","last-name","staff-id","national-id","age","gender", "phone-number", "street","city","governorate", "password" ,"staff-email" }
                )
            },
                {
                    "Department",
                    new Tuple<List<String>, List < String >>(
                    new List<String>(){"Name", "Specialty","Department Number"},
                    new List<String>(){"name", "specialty","department-number" }
                )
            },
                {
                    "Patient",
                    new Tuple<List<String>, List < String >>(
                    new List<String>(){"First Name","Last Name","Patient ID","National ID","Age","Gender","Street","City","Governorate","Phone Number","Password","Patient Email"},
                    new List<String>(){"first-name","last-name","patient-id","national-id","age","gender","street","city","governorate","phone-number", "password" ,"patient-email" }
                )
            },
                {
                    "NURSE_SERVE_AT",
                    new Tuple<List<String>, List < String >>(
                    new List<String>(){"Room ID","Nurse ID","Working Day","Start Hour","End Hour"},
                    new List<String>(){"room-id","nurse-id","working-day","start-hour","end-hour" }
                )
            },
                {
                    "DOCTOR_WORK_AT",
                    new Tuple<List<String>, List < String >>(
                    new List<String>(){"Room ID","Doctor ID","Working Day","Start Hour","End Hour"},
                    new List<String>(){"room-id","doctor-id","working-day","start-hour","end-hour" }
                )
            },
        };
        }
        public void OnGet(string profession, string ID)
        {
            sid = HttpContext.Session.GetString("sid");
            mname = db.getStaffFnameByID(sid);
            this.profession= profession;
            this.ID= ID;
            Datatable = db.GetUserAdmin(ID, profession);
        }


        public IActionResult OnPost()
        {
            List<object> list = new List<object>();
            for(int i = 0;i< professions[profession].Item2.Count; i++)
            {
                list.Add(HttpContext.Request.Form[professions[profession].Item2[i]]);
            }
            db.UpdateUserAdmin(list,profession, JsonConvert.DeserializeObject<List<string>>(ID));
            return RedirectToPage("/Admin/AdminViewTables", new { profession = this.profession });
        }
    }
}
