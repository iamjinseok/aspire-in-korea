var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("aspire-cache");

var apiService = builder.AddProject<Projects.demo_ApiService>("apiservice");

builder.AddProject<Projects.demo_Web>("webfrontend")
                        .WithReference(cache)
                        .WithReference(apiService);

var springbogt = builder.AddContainer("aspire-springboot", "aspire-springboot", "0.0.1")
                        .WithHttpEndpoint(port: 8080, targetPort: 8080)
                        .WithOtlpExporter();

var fastapi = builder.AddContainer("aspire-fastapi", "aspire-fastapi", "0.0.1")
                        .WithHttpEndpoint(port: 8081, targetPort: 80, isProxied: false)
                        .WithOtlpExporter();

builder.AddNpmApp("react", "../AspireJavaScript.React")
                        .WithReference(apiService)
                        .WithEndpoint(targetPort: 3000, scheme: "http", env: "PORT");

// bat files
string[] bat_args = new string[] { "600" };
builder.AddExecutable("cmd-service", $"{Directory.GetParent(Directory.GetCurrentDirectory())}/cmd/test.bat", "./", bat_args);

builder.Build().Run();
