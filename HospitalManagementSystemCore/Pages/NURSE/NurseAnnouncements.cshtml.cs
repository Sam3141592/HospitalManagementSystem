using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace HospitalManagementSystem.Pages.Nurse
{
    public class AnnouncmentsModel : PageModel
    {
        private readonly HosDB HDB;
        private readonly ILogger<AnnouncmentsModel> _logger;
        public AnnouncmentsModel(ILogger<AnnouncmentsModel> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        [BindProperty(SupportsGet = true)]
        [ViewData]
        public string sid { get; set; }
        public DataTable annc { get; set; }

        public string nurname { get; set; }
        public void OnGet()
        {
            nurname = HDB.getStaffFnameByID(sid);
            annc = HDB.getAnnc();
        }
    }
}
