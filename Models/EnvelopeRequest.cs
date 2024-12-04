using System.ComponentModel.DataAnnotations;

namespace ProxyServerDocusignAPI.Models;

public class EnvelopeRequest
{
    [Required(ErrorMessage = "El campo 'RecipientEmail' es obligatorio.")]
    [EmailAddress(ErrorMessage = "Debe proporcionar un correo válido.")]
    public string RecipientEmail { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "El campo 'RecipientName' es obligatorio.")]
    public string RecipientName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "El campo 'MontoPrestamo' es obligatorio.")]
    [Range(1, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0.")]
    public int MontoPrestamo { get; set; }
    
    [Required(ErrorMessage = "El campo 'PlazoPrestamo' es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "El plazo debe ser mayor a 0.")]
    public string PlazoPrestamo  { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "El campo 'TasaInteres' es obligatorio.")]
    [Range(0.1, 100, ErrorMessage = "La tasa de interés debe estar entre 0.1 y 100.")]
    public string TasaInteres { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "El campo es requerido")]
    [Range(1, double.MaxValue, ErrorMessage = "La cuota debe ser mayor a 0.")]
    public int CoutaMensual { get; set; }
}