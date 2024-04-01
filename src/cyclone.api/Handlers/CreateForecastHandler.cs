using cyclone.api.Commands;
using cyclone.infrastructure;

using MediatR;

namespace cyclone.api.Handlers;

public class CreateForecastHandler : IRequestHandler<CreateForecastCommand>
{
    private WeatherDatabaseContext _weatherDatabaseContext;

    public CreateForecastHandler(WeatherDatabaseContext weatherDatabaseContext)
    {
        _weatherDatabaseContext = weatherDatabaseContext;
    }

    public async Task Handle(CreateForecastCommand request, CancellationToken cancellationToken)
    {
        await _weatherDatabaseContext.CitizenForecasts.AddAsync(request.Forecast, cancellationToken);
        await _weatherDatabaseContext.SaveChangesAsync(cancellationToken);
    }
}