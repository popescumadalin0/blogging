namespace BloggingClient.Authentication;

public interface ISessionStorage
{
    string GetItem(string key);

    void SetItem(string key, string value);
}