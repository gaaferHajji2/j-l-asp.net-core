using GrpcDemo;
using Microsoft.AspNetCore.Mvc;

namespace JLokaGrpc.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController (Contact.ContactClient client, ILogger<ContactController> logger) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactRequest request)
        {
            var replay = await client.CreateContactAsync(request);
            logger.LogInformation($"Replay is: {replay.ContactId}");
            return Ok(replay);
        }
    }
}
