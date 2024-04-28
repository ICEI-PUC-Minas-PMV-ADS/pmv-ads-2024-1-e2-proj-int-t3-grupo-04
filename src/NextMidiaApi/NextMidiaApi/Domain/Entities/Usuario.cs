using NextMidiaApi.Api.Models;

namespace NextMidiaApi.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public List<Midia> MidiasFavoritas { get; set; }
        public List<Comentario> Comentarios { get; set; }

        public bool IsDeleted { get; set; }

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

    }
}
