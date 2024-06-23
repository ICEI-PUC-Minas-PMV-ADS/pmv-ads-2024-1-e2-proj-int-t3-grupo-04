using NextMidiaWeb.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using NextMidiaWeb.Domain.Persistence;
using RestSharp;
using Newtonsoft.Json;
using NextMidiaWeb.Models.Input;
using NextMidiaWeb.Models.ReponseObjects;
using NextMidiaWeb.Models.ViewModel;
using NextMidiaWeb.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NextMidiaWeb.Domain.Services;

namespace NextMidiaWeb.Api.Controllers
{
    public class MidiaController : Controller
    {
        #region Properties
        private readonly ILogger<LoginController> _logger;
        private readonly MidiaService _service;
        private readonly TagService _tagService;
        private readonly MidiaFavoritadaService _midiaFavoritadaService;
        private readonly ComentarioService _comentarioService;
        private readonly UsuarioService _usuarioService;
        private readonly MidiaTagDbContext _context;
        private readonly string Lang = "pt-BR";
        private readonly string TMDB_API_KEY = Configuration.ConfigurationManager.AppSetting["IntegrationAPIKeys:TMDB_API_KEY"] ?? "";
        private int pagExplorar = 1;
        #endregion

        #region Constructors
        public MidiaController(MidiaService service, TagService tagService, UsuarioService usuarioService, MidiaFavoritadaService midiaFavoritadaService, ComentarioService comentarioService, MidiaTagDbContext context, ILogger<LoginController> logger)
        {
            _service = service;
            _tagService = tagService;
            _midiaFavoritadaService = midiaFavoritadaService;
            _comentarioService = comentarioService;
            _usuarioService = usuarioService;
            _context = context;
            _logger = logger;
        }
        #endregion

        #region Methods
        async Task<ITMDBReponseObject> GetRequestContent(string url, ReponseType type)
        {
            var client = new RestClient(new RestClientOptions(url));
            var request = new RestRequest("");
            request.AddHeaders(
               [KeyValuePair.Create("accept", "application/json"),
                    KeyValuePair.Create("Authorization", $"Bearer {TMDB_API_KEY}")]
            );

            var response = await client.GetAsync(request);

            if (type == ReponseType.Object)
                return JsonConvert.DeserializeObject<TMDBMediaDetailObject>(response.Content!);
            else if (type == ReponseType.List)
                return JsonConvert.DeserializeObject<TMDBMediaListReponseObject>(response.Content!);
            else
                return JsonConvert.DeserializeObject<TMDBTrailersListObject>(response.Content!);
        }

        private List<Midia> PreencherListaMidias(TMDBMediaListReponseObject midias)
        {
            if (midias != null)
            {
                var listaMidiasDTO = new List<Midia>();
                if (midias.results.Count > 0)
                    foreach (var md in midias.results)
                    {
                        listaMidiasDTO.Add(
                                new Midia
                                {
                                    Id = md.id,
                                    Nome = md.title,
                                    Sinopse = md.overview,
                                    MediaDeVotos = md.vote_average,
                                    ContagemDeVotos = md.vote_count,
                                    ImagemCapa = md.backdrop_path,
                                    ImagemPoster = md.poster_path,
                                    DataLancamento = md.release_date,
                                    IdsGenero = md.genre_ids
                                }
                            );
                    }

                return listaMidiasDTO;
            }
            else
                return new List<Midia>();
        }
        #endregion

