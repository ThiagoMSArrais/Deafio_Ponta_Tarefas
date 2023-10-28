using Ponta.Tarefas.Domain.Models;

namespace Ponta.Tarefas.API.Application.DTO
{
    public class TarefaDTO
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public StatusDTO Status { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
