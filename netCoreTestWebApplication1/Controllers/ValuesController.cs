using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Nethereum.JsonRpc;
using Nethereum.JsonRpc.Client;
using Nethereum.RPC.Eth;

namespace netCoreTestWebApplication1.Controllers
{

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
            var client = new RpcClient(new Uri("http://10.0.75.1:8545"));
            var ethAccounts = new EthAccounts(client);
            var accounts = ethAccounts.SendRequestAsync().Result;

            Logger.Debug($"Enviroment:{Environment.MachineName}. Method:{MethodBase.GetCurrentMethod().Name}.");
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id:min(18)}")]
        public void Get(int id)
        {
            Logger.Debug($"Enviroment:{Environment.MachineName}. Method:{MethodBase.GetCurrentMethod().Name}. value:{id.ToString()}");

        }
        

        // GET api/values/5
        [HttpGet("{env}/wallets/{id}")]
        public string Get(int id, string env)
        {
            Logger.Debug($"Enviroment:{Environment.MachineName}. Method:{MethodBase.GetCurrentMethod().Name}. Id:{id}");
            return $"id:{id.ToString()}, env:{env}";
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
