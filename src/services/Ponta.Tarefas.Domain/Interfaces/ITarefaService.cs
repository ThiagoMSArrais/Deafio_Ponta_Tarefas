using Ponta.Tarefas.Domain.Models;

namespace Ponta.Tarefas.Domain.Interfaces
{
    public interface ITarefaService : IDisposable
    {
        Task Adicionar(Tarefa tarefa);
        Task Atualizar(Tarefa tarefa);
        Task Remover(Guid tarefaId);
        Task<IEnumerable<Tarefa>> ObterTarefas();
        Task<IEnumerable<Tarefa>> ObterTarefasPorStatus(Status status);
        Task<Tarefa> ObterTarefaPorId(Guid tarefaId);
        Task<Guid> ObterUsuarioIdDaTarefa(Guid tarefaId);
    }
}
