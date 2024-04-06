using System.Diagnostics;

namespace Client
{
    internal class Program
    {
        async static Task Main(string[] args)
        {
            string apiUrl = "https://localhost:7180/api/Orders"; // Replace this with your API URL
            int numberOfRequests = 200;

            // Create HttpClient instance

            
            using (HttpClient client = new HttpClient { Timeout = TimeSpan.FromSeconds(1000) })
            {
                // Create an array of tasks to store each request
                Task[] tasks = new Task[numberOfRequests];

                // Start the stopwatch
                Stopwatch stopwatch = Stopwatch.StartNew();

                // Send requests in parallel
                for (int i = 0; i < numberOfRequests; i++)
                {
                    tasks[i] = SendPostRequestAsync(client, apiUrl , i);
                    await Task.Delay(5);

                }

                // Wait for all requests to complete
                await Task.WhenAll(tasks);

                // Stop the stopwatch
                stopwatch.Stop();

                Console.WriteLine($"Total time taken for {numberOfRequests} requests: {stopwatch.ElapsedMilliseconds} ms");
                Console.WriteLine($"Average time per Request : { (stopwatch.ElapsedMilliseconds / numberOfRequests)} ms");
            }

            Console.WriteLine("All requests completed.");
        }

        static async Task SendPostRequestAsync(HttpClient client, string apiUrl , int index)
        {
            try
            {
                // Prepare your request data here
                var requestData = new { clientEmail = "client-" + index + "@test.com"  , 
                                        orderNumber  = index.ToString()
                                      }; // Example data

                // Convert request data to JSON
                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);

                // Create HttpContent from JSON
                var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

                // Send POST request
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                // Check if request was successful
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Request " + index + " : Success");
                }
                else
                {
                    Console.WriteLine($"Request " + index + " : Failed with status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request " + index + " : Exception occurred: {ex.Message}");
            }
        }
    }
}
