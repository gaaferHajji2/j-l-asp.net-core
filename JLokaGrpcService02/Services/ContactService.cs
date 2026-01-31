using GrpcDemo;

namespace JLokaGrpcService02.Services
{
    public class ContactService(ILogger<ContactService> logger) : Contact.ContactBase
    {
    }
}
