using Grpc.Core;
using Grpc.Net.Client;
using JLokaGrpcService.Protos;

namespace JLokaGrpcClient
{
    internal class RandomClient
    {
        public async Task GetRandom()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7240");
            var client = new RandomNumbers.RandomNumbersClient(channel);

            var replay = client.GetRandomNumbers(new GetRandomNumbersRequest
            {
                Count = 10,
                Min = 1,
                Max = 100,
            });

            await foreach (var item in replay.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine("The Number is: " + item.Number);
            }
            Console.ReadKey();
        }
    }
}