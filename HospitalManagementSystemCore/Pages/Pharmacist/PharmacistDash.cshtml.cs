using HospitalManagementSystem.Models;
using HospitalManagementSystem.Pages.ADMIN;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace HospitalManagementSystem.Pages.Pharmacist
{
    public class PharmacistDashModel : PageModel
    {

        public DataTable MedicineTable { get; set; }
        private readonly ILogger<PharmacistDashModel> _logger;
        private readonly HosDB HDB;
        public string sid { get; set; }

        public string mname { get; set; }
        public DataTable annc { get; set; }
        public PharmacistDashModel(ILogger<PharmacistDashModel> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        public void OnGet()
        {
            sid = HttpContext.Session.GetString("sid");
            mname = HDB.getStaffFnameByID(sid);
            MedicineTable = HDB.GetTableAdmin("Medicine");
            annc = HDB.getAnnc();
        }

    }
}
