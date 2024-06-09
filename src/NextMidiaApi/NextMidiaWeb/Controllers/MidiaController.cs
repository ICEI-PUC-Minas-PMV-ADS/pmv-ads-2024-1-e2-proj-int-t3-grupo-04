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
        private readonly MidiaTagDbContext _context;
        private readonly string Lang = "pt-BR";
        private readonly string TMDB_API_KEY = Configuration.ConfigurationManager.AppSetting["IntegrationAPIKeys:TMDB_API_KEY"] ?? "";
        private int pagExplorar = 1;
        #endregion

        #region Constructors
        public MidiaController(MidiaService service, TagService tagService, MidiaFavoritadaService midiaFavoritadaService, MidiaTagDbContext context, ILogger<LoginController> logger)
        {
            _service = service;
            _tagService = tagService;
            _midiaFavoritadaService = midiaFavoritadaService;
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
                    "https://api.themoviedb.org/3/trending/all/day",
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
                       $"https://api.themoviedb.org/3/movie/{listaMidiasDTO[0].Id.ToString()}",
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
                        Trailer = traillerDTO.key
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
                    $"https://api.themoviedb.org/3/movie/top_rated?language={Lang}&page={page}",
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
        public async Task<IActionResult> Midia(int id)
        {
            try
            {
                var midiaObj =
                    (TMDBMediaDetailObject)GetRequestContent(
                        $"https://api.themoviedb.org/3/movie/{id}language={Lang}",
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

                    return View("~/Views/Midia/DetalheMidia.cshtml", new DetalheMidiaViewModel { midia = midiaDTO });
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
                    var midia = this._service.FindById(id);
                    if (midia == null || midia.Id < 0) // Criar referência da mídia no banco de dados caso não exista.
                        this._service.Create(new Midia
                        {
                            Id = id,
                            Nome = ""
                        });

                    _midiaFavoritadaService.Create(new MidiaFavoritada
                    {
                        Data = DateTime.Now.Date,
                        Midia_Id = id,
                        Usuario_Id = long.Parse(idUsuario)
                    }); ;

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
                    $"https://api.themoviedb.org/3/search/collection?query={input.TextoLivre}&language={Lang}",
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
    }
    #endregion
}
