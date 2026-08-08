namespace FastFoodWeb.Models;
public class Producto
{
    public string Id { get; set; } = "";
    public string Nombre { get; set; } = "";
    public double Precio { get; set; }
    public double CostoTotal { get; set; }
    public string? ImagenUrl { get; set; }
    public bool Comanda { get; set; }
}
