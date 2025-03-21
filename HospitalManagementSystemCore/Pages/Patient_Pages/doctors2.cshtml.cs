using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace HospitalManagementSystem.Pages.Patient_Pages
{
    public class doctors2Model : PageModel
    {
        private readonly HosDB HDB;
        private readonly ILogger<doctors2Model> _logger;
        public doctors2Model(ILogger<doctors2Model> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        public DataTable doctors { get; set; }
        public void OnGet()
        {
            doctors = HDB.getDoctorsPa();
        }
    }
}
