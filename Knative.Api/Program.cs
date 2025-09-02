using Grpc.Net.Client;
using Knative.Grpc;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => "Healthy");

app.MapGet("get", async () =>
{
    try
    {
        using var channel = GrpcChannel.ForAddress("http://knative-grpc:8080");
        var service = new GrpcService(channel);
        var message = await service.GetAsync();
        return Results.Ok(message);
    }
    catch (Exception e)
    {
        return Results.InternalServerError(e.Message);
    }
});

app.Run();

public class GrpcService
{
    private readonly GrpcChannel _channel;

    public GrpcService(GrpcChannel channel)
    {
        _channel = channel;
    }

    public async Task<string> GetAsync()
    {
        var client = new Greeter.GreeterClient(_channel);

        var request = new HelloRequest()
        {
            Name = "Red"
        };

        var response = await client.SayHelloAsync(request);

        return response.Message;
    }
}