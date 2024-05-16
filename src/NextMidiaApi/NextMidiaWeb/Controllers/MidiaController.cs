using NextMidiaWeb.Api.Models;
using NextMidiaWeb.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NextMidiaWeb.Domain.Persistence;
using RestSharp;
using Newtonsoft.Json;

namespace NextMidiaWeb.Api.Controllers
{
    [Route("api/midia")]
    [ApiController]
    public class MidiaController : ControllerBase
    {
        #region Properties
        private readonly MidiaService _service;
        private readonly TagService _tagService;
        private readonly MidiaTagDbContext _context;
        private readonly string Lang = "pt-BR";
        private readonly string TMDB_API_KEY = Configuration.ConfigurationManager.AppSetting["IntegrationAPIKeys:TMDB_API_KEY"] ?? "";
        #endregion

        #region Endpoints
        public MidiaController(MidiaService service, TagService tagService, MidiaTagDbContext context)
        {
            _service = service;
            _tagService = tagService;
            _context = context;
        }

        [HttpGet("topRated")]
        public async Task<IActionResult> GetTopRatedMidia()
        {
            var client = new RestClient(new RestClientOptions($"https://api.themoviedb.org/3/movie/top_rated?language=pt-BR&page=1"));
            var request = new RestRequest("");
            request.AddHeaders(
               [KeyValuePair.Create("accept", "application/json"),
                KeyValuePair.Create("Authorization", $"Bearer {TMDB_API_KEY}")]
            );

            var response = await client.GetAsync(request);
            var midias = JsonConvert.DeserializeObject<TMDBReponseObject>(response.Content);
            var midiasFormatadas = new List<Midia>();

            if (midias != null)
            {
                if (midias.results.Count > 0)
                    midias.results.ForEach(
                        md => midiasFormatadas.Add(
                                new Midia
                                {
                                    Nome = md.title,
                                    Sinopse = md.overview,
                                    VoteAverage = md.vote_average,
                                    VoteCount = md.vote_count,
                                    ImagemCapa = md.backdrop_path,
                                    DataLancamento = md.release_date
                                }
                            ));
                else
                    return new StatusCodeResult(StatusCodes.Status204NoContent);
            }            

            return Ok(midiasFormatadas);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var client = new RestClient(new RestClientOptions($"https://api.themoviedb.org/3/movie/popular?language={Lang}&page=1"));
            var request = new RestRequest("");
            request.AddHeaders(
               [KeyValuePair.Create("accept", "application/json"),
                KeyValuePair.Create("Authorization", $"Bearer {TMDB_API_KEY}")]
            );

            var midias = await client.GetAsync(request);
            return Ok(new Midia());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var midia = _service.FindById(id);

            if (midia == null)
            {
                return NotFound();
            }

            return Ok(midia);
        }

        [HttpPost]
        public IActionResult Create(MidiaInput input)
        {

            Midia midia = new()
            {
                Nome = input.Nome,
                Sinopse = input.Sinopse,
            };

            _service.Create(midia);

            return CreatedAtAction(nameof(GetById), new { id = midia.Id }, midia);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, MidiaInput input)
        {
            var midia = _service.FindById(id);

            if (midia == null)
            {
                return NotFound();
            }

            midia.Update(input);

            _service.Update(midia);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var midia = _service.FindById(id);

            if (midia == null)
            {
                return NotFound();
            }

            _service.Delete(midia);

            return NoContent();
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
