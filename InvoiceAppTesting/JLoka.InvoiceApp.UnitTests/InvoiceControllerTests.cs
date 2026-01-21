using InvoiceApp.WebApi.Controllers;
using InvoiceApp.WebApi.Models;
using InvoiceApp.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace JLoka.InvoiceApp.UnitTests
{
    public class InvoiceControllerTests : IClassFixture<TestDatabaseFixture>
    {

        private readonly TestDatabaseFixture _fixture;

        public InvoiceControllerTests(TestDatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetInvoices_ShouldReturnInvoices()
        {
            //Arrange
            await using var dbContext = _fixture.CreateDbContext();
            var emailServiceMock = new Mock<IEmailService>();
            var controller = new InvoiceController(dbContext, emailServiceMock.Object);
            // Act
            var actionResult = await controller.GetInvoicesAsync();
            // Assert 
            var result = actionResult.Result as OkObjectResult;
            Assert.NotNull(result);
            var returnResult = Assert.IsAssignableFrom<List<Invoice>>(result.Value);
            Assert.NotNull(returnResult);
            Assert.Equal(2, returnResult.Count);
            Assert.Contains(returnResult, i => i.InvoiceNumber == "INV-001");
            Assert.Contains(returnResult, i => i.InvoiceNumber == "INV-002");
        }
    }
}
