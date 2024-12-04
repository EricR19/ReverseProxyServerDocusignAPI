using DocuSign.eSign.Client;

namespace ProxyServerDocusignAPI.Services;

public class DocuSignTokenService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;
    private string? _accessToken;
    private DateTime _accessTokenExpiry;
    private List<string> Scopes { get; set; } = new List<string> { "signature", "impersonation" };
    
    public DocuSignTokenService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    // Obtener el token de acceso, si es necesario renovarlo
    public async Task<string> GetAccessTokenAsync()
    {
        if (_accessToken == null || IsTokenExpired())
        {
            await RenewAccessTokenAsync();
        }
        return _accessToken!;
    }

    // Verificar si el token ha expirado
    private bool IsTokenExpired()
    {
        return _accessTokenExpiry <= DateTime.UtcNow;
    }

    // Renovar el token de acceso
    private Task RenewAccessTokenAsync()
    {
        var integratorKey = _configuration["DocuSign:IntegratorKey"];
        var userId = _configuration["DocuSign:UserId"];
        var rsaPrivateKeyPath = _configuration["DocuSign:RsaPrivateKey"];
        var apiClient = new DocuSignClient();
        var rsaPrivateKeyStream = new FileStream(rsaPrivateKeyPath!, FileMode.Open, FileAccess.Read);

        // Solicitar un nuevo token
        var authToken = apiClient.RequestJWTUserToken(
            integratorKey, 
            userId, 
            "account-d.docusign.com", 
            rsaPrivateKeyStream, 
            1, 
            Scopes);

        _accessToken = authToken.access_token;
        _accessTokenExpiry = DateTime.UtcNow.AddSeconds((double)(authToken.expires_in - 60)!);  // Dejar un margen antes de que expire
        return Task.CompletedTask;
    }
}
