using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Detector.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Detector.Database.Entity;

namespace Detector.Function
{
    public static class SyncIntrudeRecord
    {
        private static readonly string _connectionString = "Server=tcp:johnsonsqlsever.database.windows.net,1433;Initial Catalog=detector;Persist Security Info=False;User ID=johnson;Password=P@ssw0rd1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        [FunctionName("SyncIntrudeRecord")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"[request] C# HTTP trigger function processed a request.");

            try
            {
                var dbContextOptionsBuilder = new DbContextOptionsBuilder<DetectorContext>();

                dbContextOptionsBuilder.UseSqlServer(_connectionString);

                using (var context = new DetectorContext(dbContextOptionsBuilder.Options))
                {
                    context.Set<IntrudeRecordEntity>().Add(new IntrudeRecordEntity());
                    context.SaveChanges();
                }

                log.LogInformation($"[failed] C# HTTP trigger function processed a request success.");
            }
            catch (Exception ex)
            {
                log.LogInformation($"[failed] C# HTTP trigger function processed a request failed. ({ex.Message})");

                return new JsonResult(new { Status = false });
            }

            return new JsonResult(new { Status = true });
        }
    }
}
