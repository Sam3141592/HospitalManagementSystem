using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace HospitalManagementSystem.Pages.Labtech
{
    public class LabtechDashModel : PageModel
    {
        private readonly HosDB HDB;
        private readonly ILogger<LabtechDashModel> _logger;
        public LabtechDashModel(ILogger<LabtechDashModel> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        public string msg { get; set; }
        [BindProperty(SupportsGet = true)]
        [ViewData]
        public string sid { get; set; }

        public string ltname { get; set; }
        public int lttests { get; set; }

        public DataTable testsreq { get; set; }
        public DataTable testsdone { get; set; }

        public DataTable annc { get; set; }

        public string res { get; set; }

        public string testidtodone { get; set; }

        public string date { get; set; }
        public string newpname { get; set; }
        public string newtname { get; set; }


        public IActionResult OnPostMarkTest()
        {
            sid = HttpContext.Session.GetString("sid");
            res = HttpContext.Request.Form["results"];
            testidtodone = HttpContext.Request.Form["testid"];
            date = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
            HDB.MarkTestDone(testidtodone, res, sid, date);
            return RedirectToPage("/LabTechnician/LabTechnicianDash");

        }
        public IActionResult OnPostAddTest()
        {
            newpname = HttpContext.Request.Form["pname"];
            newtname = HttpContext.Request.Form["tname"];
            string date = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
            HDB.AddTestOut(newpname, newtname, date);
            return RedirectToPage("/LabTechnician/LabTechnicianDash");

        }
        public void OnGet()
        {
            sid = HttpContext.Session.GetString("sid");
            ltname = HDB.getStaffFnameByID(sid);
            testsreq = HDB.getTestReq();
            testsdone = HDB.getTestDoneById(sid);
            lttests = HDB.getTestsNo(sid);
            annc = HDB.getAnnc();

        }
    }
}
