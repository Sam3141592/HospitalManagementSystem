using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using System.Data;

namespace HospitalManagementSystem.Pages.ADMIN
{
    public class AdminAnnouncmentscshtmlModel : PageModel
    {
        private readonly ILogger<AdminAnnouncmentscshtmlModel> _logger;
        private readonly HosDB db;
        public string sid { get; set; }

        public DataTable annc { get; set; }


        public string mname { get; set; }
        public AdminAnnouncmentscshtmlModel(ILogger<AdminAnnouncmentscshtmlModel> logger, HosDB HosDB)
        {
            _logger = logger;
            this.db = HosDB;
        }
        public void OnGet()
        {
            sid = HttpContext.Session.GetString("sid");
            mname = db.getStaffFnameByID(sid);
            annc = db.getAnnc();
        }
    }
}
