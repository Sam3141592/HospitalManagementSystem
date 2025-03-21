using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace HospitalManagementSystem.Pages.Pharmacist
{
    public class PharmacistAddModel : PageModel
    {

        private readonly ILogger<PharmacistAddModel> _logger;
        private readonly HosDB db;

        public string sid { get; set; }

        public string mname { get; set; }
        public PharmacistAddModel(ILogger<PharmacistAddModel> logger, HosDB db)
        {
            _logger = logger;
            this.db = db;
        }



            public void OnGet()
        {
            sid = HttpContext.Session.GetString("sid");
            mname = db.getStaffFnameByID(sid);
        }
        public IActionResult OnPost() { 
            string name = HttpContext.Request.Form["name"];
            int amount = int.Parse(HttpContext.Request.Form["amount"]);
            db.AddMedicine(name, amount);
            return Page();
        }
    }
}
