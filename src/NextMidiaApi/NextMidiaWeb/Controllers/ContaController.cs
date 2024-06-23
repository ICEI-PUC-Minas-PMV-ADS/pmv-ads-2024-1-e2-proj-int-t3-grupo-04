using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using NextMidiaWeb.Domain.Entities;
using NextMidiaWeb.Domain.Services;
using NextMidiaWeb.Models.Input;
using NextMidiaWeb.Models.ReponseObjects;
using NextMidiaWeb.Models.ViewModel;
using RestSharp;
using System;
using System.ComponentModel.DataAnnotations;

namespace NextMidiaWeb.Controllers
{
    public class ContaController : Controller
    {
        #region Private Properties
        private readonly ILogger<ContaController> _logger;
        private readonly UsuarioService _usuarioService;
        private readonly ComentarioService _comentarioService;
        private readonly MidiaFavoritadaService _serviceMidiaFavorita;
        #endregion        

        #region Constructors
        public ContaController(ILogger<ContaController> logger, UsuarioService service, MidiaFavoritadaService midiaFavService, ComentarioService comentarioService)
        {
            _logger = logger;
            _usuarioService = service;
            _serviceMidiaFavorita = midiaFavService;
            _comentarioService = comentarioService;
        }
        #endregion

        #region Methods
        private IActionResult RedirectToIndex()
        {
            string email = HttpContext.Session.GetString("UserEmail") ?? "";
            string senha = HttpContext.Session.GetString("UserPassword") ?? "";

            var user = _usuarioService.FindByEmailAndSenha(email, senha);
            if (user != null)
            {
                ContaVidewModel model = new ContaVidewModel
                {
                    Userid = user.Id,
                    Username = user.Nome,
                    Email = user.Email,
                    Password = user.Senha
                };
                return View("~/Views/Conta/Index.cshtml", model);
            }
            else
                return Content("Erro ao carregar perfil de usuário, tente novamente.");
        }

        private async Task<List<Midia>> PreencherModelMidiaFavorita()
        {
            var id = int.Parse(HttpContext.Session.GetString("UserId"));
            var midiaIds = _serviceMidiaFavorita.GetByUserId(id);
            var listaMidiasDTO = new List<Midia>();

            foreach (var idMidia in midiaIds)
            {
                var token = Configuration.ConfigurationManager.AppSetting["IntegrationAPIKeys:TMDB_API_KEY"] ?? "";
                var client = new RestClient(new RestClientOptions($"https://api.themoviedb.org/3/movie/{idMidia.Midia_Id}?language=pt-BR"));
                var request = new RestRequest("");
                request.AddHeaders(
                   [KeyValuePair.Create("accept", "application/json"),
                    KeyValuePair.Create("Authorization", $"Bearer {token}")]
                );

                var response = await client.GetAsync(request);
                var midiaObj = JsonConvert.DeserializeObject<TMDBMediaDetailObject>(response.Content!);

                if (midiaObj != null)
                {
                    listaMidiasDTO.Add(new Midia
                    {
                        Id = midiaObj.id,
                        Nome = midiaObj.title,
                        Sinopse = midiaObj.overview,
                        MediaDeVotos = midiaObj.vote_average,
                        ContagemDeVotos = midiaObj.vote_count,
                        ImagemCapa = midiaObj.backdrop_path,
                        ImagemPoster = midiaObj.poster_path,
                        DataLancamento = midiaObj.release_date == null ? DateTime.MinValue : midiaObj.release_date.Value,
                        Bilheteria = midiaObj.revenue,
                        Generos = null,
                        Status = midiaObj.status,
                        Produtoras = null,
                        Trailer = null
                    });
                }
            }

            return listaMidiasDTO;
        }
        #endregion

        #region Endpoints
        [Route("Conta")]
        public IActionResult Index()
        {
            return RedirectToIndex();
        }

        [Route("Conta/CriarConta")]
        public IActionResult CriarConta()
        {
            return View();
        }

