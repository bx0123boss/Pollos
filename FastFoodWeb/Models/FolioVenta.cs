namespace FastFoodWeb.Models
{
    public class FolioVenta
    {
        public int IdFolio { get; set; }
        public string ModalidadVenta { get; set; } = "";
        public string Estatus { get; set; } = "";
        public int IdCliente { get; set; }
        public DateTime FechaHora { get; set; }
        public double Total { get; set; }
        public double Descuento { get; set; }
        public double Utilidad { get; set; }
        public string Mesa { get; set; } = "";
        public string CantidadPersonas { get; set; } = "";
        public string Mesero { get; set; } = "";
        public int IdMesa { get; set; }
        public int IdMesero { get; set; }
    }
}