using HospitalManagementSystem.Models;
using HospitalManagementSystem.Pages.Manager_Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace HospitalManagementSystem.Pages.DOCTOR
{
    public class DOCTOR2DashModel : PageModel
    {
        private readonly HosDB HDB;
        private readonly ILogger<DOCTOR2DashModel> _logger;
        public DOCTOR2DashModel(ILogger<DOCTOR2DashModel> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        public string msg { get; set; }
        [BindProperty(SupportsGet = true)]
        [ViewData]
        public string sid { get; set; }

        public string mname { get; set; }

        public int nuPatients { get; set; }

        public int nuFeedback { get; set; }

        public int avgRate { get; set; }

        public DataTable appts { get; set; }

        public DataTable visits { get; set; }

        public DataTable annc { get; set; }

        public void OnGet()
        {
            sid = HttpContext.Session.GetString("sid");
            mname = HDB.getStaffFnameByID(sid);
            nuPatients = HDB.getPatientsTreatedById(sid);
            nuFeedback = HDB.getFeedbackCount(sid);
            avgRate = HDB.getAverageRating(sid);
            appts = HDB.getApptsDoc(sid);
            visits = HDB.getVisitsDoc(sid);
            annc = HDB.getAnnc();
        }
    }
}
