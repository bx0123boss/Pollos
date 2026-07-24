using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_Venta.Services
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

    public static class SignalRService
    {
        private static HubConnection _connection;

        public static event Action<MesaEvent> OnCambioMesa;

        public static async Task InicializarAsync()
        {
            if (_connection != null) return;

            string url = ConfigurationManager.AppSettings["SignalRServerUrl"];
            if (string.IsNullOrEmpty(url))
            {
                url = "http://192.168.0.15:5000/mesashub";
            }

            _connection = new HubConnectionBuilder()
                .WithUrl(url)
                .WithAutomaticReconnect()
                .Build();

            _connection.On<MesaEvent>("RecibirCambioMesa", (evento) =>
            {
                OnCambioMesa?.Invoke(evento);
            });

            _connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await ConnectWithRetryAsync();
            };

            await ConnectWithRetryAsync();
        }

        private static async Task ConnectWithRetryAsync()
        {
            try
            {
                await _connection.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error conectando a SignalR: " + ex.Message);
            }
        }

        public static async Task NotificarCambioMesa(MesaEvent evento)
        {
            if (_connection != null && _connection.State == HubConnectionState.Connected)
            {
                try
                {
                    await _connection.InvokeAsync("NotificarCambioMesa", evento);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error enviando evento SignalR: " + ex.Message);
                }
            }
        }
    }
}
