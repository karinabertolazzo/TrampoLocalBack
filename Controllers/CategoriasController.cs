using Microsoft.AspNetCore.Mvc;
using TrampoLocal.API.Data;
using TrampoLocal.API.DTOs;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
    {
        return await _context.Categorias.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Categoria>> PostCategoria(CategoriaCreateDto dto)
    {
        var categoria = new Categoria
        {
            Nome = dto.Nome
        };

        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCategorias), new { id = categoria.Id }, categoria);
    }
}