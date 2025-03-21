using HospitalManagementSystem.Models;
using HospitalManagementSystem.Pages.ADMIN;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using System.Data;

namespace HospitalManagementSystem.Pages.Pharmacist
{



    public class PharmacistAddOnExistingModel : PageModel
    {
        private readonly ILogger<PharmacistAddOnExistingModel> _logger;
        private readonly HosDB db;
        public DataTable MedicineTable { get; set; }
        public string ID { get; set; }
        public string sid { get; set; }

        public string mname { get; set; }
        public PharmacistAddOnExistingModel(ILogger<PharmacistAddOnExistingModel> logger, HosDB db)
        {
            _logger = logger;
            this.db = db;

        }

        public void OnGet()
        {

            MedicineTable = db.GetTableAdmin("Medicine");
            ID = HttpContext.Session.GetString("id");
            sid = HttpContext.Session.GetString("sid");
            mname = db.getStaffFnameByID(sid);
        }

        public IActionResult OnPost()
        {
            string name = HttpContext.Request.Form["name"];
            int amount = int.Parse(HttpContext.Request.Form["amount"]);
            db.AddStockMedicine(name, amount);
            return RedirectToPage("/Pharmacist/PharmacistML");
        }

    }
}



