using System;
using System.Collections.Generic;

namespace TrampoLocal.API.DTOs
{
    public class ProfissionalResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }

        public List<ServicoResponseDto> Servicos { get; set; } = new();
        public List<AvaliacaoResponseDto> Avaliacoes { get; set; } = new();
    }
}