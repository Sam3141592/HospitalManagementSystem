using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using System.Data;

namespace HospitalManagementSystem.Pages.Pharmacist
{
    public class PharmacistAnnouncementsModel : PageModel
    {
        public string sid { get; set; }

        public string mname { get; set; }
        private readonly ILogger<PharmacistAnnouncementsModel> _logger;
        private readonly HosDB HDB;
        public DataTable annc { get; set; }
        public PharmacistAnnouncementsModel(ILogger<PharmacistAnnouncementsModel> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        public void OnGet()
        {
            sid = HttpContext.Session.GetString("sid");
            mname = HDB.getStaffFnameByID(sid);
            annc = HDB.getAnnc();
        }
    }
}
