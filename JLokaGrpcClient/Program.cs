// See https://aka.ms/new-console-template for more information
using JLokaGrpcClient;

Console.WriteLine("Hello, World!");
var contactClient = new InvoiceClient();
await contactClient.CreateContactAsync();