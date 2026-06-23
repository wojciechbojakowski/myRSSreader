namespace myRSSreader.Client.Models;

public class RssItemDto
{
    public string Title {get;set;}=string.Empty;
    public string Link {get;set;}=string.Empty;
    public string Description {get;set;}=string.Empty;
    public DateTimeOffset PublishDate {get;set;}
    public string SourceUrl {get; set;} = string.Empty;
}