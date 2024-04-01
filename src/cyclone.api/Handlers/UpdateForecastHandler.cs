using cyclone.api.Commands;
using cyclone.infrastructure;

using MediatR;

namespace cyclone.api.Handlers;

public class UpdateForecastHandler : IRequestHandler<UpdateForecastCommand, bool>
{
    private WeatherDatabaseContext _weatherDatabaseContext;

    public UpdateForecastHandler(WeatherDatabaseContext weatherDatabaseContext)
    {
        _weatherDatabaseContext = weatherDatabaseContext;
    }

    public async Task<bool> Handle(UpdateForecastCommand request, CancellationToken cancellationToken)
    {
        var forecast = await _weatherDatabaseContext.CitizenForecasts.FindAsync(request.CitizenForecast.Id);

        if (forecast == null)
        {
            return false;
        }

        forecast.PlaceName = request.CitizenForecast.PlaceName;
        forecast.Temperature = request.CitizenForecast.Temperature;
        forecast.Timestamp = request.CitizenForecast.Timestamp;
        
        await _weatherDatabaseContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}