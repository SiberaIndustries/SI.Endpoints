# SI.Endpoints

## Introduction

TODO

## Getting Started

### 1. Install and reference the Nuget SimpleEndpoints

```
Install-Package SI.Endpoints
```

### 2. Edit your `ConfigureServices()` method in `Startup.cs`

```cs
public void ConfigureServices(IServiceCollection services)
{
    // ..
    services.AddControllers();
    services.AddEndpointsRouting();
    // ..
}
```

### 3. Create your first Endpoint and have fun

```cs
// Models
public class SayHelloRequest
{
    public string Name { get; set; }
}

public class SayHelloResponse
{
    public string Message { get; set; }
}

// The Endpoint
public class SayHello : Endpoint<SayHelloRequest, SayHelloResponse>
{
    [HttpGet]
    public override ActionResult<SayHelloResponse> Handle(SayHelloRequest request)
    {
        return new SayHelloResponse
        {
            Message = "Welcome " + request.Name
        };
    }
}
```

Check out your first endpoint and navigate to: `<uri>/SayHello?Name=World!`

## Optionally Configure your endpoints

```cs
public void ConfigureServices(IServiceCollection services)
{
    // ..
    services.AddControllers();
    services.AddEndpointsRouting(options =>
    {
        options.UseFeatures();              // Groups endpoints by the same last namespace part
        options.WithPrefix("Ultra-Api");    // Adds the given prefix name to all endpoint routes
    });
    // ..
}
```

The following example shows how endpoints are configured with the given configuration:

```cs
MyApp.Endpoints.Messaging.HelloEndpoint            // <uri>/Ultra-Api/Messaging/SayHello
MyApp.Endpoints.Messaging.SayGoodByeEndpoint       // <uri>/Ultra-Api/Messaging/SayGoodBye
MyApp.AnotherEndpointNamespace.Calculate.Multiply  // <uri>/Ultra-Api/Calculate/Multiply
MyApp.AnotherEndpointNamespace.Calculate.Subtract  // <uri>/Ultra-Api/Calculate/Subtract
```

## Use the Swagger integration

If you use swagger, then you will see that every endpoint is grouped separetly. If you want to group them by feature (like a standard controller in mvc), then:

### 1. Install the NuGet-Package

```
Install-Package SI.Endpoints.Swagger
```

### 2. Add `EnableAnnotations()` and `EnableFeatureFilter()` to the Swagger configuration in `Startup.cs`

```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddEndpointsRouting(options => options.UseFeatures() });

    // Add swagger
    services.AddSwaggerGen(options =>
    {
        options.EnableAnnotations();    // Make use of the Annotations
        options.EnableFeatureFilter();  // Automatically group endpoints by the [feature] template
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "Sample endpoints", Version = "v1" });
    });
}
```

## Open Source License Acknowledgements and Third-Party Copyrights

- Icon made by [Freepik](https://www.flaticon.com/authors/freepik)
