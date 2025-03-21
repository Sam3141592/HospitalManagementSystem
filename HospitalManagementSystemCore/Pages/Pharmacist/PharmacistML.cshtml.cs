using HospitalManagementSystem.Models;
using HospitalManagementSystem.Pages.ADMIN;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using System.Data;

namespace HospitalManagementSystem.Pages.Pharmacist { 


    
public class PharmacistMLModel : PageModel
    {
    private readonly ILogger<IndexModel> _logger;
    private readonly HosDB db;
    public DataTable MedicineTable { get; set; }
    public string ID { get; set; }
    public string sid { get; set; }

    public string mname { get; set; }
        public PharmacistMLModel(ILogger<IndexModel> logger, HosDB db)
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
            string id = HttpContext.Request.Form["id"];
            db.DecreaseMedicine(name,id);
            return RedirectToPage("/Pharmacist/PharmacistML");
        }

    }
}
