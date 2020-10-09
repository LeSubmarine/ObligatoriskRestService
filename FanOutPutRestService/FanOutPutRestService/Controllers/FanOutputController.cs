using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FanLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis;

namespace FanOutPutRestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FanOutputController : ControllerBase
    {
        private static List<FanOutput> sqlDbFanOutput = new List<FanOutput>(Utility.GenerateFanOutputs(35));
        // GET: api/FanOutput
        [HttpGet]
        public IEnumerable<FanOutput> Get()
        {
            return sqlDbFanOutput;
        }

        // GET: api/FanOutput/5
        [HttpGet("{id}", Name = "Get")]
        public FanOutput Get(int id)
        {
            return sqlDbFanOutput.Find(fanOutput => fanOutput.Id == id);
        }

        // POST: api/FanOutput
        [HttpPost]
        public void Post([FromBody] FanOutput value)
        {
            if ((sqlDbFanOutput.Find(fanoutput => (fanoutput.Id == value.Id) || (fanoutput.Name == value.Name))) == null)
            {
                try
                {
                    new FanOutput(value.Id, value.Name, value.Temperature, value.Humidity);
                    sqlDbFanOutput.Add(value);
                }
                catch (FormatException)
                {
                }
                
            }
        }

        // PUT: api/FanOutput/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] FanOutput value)
        {
            if (value.Id != id)
            {
                return;
            }
            try
            {
                new FanOutput(value.Id, value.Name, value.Temperature, value.Humidity);
                sqlDbFanOutput.Remove(sqlDbFanOutput.Find(fanoutput => fanoutput.Id == id));
                sqlDbFanOutput.Add(value);
            }
            catch (FormatException)
            {
            }

            
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            sqlDbFanOutput.Remove(sqlDbFanOutput.Find(fanoutput => fanoutput.Id == id));

        }
    }
}
