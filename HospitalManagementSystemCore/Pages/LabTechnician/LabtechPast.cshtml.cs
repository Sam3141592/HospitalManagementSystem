using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace HospitalManagementSystem.Pages.Labtech
{

    public class LabtechPrevModel : PageModel
    {
        private readonly HosDB HDB;
        private readonly ILogger<LabtechPrevModel> _logger;
        public LabtechPrevModel(ILogger<LabtechPrevModel> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        [BindProperty(SupportsGet = true)]
        [ViewData]
        public string sid { get; set; }
        public DataTable testsdone { get; set; }
        public string ltname { get; set; }
        public void OnGet()
        {
            sid = HttpContext.Session.GetString("sid");
            ltname = HDB.getStaffFnameByID(sid);
            testsdone = HDB.getTestDoneById(sid);
        }
    }
}
