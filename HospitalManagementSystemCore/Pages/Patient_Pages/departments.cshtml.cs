using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace HospitalManagementSystem.Pages.Patient_Pages
{
    public class departmentsModel : PageModel
    {
        private readonly HosDB HDB;
        private readonly ILogger<departmentsModel> _logger;
        public departmentsModel(ILogger<departmentsModel> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        public string msg { get; set; }
        [BindProperty(SupportsGet = true)]
        [ViewData]
        public string pid { get; set; }
        public string pname { get; set; }

        public DataTable deptsPa { get; set; }

        public void OnGet()
        {
            pid = HttpContext.Session.GetString("pid");
            pname = HDB.getPnameByID(pid);
            deptsPa = HDB.getDeptsPa();
        }
    }
}
