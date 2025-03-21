using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace HospitalManagementSystem.Pages.Patient_Pages
{
    public class IndexModel2 : PageModel
    {
        private readonly HosDB HDB;
        private readonly ILogger<IndexModel2> _logger;
        public IndexModel2(ILogger<IndexModel2> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        public string msg { get; set; }
        [BindProperty(SupportsGet = true)]
        [ViewData]
       
        public DataTable doctors { get; set; }

        public DataTable deptsPa { get; set; }
        public void OnGet()
        {
            deptsPa = HDB.getDeptsPa();
            doctors = HDB.getDoctorsPa();
        }
    }
}
