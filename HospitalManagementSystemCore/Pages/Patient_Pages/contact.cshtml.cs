using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace HospitalManagementSystem.Pages.Patient_Pages
{
    public class contactModel : PageModel
    {
        private readonly HosDB HDB;
        private readonly ILogger<contactModel> _logger;
        public contactModel(ILogger<contactModel> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        public string msg { get; set; }
        [BindProperty(SupportsGet = true)]
        [ViewData]
        public string pid { get; set; }
        public string pname { get; set; }

        public void OnGet()
        {
            pid = HttpContext.Session.GetString("pid");
            pname = HDB.getPnameByID(pid);

        }
    }
}
