using ProxyServerDocusignAPI.Models;

namespace ProxyServerDocusignAPI.Services;

public interface IDocuSignService
{
    Task<string> SendEnvelopeAsync(string recipientEmail, string recipientName, int montoPrestamo, string plazoPrestamo, string tasaInteres, int coutaMensual);
    Task<string> GetEnvelopeInformation(string envelopeId);  
}