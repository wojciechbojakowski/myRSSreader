namespace myRSSreader.Client.Models;

public class RssFeed{
    public string Id {get;set;} = Guid.NewGuid().ToString();
    public string Name {get;set;} = string.Empty;
    public string Url {get;set;} = string.Empty;
    public string Category {get;set;} = "Inne";


}