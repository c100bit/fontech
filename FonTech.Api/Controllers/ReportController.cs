using Microsoft.AspNetCore.Mvc;

namespace FonTech.Api.Controllers;

public class ReportController : ControllerBase
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}