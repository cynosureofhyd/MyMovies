using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.Apis.Discovery.v1;
using Google.Apis.Discovery.v1.Data;
using System.Threading.Tasks;
using Google.Apis.Services;

namespace MyMovies.ExternalApis
{
    public class GMovieApi
    {
        public static void Call()
        {
            try
            {
                GMovieApi.Run().Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine("ERROR: " + e.Message);
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static async Task Run()
        {
            // Create the service.
            var service = new DiscoveryService(new BaseClientService.Initializer
            {
                ApplicationName = "Discovery Sample",
                ApiKey = "[YOUR_API_KEY_HERE]"
            });

            // Run the request.
            Console.WriteLine("Executing a list request...");
            var result = await service.Apis.List().ExecuteAsync();

            // Display the results.
            if (result.Items != null)
            {
                foreach (DirectoryList.ItemsData api in result.Items)
                {
                    Console.WriteLine(api.Id + " - " + api.Title);
                }
            }
        }
    }
}