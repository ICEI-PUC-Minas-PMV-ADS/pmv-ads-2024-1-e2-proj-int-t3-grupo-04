using NextMidiaApi.Api.Models;
using NextMidiaApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NextMidiaApi.Domain.Persistence;

namespace NextMidiaApi.Api.Controllers
{
    [Route("api/midia")]
    [ApiController]
    public class MidiaController : ControllerBase
    {

        private readonly MidiaService _service;
        private readonly TagService _tagService;
        private readonly MidiaTagDbContext _context;

        public MidiaController(MidiaService service, TagService tagService, MidiaTagDbContext context)
        {
            _service = service;
            _tagService = tagService;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var midias = _service.FindAll();

            return Ok(midias);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
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
        public IActionResult Update(Guid id, MidiaInput input)
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
        public IActionResult Delete(Guid id)
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
        public async Task<IActionResult> AddOrUpdateRelationship(Guid id, Guid tagId)
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
}
