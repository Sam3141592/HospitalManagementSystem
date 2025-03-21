using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using HospitalManagementSystem.Models;
using System.Data;
using System.Net.NetworkInformation;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Drawing;

namespace HospitalManagementSystem.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            return RedirectToPage("/Patient_Pages/Unregistered_Patient");
        }
    }
}