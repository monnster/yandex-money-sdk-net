namespace Yandex.Money.Api.Sdk.Interfaces
{
    /// <summary>
    /// <see cref="http://tech.yandex.ru/money/doc/dg/concepts/protocol-request-docpage/"/>
    /// </summary>
    public interface IAuthenticator
    {
        /// <summary>
        /// Authentication scheme must return fixed value of "Bearer"
        /// </summary>
        string AuthenticationScheme { get; }
        /// <summary>
        /// access token
        /// </summary>
        string Token { get; }
    }
}