// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using Dapper;

namespace FunctionApp1
{
    public static class fcSincronizaCliente
    {
       

        [FunctionName("SincronizaCliente")]
        public static void Run([EventGridTrigger]EventGridEvent eventGridEvent, ILogger log)
        {
            const string Conexao = "Server=tcp:dbsadinfo.database.windows.net,1433;Initial Catalog=DBCatalogo;Persist Security Info=False;User ID=silvio.souza;Password=Davisamuel@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            const string SqlInsertCliente = "Insert into Cliente(Id, Nome) values(@Id, @Nome)";


            log.LogInformation(eventGridEvent.Data.ToString());
            var registro = (eventGridEvent.Data as JObject).ToObject<Model.Cliente>();
            using (var connection = new SqlConnection(Conexao))
            {
                connection.Open();
                connection.Execute(SqlInsertCliente, registro);
            }
           
        }
    }
}