        [Route("Conta/CriarConta")]
        [HttpPost]
        public IActionResult CriarConta([FromForm] UsuarioInput usuarioInpt)
        {
            var temErro = false;
            if (usuarioInpt != null
                && usuarioInpt.Nome != null
                && usuarioInpt.Senha != null
                && usuarioInpt.Email != null)
            {
                //Nome
                if (usuarioInpt.Nome.Length < 5)
                {
                    temErro = true;
                    ViewBag.UsuarioError = "O nome deve ter no mínimo 5 caracteres.";
                }
                //Email
                else if (usuarioInpt.Email.Length < 8)
                {
                    temErro = true;
                    ViewBag.EmailError = "O e-mail deve ter no mínimo 5 caracteres.";
                }
                else if (!usuarioInpt.Email.Contains("@") || !usuarioInpt.Email.Contains(".com"))
                {
                    temErro = true;
                    ViewBag.EmailError = "Insira um endereço de e-mail válido";
                }
                //Senha
                else if (usuarioInpt.Senha.Length < 8)
                {
                    temErro = true;
                    ViewBag.SenhaError = "A senha deve conter no mínimo 8 caracteres.";
                }
                //Confirmação Senha
                else if (usuarioInpt.ConfirmacaoSenha.Length < 8)
                {
                    temErro = true;
                    ViewBag.ConfirmacaoSenhaError = "A senha deve conter no mínimo 8 caracteres.";
                }
                else if (usuarioInpt.ConfirmacaoSenha.Trim() != usuarioInpt.Senha.Trim())
                {
                    temErro = true;
                    ViewBag.ConfirmacaoSenhaError = "A senha e a confirmação de senha devem ser iguais.";
                }
            }
            else
            {
                ViewBag.GeneralError = "Preencha os campos para criar um usuário.";
                return View();
            }

            if (!temErro)
            {
                if (_usuarioService.FindAll()
                    .Where(user => user.Nome == usuarioInpt.Nome).Count() > 0)
                    ViewBag.GeneralError = "Já existe um usuário com este username cadastrado.";
                if (_usuarioService.FindAll()
                   .Where(user => user.Email == usuarioInpt.Email).Count() > 0)
                    ViewBag.GeneralError = "Já existe um usuário com este email cadastrado.";
                else
                    _usuarioService.Create(new Usuario
                    {
                        Nome = usuarioInpt.Nome,
                        Email = usuarioInpt.Email,
                        Senha = usuarioInpt.Senha
                    });
            }
            else
                return View("~/Views/Conta/CriarConta.cshtml");

            return View("~/Views/Login/Login.cshtml");
        }

        [Route("Conta/InserirComentarioMidia")]
        [HttpPost]
        public IActionResult InserirComentarioMidia([FromForm] ComentarioInput comentarioInput)
        {
            try
            {
                var idUsuario = HttpContext.Session.GetString("UserId") ?? "";
                if (idUsuario != "")
                {
                    if (comentarioInput == null)
                        return Content("Preencha a caixa de texto para inserir um comentário.");

                    this._comentarioService.Create(new Comentario
                    {
                        Midia_Id = comentarioInput.IdMidia,
                        Usuario_Id = int.Parse(idUsuario),
                        Texto = comentarioInput.Texto,
                        Data = DateTime.Now,
                    });
                    
                    return RedirectToAction($"Midia", "Midia", new { id = comentarioInput.IdMidia});
                }
                else
                    return Content("É necessário fazer o login para comentar nesta mídia.");

            }
            catch (Exception ex)
            {
                return Content($"Ocorreu um erro ao inserir o comentário na mídia: {ex.Message + ex.StackTrace + ex.InnerException}");
            }
        }

        [Route("Conta/AtualizarCadastro")]
        [HttpPost]
        public IActionResult AtualizarCadastro([FromForm] UsuarioInput usuarioInput)
        {
            try
            {
                // Deve estar logado e ter enviado dados válidos
                if (usuarioInput != null && HttpContext.Session.GetString("UserId") != null)
                {
                    var id = long.Parse(HttpContext.Session.GetString("UserId"));
                    var usuario = _usuarioService.FindById(id);

                    #region Validaçoes
                    if (_usuarioService.FindAll().Where(user => user.Nome == usuarioInput.Nome && user.Id != id).Count() > 0)
                        return Content("Este login já está sendo utilizado, escolha outro.");
                    else
                        usuario.Nome = usuarioInput.Nome;

                    if (_usuarioService.FindAll().Where(user => user.Email == usuarioInput.Email && user.Id != id).Count() > 0)
                        return Content("Este e-mail já está sendo utilizado, escolha outro.");
                    else
                        usuario.Nome = usuarioInput.Nome;
                    #endregion                    

                    _usuarioService.Update(usuario);
                    return this.RedirectToIndex();
                }
                else
                    return Content("Ocorreu algum erro, faça login novamente no sistema.");
            }
            catch (Exception e)
            {

                return Content($"Erro no endpoint ContaController: {e.Message}");
            }
        }

        [Route("Conta/MidiasFavoritas")]
        public async Task<IActionResult> MidiasFavoritas()
        {
            try
            {
                if (HttpContext.Session.GetString("UserId") != null)
                {
                    List<Midia> listaMidiasDTO = await PreencherModelMidiaFavorita();

                    return View("~/Views/Conta/MidiasFavoritas.cshtml", new MidiaViewModel { midias = listaMidiasDTO });
                }
                else
                    return Content($"Ocorreu um erroao carregar as mídias favoritas, favor retornar à página de usuário.");
            }
            catch (Exception e)
            {
                return Content($"Erro no endpoint ContaController: {e.Message}");
            }
        }

        [Route("Conta/{id}/RemoverFavorito")]
        public IActionResult RemoverFavorito(int id)
        {
            try
            {
                var idUsuario = HttpContext.Session.GetString("UserId") ?? "";
                if (idUsuario != "")
                {
                    var midiaFav = _serviceMidiaFavorita.GetById(id, int.Parse(idUsuario));
                    _serviceMidiaFavorita.Delete(midiaFav);

                    return RedirectToAction($"Midia", "Midia", new { id = id });
                }
                else
                    return Content("É necessário fazer o login para favoritar esta mídia.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
