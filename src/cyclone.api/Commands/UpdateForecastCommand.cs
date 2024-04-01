using cyclone.core.DTO;

using MediatR;

namespace cyclone.api.Commands;

public class UpdateForecastCommand: IRequest<bool>
{
    public CitizenForecast CitizenForecast { get; set; }

    public UpdateForecastCommand(CitizenForecast citizenForecast)
    {
        CitizenForecast = citizenForecast;
    }
}