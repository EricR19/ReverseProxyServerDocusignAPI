using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using DocuSign.eSign.Model;
using System.Collections.Generic;
namespace ProxyServerDocusignAPI.Services;

public class DocuSignService : IDocuSignService
{
    
    private readonly string? _basePath;
    private readonly string? _templateId;
    private readonly DocuSignTokenService _tokenService;
    private readonly string? _accountId;
    
    public DocuSignService(DocuSignTokenService tokenService, IConfiguration configuration)
    {
        _tokenService = tokenService;
        _basePath = configuration["DocuSign:BasePath"];
        _templateId = configuration["DocuSign:TemplateId"];
        _accountId = configuration["DocuSign:AccountId"];
    }
    
    //Metodo que crea y envia el sobre
    public Task<string> SendEnvelopeAsync(string recipientEmail, string recipientName, int montoPrestamo, string plazoPrestamo, string tasaInteres, int coutaMensual)
    {
        var docuSignClient = new DocuSignClient(_basePath);
       
            var accessToken =  _tokenService.GetAccessTokenAsync();
            docuSignClient.Configuration.DefaultHeader.Add("Authorization", $"Bearer {accessToken.Result}");
            
            var envelopesApi = new EnvelopesApi(docuSignClient);
            EnvelopeDefinition envelope = new EnvelopeDefinition
            {
                TemplateId = _templateId,
                TemplateRoles = new List<TemplateRole>()
                {
                    new TemplateRole
                    {
                        Name = recipientName,
                        Email = recipientEmail,
                        RoleName = "Signer",
                        Tabs = new Tabs
                        {
                            TextTabs = new List<Text>
                            {
                                new Text
                                {
                                    TabLabel = "MontoPrestamo",
                                    Value = montoPrestamo.ToString()
                                },
                                new Text
                                {
                                    TabLabel = "PlazoPrestamo",
                                    Value = plazoPrestamo
                                },
                                new Text
                                {
                                    TabLabel = "TasaInteres",
                                    Value = tasaInteres
                                },
                                new Text
                                {
                                    TabLabel = "CoutaMensual",
                                    Value = coutaMensual.ToString()
                                }
                            }
                        }
                    }
                    
                },
                Status = "sent"
            };
            
            // Enviar sobre
            var result = envelopesApi.CreateEnvelope(_accountId, envelope);
            string envelopeId = result.EnvelopeId;
            return  Task.FromResult($" Sobre enviado a: {recipientName} \n email: ({recipientEmail}) \n EnvelopeID: {envelopeId} \n Envelope Status: {envelope.Status} ");
    }
    
    public async Task<string> GetEnvelopeInformation(string envelopeId)
    {
        if (string.IsNullOrEmpty(envelopeId))
        {
            throw new ArgumentException("El ID del sobre no puede estar vacío.", nameof(envelopeId));
        }

        // Configuración del cliente
        var docuSignClient = new DocuSignClient(_basePath);
        var accessToken = await _tokenService.GetAccessTokenAsync();
        docuSignClient.Configuration.DefaultHeader.Add("Authorization", $"Bearer {accessToken}");

        // Obtener información del sobre
        var envelopesApi = new EnvelopesApi(docuSignClient);
        var envelope = envelopesApi.GetEnvelope(_accountId, envelopeId);

        // Crear un string con la información que quieres retornar
        var envelopeInfo = $"Sobre ID: {envelope.EnvelopeId}\n" +
                           $"Estado: {envelope.Status}\n" +
                           $"Fecha de creación: {envelope.CreatedDateTime}\n" +
                           $"Fecha de envío: {envelope.SentDateTime}\n";

        return envelopeInfo;
    }

}