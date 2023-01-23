using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;
using TestFaker.Models;
using TestFaker.Service.IService;

namespace TestFaker.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IDataService _service;
    private const int Size = 20;
    private static int SeedValue;
    private static int oldSeed = 0;
    private static int newSeed = 0;
    private static double oldErrorAmount = 5;

    public HomeController(ILogger<HomeController> logger, IDataService service)
    {
        _logger = logger;
        _service = service;
    }
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult _TestData(string errorAmount, int seed)
    {
        if (seed != 0)
            newSeed = seed;
        double errorsAmount = double.Parse(errorAmount, CultureInfo.InvariantCulture.NumberFormat);
        if (errorsAmount == 5)
        {
            errorsAmount = oldErrorAmount;
        }else if (errorsAmount != 5)
        {
            oldErrorAmount = errorsAmount;
        }
        if (newSeed == oldSeed)
        {
            SeedValue += 10;
        }else if (newSeed != oldSeed)
        {
            SeedValue = newSeed+10;
        }
        
        var model = _service.GetData(Size, Request.Cookies["locale"], errorsAmount, SeedValue);
        if (model.Count() == 0) return StatusCode(204);
        return PartialView(model);
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