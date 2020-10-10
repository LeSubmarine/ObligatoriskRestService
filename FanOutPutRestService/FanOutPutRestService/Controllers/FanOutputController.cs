using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        private const string connectionString = "Server=tcp:sqlserverhen.database.windows.net,1433;Initial Catalog=SqlServerFans;Persist Security Info=False;User ID=hensql99;Password=abesild123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


        // GET: api/FanOutput
        [HttpGet]
        public IEnumerable<FanOutput> Get()
        {
            List<FanOutput> fanOutputslist = new List<FanOutput>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "SELECT * FROM FanOutput";

                SqlCommand command = new SqlCommand(queryString, connection);

                command.Connection.Open();
                //command.ExecuteNonQuery();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    FanOutput fanOutputObj = new FanOutput();

                    fanOutputObj.Id = reader.GetInt32(0); //læser int fra første søjle
                    fanOutputObj.Name = reader.GetString(1); //læser int fra anden søjle
                    fanOutputObj.Temperature = Convert.ToDouble(reader.GetDecimal(2)); //læser int fra tredje søjle
                    fanOutputObj.Humidity = Convert.ToDouble(reader.GetDecimal(3)); //læser datetime fra fjerde søjle

                    fanOutputslist.Add(fanOutputObj);
                }
            }

            return fanOutputslist;
        }


        // GET: api/FanOutput/5
        [HttpGet("{id}", Name = "Get")]
        public FanOutput Get(int id)
        {
            FanOutput fanOutputObj = new FanOutput();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "SELECT * FROM FanOutput";

                SqlCommand command = new SqlCommand(queryString, connection);

                command.Connection.Open();
                //command.ExecuteNonQuery();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    fanOutputObj.Id = reader.GetInt32(0); //læser int fra første søjle
                    fanOutputObj.Name = reader.GetString(1); //læser int fra anden søjle
                    fanOutputObj.Temperature = Convert.ToDouble(reader.GetDecimal(2)); //læser int fra tredje søjle
                    fanOutputObj.Humidity = Convert.ToDouble(reader.GetDecimal(3)); //læser datetime fra fjerde søjle
                }
            }

            return fanOutputObj;

        }


        // POST: api/FanOutput
        [HttpPost]
        public void Post([FromBody] FanOutput value)
        {
            if ((new List<FanOutput>(Get()).Find(fanoutput => (fanoutput.Id == value.Id) || (fanoutput.Name == value.Name))) == null)
            {
                try
                {
                    new FanOutput(value.Id, value.Name, value.Temperature, value.Humidity);
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {

                        string queryString = "INSERT INTO FanOutput (Id, Name, Temperature, Humidity) VALUES (@Id, @Name, @Temperature, @Humidity)";

                        SqlCommand command = new SqlCommand(queryString, connection);
                        command.Parameters.AddWithValue("@Id", value.Id);
                        command.Parameters.AddWithValue("@Name", value.Name);
                        command.Parameters.AddWithValue("@Temperature", value.Temperature);
                        command.Parameters.AddWithValue("@Humidity", value.Humidity);

                        connection.Open();
                        int result = command.ExecuteNonQuery();

                        // Check Error
                        if (result < 0)
                            Console.WriteLine("Error inserting data into Database!");
                    }
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
                Delete(id);
                Post(value);
            }
            catch (FormatException)
            {
            }

            
        }


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            if (new List<FanOutput>(Get()).Find(fanoutput => fanoutput.Id == id) != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string queryString = "DELETE FROM FanOutput WHERE Id=@Id";

                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0)
                        Console.WriteLine("Error deleting data into Database!");
                } 
            }
        }
    }
}
