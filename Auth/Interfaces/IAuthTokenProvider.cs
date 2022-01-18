namespace FirebasePusher.Auth.Interfaces
{
  public interface IAuthTokenProvider
  {
    Task<string> CreateAccessTokenAsync(CancellationToken cancellationToken);
  }
}
