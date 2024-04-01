using cyclone.core.DTO;

using MediatR;

namespace cyclone.api.Queries;

public class GetAllForecastsQuery : IRequest<IEnumerable<CitizenForecast>>
{
    
}