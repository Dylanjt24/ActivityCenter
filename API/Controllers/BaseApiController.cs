using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // create base controller for other controllers to derive from to reduce attribute redundancy
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        
    }
}