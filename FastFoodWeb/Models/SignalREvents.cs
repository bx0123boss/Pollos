namespace FastFoodWeb.Models
{
    public class MesaEvent
    {
        public string Tipo { get; set; } = string.Empty;
        public int IdMesa { get; set; }
        public string Estado { get; set; } = string.Empty;
        public int? IdArticulo { get; set; }
        public int? Cantidad { get; set; }
        public string Mensaje { get; set; } = string.Empty;
    }
}
