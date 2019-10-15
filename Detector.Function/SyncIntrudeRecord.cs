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
        private static IConfiguration _config;

        [FunctionName("SyncIntrudeRecord")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"[request] C# HTTP trigger function processed a request.");

            try
            {
                var connectionString = _config.GetConnectionString("Database");
                var dbContextOptionsBuilder = new DbContextOptionsBuilder<DetectorContext>();

                dbContextOptionsBuilder.UseSqlServer(connectionString);

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
