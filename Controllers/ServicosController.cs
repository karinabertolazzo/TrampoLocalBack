using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrampoLocal.API.Data;
using TrampoLocal.API.Models;

namespace TrampoLocal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ServicosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServicoResponseDto>>> GetServicos()
        {
            var servicos = await _context.Servicos
                .Include(s => s.Profissional)
                .Select(s => new ServicoResponseDto
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    Descricao = s.Descricao,
                    Preco = s.Preco,
                    ProfissionalId = s.ProfissionalId,
                    ProfissionalNome = s.Profissional.Nome
                })
                .ToListAsync();

            return Ok(servicos);
        }

        [HttpGet("profissional/{profissionalId}")]
        public async Task<ActionResult<IEnumerable<ServicoResponseDto>>> GetServicosPorProfissional(int profissionalId)
        {
            var servicos = await _context.Servicos
                .Where(s => s.ProfissionalId == profissionalId)
                .Select(s => new ServicoResponseDto
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    Descricao = s.Descricao,
                    Preco = s.Preco,
                    ProfissionalId = s.ProfissionalId,
                    ProfissionalNome = s.Profissional.Nome
                })
                .ToListAsync();

            return Ok(servicos);
        }

        // GET: api/Servicos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServicoResponseDto>> GetServico(int id)
        {
            var servico = await _context.Servicos
                .Where(s => s.Id == id)
                .Select(s => new ServicoResponseDto
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    Descricao = s.Descricao,
                    Preco = s.Preco,
                    ProfissionalId = s.ProfissionalId,
                    ProfissionalNome = s.Profissional.Nome
                })
                .FirstOrDefaultAsync();

            if (servico == null)
                return NotFound();

            return Ok(servico);
        }
        [HttpPost]
        public async Task<ActionResult<ServicoResponseDto>> PostServico(ServicoCreateDto dto)
        {
            var servico = new Servico
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                Preco = dto.Preco,
                ProfissionalId = dto.ProfissionalId
            };

            _context.Servicos.Add(servico);
            await _context.SaveChangesAsync();

            var servicoDto = await _context.Servicos
                .Where(s => s.Id == servico.Id)
                .Select(s => new ServicoResponseDto
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    Descricao = s.Descricao,
                    Preco = s.Preco,
                    ProfissionalId = s.ProfissionalId,
                    ProfissionalNome = s.Profissional.Nome
                })
                .FirstAsync();

            return CreatedAtAction(nameof(GetServico), new { id = servico.Id }, servicoDto);
        }

        // PUT: api/Servicos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServico(int id, Servico servico)
        {
            if (id != servico.Id) return BadRequest();

            _context.Entry(servico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Servicos.Any(e => e.Id == id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Servicos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServico(int id)
        {
            var servico = await _context.Servicos.FindAsync(id);
            if (servico == null) return NotFound();

            _context.Servicos.Remove(servico);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}