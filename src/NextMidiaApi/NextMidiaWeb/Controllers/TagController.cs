using NextMidiaWeb.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using NextMidiaWeb.Models.Input;
using Newtonsoft.Json;
using NextMidiaWeb.Models.ReponseObjects;
using NextMidiaWeb.Models.ViewModel;
using RestSharp;
using System.Globalization;
using NextMidiaWeb.Models.Enums;

namespace NextMidiaWeb.Api.Controllers
{
    public class TagController : Controller
    {

        private readonly TagService _service;

        public TagController(TagService service)
        {
            _service = service;
        }

        [Route("Tag/{nomeTag}/{idTag}")]
        public async Task<IActionResult> IndexAsync(int idTag, string nomeTag)
        {
            try
            {
                RestClient client;
                var request = new RestRequest("");
                var token = Configuration.ConfigurationManager.AppSetting["IntegrationAPIKeys:TMDB_API_KEY"] ?? "";
                request.AddHeaders(
                  [KeyValuePair.Create("accept", "application/json"),
                    KeyValuePair.Create("Authorization", $"Bearer {token}")]
               );

                if (Enum.IsDefined(typeof(eGenerosFilme), idTag))
                    client = new RestClient(new RestClientOptions($"https://api.themoviedb.org/3/discover/movie?include_adult=false&include_video=false&language=pt-BR&page=1&sort_by=popularity.desc&with_genres={idTag}"));
                else
                    client = new RestClient(new RestClientOptions($"https://api.themoviedb.org/3/discover/movie?include_adult=false&include_video=false&language=pt-BR&page=1&sort_by=popularity.desc&with_keywords={idTag}"));               

                var response = await client.GetAsync(request);
                var midiaListObj = JsonConvert.DeserializeObject<TMDBMediaListReponseObject>(response.Content!);
                var listaMidiasDTO = new List<Midia>();

                if (midiaListObj != null && midiaListObj.results != null)
                {
                    foreach (var midia in midiaListObj.results)
                    {                        
                        var request2 = new RestRequest("");
                        request2.AddHeaders(
                          [KeyValuePair.Create("accept", "application/json"),
                            KeyValuePair.Create("Authorization", $"Bearer {token}")]
                       );
                        var client2 = new RestClient(new RestClientOptions($"https://api.themoviedb.org/3/movie/{midia.id}/videos"));
                        var response2 = await client2.GetAsync(request2);
                        var trailer = JsonConvert.DeserializeObject<TMDBTrailersListObject>(response2.Content!);

                        var traillerDTO = trailer.results
                       .Where(trailer => trailer.official)
                       .Where(trailer => trailer.site == "YouTube")
                       .Where(trailer => trailer.type == "Trailer")
                       .FirstOrDefault();

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
                            Trailer = traillerDTO == null ? null : traillerDTO.key,                            
                        });
                    }
                }

                return View("~/Views/Tag/Index.cshtml", new TagViewModel { Midias = listaMidiasDTO, Tag = nomeTag });
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

    }
}
