using Grpc.Core;
using JLokaGrpcService.Protos;

namespace JLokaGrpcService.Services
{
    public class ContactGrpcService(ILogger<ContactGrpcService> logger): ContactService.ContactServiceBase
    {
        public override Task<CreateContactResponse> CreateContact(CreateContactRequest request, ServerCallContext context)
        {
            return Task.FromResult(new CreateContactResponse
            {
                ContactId = Guid.NewGuid().ToString(),
            });
        }
    }
}