using LogicaNegocio.Entidades;
using LogicaNegocio.ExcepcionesEntidades.Usuarios;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioUsuarioEF : IRepositorioUsuario
    {
        public ObligatorioContext DbCtx { get; set; }

        public RepositorioUsuarioEF(ObligatorioContext dbCtx)
        {
            DbCtx = dbCtx;
        }

        public void Add(Usuario item)
        {
            Usuario? usuarioXEmail = null;

            try { usuarioXEmail = FindByEmail(item.Email.Valor); }
            catch (ExceptionUsuario) { }

            if (usuarioXEmail != null)
                throw new ExceptionUsuario("Ya existe un Usuario con ese email");

            item.FechaAlta = DateTime.Now;

            DbCtx.Usuarios.Add(item);
            DbCtx.SaveChanges();
        }

        public void Delete(int id)
        {
            Usuario usuario = FindById(id);
            DbCtx.Usuarios.Remove(usuario);
            DbCtx.SaveChanges();
        }

        public IEnumerable<Usuario> FindAllOrdered()
        {
            return DbCtx.Usuarios.AsEnumerable().OrderBy(x => x.Id);
        }

        public Usuario FindById(int id)
        {
            return DbCtx.Usuarios
                            .AsEnumerable()
                            .FirstOrDefault(u => u.Id == id) ?? throw new ExceptionUsuario("No existe un Usuario con ese Id");
        }

        public void Update(Usuario item, int id)
        {
            Usuario usuarioXId = FindById(id);
            Usuario? usuarioXEmail = null;

            // FindByEmail throweara si no encuentra el usuario. 
            // En este caso lo que se espera es que no encuentre el usuario,
            // y que por ende throweee ExceptionUsuario..
            try { usuarioXEmail = FindByEmail(item.Email.Valor); }
            catch (ExceptionUsuario) { }

            if (usuarioXEmail != null && usuarioXEmail.Id != usuarioXId.Id)
                throw new ExceptionUsuario("Ya existe otro Usuario con ese email");

            usuarioXId.Email = item.Email;
            usuarioXId.Rol = item.Rol;

            // es opcional provisionar la Contrasena en el metodo update,
            // ya que la Contrasena no se entrega en el cuerpo de respuesta
            // al ser la misma consultada. Por lo que si viene vacia,
            // se mantiene la ya existente.
            if (!string.IsNullOrEmpty(item.Contrasena?.Valor))
            {
                // note: no es necesario validarla ya que el constructor de Usuario
                // se encarga de ejecutar dicha validacion cuando la Contrasena es
                // provista.
                usuarioXId.Contrasena = item.Contrasena;
            }

            DbCtx.Usuarios.Update(usuarioXId);
            DbCtx.SaveChanges();
        }
        
        public Usuario FindByEmail(string email)
        {
            Usuario usuario = DbCtx.Usuarios
                                        .AsEnumerable()
                                        .Where(Usuario => Usuario.Email.Valor == email)
                                        .SingleOrDefault() ?? throw new ExceptionUsuario("No existe un Usuario con ese email");
            return usuario;
        }

        public Usuario FindByCredentials(string email, string password)
        {
            Usuario usuario = DbCtx.Usuarios
                                        .AsEnumerable()
                                        .Where(Usuario => Usuario.Email.Valor == email &&
                                                Usuario.Contrasena.Valor == password)
                                        .SingleOrDefault() ?? throw new ExceptionUsuario("Email y/o contraseña incorrectos");
            return usuario;
        }

        public IEnumerable<Usuario> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}