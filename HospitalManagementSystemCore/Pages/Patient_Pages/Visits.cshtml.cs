using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Security.Cryptography;

namespace HospitalManagementSystem.Pages.Patient_Pages
{
    public class RecordModel : PageModel
    {
        private readonly HosDB HDB;
        private readonly ILogger<RecordModel> _logger;
        public RecordModel(ILogger<RecordModel> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        public string msg { get; set; }
        [BindProperty(SupportsGet = true)]
        [ViewData]

        public string pname { get; set; }
        public string pid { get; set; }

        public DataTable visits { get; set; }
        public string vidFeed { get; set; }

        public void OnGet()
        {
            pid = HttpContext.Session.GetString("pid");
            pname = HDB.getPnameByID(pid);
            visits = HDB.getVisitsPa(pid);
        }        
       public IActionResult OnPost()
        {
            vidFeed = HttpContext.Request.Form["visitid"];

            return Redirect($"/Patient_Pages/GiveFeedback?vidFeed={vidFeed}");
        }
    }
}
