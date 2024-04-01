using cyclone.api.Commands;
using cyclone.infrastructure;

using MediatR;

namespace cyclone.api.Handlers;

public class DeleteForecastHandler : IRequestHandler<DeleteForecastCommand, bool>
{
    WeatherDatabaseContext _weatherDatabaseContext;

    public DeleteForecastHandler(WeatherDatabaseContext weatherDatabaseContext)
    {
        _weatherDatabaseContext = weatherDatabaseContext;
    }

    public async Task<bool> Handle(DeleteForecastCommand request, CancellationToken cancellationToken)
    {
        var forecast = await _weatherDatabaseContext.CitizenForecasts.FindAsync(request.ForecastId);

        if (forecast == null)
        {
            return false;
        }
        
        _weatherDatabaseContext.CitizenForecasts.Remove(forecast);
        await _weatherDatabaseContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}