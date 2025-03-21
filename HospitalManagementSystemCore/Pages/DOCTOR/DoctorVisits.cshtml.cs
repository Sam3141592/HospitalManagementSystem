using ChartExample.Models.Chart;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Data;
using Microsoft.AspNetCore.Http;

namespace HospitalManagementSystem.Pages.DOCTOR
{
    public class DoctorVisitsModel : PageModel
    {
        private readonly HosDB HDB;
        private readonly ILogger<DoctorVisitsModel> _logger;
        public DoctorVisitsModel(ILogger<DoctorVisitsModel> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        [BindProperty(SupportsGet = true)]
        [ViewData]
        public string sid { get; set; }

        public string mname { get; set; }

        public DataTable visits { get; set; }
        public Tuple<string[], int[]> Statistics_Of_Vistis { get; set; }
        public ChartJs Chart { get; set; }
        public string ChartJson { get; set; }


        public void OnGet()
        {
            sid = HttpContext.Session.GetString("sid");
            mname = HDB.getStaffFnameByID(sid);
            visits = HDB.getVisitsDoc(sid);
            Statistics_Of_Vistis = HDB.ViewVisitsDoctor(sid);
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
                'rgba(255, 99, 132, 0.9)',
                'rgba(54, 162, 235, 0.9)',
                'rgba(255, 206, 86, 0.9)',
                'rgba(75, 192, 192, 0.9)',
                'rgba(153, 102, 255, 0.9)',
                'rgba(255, 159, 64, 0.9)'
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
                Chart.data.datasets[0].label = "Your visits";
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
