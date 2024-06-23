using NextMidiaWeb.Models.Input;

namespace NextMidiaWeb.Domain.Entities
{
    public class Usuario
    {
        #region Constructors
        public Usuario()
        {
            Id = -1;
            Nome = string.Empty;
            Email = string.Empty;
            Senha = string.Empty;
            MidiasFavoritas = new List<Midia>();
            Comentarios = new List<Comentario>();
            Is_Deleted = false;
        }        

        public Usuario(int id)
        {
            
        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public List<Midia> MidiasFavoritas { get; set; }
        public List<Comentario> Comentarios { get; set; }
        public bool Is_Deleted { get; set; }
        #endregion

        #region Methods
        public void UpdatePassword(UsuarioChangePasswordInput input)
        {
            Senha = input.Senha;
        }

        public void Update(UsuarioInput input)
        {
            Nome = input.Nome;
            Email = input.Email;
        }

        public void FavoritarMidia(Midia midia)
        {
            MidiasFavoritas.Add(midia);
        }

        public void ComentarMidia(Comentario comentario)
        {
            Comentarios.Add(comentario);
        }

        public void Delete()
        {
            Is_Deleted = true;
        }
        #endregion        
    }
}
