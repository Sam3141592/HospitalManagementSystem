using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Data;
using System.Xml.Linq;
using Newtonsoft.Json;


namespace HospitalManagementSystem.Pages.Admin
{
    public class AdminViewTablesModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly HosDB db;
        public DataTable Datatable { get; set; }
        [BindProperty(SupportsGet = true)]
        public String profession { get; set; }
        public Dictionary<String,Tuple<List<String>,int>> professions { get; set; }

        public string sid { get; set; }
        public string mname { get; set; }

        public AdminViewTablesModel(ILogger<IndexModel> logger, HosDB db)
        {
            _logger = logger;
            this.db = db;
                   
            
            professions = new Dictionary<String, Tuple<List<String>, int>>()
            {
                {
                    "Doctor",
                    new Tuple<List<String>, int>(
                    new List<String>(){"First Name","Last Name","Staff ID","National ID","Department Number","Age","Gender","Street","City","Governorate","Phone Number", "Password", "Staff_Email"},
                    10
                )
            },
                {
                    "Nurse",
                    new Tuple<List<String>, int>(
                    new List<String>(){"First Name","Last Name","Staff ID","National ID","Department Number","Age","Gender","Street","City","Governorate","Phone Number", "Password", "Staff_Email"},
                    10
                )
            },
                {
                    "Pharmacist",
                    new Tuple<List<String>, int>(
                    new List<String>(){"First Name","Last Name","Staff ID","National ID","Age","Gender", "Phone Number", "Street","City","Governorate", "Password", "Staff_Email"},
                    10
                )
            },
                {
                    "LabTechnician",
                    new Tuple<List<String>, int>(
                    new List<String>(){"First Name","Last Name","Staff ID","National ID","Age","Gender","Street","City","Governorate","Phone Number", "Password", "Staff_Email"},
                    10
                )
            },
                {
                    "Department",
                    new Tuple<List<String>, int>(
                    new List<String>(){"Name", "Specialty", "Department Number"},
                    3
                )
            },
                {
                    "Patient",
                    new Tuple<List<String>, int>(
                    new List<String>(){"First Name","Last Name","Patient ID","National ID","Age","Gender","Street","City","Governorate","Phone Number"},
                    9
                )
            },
                {
                    "NURSE_SERVE_AT",
                    new Tuple<List<String>, int>(
                    new List<String>(){"Room ID","Nurse ID","Working Day","Start Hour","End Hour"},
                    5
                )
            },
                {
                    "DOCTOR_WORK_AT",
                    new Tuple<List<String>, int>(
                    new List<String>(){"Room ID","Doctor ID","Working Day","Start Hour","End Hour"},
                    5
                )
            },
        };
        }
        public void OnGet()
        {
            sid = HttpContext.Session.GetString("sid");
            mname = db.getStaffFnameByID(sid);
            Datatable = db.GetTableAdmin(profession);
            
        }
        public IActionResult OnPost()
        {
            string ID = HttpContext.Request.Form["selectedItem"];
            string profession = HttpContext.Request.Form["profession"];
            db.RemoveUserAdmin(JsonConvert.DeserializeObject<List<string>>(ID), profession);

            return RedirectToPage("/Admin/AdminViewTables",new { profession = this.profession });
        }
        public IActionResult OnPostEdit() {

            string ID = HttpContext.Request.Form["selectedItem"];
            string profession = HttpContext.Request.Form["profession"];
            var routeValues = new { profession = profession, ID = ID };

            return RedirectToPage("/Admin/AdminEdit", routeValues);
        }
    }
}
