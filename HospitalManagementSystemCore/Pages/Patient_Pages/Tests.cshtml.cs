using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace HospitalManagementSystem.Pages.Patient_Pages
{
    public class TestsModel : PageModel
    {
        private readonly HosDB HDB;
        private readonly ILogger<TestsModel> _logger;
        public TestsModel(ILogger<TestsModel> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        public string msg { get; set; }
        [BindProperty(SupportsGet = true)]
        [ViewData]

        public string pname { get; set; }
        public string pid { get; set; }

        public DataTable tests { get; set; }
        public void OnGet()
        {
            pid = HttpContext.Session.GetString("pid");
            pname = HDB.getPnameByID(pid);
            tests = HDB.getTestsPa(pid);
        }
        
    }
}
