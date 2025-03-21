using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace HospitalManagementSystem.Pages.DOCTOR
{
    public class DoctorMLModel : PageModel
    {
        private readonly ILogger<DoctorMLModel> _logger;
        private readonly HosDB db;
        public DoctorMLModel(ILogger<DoctorMLModel> logger, HosDB db)
        {
            _logger = logger;
            this.db = db;

        }

        public DataTable MedicineTable { get; set; }
        public string ID { get; set; }
        public string sid { get; set; }

        public string mname { get; set; }
        public void OnGet()
        {
            MedicineTable = db.GetTableAdmin("Medicine");
            sid = HttpContext.Session.GetString("sid");
            mname = db.getStaffFnameByID(sid);
        }
    }
}
