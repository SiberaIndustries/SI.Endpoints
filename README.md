# SI.Endpoints

[![NuGet](https://img.shields.io/nuget/v/SI.Endpoints.svg)](https://www.nuget.org/packages/SI.Endpoints)
[![NuGet](https://img.shields.io/nuget/v/SI.Endpoints.Swagger.svg)](https://www.nuget.org/packages/SI.Endpoints.Swagger)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=SiberaIndustries_SI.Endpoints&metric=alert_status)](https://sonarcloud.io/dashboard?id=SiberaIndustries_SI.Endpoints)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=SiberaIndustries_SI.Endpoints&metric=coverage)](https://sonarcloud.io/dashboard?id=SiberaIndustries_SI.Endpoints)

## Introduction

Have fun using this lightweight endpoint API library. No more bloated `Program.cs` files and no more bloated Controllers. This library is in between those two.

## Getting Started

### 1. Install and reference the NuGet package

```
Install-Package SI.Endpoints
```

### 2. Register the Endpoints

```cs
services.AddControllers();
services.AddEndpointsRouting();
```

### 3. Create your first Endpoint and have fun

```cs
// DTOs
public record SayHelloRequest(string Name);
public record SayHelloResponse(string Message);

// Endpoint
public class SayHello : Endpoint<SayHelloRequest, SayHelloResponse>
{
    [HttpPost]
    public override ActionResult<SayHelloResponse> Handle(SayHelloRequest request)
    {
        return new SayHelloResponse("Hello " + request.Name);
    }
}
```

Check out your first endpoint and navigate to: `<uri>/SayHello?Name=World!`

## Optionally Configure your endpoints

```cs
services.AddControllers();
services.AddEndpointsRouting(options =>
{
    options.UseFeatures();              // Groups endpoints by the same last namespace part
    options.WithPrefix("Ultra-Api");    // Adds the given prefix name to all endpoint routes
});
```

The following example shows how endpoints are configured with the given configuration:

```cs
MyApp.Endpoints.Messaging.HelloEndpoint            // <uri>/Ultra-Api/Messaging/SayHello
MyApp.Endpoints.Messaging.SayGoodByeEndpoint       // <uri>/Ultra-Api/Messaging/SayGoodBye
MyApp.AnotherEndpointNamespace.Calculate.Multiply  // <uri>/Ultra-Api/Calculate/Multiply
MyApp.AnotherEndpointNamespace.Calculate.Subtract  // <uri>/Ultra-Api/Calculate/Subtract
```

## Use the Swagger integration

If you use swagger, then you will see that every endpoint is grouped separetly. If you want to group them by feature (like a standard controller in mvc), then simply enable the feature filter.

### A. Using Swashbuckle

**1. Install the NuGet-Package**

```
Install-Package SI.Endpoints.Swagger
```

**2. Add `EnableAnnotations()` and `EnableFeatureFilter()` to the Swagger configuration**

```cs
services.AddControllers();
services.AddEndpointsRouting(options => options.UseFeatures() });

// Add swagger
services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();    // Make use of the Annotations
    options.EnableFeatureFilter();  // Automatically group endpoints by the [feature] template
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Sample endpoints", Version = "v1" });
});
```

### B. Using NSwag

**1. Install the NuGet-Package**

```
Install-Package SI.Endpoints.NSwag
```

**2. Add `EnableAnnotations()` and `EnableFeatureFilter()` to the Swagger configuration**

```cs
services.AddControllers();
services.AddEndpointsRouting(options => options.UseFeatures() });

// Add swagger
services.AddOpenApiDocument(options => options.EnableFeatureFilter()); // Option A
services.AddSwaggerDocument(options => options.EnableFeatureFilter()); // Option B
```

## Open Source License Acknowledgements and Third-Party Copyrights

- Icon made by [Freepik](https://www.flaticon.com/authors/freepik)
