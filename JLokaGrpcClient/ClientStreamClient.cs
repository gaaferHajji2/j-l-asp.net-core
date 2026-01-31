using Grpc.Net.Client;
using JLokaGrpcService.Protos;

namespace JLokaGrpcClient
{
    internal class ClientStreamClient
    {
        public async Task SendClientRandomNumbers()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7240");
            var client = new RandomNumbersClient.RandomNumbersClientClient(channel);
            //create the streaming request
            using var clientStreamingCall = client.SendRandomNumbersClient();
            var random = new Random();
            for(var i=0; i<5; ++i)
            {
                await clientStreamingCall.RequestStream.WriteAsync(new SendRandomNumbersRequest
                {
                    Number = random.Next(1, 100)
                });
                await Task.Delay(100);
            }
            // in this way we tell the server that the streaming end
            await clientStreamingCall.RequestStream.CompleteAsync();

            // Get the response
            var response = await clientStreamingCall;
            Console.WriteLine($"Count: {response.Count}, Sum: {response.Sum}");
            Console.ReadKey();
        }
    }
}
