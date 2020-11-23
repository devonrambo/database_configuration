using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;

namespace Company.Function
{
    public static class ZoomFunction
    {
        [FunctionName("ZoomFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var result = new List<string>();

            try 
            { 
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "1026nba.database.windows.net"; 
                builder.UserID = "devonrambo";            
                builder.Password = "Grrmab84twow";     
                builder.InitialCatalog = "nbafinaldb";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT player_id FROM dbo.nbastats WHERE player_name = @name");
                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        string name = req.Query["name"];

                        var nba_name = command.CreateParameter();   
                        nba_name.ParameterName = "@name";
                        nba_name.Value = name;
                        command.Parameters.Add(nba_name);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0}", reader.GetString(0));
                                result.Add(reader.GetString(0));
                            }
                        }
                    }     
                }
                return new OkObjectResult(result);
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                return new OkObjectResult(e.ToString());
            }
            Console.ReadLine();




        //     string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        //     dynamic data = JsonConvert.DeserializeObject(requestBody);
        //     name = name ?? data?.name;

        //     string responseMessage = string.IsNullOrEmpty(name)
        //         ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
        //         : $"Hello, {name}. This HTTP triggered function executed successfully.";


        }
    }
}
