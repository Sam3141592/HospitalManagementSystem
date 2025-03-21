using HospitalManagementSystem.Models;
using HospitalManagementSystem.Pages.Manager_Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using System.Data;

namespace HospitalManagementSystem.Pages.ADMIN
{
    public class AdminDashModel : PageModel
    {

        private readonly ILogger<AdminDashModel> _logger;
        private readonly HosDB db;
        public string sid { get; set; }

        public string mname { get; set; }
        public DataTable annc { get; set; }
        public AdminDashModel(ILogger<AdminDashModel> logger, HosDB HosDB)
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
