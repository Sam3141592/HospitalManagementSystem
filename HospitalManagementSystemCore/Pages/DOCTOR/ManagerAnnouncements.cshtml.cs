using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace HospitalManagementSystem.Pages.Manager_Pages
{
    public class ManagerAnnouncementsModel : PageModel
    {
        private readonly HosDB HDB;
        private readonly ILogger<ManagerAnnouncementsModel> _logger;
        public ManagerAnnouncementsModel(ILogger<ManagerAnnouncementsModel> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        public string msg { get; set; }
        [BindProperty(SupportsGet = true)]
        [ViewData]
        public string sid { get; set; }

        public string mname { get; set; }

        public DataTable annc { get; set; }

        public string newAnnc { get; set; }
        public string deptName { get; set; }
        public void OnGet()
        {
            sid = HttpContext.Session.GetString("sid");
            mname = HDB.getStaffFnameByID(sid);
            annc = HDB.getAnnc();
            deptName = HDB.getDeptName(sid);

        }
        public IActionResult OnPostAddAnnc()
        {
            sid = HttpContext.Session.GetString("sid");
            newAnnc = HttpContext.Request.Form["content"];
            string date = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
            HDB.AddAnnc(sid, date, newAnnc);
            return RedirectToPage("/DOCTOR/ManagerAnnouncements");

        }
    }
}
