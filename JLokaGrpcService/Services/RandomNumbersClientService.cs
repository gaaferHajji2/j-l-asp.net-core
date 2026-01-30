using Grpc.Core;
using JLokaGrpcService.Protos;

namespace JLokaGrpcService.Services
{
    public class RandomNumbersClientService(ILogger<RandomNumbersClientService> logger) : RandomNumbersClient.RandomNumbersClientBase
    {
        public override async Task<SendRandomNumbersResponse> SendRandomNumbersClient(IAsyncStreamReader<SendRandomNumbersRequest> requestStream, ServerCallContext context)
        {
            var count = 0;
            var sum = 0;
            await foreach (var request in requestStream.ReadAllAsync())
            {
                logger.LogInformation($"Received: {request.Number}");
                ++count;
                sum += request.Number;
            }
            return new SendRandomNumbersResponse
            {
                Sum = sum,
                Count = count
            };
        }
    }
}
