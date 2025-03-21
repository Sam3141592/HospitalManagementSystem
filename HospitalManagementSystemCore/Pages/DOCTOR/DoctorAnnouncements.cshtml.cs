using HospitalManagementSystem.Models;
using HospitalManagementSystem.Pages.Manager_Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace HospitalManagementSystem.Pages.DOCTOR
{
    public class DoctorAnnouncementsModel : PageModel
    {
        private readonly HosDB HDB;
        private readonly ILogger<DoctorAnnouncementsModel> _logger;
        public DoctorAnnouncementsModel(ILogger<DoctorAnnouncementsModel> logger, HosDB HosDB)
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
        public void OnGet()
        {
            sid = HttpContext.Session.GetString("sid");
            mname = HDB.getStaffFnameByID(sid);
            annc = HDB.getAnnc();

        }
    }
}
