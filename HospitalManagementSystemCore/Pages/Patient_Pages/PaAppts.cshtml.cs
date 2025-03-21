using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using System.Data;
using System.Security.Cryptography;

namespace HospitalManagementSystem.Pages.Patient_Pages
{
    public class PaApptsModel : PageModel
    {
        private readonly HosDB HDB;
        private readonly ILogger<PaApptsModel> _logger;
        public PaApptsModel(ILogger<PaApptsModel> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        public string msg { get; set; }
        [BindProperty(SupportsGet = true)]
        [ViewData]
        public string pid { get; set; }
        public string pname { get; set; }
        public DataTable appts { get; set; }
        public string apptIDToDelete { get; set; }
        public void OnGet()
        {
            pid = HttpContext.Session.GetString("pid");
            pname = HDB.getPnameByID(pid);
            appts = HDB.getApptsPa(pid);
        }
        public IActionResult OnPost()
        {
            apptIDToDelete = HttpContext.Request.Form["apptid"];
            HDB.deleteAppt(apptIDToDelete);
            return RedirectToPage("/Patient_Pages/PaAppts");
        }
    }
}
