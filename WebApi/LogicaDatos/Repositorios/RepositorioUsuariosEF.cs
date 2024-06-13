using LogicaNegocio.Dominio;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorios;
//using BCrypt.Net; Agregar antes de usar el encriptar
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LogicaDatos.Repositorios
{
    public class RepositorioUsuariosEF : IRepositorioUsuarios
    {
        //Declaramos vacio inicialmente
        public LibreriaContext Contexto { get; set; }

        public RepositorioUsuariosEF(LibreriaContext ctx) { 
            Contexto = ctx;
        }

        public void Add(Usuario obj)
        {   
            //Validar con IValidar(Campos obligatorios)
            obj.Validar();
            //Verifica que el email sea unico
            if(!EmailEsUnico(obj))throw new ExcepcionCustomException("El email ya se encuentra en uso.");
            //Agregar a la BD
            Contexto.Usuarios.Add(obj);
            Contexto.SaveChanges();

        }

        public List<Usuario> GetAll()
        {
            return Contexto.Usuarios.ToList();
        }

        public Usuario GetById(int id)
        {
            return Contexto.Usuarios.Find(id);
        }

        public void Remove(int id)
        {
            Usuario aBorrar = GetById(id);
            if (aBorrar != null) {
                Contexto.Usuarios.Remove(aBorrar);
                Contexto.SaveChanges(true);
            }
            else
            {
                throw new ExcepcionCustomException("El Usuario con id " + id + " no existe");
            }
        }

        public void Update(Usuario obj)
        {
            obj.Validar();
            //Guardar cambios
            Contexto.Usuarios.Update(obj);
            Contexto.SaveChanges();
        }

        public Usuario BuscarPorMail(string mail, string password)
        {

            Usuario variable= Contexto.Usuarios
                .Where(usu => usu.Email == mail && usu.PasswordEnctriptado == password)
                .SingleOrDefault();
            return variable;
        }

        public string EncriptarPassword(string password)
        {
            //Metodo hash con salt, de tipo lento y agregado a traves del paquete NuGet llamado BCrypt.NET
            //return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
            //string fixedSalt = BCrypt.Net.BCrypt.GenerateSalt(12); // Genera una vez y reutiliza
            string fixedSalt = "$2a$12$VHg6TJy5MpqjQ6Pm1os4ke";
            return BCrypt.Net.BCrypt.HashPassword(password, fixedSalt);
        }

        public Usuario TraerUsuarioConMail(string mail)
        {

            Usuario variable = Contexto.Usuarios
                .Where(usu => usu.Email == mail)
                .SingleOrDefault();
            return variable;
        }

        public bool EmailEsUnico(Usuario obj)
        {
            Usuario u = Contexto.Usuarios.Where(u => u.Email == obj.Email).FirstOrDefault();
            if (u != null && u.Id != obj.Id)
            {             
                return false;
            }
            return true;
        }
    }
}
