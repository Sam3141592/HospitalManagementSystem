using HospitalManagementSystem.Models;
using HospitalManagementSystem.Pages.Labtech;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace HospitalManagementSystem.Pages
{
    public class NURSEDashModel : PageModel
    {
        private readonly HosDB HDB;
        private readonly ILogger<NURSEDashModel> _logger;
        public NURSEDashModel(ILogger<NURSEDashModel> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        [BindProperty(SupportsGet = true)]
        [ViewData]
        public string sid { get; set; }
        public DataTable annc { get; set; }
        public DataTable schedule { get; set; }
        public string nurname { get; set; }
        public void OnGet()
        {
            nurname = HDB.getStaffFnameByID(sid);
            annc = HDB.getAnnc();
            schedule = HDB.getNurseSchedule(sid);

        }
    }
}
