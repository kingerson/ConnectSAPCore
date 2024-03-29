﻿using ConnectSAPCore.Infra.CrossCutting.Sap;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace ConnectSAPCore.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ISapService _sapService;
        public ValuesController(ISapService sapService) => _sapService = sapService;


        // GET api/values/5
        [HttpPost]
        [Route("Dummy")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Dummy()
        {
            var result = await _sapService.GetMoneda();
            return Ok(result);
        }

    }
}
