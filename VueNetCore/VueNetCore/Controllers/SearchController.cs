using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace VueNetCore.Controllers
{
    public class SearchController : ControllerBase
    {
        private readonly IDataService dataService;
        private readonly ILogger<FormController> logger;

        public SearchController(IDataService dataService, ILogger<FormController> logger)
        {
            this.dataService = dataService;
            this.logger = logger;
        }

        [HttpGet("/api/search/status")]
        public ActionResult Status()
        {
            return Ok("Up and running");
        }

        [HttpGet("/api/search")]
        public ActionResult Search(string query)
        {
            var results = dataService.Search(query);
            return Ok(results);
        }
    }
}
