namespace FirebasePusher.Interfaces
{
  public interface IPushMessage
  {
    string Title { get; }

    string Body { get; }

    string Topic { get; }

    string To { get; }

    IReadOnlyDictionary<string, string> Data { get; }

    bool HasTopic => !string.IsNullOrWhiteSpace(this.Topic);

    void AddData(string key, string value);
  }
}
