using Ponta.Core.Data;
using Ponta.Tarefas.Domain.Models;

namespace Ponta.Tarefas.Domain.Interfaces
{
    public interface ITarefaRepository : IRepository<Tarefa>
    {
        Task Adicionar(Tarefa tarefa);
        void Atualizar(Tarefa tarefa);
        void Remover(Guid tarefaId);
        Task<IEnumerable<Tarefa>> ObterTarefas();
        Task<IEnumerable<Tarefa>> ObterTarefasPorStatus(Status status);
        Task<Tarefa> ObterTarefaPorId(Guid tarefaId);
        Task<Guid> ObterUsuarioIdDaTarefa(Guid tarefaId);
    }
}
