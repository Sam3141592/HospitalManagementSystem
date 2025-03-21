using HospitalManagementSystem.Models;
using HospitalManagementSystem.Pages.Labtech;
using HospitalManagementSystem.Pages.Manager_Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace HospitalManagementSystem.Pages.Patient_Pages
{
    public class IndexModel : PageModel
    {
        private readonly HosDB HDB;
        private readonly ILogger<IndexModel> _logger;
        public IndexModel(ILogger<IndexModel> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        public string msg { get; set; }
        [BindProperty(SupportsGet = true)]
        [ViewData]
        public string pid { get; set; }
        public string pname { get; set; }
        public DataTable visits { get; set; }

        public DataTable appts { get; set; }
        public DataTable doctors { get; set; }

        public DataTable deptsPa { get; set; }
        public string apptIDToDelete { get; set; }

        public void OnGet()
        {
            pid = HttpContext.Session.GetString("pid");
            pname = HDB.getPnameByID(pid);
            visits = HDB.getVisitsPa(pid);
            appts = HDB.getApptsPa(pid);
            deptsPa = HDB.getDeptsPa();
            doctors = HDB.getDoctorsPa();

        }
        public IActionResult OnPost()
        {
            apptIDToDelete = HttpContext.Request.Form["apptid"];
            HDB.deleteAppt(apptIDToDelete);
            return RedirectToPage("/Patient_Pages/Registered_Patient");
        }
    }
}
