namespace WebMVC.ClasesAuxiliares
{
    public class HerramientasAPI
    {
        public static string LeerContenidoRespuesta(HttpResponseMessage respuesta)
        {
            HttpContent content = respuesta.Content;
            var tarea2 = content.ReadAsStringAsync();
            tarea2.Wait();
            string cuerpo = tarea2.Result;
            return cuerpo;
        }
    }
}
