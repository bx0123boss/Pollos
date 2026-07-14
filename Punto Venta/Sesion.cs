using System.Collections.Generic;

namespace JaegerSoft
{
    public static class Sesion
    {
        public static string IdUsuario { get; set; } = "";
        public static string NombreUsuario { get; set; } = "";
        public static List<string> PermisosActuales { get; set; } = new List<string>();

        public static bool TienePermiso(string accion)
        {

            if (PermisosActuales.Contains("ADMIN_TODO")) return true;
            return PermisosActuales.Contains(accion.ToUpper());
        }
        public static void CerrarSesion()
        {
            IdUsuario = "";
            NombreUsuario = "";
            PermisosActuales.Clear();
        }
    }
}