using Homework_June_7.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Homework_June_7.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TLSController : ControllerBase
    {
        [HttpGet]
        [Route("Scrape")]
        public List<TLSNews> Scrape()
        {
           return Homework_June_7.Data.TLSScraper.Scrape();
        }
    }
}
