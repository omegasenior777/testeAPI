using Microsoft.AspNetCore.Mvc;
using FilmesApi.Models;

namespace FilmesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private static List<Filme> filmes = new List<Filme>();
        private static int id = 0;
        [HttpPost]
        public IActionResult AdicionaFilme([FromBody]  Filme filme)
        {
            filme.Id = id++;
            filmes.Add(filme);
            return CreatedAtAction(nameof(RecuperaFilmePorId),
                new { id = filme.Id },
                filme);
        }
        [HttpGet]
        public IEnumerable<Filme> RecuperaFilmes([FromQuery] int skip = 0,
        [FromQuery] int take = 50)
        {
             return filmes.Skip(skip).Take(take);
            // return filmes.Skip(50).Take(10);
            //pula 50 elementos e seleciona 10 -- de 100 Ids pega do 50 ao 59
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaFilmePorId(int id)
        {
            var filme = filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound();
            return Ok(filme);
        }


    }
}
