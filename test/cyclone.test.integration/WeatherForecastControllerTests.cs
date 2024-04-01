using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

using cyclone.core.DTO;

using Microsoft.AspNetCore.Mvc.Testing;

using NUnit.Framework;

using Assert = NUnit.Framework.Assert;

namespace cyclone.test.integration;

public class WeatherForecastControllerTests: IClassFixture<WebApplicationFactory<Program>> 
{
    private WebApplicationFactory<Program> _factory;
    private CitizenForecast _citizenForecastAsgard;
    private CitizenForecast _citizenForecastWakanda;
    private CitizenForecast _citizenForecastEnfield;
    private CitizenForecast _citizenForecastSouthampton;

    [SetUp]
    public void SetUp()
    {
        _factory = new WebApplicationFactory<Program>();
        
        _citizenForecastAsgard = new CitizenForecast
        {
            PlaceName = "Asgard", Temperature = 14.2, Timestamp = DateTime.Now
        };

        _citizenForecastWakanda = new CitizenForecast
        {
            PlaceName = "Wakanda", Temperature = 20.8, Timestamp = DateTime.Now
        };
        
        _citizenForecastEnfield = new CitizenForecast
        {
            PlaceName = "Enfield", Temperature = 10.1, Timestamp = DateTime.Now
        };
        
        _citizenForecastSouthampton = new CitizenForecast
        {
            PlaceName = "Southampton", Temperature = 15.2, Timestamp = DateTime.Now
        };
    }

    [Test]
    public async Task PostWeatherForecast_ShouldReturn201()
    {
        // Arrange
        using var client = _factory.CreateClient();
        
        var json = JsonSerializer.Serialize(_citizenForecastAsgard);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        
        // Act
        var postResponse = await client.PostAsync("/weatherforecast", content);
        
        var asgardForecast = await postResponse.Content.ReadFromJsonAsync<CitizenForecast>();
        
        // Assert
        Assert.That(postResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.That(postResponse.Content.Headers.ContentType.ToString(), Is.EqualTo("application/json; charset=utf-8"));

        Assert.That(asgardForecast, Is.Not.Null);
        Assert.That(asgardForecast.PlaceName, Is.EqualTo("Asgard"));
        Assert.That(asgardForecast.Temperature, Is.EqualTo(14.2));
    }
    
    [Test]
    public async Task GetWeatherForecast_ShouldReturnOneForecast()
    {
        // Arrange
        using var client = _factory.CreateClient();
        
        var json = JsonSerializer.Serialize(_citizenForecastWakanda);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        
        // Act
        var postResponse = await client.PostAsync("/weatherforecast", content);
        var wakandaForecastFromPost = await postResponse.Content.ReadFromJsonAsync<CitizenForecast>();

        var getResponse = await client.GetAsync($"/weatherforecast/{wakandaForecastFromPost.Id}");
        var wakandaForecastFromGet = await getResponse.Content.ReadFromJsonAsync<CitizenForecast>();
            
        // Assert
        Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(getResponse.Content.Headers.ContentType.ToString(), Is.EqualTo("application/json; charset=utf-8"));
        
        Assert.That(wakandaForecastFromGet, Is.Not.Null);
        Assert.That(wakandaForecastFromGet.PlaceName, Is.EqualTo("Wakanda"));
        Assert.That(wakandaForecastFromGet.Temperature, Is.EqualTo(20.8));
    }
    
    [Test]
    public async Task PutWeatherForecast_ShouldReturn204()
    {
        // Arrange
        using var client = _factory.CreateClient();
        var timestamp = DateTime.Now;

        var json = JsonSerializer.Serialize(_citizenForecastEnfield);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        // Act
        var postResponse = await client.PostAsync("/weatherforecast", content);
        var enfieldForecastFromPost = await postResponse.Content.ReadFromJsonAsync<CitizenForecast>();
        
        // update Enfield forecast
        _citizenForecastEnfield.Temperature = 21.1;
        var updatedJson = JsonSerializer.Serialize(_citizenForecastEnfield);
        var updatedContent = new StringContent(updatedJson, System.Text.Encoding.UTF8, "application/json");
        var putResponse = await client.PutAsync($"/weatherforecast/{enfieldForecastFromPost.Id}", updatedContent);

        // Assert
        Assert.That(putResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }
    
    [Test]
    public async Task DeleteWeatherForecast_ShouldReturn200()
    {
        // Arrange
        using var client = _factory.CreateClient();
        var timestamp = DateTime.Now;

        var json = JsonSerializer.Serialize(_citizenForecastSouthampton);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync("/weatherforecast", content);
        var southamptonForecast = await response.Content.ReadFromJsonAsync<CitizenForecast>();

        var deleteResponse = await client.DeleteAsync($"/weatherforecast/{southamptonForecast.Id}");

        // Assert
        Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    
    [Test]
    public async Task GetWeatherForecasts_ShouldReturnWeatherForecast()
    {
        // Arrange
        using var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/weatherforecast");
        
        // Assert
        response.EnsureSuccessStatusCode();

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That("application/json; charset=utf-8", Is.EqualTo(response.Content.Headers.ContentType.ToString()));
        
        var forecasts = await response.Content.ReadFromJsonAsync<IEnumerable<CitizenForecast>>();
        Assert.That(forecasts, Is.Not.Null);
        Assert.That(forecasts, Is.Not.Empty);
    }
}