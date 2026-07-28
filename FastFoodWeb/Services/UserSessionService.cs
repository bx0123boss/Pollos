namespace FastFoodWeb.Services
{
    public class UserSessionService
    {
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string TipoUsuario { get; set; }
        public bool IsLoggedIn => !string.IsNullOrEmpty(Usuario);

        public void SetUser(int id, string usuario, string tipo)
        {
            IdUsuario = id;
            Usuario = usuario;
            TipoUsuario = tipo;
        }

        public void Logout()
        {
            IdUsuario = 0;
            Usuario = null;
            TipoUsuario = null;
        }
    }
}