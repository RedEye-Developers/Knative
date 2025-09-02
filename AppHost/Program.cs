using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddDockerComposeEnvironment("knative");

builder.AddProject<Knative_Api>("knative-api");
builder.AddProject<Knative_Grpc>("knative-grpc");

builder.Build().Run();