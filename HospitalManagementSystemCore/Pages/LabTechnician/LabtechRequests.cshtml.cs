using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace HospitalManagementSystem.Pages.Labtech
{
    public class LabtechRequestsModel : PageModel
    {
        private readonly HosDB HDB;
        private readonly ILogger<LabtechRequestsModel> _logger;
        public LabtechRequestsModel(ILogger<LabtechRequestsModel> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }

        [BindProperty(SupportsGet = true)]
        [ViewData]
        public string sid { get; set; }
        public DataTable testsreq { get; set; }
        public string ltname { get; set; }
        public string res { get; set; }

        public string testidtodone { get; set; }

        public string date { get; set; }
        public IActionResult OnPost()
        {

            res = HttpContext.Request.Form["results"];
            testidtodone = HttpContext.Request.Form["testid"];
            date = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
            HDB.MarkTestDone(testidtodone, res, sid, date);
            return RedirectToPage("/LabTechnician/LabTechnicianDash", new { sid = this.sid });

        }
        public void OnGet()
        {
            sid = HttpContext.Session.GetString("sid");
            ltname = HDB.getStaffFnameByID(sid);
            testsreq = HDB.getTestReq();
        }
    }
}
