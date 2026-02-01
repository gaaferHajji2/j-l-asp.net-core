using Grpc.Core;
using Grpc.Net.Client;
using JLokaGrpcService.Protos;

namespace JLokaGrpcClient
{
    internal class BiStreamChat
    {
        public async Task SendMessage()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7240");
            var client = new Chat.ChatClient(channel);
            // Create streaming request
            using var streamingCall = client.SendMessage();
            Console.WriteLine("Start background task to receive messages...");
            var responseReaderTask = Task.Run(async () =>
            {
                await foreach (var response in streamingCall.ResponseStream.ReadAllAsync())
                {
                    Console.WriteLine($"Replay from server: {response.Message}");
                }
            });
            Console.WriteLine("Starting to send messages...");
            while(true)
            {
                Console.Write("Your Message + enter: ");
                var message = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(message))
                {
                    break;
                }
                await streamingCall.RequestStream.WriteAsync(new ChatMessage
                {
                    Message = message,
                });
            }

            Console.WriteLine("Disconnecting...");
            await streamingCall.RequestStream.CompleteAsync();
            await responseReaderTask;
        }
    }
}