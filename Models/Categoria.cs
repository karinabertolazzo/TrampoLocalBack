using TrampoLocal.API.Models;

public class Categoria
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public List<Profissional> Profissionais { get; set; }
}