namespace FastFoodWeb.Models;
public class Mesa
{
    public int Id { get; set; } // Coincide con Id en SQL (007.MESAS.sql)
    public string Nombre { get; set; } = string.Empty;
    public string Estatus { get; set; } = "NUEVA"; // Valor por defecto
    public string Mesero { get; set; } = "";
}