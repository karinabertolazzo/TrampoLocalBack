using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrampoLocal.API.Data;
using TrampoLocal.API.DTOs;
using TrampoLocal.API.Models;

namespace TrampoLocal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfissionaisController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProfissionaisController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/profissionais
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfissionalResponseDto>>> Get()
        {
            var profissionais = await _context.Profissionais
                .Include(p => p.Categoria) 
                .Include(p => p.Servicos)
                .Include(p => p.Avaliacoes)
                .ToListAsync();

            var dtoList = profissionais.Select(p => new ProfissionalResponseDto
            {
                Id = p.Id,
                Nome = p.Nome,
                Cpf = p.CPF,
                Telefone = p.Telefone,
                Cidade = p.Cidade,
                Categoria = p.Categoria.Nome, 
                Descricao = p.Descricao,
                DataCadastro = p.DataCadastro,

                Servicos = p.Servicos.Select(s => new ServicoResponseDto
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    Descricao = s.Descricao,
                    Preco = s.Preco,
                    ProfissionalId = p.Id,
                    ProfissionalNome = p.Nome
                }).ToList(),

                Avaliacoes = p.Avaliacoes.Select(a => new AvaliacaoResponseDto
                {
                    Id = a.Id,
                    Nota = a.Nota,
                    Comentario = a.Comentario,
                    ProfissionalId = a.ProfissionalId,
                    ProfissionalNome = p.Nome
                }).ToList()
            }).ToList();

            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProfissionalResponseDto>> GetProfissional(int id)
        {
            var profissional = await _context.Profissionais
                .Include(p => p.Categoria)
                .Include(p => p.Servicos)
                .Include(p => p.Avaliacoes)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (profissional == null)
                return NotFound();

            var dto = new ProfissionalResponseDto
            {
                Id = profissional.Id,
                Nome = profissional.Nome,
                Cpf = profissional.CPF,
                Telefone = profissional.Telefone,
                Cidade = profissional.Cidade,
                Categoria = profissional.Categoria.Nome,
                Descricao = profissional.Descricao,
                DataCadastro = profissional.DataCadastro,
                Servicos = profissional.Servicos.Select(s => new ServicoResponseDto
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    Descricao = s.Descricao,
                    Preco = s.Preco,
                    ProfissionalId = profissional.Id,
                    ProfissionalNome = profissional.Nome
                }).ToList(),
                Avaliacoes = profissional.Avaliacoes.Select(a => new AvaliacaoResponseDto
                {
                    Id = a.Id,
                    Nota = a.Nota,
                    Comentario = a.Comentario,
                    ProfissionalId = a.ProfissionalId,
                    ProfissionalNome = profissional.Nome
                }).ToList()
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<ProfissionalResponseDto>> Post(ProfissionalCreateDto dto)
        {
            var profissional = new Profissional
            {
                Nome = dto.Nome,
                CPF = dto.CPF,
                Telefone = dto.Telefone,
                Cidade = dto.Cidade,
                CategoriaId = dto.CategoriaId, 
                Descricao = dto.Descricao,
                DataCadastro = DateTime.UtcNow
            };

            _context.Profissionais.Add(profissional);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProfissional),
                new { id = profissional.Id },
                profissional);
        }
    }
}