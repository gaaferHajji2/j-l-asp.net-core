using Grpc.Net.Client;
using JLokaGrpcService.Protos;

namespace JLokaGrpcClient
{
    internal class InvoiceClient
    {
        public async Task CreateContactAsync()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5077");
            var client = new ContactService.ContactServiceClient(channel);
            var reply = await client.CreateContactAsync(new CreateContactRequest
            {
                Email = "john.doe@abc.com",
                FirstName = "John",
                LastName = "Doe",
                IsActive = true,
                Phone = "1234567890",
                YearOfBirth = 1980
            });

            Console.WriteLine("Created Contact: " + reply.ContactId);
            Console.ReadKey();
        }
    }
}