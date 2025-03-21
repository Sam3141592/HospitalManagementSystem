using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.Data;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace HospitalManagementSystem.Pages.Patient_Pages
{
    public class GiveFeedbackModel : PageModel
    {
        private readonly HosDB HDB;
        private readonly ILogger<GiveFeedbackModel> _logger;
        public GiveFeedbackModel(ILogger<GiveFeedbackModel> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        public string msg { get; set; }
        [BindProperty(SupportsGet = true)]
        [ViewData]

        public string pname { get; set; }
        public string pid { get; set; }
        [BindProperty(SupportsGet = true)]
        [ViewData]
        public string vidFeed { get; set; }
        [Required]
        public string comment { get; set; }
        [Required]
        public int rating { get; set; }


        public void OnGet()
        {
            pid = HttpContext.Session.GetString("pid");
            pname = HDB.getPnameByID(pid);
        }
        public IActionResult OnPost()
        {
            var v = HttpContext.Request.Form["select"];
            var ratingValue = HttpContext.Request.Form["select"];
            int rating = 0;
            if (int.TryParse(ratingValue, out int parsedRating))
            {
                rating = parsedRating;
            }
            comment = HttpContext.Request.Form["message"];
            HDB.AddFeedback(vidFeed, rating, comment);
            return RedirectToPage("/Patient_Pages/Visits");
        }
    }
}
