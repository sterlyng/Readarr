using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Readarr.Http
{
    public class ApiInfoController : Controller
    {
        [HttpGet("/api")]
        [Produces("application/json")]
        public object GetApiInfo()
        {
            return new ApiInfoResource
            {
                Current = "v1"
            };
        }
    }
}
