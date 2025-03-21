using HospitalManagementSystem.Models;
using HospitalManagementSystem.Pages.Labtech;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Xml.Linq;

namespace HospitalManagementSystem.Pages.Manager_Pages
{
    public class ManagerDashboardModel : PageModel
    {
        private readonly HosDB HDB;
        private readonly ILogger<ManagerDashboardModel> _logger;
        public ManagerDashboardModel(ILogger<ManagerDashboardModel> logger, HosDB HosDB)
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

        public DataTable deptFeedback { get; set; }

        public DataTable annc { get; set; }

        public string deptName { get; set; }
        public void OnGet()
        {
            sid = HttpContext.Session.GetString("sid");
            mname = HDB.getStaffFnameByID(sid);
            nuPatients = HDB.getPatientsTreatedById(sid);
            nuFeedback = HDB.getFeedbackCount(sid);
            avgRate = HDB.getAverageRating(sid);
            appts = HDB.getApptsDoc(sid);
            visits = HDB.getVisitsDoc(sid);
            deptFeedback = HDB.getDeptFeedback(sid);
            annc = HDB.getAnnc();
            deptName = HDB.getDeptName(sid);

        }
    }
}
