using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Cryptography;

namespace HospitalManagementSystem.Pages.Patient_Pages
{
    public class BA2Model : PageModel
    {
        [BindProperty(SupportsGet = true)]
        [ViewData]
        public string dept { get; set; }

        [BindProperty]
        [Required]
        public string deptSelected { get; set; }

        private readonly HosDB HDB;
        private readonly ILogger<BA2Model> _logger;

        public DataTable DataTableDeptName { get; set; }
        public DataTable DataTableDoctorName { get; set; }
        public string pid { get; set; }
        public string pname { get; set; }

        public BA2Model(ILogger<BA2Model> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }

        public void OnGet()
        {
            pname = HDB.getPnameByID(pid);
            DataTableDeptName = HDB.GetAllDeptsNames();
        }

        public IActionResult OnPost()
        {
            string deptName = HttpContext.Request.Form["select"];
            return RedirectToPage("BA2_1", new {deptName = deptName,});
        }

        
    }
}
