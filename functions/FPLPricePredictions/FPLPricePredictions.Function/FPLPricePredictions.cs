using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using PuppeteerSharp;
using System.IO;
using FPLPricePredictions.Function.Helpers;
using System.Collections.Generic;
using FPLPricePredictions.Function.Dtos;
using System.Net;
using System.Text.Json;

namespace FPLPricePredictions.Function
{
    public static class FPLPricePredictions
    {
        [FunctionName("fpl-prices")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("FPL Price predictions trigger function processed a request.");

            try
            {
                var risingPlayers = await Fetch.RisingPlayers();
                var fallingPlayers = await Fetch.FallingPlayers();

                return new OkObjectResult(JsonSerializer.Serialize(new PriceChangingPlayersDto
                {
                    RisingPlayers = risingPlayers,
                    FallingPlayers = fallingPlayers
                }));
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
