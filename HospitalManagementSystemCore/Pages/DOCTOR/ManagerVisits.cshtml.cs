using ChartExample.Models.Chart;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Data;


namespace HospitalManagementSystem.Pages.Manager_Pages
{
    public class ManagerVisitsModel : PageModel
    {
        private readonly HosDB HDB;
        private readonly ILogger<ManagerVisitsModel> _logger;
        public ManagerVisitsModel(ILogger<ManagerVisitsModel> logger, HosDB HosDB)
        {
            _logger = logger;
            this.HDB = HosDB;
        }
        public string msg { get; set; }
        [BindProperty(SupportsGet = true)]
        [ViewData]
        public string sid { get; set; }

        public string mname { get; set; }

        public DataTable visits { get; set; }
        public Tuple<List<string>,List <string>> Statistics_Of_Vistis { get; set; }
        public ChartJs Chart { get; set; }
        public string ChartJson { get; set; }

        public string deptName { get; set; }
        public void OnGet()
        {
            sid = HttpContext.Session.GetString("sid");
            mname = HDB.getStaffFnameByID(sid);
            visits = HDB.getVisitsDoc(sid);
            deptName = HDB.getDeptName(sid);
            //Statistics_Of_Vistis = HDB.ViewVisitsByDepartment(sid);
            //var chartData = @"
            //    {
            //    type: 'bar',
            //    responsive: true,
            //    data:
            //    {
            //    labels: ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'],
            //    datasets: [{
            //    label: 'Favourite Colors Votes',
            //    data: [12, 19, 3, 5, 2, 3],
            //    backgroundColor: [
            //    'rgba(255, 99, 132, 0.2)',
            //    'rgba(54, 162, 235, 0.2)',
            //    'rgba(255, 206, 86, 0.2)',
            //    'rgba(75, 192, 192, 0.2)',
            //    'rgba(153, 102, 255, 0.2)',
            //    'rgba(255, 159, 64, 0.2)'
            //    ],
            //    borderColor: [
            //    'rgba(255, 99, 132, 1)',
            //    'rgba(54, 162, 235, 1)',
            //    'rgba(255, 206, 86, 1)',
            //    'rgba(75, 192, 192, 1)',
            //    'rgba(153, 102, 255, 1)',
            //    'rgba(255, 159, 64, 1)'
            //    ],
            //    borderWidth: 1
            //    }]
            //    },
            //    options:
            //    {
            //    scales:
            //    {
            //    y: [{ ticks:
            //    { beginAtZero: true}
            //    }]}}
            //    }"; //end of chartdata

            //try
            //{
            //    Chart = JsonConvert.DeserializeObject<ChartJs>(chartData);
            //    string[] labelsArray = Statistics_Of_Vistis.Item1.ToArray();
            //    Chart.data.labels = labelsArray;
            //    Dataset dataset = new DataSet();
            //    Chart.data.datasets[0].label = "Vistis of the department";
            //    string[] dataArray = Statistics_Of_Vistis.Item2.ToArray();
            //    int[] intArray = Array.ConvertAll(dataArray, int.Parse);
            //   Chart.data.datasets[0].data = intArray;

            //    ChartJson = JsonConvert.SerializeObject(Chart, new JsonSerializerSettings
            //    {
            //        NullValueHandling = NullValueHandling.Ignore,
            //    });
            //}
            //catch
            //(Exception ex)
            //{}
                       
        }
    }
}
