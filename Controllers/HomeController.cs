using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ThesisOct2023.Models;
using System.Net.Http.Headers;
using System.Data;
using Newtonsoft.Json;

namespace ThesisOct2023.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string baseURL = "https://developer.nrel.gov/api/alt-fuel-stations/v1.json?limit=1&api_key=jSMW58akzmQYtSjafRK5dFFrGn91Ng4hAkrFcrEr";

		public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
           
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
			//Calling the web API and populating the data in view using DataTable
            DataTable dt = new DataTable();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage getData = await client.GetAsync("/foods/list");

                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    dt= JsonConvert.DeserializeObject<DataTable>(results);  
                }
                else
                {
                    Console.WriteLine($"Status code is {getData.StatusCode}");
                }
            }

                return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}