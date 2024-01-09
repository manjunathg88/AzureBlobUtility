using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using AzureBlobUtility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

TokenCredential creds = new TokenCredential("<AccessToken>"); //replace <AccessToken> with actual access token
StorageCredentials storageCreds = new StorageCredentials(creds);
string storageAccountUrl = "<StorageAccountNamespace>"; //replace <StorageAccountNamespace> with actual Storage Account Namespace
CloudBlobClient client = new CloudBlobClient(new StorageUri(new Uri(storageAccountUrl)), storageCreds);

builder.Services.AddSingleton(cl => client);
builder.Services.TryAddSingleton<IBlobUtility, BlobUtility>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
