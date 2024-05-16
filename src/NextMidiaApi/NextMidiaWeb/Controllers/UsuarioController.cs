using NextMidiaWeb.Api.Models;
using NextMidiaWeb.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace NextMidiaWeb.Api.Controllers
{
    [Route("usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        #region Properties
        private readonly UsuarioService _service;
        private readonly MidiaService _midiaService;
        #endregion

        #region Constructors
        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }
        #endregion        

        #region Endpoints           
        public IActionResult Login(LoginInput input)
        {

            var user = _service.FindByEmailAndSenha(input.Email, input.Senha);

            if (user != null)
            {
                return Ok(user);
            }

            return BadRequest(new { Message = "Dados inválidos." });
        }
        
        public IActionResult GetAll()
        {
            var usuarios = _service.FindAll();

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var usuario = _service.FindById(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult Create(UsuarioInput input)
        {
            if (input.Senha != input.ConfirmacaoSenha)
            {
                return BadRequest("Senha e confirmação de senha devem ser iguais");
            }

            var usuarioData = _service.FindByEmail(input.Email);

            if (usuarioData != null)
            {
                return BadRequest("Email já esta sendo utilizado");
            }

            Usuario usuario = new()
            {
                Nome = input.Nome,
                Email = input.Email,
                Senha = input.Senha,
            };

            _service.Create(usuario);

            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, UsuarioInput input)
        {
            var usuario = _service.FindById(id);

            if (usuario == null)
            {
                return NotFound();
            }

            var usuarioEmail = _service.FindByEmail(input.Email);

            if (usuarioEmail != null)
            {
                return BadRequest("Email já esta sendo utilizado");
            }

            usuario.Update(input);

            _service.Update(usuario);

            return NoContent();
        }

        [HttpPut("{id}/mudar-senha")]
        public IActionResult UpdatePassword(long id, UsuarioChangePasswordInput input)
        {
            var usuario = _service.FindById(id);

            if (usuario == null)
            {
                return NotFound();
            }

            if (input.Senha != input.ConfirmacaoSenha)
            {
                return BadRequest("Senha e confirmação de senha devem ser iguais");
            }

            usuario.UpdatePassword(input);

            _service.Update(usuario);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var usuario = _service.FindById(id);

            if (usuario == null)
            {
                return NotFound();
            }

            _service.Delete(usuario);

            return NoContent();
        }

        [HttpPost("{id}/favoritar-midia/{idMidia}")]
        public IActionResult FavoritarMidia(long id, long idMidia)
        {
            var usuario = _service.FindById(id);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            var midia = _midiaService.FindById(idMidia);
            if (midia == null)
            {
                return NotFound("Midia não encontrada.");
            }

            usuario.FavoritarMidia(midia);
            _service.Update(usuario);

            return NoContent();
        }

        [HttpGet("{id}/midia-favoritas")]
        public IActionResult MidiasFavoritas(long id)
        {
            var usuario = _service.FindById(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario.MidiasFavoritas);
        }

        [HttpPost("{id}/comentar-midia/{idMidia}")]
        public IActionResult ComentarMidia(long id, long idMidia, ComentarioInput comentarioInput)
        {
            var usuario = _service.FindById(id);

            if (usuario == null)
            {
                return NotFound();
            }

            var midia = _midiaService.FindById(idMidia);

            if (midia == null)
            {
                return NotFound("Midia não encontrada");
            }

            Comentario comentario = new Comentario()
            {
                Usuario = usuario,
                Midia = midia,
                Texto = comentarioInput.Texto,
                Nota = comentarioInput.Nota,
                Data = DateTime.Now
            };

            usuario.ComentarMidia(comentario);
            _service.Update(usuario);

            return NoContent();
        }
        #endregion
    }
}
