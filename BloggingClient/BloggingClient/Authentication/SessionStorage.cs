using System.Collections.Generic;

namespace BloggingClient.Authentication;

public class SessionStorage : ISessionStorage
{
    private readonly Dictionary<string, string> _items = new();

    public string GetItem(string key)
    {
        if (!_items.ContainsKey(key))
        {
            return null;
        }

        return _items[key];
    }

    public void SetItem(string key, string value)
    {
        _items[key] = value;
    }
}