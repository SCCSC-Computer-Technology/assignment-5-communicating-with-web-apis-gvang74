using ClassDemoWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace ClassDemoWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public string? uri { get; private set; }
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)  // pg 703
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;   // pg 703
            
        }
        public async Task<IActionResult> Students(int id)
        {

            string uri;
            if (id>0)
            {
                ViewData["Title"] = $"Student Profile";
                uri = $"api/studentprofiles/?id{id}";
            }
            else
            {
                ViewData["Title"] = "All Profiles";
                uri = "api/studentprofiles/";
            }
            HttpClient client = _httpClientFactory.CreateClient(
            name: "StudentProfileWebApi");
            HttpRequestMessage request = new(
            method: HttpMethod.Get, requestUri: uri);
            HttpResponseMessage response = await client.SendAsync(request);
            IEnumerable<StudentProfile>? model = await response.Content
            .ReadFromJsonAsync<IEnumerable<StudentProfile>>();
            return View(model); // pg 704
        }

        public async Task<IActionResult> StudentsPost(StudentProfile student)
        {
            if (student != null)
            {
                ViewData["Title"] = "Student Profile Created";
                uri = "api/studentprofiles";
            }
            else
            {
                return NotFound();
            }
            HttpClient client = _httpClientFactory.CreateClient(
            name: "StudentProfileWebApi");
            HttpRequestMessage request = new(
            method: HttpMethod.Get, requestUri: uri);
            HttpResponseMessage response = await client.SendAsync(request);
            IEnumerable<StudentProfile>?model = await response.Content
            .ReadFromJsonAsync<IEnumerable<StudentProfile>>();
            return View(model);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
