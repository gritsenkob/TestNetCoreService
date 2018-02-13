using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace netCoreTestWebApplication1.Controllers
{
    public class TestInputData
    {
        public int TestInt { get; set; }

        public decimal TestDecimal { get; set; }

        public string TestString { get; set; }

    }

    public class TestOutputData : TestInputData
    {
        public DateTime ResponseDateTime { get; set; }
    }

    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        ILog _logger;
        public ILog Logger
        {
            get
            {
                return _logger ?? (_logger = LogManager.GetLogger(GetType()));
            }
            set
            {
                _logger = value;
            }
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            Logger.Debug($"Enviroment:{Environment.MachineName}. Method:{MethodBase.GetCurrentMethod().Name}.");
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id:min(18)}")]
        public string Get(int id)
        {
            Logger.Debug($"Enviroment:{Environment.MachineName}. Method:{MethodBase.GetCurrentMethod().Name}. Id:{id}");
            return "value:";
        }


        // POST api/values
        [HttpPost]
        public TestOutputData Post([FromBody]TestInputData value)
        {
            Logger.Debug($"Enviroment:{Environment.MachineName}. Method:{MethodBase.GetCurrentMethod().Name}. {JsonConvert.SerializeObject(value)}");
            TestOutputData responseOutputData = new TestOutputData
            {
                TestDecimal = value.TestDecimal + 1,
                TestInt = value.TestInt,
                TestString = $"Enviroment:{Environment.MachineName}. Request string:\"{value.TestString}\""
            };
            responseOutputData.ResponseDateTime = DateTime.Now;
            return responseOutputData;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]TestInputData value)
        {
            Logger.Debug($"Enviroment:{Environment.MachineName}. Method:{MethodBase.GetCurrentMethod().Name}. {JsonConvert.SerializeObject(value)}");
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Logger.Debug($"Enviroment:{Environment.MachineName}. Method:{MethodBase.GetCurrentMethod().Name}. Id:{id}");
        }
    }
}
