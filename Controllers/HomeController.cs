using Hangfire;
using Microsoft.AspNetCore.Mvc;
using webapi.Services;

namespace webapi.Controllers;
[ApiController]
[Route("")]
public class HomeController : ControllerBase
{
    [HttpGet(Name = "")]
    public IActionResult Index()
    {
        //BackgroundJob.Enqueue<IServiceJob>(x => x.GenString());
        return Ok();
    }
}