﻿using Microsoft.AspNetCore.Mvc;
using SI.Endpoints.Core;

namespace SI.Endpoints
{
    public abstract class Endpoint : EndpointBase
    {
        public abstract ActionResult Handle();
    }

    public abstract class Endpoint<TRequest, TResponse> : EndpointBase
    {
        public abstract ActionResult<TResponse> Handle(TRequest request);
    }

    public abstract class EndpointWithResponse<TResponse> : EndpointBase
    {
        public abstract ActionResult<TResponse> Handle();
    }

    public abstract class EndpointWithRequest<TRequest> : EndpointBase
    {
        public abstract ActionResult Handle(TRequest request);
    }

    public abstract class AsyncEndpoint : EndpointBase
    {
        public abstract Task<ActionResult> HandleAsync();
    }

    public abstract class AsyncEndpoint<TRequest, TResponse> : EndpointBase
    {
        public abstract Task<ActionResult<TResponse>> HandleAsync(TRequest request);
    }

    public abstract class AsyncEndpointWithResponse<TResponse> : EndpointBase
    {
        public abstract Task<ActionResult<TResponse>> HandleAsync();
    }

    public abstract class AsyncEndpointWithRequest<TRequest> : EndpointBase
    {
        public abstract Task<ActionResult> HandleAsync(TRequest request);
    }

#if NETSTANDARD2_1_OR_GREATER || NET6_0_OR_GREATER
    public abstract class AsyncEnumerableEndpoint<TRequest, TResponse> : EndpointBase
    {
        public abstract ActionResult<IAsyncEnumerable<TResponse>> HandleAsync(TRequest request);
    }

    public abstract class AsyncEnumerableEndpointWithResponse<TResponse> : EndpointBase
    {
        public abstract ActionResult<IAsyncEnumerable<TResponse>> HandleAsync();
    }
#endif
}
