using NextMidiaWeb.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using NextMidiaWeb.Models.Input;

namespace NextMidiaWeb.Api.Controllers
{
    [Route("api/categoria")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {

        private readonly CategoriaService _service;

        public CategoriaController(CategoriaService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categorias = _service.FindAll();

            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var categoria = _service.FindById(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(categoria);
        }

        [HttpPost]
        public IActionResult Create(CategoriaInput input)
        {

            Categoria categoria = new()
            {
                Nome = input.Nome
            };

            _service.Create(categoria);

            return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, categoria);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CategoriaInput input)
        {
            var categoria = _service.FindById(id);

            if (categoria == null)
            {
                return NotFound();
            }

            categoria.Update(input);

            _service.Update(categoria);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var categoria = _service.FindById(id);

            if (categoria == null)
            {
                return NotFound();
            }

            _service.Delete(categoria);

            return NoContent();
        }

    }
}
