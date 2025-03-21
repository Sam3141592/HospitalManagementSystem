using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using HospitalManagementSystem.Models;
using System.Data;
using System.Net.NetworkInformation;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Pages
{
    public class msigninModel : PageModel
    {
        private readonly HosDB HDB;
        private readonly ILogger<msigninModel> _logger;
        public string msg { get; set; }

        [BindProperty]
        public string id { get; set; }
        [BindProperty]
        public string password { get; set; }

        public msigninModel(ILogger<msigninModel> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        public IActionResult OnPost()
        {
            HttpContext.Session.SetString("sid", id);
            if ((bool)HDB.CheckPassword(id, password))
            {
                HttpContext.Session.SetString("id", id);

                if (HDB.GetUserType(id) == "DOCTOR")

                {
                    if (HDB.isDocOrMang(id) == "DOCTOR2")
                    {
                        return RedirectToPage("/" + HDB.GetUserType(id) + "/DOCTOR2Dash", new { sid = id });
                    }
                    else
                    {
                        return RedirectToPage("/" + HDB.GetUserType(id) + "/DOCTORDash", new { sid = id });
                    }
                }
                else
                {
                    return RedirectToPage("/" + HDB.GetUserType(id) + "/" + HDB.GetUserType(id) + "Dash", new { sid = id });
                }
                
            }
            else { msg = "Incorrect Credentials"; return Page(); }
        }
    }
}
