using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Infrastructure;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILoggerFactory _loggerFactory;

    public LoggingBehaviour(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var logger = _loggerFactory.CreateLogger(request.GetType());

        logger.LogInformation($"********************** Starting to handle {request.GetType().Name} **********************");

        TResponse response;
        try
        {
            response = await next();
        }
        catch(Exception ex)
        {
            logger.LogError(ex, $"********************** Error in {request.GetType().Name} **********************");
            throw;
        }

        logger.LogInformation($"********************** Finsihed handling {request.GetType().Name} **********************");

        return response;
    }
}