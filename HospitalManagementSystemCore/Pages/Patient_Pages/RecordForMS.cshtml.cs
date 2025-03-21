using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace HospitalManagementSystem.Pages.Patient_Pages
{
    public class RecordForMSModel : PageModel
    {
        private readonly HosDB HDB;
        private readonly ILogger<RecordForMSModel> _logger;
        public RecordForMSModel(ILogger<RecordForMSModel> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        [BindProperty(SupportsGet = true)]
        [ViewData]

        public string pname { get; set; }
        [BindProperty(SupportsGet = true)]
        [ViewData]
        public string pid { get; set; }

        public DataTable visits { get; set; }
        [BindProperty(SupportsGet = true)]
        [ViewData]
        public string sid { get; set; }
        public DataTable tests { get; set; }


        public void OnGet()
        {
            pname = HDB.getPnameByID(pid);
            visits = HDB.getVisitsPa(pid);
            tests = HDB.getTestsPa(pid);

        }
    }
}
