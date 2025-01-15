namespace Gnivc.Test.Service.Infrastructure;

public class DefaultLogger : ILogger
{
	private readonly ILogger<DefaultLogger> _logger;

	public DefaultLogger(ILogger<DefaultLogger> logger)
	{
		_logger = logger;
	}

	public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
		Func<TState, Exception?, string> formatter)
	{
		_logger.Log(logLevel, eventId, state, exception, formatter);
	}

	public bool IsEnabled(LogLevel logLevel)
	{
		return _logger.IsEnabled(logLevel);
	}

	public IDisposable BeginScope<TState>(TState state)
	{
		return _logger.BeginScope(state);
	}
}