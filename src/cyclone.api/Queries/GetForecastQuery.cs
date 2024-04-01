using cyclone.core.DTO;

using MediatR;

namespace cyclone.api.Queries;

public class GetForecastQuery : IRequest<CitizenForecast>
{
    public int ForecastId { get; set; }

    public GetForecastQuery(int forecastId)
    {
        ForecastId = forecastId;
    }
}