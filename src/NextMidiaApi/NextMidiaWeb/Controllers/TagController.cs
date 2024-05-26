using NextMidiaWeb.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using NextMidiaWeb.Models.Input;

namespace NextMidiaWeb.Api.Controllers
{        
    public class TagController : Controller
    {

        private readonly TagService _service;

        public TagController(TagService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("Tag")]
        public IActionResult Index()
        {
            return View();
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
