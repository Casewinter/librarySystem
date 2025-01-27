using Microsoft.AspNetCore.Mvc;
using Books.DTOs;
namespace library.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {

        [HttpGet(Name = "books")]
        public string Get()
        {
            return "sucess";
        }
    }
}
