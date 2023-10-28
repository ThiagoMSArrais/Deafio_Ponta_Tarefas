using Ponta.Core.DomainObjects;

namespace Ponta.Tarefas.Domain.Models
{
    public class Tarefa : Entity, IAggregateRoot
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public Status Status { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
