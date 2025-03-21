using ChartExample.Models.Chart;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics.Metrics;

namespace HospitalManagementSystem.Pages.ADMIN
{
    public class AdminViewModel : PageModel
    {
        private readonly ILogger<AdminViewModel> _logger;
        private readonly HosDB db;
        public DataTable DatatableNurse { get; set; }
        public DataTable DatatablePatient { get; set; }
        public DataTable DatatableLabTech { get; set; }
        public DataTable DatatablePharmacist { get; set; }
        public DataTable DatatableDoctors { get; set; }
        public DataTable DatatableDepartement { get; set; }
        public DataTable DatatableNurseSchedule { get; set; }
        public DataTable DatatableDoctorSchedule { get; set; }

        public Tuple<string[], int[]> Statistics_Of_Vistis { get; set; }
        public ChartJs Chart { get; set; }
        public string ChartJson { get; set; }

        public string sid { get; set; }
        public string mname { get; set; }

        public AdminViewModel(ILogger<AdminViewModel> logger, HosDB db)
        {
            _logger = logger;
            this.db = db;
            
            
        }
        public void OnGet()
        {
            sid = HttpContext.Session.GetString("sid");
            mname = db.getStaffFnameByID(sid);
            DatatableNurse = db.GetTableAdmin("Nurse");
            DatatableDoctors = db.GetTableAdmin("Doctor");
            DatatableLabTech = db.GetTableAdmin("LabTechnician");
            DatatableDepartement = db.GetTableAdmin("Department");
            DatatablePharmacist = db.GetTableAdmin("Pharmacist");
            DatatablePatient = db.GetTableAdmin("Patient");
            DatatableNurseSchedule = db.GetTableAdmin("NURSE_SERVE_AT");
            DatatableDoctorSchedule = db.GetTableAdmin("DOCTOR_WORK_AT");
            Statistics_Of_Vistis = db.ViewVisitsAdmin();
            var chartData = @"
                {
                type: 'bar',
                responsive: true,
                data:
                {
                labels: [] ,
                datasets: [{
                label: '',
                data: [12, 19, 3, 5, 2, 3],
                backgroundColor: [
                'rgba(255, 159, 64, 0.9)',
                'rgba(255, 99, 132, 0.9)',
                'rgba(54, 162, 235, 0.9)',
                'rgba(255, 206, 86, 0.9)',
                'rgba(75, 192, 192, 0.9)',
                'rgba(153, 102, 255, 0.9)',
              
                ],
                borderColor: [
                'rgba(255, 99, 132, 1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)',
                'rgba(255, 159, 64, 1)'
                ],
              borderWidth: 1
                            }]
                },
                options: {
                    scales: {
                        x: {
                            title: {
                                display: true,
                                align: 'center',
                                text:  'Months',

                            }
                        },
                        y: {
                            title: {
                                display: true,
                                text: 'Number of visits',
                                align: 'center',
                                color: '#911',

                            },
                            ticks: {
                                beginAtZero: true,
                                    
                                }
                        }
                    }
                }
            }"; //end of chartdata

            try
            {
                Chart = JsonConvert.DeserializeObject<ChartJs>(chartData);
                string[] labelsArray = Statistics_Of_Vistis.Item1;
                Chart.data.labels = labelsArray;
                Chart.data.datasets[0].label = "Visits of the hospital each month";
                int[] dataArray = Statistics_Of_Vistis.Item2;
                Chart.data.datasets[0].data = dataArray;


                ChartJson = JsonConvert.SerializeObject(Chart, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
            }
            catch
            (Exception ex)
            { }

        }
    }
}
