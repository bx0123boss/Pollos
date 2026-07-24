using Microsoft.AspNetCore.SignalR;
using FastFoodWeb.Models;
using System.Threading.Tasks;

namespace FastFoodWeb.Hubs
{
    public class MesasHub : Hub
    {
        // Los clientes pueden invocar este mtodo enviando un objeto MesaEvent
        public async Task NotificarCambioMesa(MesaEvent evento)
        {
            // Transmitimos el evento a todos los clientes conectados bajo el mtodo "RecibirCambioMesa"
            await Clients.All.SendAsync("RecibirCambioMesa", evento);
        }
        
        // Mtodos opcionales para manejar grupos
        public async Task UnirseAGrupo(string nombreGrupo)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, nombreGrupo);
        }

        public async Task SalirDeGrupo(string nombreGrupo)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, nombreGrupo);
        }
    }
}
