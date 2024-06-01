using System.Reflection.Metadata;

namespace BloggingClient.Models;

public class FilterCheck
{
    public FilterCheck()
    {
    }

    public FilterCheck(string name)
    {
        Name = name;
        IsChecked = false;
    }

    public bool IsChecked { get; set; }
    public string Name { get; set; }
}