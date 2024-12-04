using Microsoft.AspNetCore.Authorization;

namespace ProxyServerDocusignAPI.Controllers;

using Microsoft.AspNetCore.Mvc;
using Services;
using Models;


[ApiController]
[Route("api/[controller]")]
public class EnvelopeController : ControllerBase
{
    
    private readonly IDocuSignService _docuSignService;

    public EnvelopeController(IDocuSignService docuSignService)
    {
        _docuSignService = docuSignService;
    }
    /// <summary>
    /// Envia el sobre
    /// </summary>
    /// <returns>Lista la informacion a quien se le envio el sobre</returns>
    [HttpPost("send-envelope")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> SendEnvelope([FromBody] EnvelopeRequest request)
    {
        try
        {
            var result = await _docuSignService.SendEnvelopeAsync(
                request.RecipientEmail,
                request.RecipientName,
                request.MontoPrestamo,
                request.PlazoPrestamo,
                request.TasaInteres,
                request.CoutaMensual
            );

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
    /// <summary>
    /// Obtiene los datos del envelope.
    /// </summary>
    /// <returns>Envelope</returns>
    [HttpGet("envelope/{envelopeId}")]
    [Authorize] // Requiere autenticación JWT
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEnvelope(string envelopeId)
    {
        try
        {
            var envelopeInfo = await _docuSignService.GetEnvelopeInformation(envelopeId);

            return Ok(envelopeInfo);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    
}
