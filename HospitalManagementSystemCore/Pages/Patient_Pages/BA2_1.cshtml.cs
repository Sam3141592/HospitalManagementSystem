using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace HospitalManagementSystem.Pages.Patient_Pages
{

    


    public class BA2_1Model : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string deptName { get; set; }
        public string pid { get; set; }
        public string pname { get; set; }
        private readonly HosDB HDB;
        private readonly ILogger<BA2_1Model> _logger;
        public DataTable doctorName { get; set; }
        public BA2_1Model(ILogger<BA2_1Model> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        public void OnGet()
        {
            doctorName = HDB.GetDoctorNameByDeptName(deptName);
        }
        public IActionResult OnPost()
        {
            string docName = HttpContext.Request.Form["select"];
            return RedirectToPage("BA2_2", new { deptName = deptName, docName=docName });
        }
    }
}
