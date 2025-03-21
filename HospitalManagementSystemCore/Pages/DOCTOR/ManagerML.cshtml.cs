using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace HospitalManagementSystem.Pages.DOCTOR
{
    public class ManagerMLModel : PageModel
    {
        private readonly ILogger<ManagerMLModel> _logger;
        private readonly HosDB db;
        public ManagerMLModel(ILogger<ManagerMLModel> logger, HosDB db)
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
