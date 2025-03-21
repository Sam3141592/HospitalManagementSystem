using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace HospitalManagementSystem.Pages.Manager_Pages
{
    public class ManagerDepartementModel : PageModel
    {
        private readonly HosDB HDB;
        private readonly ILogger<ManagerDepartementModel> _logger;
        public ManagerDepartementModel(ILogger<ManagerDepartementModel> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }

        [BindProperty(SupportsGet = true)]
        [ViewData]
        public string sid { get; set; }
        public string mname { get; set; }
        public int deptAvgRate { get; set; }
        public DataTable deptFeedback { get; set; }
        public string deptName { get; set; }

        public int nuFeedbackDept { get; set; }
        public void OnGet()
        {
            sid = HttpContext.Session.GetString("sid");
            mname = HDB.getStaffFnameByID(sid);
            deptFeedback = HDB.getDeptFeedback(sid);
            deptName = HDB.getDeptName(sid);
            deptAvgRate = HDB.getDeptAvgRate(sid);
            nuFeedbackDept = HDB.getNuFeebackDept(sid);

        }
    }
}
