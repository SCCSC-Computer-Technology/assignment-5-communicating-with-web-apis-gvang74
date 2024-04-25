This code looks really good. I thought the “Students.cshtml” should be located in the views/home folder, but I might be wrong as I have more experience with making these in MVC applications. APIs still tend to throw me off a little bit. I think these sort of note: “// PUT: api/StudentProfiles/5”, are a smart idea and help you keep track of both what the URL will (should/might) look like and what the method in the controller is doing. Overall it looks really good, I did get some errors on my end in the HomeController.cs for the “_logger”, and the “db” variables, as well as an issues with the “Student” object. I believe you need those line to look like this (lines 10-17):


public HomeController(ILogger<HomeController> logger,
StudentProfileContext injectedContext,
IHttpClientFactory httpClientFactory)
{
    var _logger = logger;
    var db = injectedContext;
    clientFactory = httpClientFactory;
}

And in the “Task<IActionResult> Customers(string country)” method i believe you meant to use “StudentProfile” like this (lines 42-43):

IEnumerable<StudentProfile>? model = await response.Content
  .ReadFromJsonAsync<IEnumerable<StudentProfile>>();

