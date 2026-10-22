using Microsoft.AspNetCore.Mvc;
using MzansiJobs.Domain.Exceptions;

namespace MzansiJobs.API.Middleware;

public sealed class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(
                exception,
                "Unhandled exception for {Method} {Path}",
                context.Request.Method,
                context.Request.Path);

            await WriteProblemDetailsAsync(context, exception);
        }
    }

    private static async Task WriteProblemDetailsAsync(
        HttpContext context,
        Exception exception)
    {
        var (statusCode, title, detail) = exception switch
        {
            NotFoundException =>
                (StatusCodes.Status404NotFound,
                 "Resource not found",
                 exception.Message),

            BadRequestException =>
                (StatusCodes.Status400BadRequest,
                 "Bad request",
                 exception.Message),

            UnauthorizedException =>
                (StatusCodes.Status401Unauthorized,
                 "Unauthorized",
                 exception.Message),

            DomainException =>
                (StatusCodes.Status400BadRequest,
                 "Domain validation failed",
                 exception.Message),

            _ =>
                (StatusCodes.Status500InternalServerError,
                 "Internal server error",
                 "An unexpected error occurred.")
        };

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/problem+json";

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Instance = context.Request.Path
        };

        problemDetails.Extensions["traceId"] = context.TraceIdentifier;

        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}