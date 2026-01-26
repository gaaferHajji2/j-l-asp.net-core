using Grpc.Core;
using JLokaGrpcService.Protos;

namespace JLokaGrpcService.Services
{
    public class RandomNumbersService(ILogger<RandomNumbersService> _logger): RandomNumbers.RandomNumbersBase
    {
        public override async Task GetRandomNumbers(GetRandomNumbersRequest request, IServerStreamWriter<GetRandomNumbersResponse> responseStream, 
            ServerCallContext context)
        {
            var random = new Random();
            for (var i = 0; i < request.Count; i++)
            {
                await responseStream.WriteAsync(new GetRandomNumbersResponse
                {
                    Number = random.Next(request.Min, request.Max)
                });
                await Task.Delay(1000);
            }
        }
    }
}
