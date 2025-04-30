using System.ComponentModel.DataAnnotations;

namespace Cv4.Models;

public class OrderFormViewModel
{
    [Display(Name="Jmeno")]
    [Required, MinLength(2)]
    public string? Name { get; set; }
    
    [Display(Name="E-mail")]
    [EmailAddress]
    public string? Email { get; set; }
    
    [Display(Name="Vek")]
    [Range(15, 150)]
    public int? Age { get; set; }
    
    [Display(Name="Potvrzeni")]
    public bool Confirm { get; set; }
}