        #region Endpoints    
        [Route("Midia")]
        public IActionResult Index()
        {
            try
            {
                var midias =
                (TMDBMediaListReponseObject)GetRequestContent(
                    $"https://api.themoviedb.org/3/movie/now_playing?language={Lang}&page=1",
                    ReponseType.List
                ).Result;

                if (midias != null)
                {
                    var listaMidiasDTO = new List<Midia>();
                    if (midias.results.Count > 0)
                        foreach (var md in midias.results)
                        {
                            listaMidiasDTO.Add(
                                    new Midia
                                    {
                                        Id = md.id,
                                        Nome = md.title,
                                        Sinopse = md.overview,
                                        MediaDeVotos = md.vote_average,
                                        ContagemDeVotos = md.vote_count,
                                        ImagemCapa = md.backdrop_path,
                                        ImagemPoster = md.poster_path,
                                        DataLancamento = md.release_date,
                                        IdsGenero = md.genre_ids
                                    }
                                );
                        }

                    #region Midia do Dia
                    var trailerObj =
                  (TMDBTrailersListObject)GetRequestContent(
                      $"https://api.themoviedb.org/3/movie/{listaMidiasDTO[0].Id.ToString()}/videos",
                      ReponseType.Trailer
                  ).Result;

                    var traillerDTO = trailerObj.results
                        .Where(trailer => trailer.official)
                        .Where(trailer => trailer.site == "YouTube")
                        .Where(trailer => trailer.type == "Trailer")
                        .FirstOrDefault();

                    var midiaObj =
                   (TMDBMediaDetailObject)GetRequestContent(
                       $"https://api.themoviedb.org/3/movie/{listaMidiasDTO[0].Id.ToString()}?language={Lang}",
                       ReponseType.Object
                   ).Result;

                    var generos = new List<Genero>();
                    if (midiaObj.genres != null)
                        foreach (var genero in midiaObj.genres)
                            generos.Add(new Genero { Id = genero.id, Nome = genero.name }); ;

                    var produtoras = new List<Produtora>();
                    if (midiaObj.production_companies != null)
                        foreach (var produtora in midiaObj.production_companies)
                            produtoras.Add(new Produtora { Id = produtora.id, Nome = produtora.name, Logo = produtora.logo_path, PaisOrigem = produtora.origin_country }); ;

                    var midiaDTO = new Midia
                    {
                        Id = midiaObj.id,
                        Nome = midiaObj.title,
                        Sinopse = midiaObj.overview,
                        MediaDeVotos = midiaObj.vote_average,
                        ContagemDeVotos = midiaObj.vote_count,
                        ImagemCapa = midiaObj.backdrop_path,
                        ImagemPoster = midiaObj.poster_path,
                        DataLancamento = midiaObj.release_date,
                        Bilheteria = midiaObj.revenue,
                        Verba = midiaObj.budget,
                        Generos = generos,
                        Status = midiaObj.status,
                        Produtoras = produtoras,
                        Trailer = traillerDTO == null ? string.Empty : traillerDTO.key
                    };

                    listaMidiasDTO.RemoveAt(0);
                    #endregion                                        

                    return View("~/Views/Midia/Midia.cshtml", new MidiaViewModel { midias = listaMidiasDTO, midiaDia = midiaDTO });
                }
                else
                    return Content("Ocorreu um erro ao retornar os dados, retorna a página e teste novamente.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("Midia/Explorar")]
        public IActionResult Explorar(int page = 1)
        {
            try
            {
                var midiaDTO = new List<Midia>();
                var midias =
                (TMDBMediaListReponseObject)GetRequestContent(
                    $"https://api.themoviedb.org/3/movie/popular?language={Lang}&page={page}",
                    ReponseType.List
                ).Result;

                var lista = PreencherListaMidias(midias);
                if (lista.Count > 0)
                    return View("~/Views/Midia/Explorar.cshtml", new MidiaViewModel { midias = lista });
                else
                    return Content("A busca nao retornou resultados.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("Midia/{id}")]
        public async Task<IActionResult> Midia(long id)
        {
            try
            {
                var midiaObj =
                    (TMDBMediaDetailObject)GetRequestContent(
                        $"https://api.themoviedb.org/3/movie/{id}?language=pt-BR",
                        ReponseType.Object
                    ).Result;

                var trailerObj =
                   (TMDBTrailersListObject)GetRequestContent(
                       $"https://api.themoviedb.org/3/movie/{id}/videos",
                       ReponseType.Trailer
                   ).Result;

                var traillerDTO = trailerObj.results
                    .Where(trailer => trailer.official)
                    .Where(trailer => trailer.site == "YouTube")
                    .Where(trailer => trailer.type == "Trailer")
                    .FirstOrDefault();

                if (midiaObj != null)
                {
                    var generos = new List<Genero>();
                    if (midiaObj.genres != null)
                        foreach (var genero in midiaObj.genres)
                            generos.Add(new Genero { Id = genero.id, Nome = genero.name }); ;

                    var produtoras = new List<Produtora>();
                    if (midiaObj.production_companies != null)
                        foreach (var produtora in midiaObj.production_companies)
                            produtoras.Add(new Produtora { Id = produtora.id, Nome = produtora.name, Logo = produtora.logo_path, PaisOrigem = produtora.origin_country }); ;

                    var midiaDTO = new Midia
                    {
                        Id = midiaObj.id,
                        Nome = midiaObj.title,
                        Sinopse = midiaObj.overview,
                        MediaDeVotos = midiaObj.vote_average,
                        ContagemDeVotos = midiaObj.vote_count,
                        ImagemCapa = midiaObj.backdrop_path,
                        ImagemPoster = midiaObj.poster_path,
                        DataLancamento = midiaObj.release_date,
                        Bilheteria = midiaObj.revenue,
                        Verba = midiaObj.budget,
                        Generos = generos,
                        Status = midiaObj.status,
                        Produtoras = produtoras,
                        Trailer = traillerDTO == null ? null : traillerDTO.key
                    };

                    #region Comentários
                    var listaComentariosDTO = new List<ComentarioUsuario>();
                    var comentarios = this._comentarioService.GetCommentList(midiaObj.id);

                    foreach (var comentario in comentarios)
                    {
                        listaComentariosDTO.Add(new ComentarioUsuario
                        {
                            NomeUsuario = this._usuarioService.FindById(comentario.Usuario_Id).Nome,
                            Data = comentario.Data,
                            Texto = comentario.Texto
                        });
                    }
                    #endregion                    

                    return View("~/Views/Midia/DetalheMidia.cshtml", new DetalheMidiaViewModel { midia = midiaDTO, mostrarComentarios = true, comentariosUsuario = listaComentariosDTO });
                }
                else
                    return Content("Ocorreu um erro ao retornar os dados, retorna a página e teste novamente.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("Midia/{id}/Favoritar")]
        public IActionResult FavoritarMidia(int id)
        {
            try
            {
                var idUsuario = HttpContext.Session.GetString("UserId") ?? "";
                if (idUsuario != "")
                {
                    // Criar referência da mídia no banco de dados caso não exista.
                    var midia = this._service.FindById((int)id);
                    if (midia == null || midia.Id < 0)
                        this._service.Create(new Midia
                        {
                            Id = id,
                            Nome = ""
                        });

                    // Favoritar
                    var isFav = _midiaFavoritadaService
                        .GetById(id, int.Parse(idUsuario)) != null;

                    if (!isFav)
                        _midiaFavoritadaService.Create(new MidiaFavoritada
                        {
                            Data = DateTime.Now.Date,
                            Midia_Id = id,
                            Usuario_Id = int.Parse(idUsuario)
                        });

                    return this.Midia(id).Result;
                }
                else
                    return Content("É necessário fazer o login para favoritar esta mídia.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("Midia/{id}/RemoverFavorito")]
        public IActionResult RemoverFavorito(int id)
        {
            try
            {
                var idUsuario = HttpContext.Session.GetString("UserId") ?? "";
                if (idUsuario != "")
                {
                    var midiaFav = _midiaFavoritadaService.GetById(id, int.Parse(idUsuario));
                    _midiaFavoritadaService.Delete(midiaFav);
                    return View("~/Views/Conta/Index.cshtml");
                }
                else
                    return Content("É necessário fazer o login para favoritar esta mídia.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("Midia/RealizarBusca")]
        public IActionResult RealizarBusca([FromForm] PesquisaFiltrosInput input)
        {
            try
            {
                if (input.TextoLivre == null)
                    return Content("A caixa de texto deve ser preenchida para realizar buscas.");

                var midias =
               (TMDBMediaListReponseObject)GetRequestContent(
                    $"https://api.themoviedb.org/3/search/movie?query={input.TextoLivre}&include_adult=false&language=pt-BR&page=1",
                   ReponseType.List
               ).Result;

                var lista = PreencherListaMidias(midias);
                if (input.FiltrarPor != null)
                {
                    switch (input.FiltrarPor)
                    {
                        case eFiltrarPor.DataLancamento:
                            lista = lista.OrderBy(lst => lst.DataLancamento).ToList();
                            break;

                        case eFiltrarPor.DataLancamentoDescrescente:
                            lista = lista.OrderByDescending(lst => lst.DataLancamento).ToList();
                            break;

                        case eFiltrarPor.Visualizacoes:
                            lista = lista.OrderBy(lst => lst.Bilheteria).ToList();
                            break;

                        case eFiltrarPor.Popularidade:
                            lista = lista.OrderBy(lst => lst.MediaDeVotos).ToList();
                            break;

                        default:
                            break;
                    }
                }

                if (lista.Count > 0)
                    return View("~/Views/Midia/Explorar.cshtml", new MidiaViewModel { midias = lista });
                else
                    return Content("A busca nao retornou resultados.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("{id}/add-tag/{tagId}")]
        public async Task<IActionResult> AddOrUpdateRelationship(int id, int tagId)
        {

            var midia = _service.FindById(id);

            if (midia == null)
            {
                return NotFound();
            }

            var tag = _tagService.FindById(id);

            if (tag == null)
            {
                return NotFound();
            }

            var midiaTag = _context.MidiaTags
                .First(midiaTag => midiaTag.Midia == midia && midiaTag.Tag == tag);

            if (midiaTag != null)
            {
                midiaTag.Peso += 1;
            }
            else
            {
                midiaTag = new MidiaTag
                {
                    Midia = midia,
                    Tag = tag,
                    Peso = 1
                };
                _context.MidiaTags.Add(midiaTag);
            }

            _context.SaveChanges();
            return Ok(midiaTag);
        }
        #endregion
    }
}
