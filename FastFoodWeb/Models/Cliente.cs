using System.ComponentModel.DataAnnotations;

namespace FastFoodWeb.Models;

public class Cliente
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    public string Telefono { get; set; }

    public string Direccion { get; set; }

    public string Referencia { get; set; }
    public string Colonia { get; set; }
}