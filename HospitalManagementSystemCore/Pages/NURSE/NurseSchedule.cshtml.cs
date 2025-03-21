using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace HospitalManagementSystem.Pages.Nurse
{
    public class NurseScheduleModel : PageModel
    {
        private readonly HosDB HDB;
        private readonly ILogger<NurseScheduleModel> _logger;
        public NurseScheduleModel(ILogger<NurseScheduleModel> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        [BindProperty(SupportsGet = true)]
        [ViewData]
        public string sid { get; set; }

        public DataTable schedule { get; set; }
        public string nurname { get; set; }
        public void OnGet()
        {
            schedule = HDB.getNurseSchedule(sid);
            nurname = HDB.getStaffFnameByID(sid);
        }
    }
}
