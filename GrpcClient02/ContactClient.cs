using Grpc.Net.Client;
using GrpcDemo;

namespace GrpcClient02
{
    internal class ContactClient
    {
        public async Task SendRequest()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7214");
            var client = new Contact.ContactClient(channel);
        }
    }
}
