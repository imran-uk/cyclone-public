using cyclone.api.Commands;
using cyclone.api.Queries;
using cyclone.core.DTO;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace cyclone.api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IMediator _mediator;
    
    public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IEnumerable<CitizenForecast>> Get()
    {
        var query = new GetAllForecastsQuery();
        var result = await _mediator.Send(query);
        
        // TODO
        // see the message that appears if i change this to Ok(...)
        return result;
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var query = new GetForecastQuery(id);
        var result = await _mediator.Send(query);
        
        if (result == null)
        {
            _logger.LogInformation($"Forecast with id {id} not found");
            return NotFound();
        }

        return Ok(result);
    }
    
    [HttpPost(Name = "CreateForecast")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Post([FromBody] CitizenForecast forecast)
    {
        var command = new CreateForecastCommand(forecast);
        await _mediator.Send(command);
        
        _logger.LogInformation($"Created forecast with id {forecast.Id}");

        return Created($"/WeatherForecast/{forecast.Id}", forecast);
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(int id, [FromBody] CitizenForecast updatedForecast)
    {
        var forecast = updatedForecast with { Id = id };
        var updateCommand = new UpdateForecastCommand(forecast);
        var result = await _mediator.Send(updateCommand);
        
        if (result == false)
        {
            _logger.LogInformation($"Forecast with id {id} not found");
            return NotFound();
        }

        _logger.LogInformation($"Updated forecast with id {updatedForecast.Id}");
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> Delete(int id)
    {
        var command = new DeleteForecastCommand(id);
        var result = await _mediator.Send(command);

        if (result == null)
        {
            _logger.LogInformation($"Forecast with id {id} not found");
            return Results.NotFound();
        }
        
        _logger.LogInformation($"Deleted forecast with id {id}");
        return Results.Ok();
    }
}
