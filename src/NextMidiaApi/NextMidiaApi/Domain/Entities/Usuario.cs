using NextMidiaApi.Api.Models;

namespace NextMidiaApi.Domain.Entities
{
    public class Usuario
    {
        #region Constructors
        public Usuario()
        {
            Id = Guid.NewGuid();
            Nome = string.Empty;
            Email = string.Empty;
            Senha = string.Empty;
            MidiasFavoritas = new List<Midia>();
            Comentarios = new List<Comentario>();
            IsDeleted = false;
        }        
        #endregion

        #region Properties
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public List<Midia> MidiasFavoritas { get; set; }
        public List<Comentario> Comentarios { get; set; }
        public bool IsDeleted { get; set; }
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
            IsDeleted = true;
        }
        #endregion        
    }
}
