using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagementSystem.Pages
{
    public class psignupModel : PageModel
    {
        private static int Pcount = 0;

        public int getCount()
        {
            return Pcount;
        }
        public int IncrementCounter()
        {
            return ++Pcount;
        }
        private readonly HosDB HDB;
        private readonly ILogger<psignupModel> _logger;
        public string msg { get; set; }

        [BindProperty]
        public string number { get; set; }
        [BindProperty]
        public string gender { get; set; }
        [BindProperty]
        public string fname { get; set; }
        [BindProperty]
        public string lname { get; set; }

        [BindProperty]
        public int age { get; set; }

        [BindProperty]
        public string governerate { get; set; }
        [BindProperty]
        public string city { get; set; }
        [BindProperty]
        public string street { get; set; }

        [BindProperty]
        public string nationalid { get; set; }

        [BindProperty]
        public string email { get; set; }
        [BindProperty]
        public string password { get; set; }

        public psignupModel(ILogger<psignupModel> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        public IActionResult OnPost()
        {
            //public void addPatient(string fname, string lname, int age, string govern, string city, string street, string nid, string email, string pass, string pid, string g, string number)

            HDB.addPatient(fname, lname, age, governerate, city, street, nationalid, email, password, "P" + Convert.ToString(IncrementCounter()), gender, number);
            HttpContext.Session.SetString("pid", "P" + Convert.ToString(getCount()));
            return RedirectToPage("/Patient_Pages/Registered_Patient");
        }
    }
}

