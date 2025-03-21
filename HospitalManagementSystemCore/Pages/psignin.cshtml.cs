using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using HospitalManagementSystem.Models;
using System.Data;
using System.Net.NetworkInformation;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Pages
{
    public class psigninModel : PageModel
    {
        private readonly HosDB HDB;
        private readonly ILogger<psigninModel> _logger;

        public string msg { get; set; }

        [BindProperty]
        public string email { get; set; }
        [BindProperty]
        public string password { get; set; }

        public psigninModel(ILogger<psigninModel> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        public IActionResult OnPost()
        {   
            if ((bool)HDB.CheckPasswordPatientE(email, password))
            {
                HttpContext.Session.SetString("pid", HDB.getPatientIDByEmail(email));
                return RedirectToPage("/Patient_Pages/Registered_Patient");
            }
            else { msg = "Incorrect Credentials"; return Page(); }
        }
    }
}
