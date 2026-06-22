using System.Xml;
using System.ServiceModel.Syndication;
using Microsoft.AspNetCore.Mvc;
using myRSSreader.Server.Models;

namespace myRSSreader.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public FeedController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet("parse")]
    public async Task<ActionResult<List<RssItemDto>>> ParseFeed([FromQuery] string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            return BadRequest("Adres URL nie może być pusty.");
        }

        try
        {
            var client = _httpClientFactory.CreateClient();

            using var stream = await client.GetStreamAsync(url);

            using var xmlReader = XmlReader.Create(stream);

            var feed = SyndicationFeed.Load(xmlReader);

            var items = feed.Items.Select(item => new RssItemDto
            {
                Title = item.Title?.Text ?? "Brak tytułu",
                Link = item.Links.FirstOrDefault()?.Uri.ToString() ?? item.Id ?? String.Empty,
                Description = item.Summary?.Text ?? "Brak Opisu",
                PublishDate = item.PublishDate
            }).ToList();

            return Ok(items);
        }
        catch(HttpRequestException ex)
        {
            return StatusCode(502, $"Błąd podczas pobierania feedu z zewnętrzneg serwera: {ex.Message}");
        }
        catch(XmlException ex)
        {
            return BadRequest($"Podany URL nie zwraca poprawnego formatu XML/RSS: {ex.Message}");
        }
        catch(Exception ex)
        {
            return StatusCode(500, $"Wystąpił nieoczekiwaniy błąd: {ex.Message}");
        }
    }
}