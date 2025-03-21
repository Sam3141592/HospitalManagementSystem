using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace HospitalManagementSystem.Pages.Labtech
{
    public class LabtechAnnouncmentsModel : PageModel
    {
        private readonly HosDB HDB;
        private readonly ILogger<LabtechAnnouncmentsModel> _logger;
        public LabtechAnnouncmentsModel(ILogger<LabtechAnnouncmentsModel> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }

        [BindProperty(SupportsGet = true)]
        [ViewData]
        public string sid { get; set; }
        public string ltname { get; set; }
        public DataTable annc { get; set; }
        public void OnGet()
        {
            annc = HDB.getAnnc();
            ltname = HDB.getStaffFnameByID(sid);
        }
    }
}
