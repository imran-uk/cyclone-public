using cyclone.api.Queries;
using cyclone.core.DTO;
using cyclone.infrastructure;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace cyclone.api.Handlers;

public class GetAllForecastsHandler : IRequestHandler<GetAllForecastsQuery, IEnumerable<CitizenForecast>>
{
    private readonly WeatherDatabaseContext _weatherDatabase;

    public GetAllForecastsHandler(WeatherDatabaseContext weatherDatabase)
    {
        _weatherDatabase = weatherDatabase;
    }

    public async Task<IEnumerable<CitizenForecast>> Handle(GetAllForecastsQuery request, CancellationToken cancellationToken)
    {
        var forecasts = await _weatherDatabase.CitizenForecasts.ToListAsync(cancellationToken: cancellationToken);
        
        return forecasts;
    }
}