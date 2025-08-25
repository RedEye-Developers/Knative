using Grpc.Core;

namespace Knative.Grpc.Services;

public class GreeterService : Greeter.GreeterBase
{
    private static int _staticCount = 0;
    
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        _staticCount++;
        Console.WriteLine(_staticCount);
        
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }
}