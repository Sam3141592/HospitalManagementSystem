using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Data;

namespace HospitalManagementSystem.Pages.Patient_Pages
{
    public class BA2_2Model : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string deptName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string docName { get; set; }
        public string pid { get; set; }
        public string pname { get; set; }
        private readonly HosDB HDB;
        private readonly ILogger<BA2_2Model> _logger;
        public DataTable doctorWorkingDay { get; set; }
        public string JSONWorkingDays { get; set; }
        public BA2_2Model(ILogger<BA2_2Model> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        public void OnGet()
        {
            doctorWorkingDay = HDB.GetWorkingDayDoctor(docName);
            JSONWorkingDays = JsonConvert.SerializeObject(doctorWorkingDay);
        }
        public IActionResult OnPost()
        {
            string date = HttpContext.Request.Form["date"];
            return RedirectToPage("BA2_3", new { deptName = deptName, docName = docName ,date = date});
        }
    }
}
