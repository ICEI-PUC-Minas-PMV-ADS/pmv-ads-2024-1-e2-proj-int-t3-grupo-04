using NextMidiaWeb.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using NextMidiaWeb.Models.Input;
using Newtonsoft.Json;
using NextMidiaWeb.Models.ReponseObjects;
using NextMidiaWeb.Models.ViewModel;
using RestSharp;
using System.Globalization;
using NextMidiaWeb.Models.Enums;
using Azure;

namespace NextMidiaWeb.Api.Controllers
{
    public class TagController : Controller
    {
        #region Private Properties
        private readonly TagService _service;
        #endregion

        #region Constructors
        public TagController(TagService service)
        {
            _service = service;
        }
        #endregion

        #region Methods
        private static async Task<List<Midia>> PreencherDTOMidiaTag(TMDBMediaListReponseObject? midiaListObj)
        {
            var listaMidiasDTO = new List<Midia>();

            if (midiaListObj != null && midiaListObj.results != null)
            {
                foreach (var midia in midiaListObj.results)
                {
                   // var request2 = new RestRequest("");
                   // request2.AddHeaders(
                   //   [KeyValuePair.Create("accept", "application/json"),
                   //         KeyValuePair.Create("Authorization", $"Bearer {Configuration.ConfigurationManager.AppSetting["IntegrationAPIKeys:TMDB_API_KEY"] ?? ""}")]
                   //);
                   // var client2 = new RestClient(new RestClientOptions($"https://api.themoviedb.org/3/movie/{midia.id}/videos"));
                   // var response2 = await client2.GetAsync(request2);
                   // var trailer = JsonConvert.DeserializeObject<TMDBTrailersListObject>(response2.Content!);

                   // var traillerDTO = trailer.results
                   //.Where(trailer => trailer.official)
                   //.Where(trailer => trailer.site == "YouTube")
                   //.Where(trailer => trailer.type == "Trailer")
                   //.FirstOrDefault();

                    listaMidiasDTO.Add(new Midia
                    {
                        Id = midia.id,
                        Nome = midia.title,
                        Sinopse = midia.overview,
                        MediaDeVotos = midia.vote_average,
                        ContagemDeVotos = midia.vote_count,
                        ImagemCapa = midia.backdrop_path,
                        ImagemPoster = midia.poster_path,
                        DataLancamento = midia.release_date,
                        Bilheteria = midia.revenue,
                        Generos = null,
                        Status = midia.status,
                        Produtoras = null,
                        Trailer = null,
                        IdsGenero = midia.genre_ids
                    });
                }
            }

            return listaMidiasDTO;
        }
        #endregion

        #region Endpoints
        [Route("Tag")]
        public async Task<IActionResult> IndexAsync()
        {
            try
            {
                List<Midia> listaMidiasDTO = new List<Midia>();
                var generos = Enum.GetValues(typeof(eGenerosFilme)).Cast<eGenerosFilme>();

                foreach (var id in generos)
                {
                    // Reduzir o número de requisiçoes para otimizar o carregamento da página.
                    if ((long)id == (long)eGenerosFilme.Terror
                        || (long)id == (long)eGenerosFilme.Thriller
                        || (long)id == (long)eGenerosFilme.Documentário
                        || (long)id == (long)eGenerosFilme.Romance
                        || (long)id == (long)eGenerosFilme.Guerra
                        || (long)id == (long)eGenerosFilme.Crime
                        || (long)id == (long)eGenerosFilme.História)
                        continue;

                    RestClient client = new RestClient(new RestClientOptions(
                    $"https://api.themoviedb.org/3/discover/movie?include_adult=false&include_video=false&language=pt-BR&page=1&sort_by=popularity.desc&with_genres={(long)id}"));

                    var request = new RestRequest("");
                    request.AddHeaders(
                      [KeyValuePair.Create("accept", "application/json"),
                    KeyValuePair.Create("Authorization", $"Bearer {Configuration.ConfigurationManager.AppSetting["IntegrationAPIKeys:TMDB_API_KEY"] ?? ""}")]
                    );

                    var response = await client.GetAsync(request);
                    var midiaListObj = JsonConvert.DeserializeObject<TMDBMediaListReponseObject>(response.Content!);
                    listaMidiasDTO.AddRange(await PreencherDTOMidiaTag(midiaListObj));
                }

                listaMidiasDTO = listaMidiasDTO.Distinct().ToList();
                return View("~/Views/Tag/Index.cshtml", new TagViewModel { Midias = listaMidiasDTO, Tag = string.Empty, tagEspecifica = false }); ;
            }
            catch (Exception ex)
            {
                return Content($"Ocorreu um erro ao carregar a página de Tags : {ex.Message}");
            }
        }

        [Route("Tag/{nomeTag}/{idTag}")]
        public async Task<IActionResult> BuscaTag(int idTag, string nomeTag)
        {
            try
            {
                RestClient client;
                var request = new RestRequest("");
                request.AddHeaders(
                  [KeyValuePair.Create("accept", "application/json"),
                    KeyValuePair.Create("Authorization", $"Bearer {Configuration.ConfigurationManager.AppSetting["IntegrationAPIKeys:TMDB_API_KEY"] ?? ""}")]
               );

                if (Enum.IsDefined(typeof(eGenerosFilme), idTag))
                    client = new RestClient(new RestClientOptions($"https://api.themoviedb.org/3/discover/movie?include_adult=false&include_video=false&language=pt-BR&page=1&sort_by=popularity.desc&with_genres={idTag}"));
                else
                    client = new RestClient(new RestClientOptions($"https://api.themoviedb.org/3/discover/movie?include_adult=false&include_video=false&language=pt-BR&page=1&sort_by=popularity.desc&with_keywords={idTag}"));

                var response = await client.GetAsync(request);
                var midiaListObj = JsonConvert.DeserializeObject<TMDBMediaListReponseObject>(response.Content!);
                List<Midia> listaMidiasDTO = await PreencherDTOMidiaTag(midiaListObj);

                return View("~/Views/Tag/Index.cshtml", new TagViewModel { Midias = listaMidiasDTO, Tag = nomeTag, tagEspecifica = true }); ;
            }
            catch (Exception ex)
            {
                return Content($"Ocorreu um erro ao carregar a tela de tags: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var tags = _service.FindAll();

            return Ok(tags);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var tag = _service.FindById(id);

            if (tag == null)
            {
                return NotFound();
            }

            return Ok(tag);
        }

        [HttpPost]
        public IActionResult Create(TagInput input)
        {

            Tag tag = new()
            {
                Nome = input.Nome
            };

            _service.Create(tag);

            return CreatedAtAction(nameof(GetById), new { id = tag.Id }, tag);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TagInput input)
        {
            var tag = _service.FindById(id);

            if (tag == null)
            {
                return NotFound();
            }

            tag.Update(input);

            _service.Update(tag);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var tag = _service.FindById(id);

            if (tag == null)
            {
                return NotFound();
            }

            _service.Delete(tag);

            return NoContent();
        }

        #endregion        
    }
}
