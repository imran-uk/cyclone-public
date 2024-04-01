using cyclone.api.Queries;
using cyclone.core.DTO;
using cyclone.infrastructure;

using MediatR;

namespace cyclone.api.Handlers;

public class GetForecastHandler : IRequestHandler<GetForecastQuery, CitizenForecast>
{
    private readonly WeatherDatabaseContext _weatherDatabaseContext;

    public GetForecastHandler(WeatherDatabaseContext weatherDatabaseContext)
    {
        _weatherDatabaseContext = weatherDatabaseContext;
    }

    public async Task<CitizenForecast> Handle(GetForecastQuery request, CancellationToken cancellationToken)
    {
        var forecast = await _weatherDatabaseContext.CitizenForecasts.FindAsync(request.ForecastId);
        
        return forecast;
    }
}