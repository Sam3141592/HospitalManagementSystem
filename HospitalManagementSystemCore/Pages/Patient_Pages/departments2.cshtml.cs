using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace HospitalManagementSystem.Pages.Patient_Pages
{
    public class departments2Model : PageModel
    {
        private readonly HosDB HDB;
        private readonly ILogger<departments2Model> _logger;
        public departments2Model(ILogger<departments2Model> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        public string msg { get; set; }


        public DataTable deptsPa { get; set; }

        public void OnGet()
        {
            deptsPa = HDB.getDeptsPa();
        }
    }
}
