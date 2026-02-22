using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrampoLocal.API.Data;
using TrampoLocal.API.Models;

namespace TrampoLocal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvaliacoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AvaliacoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Avaliacoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AvaliacaoResponseDto>>> GetAvaliacoes()
        {
            var avaliacoes = await _context.Avaliacoes
                .Include(a => a.Profissional)
                .ToListAsync();

            var dtos = avaliacoes.Select(a => new AvaliacaoResponseDto
            {
                Id = a.Id,
                Nota = a.Nota,
                Comentario = a.Comentario,
                ProfissionalId = a.ProfissionalId,
                ProfissionalNome = a.Profissional.Nome
            }).ToList();

            return Ok(dtos);
        }

        // GET: api/Avaliacoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AvaliacaoResponseDto>> GetAvaliacao(int id)
        {
            var avaliacao = await _context.Avaliacoes
                .Include(a => a.Profissional)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (avaliacao == null)
                return NotFound();

            var response = new AvaliacaoResponseDto
            {
                Id = avaliacao.Id,
                Nota = avaliacao.Nota,
                Comentario = avaliacao.Comentario,
                ProfissionalId = avaliacao.ProfissionalId,
                ProfissionalNome = avaliacao.Profissional.Nome
            };

            return Ok(response);
        }

        [HttpGet("profissional/{profissionalId}")]
        public async Task<ActionResult<IEnumerable<AvaliacaoResponseDto>>> GetPorProfissional(int profissionalId)
        {
            var avaliacoes = await _context.Avaliacoes
                .Include(a => a.Profissional) // 👈 ISSO AQUI
                .Where(a => a.ProfissionalId == profissionalId)
                .ToListAsync();

            var response = avaliacoes.Select(a => new AvaliacaoResponseDto
            {
                Id = a.Id,
                Nota = a.Nota,
                Comentario = a.Comentario,
                ProfissionalId = a.ProfissionalId,
                ProfissionalNome = a.Profissional.Nome
            });

            return Ok(response);
        }

        // POST: api/Avaliacoes
        [HttpPost]
        public async Task<ActionResult<AvaliacaoResponseDto>> PostAvaliacao(AvaliacaoCreateDto dto)
        {
            var profissional = await _context.Profissionais.FindAsync(dto.ProfissionalId);
            if (profissional == null)
                return NotFound("Profissional não encontrado");

            var avaliacao = new Avaliacao
            {
                Nota = dto.Nota,
                Comentario = dto.Comentario,
                ProfissionalId = dto.ProfissionalId
            };

            _context.Avaliacoes.Add(avaliacao);
            await _context.SaveChangesAsync();

            var responseDto = new AvaliacaoResponseDto
            {
                Id = avaliacao.Id,
                Nota = avaliacao.Nota,
                Comentario = avaliacao.Comentario,
                ProfissionalId = avaliacao.ProfissionalId,
                ProfissionalNome = profissional.Nome
            };

            return CreatedAtAction("GetAvaliacao", new { id = avaliacao.Id }, responseDto);
        }

        // DELETE: api/Avaliacoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAvaliacao(int id)
        {
            var avaliacao = await _context.Avaliacoes.FindAsync(id);
            if (avaliacao == null) return NotFound();

            _context.Avaliacoes.Remove(avaliacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}