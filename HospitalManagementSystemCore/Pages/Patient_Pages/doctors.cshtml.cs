using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace HospitalManagementSystem.Pages.Patient_Pages
{
    public class doctorsModel : PageModel
    {
        private readonly HosDB HDB;
        private readonly ILogger<doctorsModel> _logger;
        public doctorsModel(ILogger<doctorsModel> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        public string msg { get; set; }
        [BindProperty(SupportsGet = true)]
        [ViewData]
        public string pid { get; set; }
        public string pname { get; set; }
        
        public DataTable doctors { get; set; }
        public void OnGet()
        {
            pid = HttpContext.Session.GetString("pid");
            pname = HDB.getPnameByID(pid);
            doctors = HDB.getDoctorsPa();
        }
    }
}
