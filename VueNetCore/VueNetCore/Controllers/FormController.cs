using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace VueNetCore.Controllers
{
    public class FormController : ControllerBase
    {
        private readonly IDataService dataService;
        private readonly ILogger<FormController> logger;

        public FormController(IDataService dataService, ILogger<FormController> logger)
        {
            this.dataService = dataService;
            this.logger = logger;
        }

        [HttpPost("/api/form")]
        public async Task<ActionResult> Add()
        {
            string entry = string.Empty;
            using (StreamReader stream = new StreamReader(HttpContext.Request.Body))
            {
                entry = await stream.ReadToEndAsync();
            }

            dataService.Add(entry);
            logger.LogInformation("Entry added");
            
            return Ok();
        }
    }
}
