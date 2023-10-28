using Microsoft.EntityFrameworkCore;
using Ponta.Core.Data;
using Ponta.Tarefas.Domain.Interfaces;
using Ponta.Tarefas.Domain.Models;
using System.Linq;

namespace Ponta.Tarefas.Infra.Data.Repository
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly TarefaContext _context;

        public TarefaRepository(TarefaContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task Adicionar(Tarefa tarefa)
        {
            await _context.Tarefas.AddAsync(tarefa);
        }

        public void Atualizar(Tarefa tarefa)
        {
            _context.Tarefas.Update(tarefa);
        }

        public async Task<Tarefa> ObterTarefaPorId(Guid tarefaId)
        {
            return await _context.Tarefas.FindAsync(tarefaId);
        }

        public async Task<Guid> ObterUsuarioIdDaTarefa(Guid tarefaId)
        {
            return await _context.Tarefas.Where(t => t.Id == tarefaId).Select(t => t.UsuarioId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Tarefa>> ObterTarefas()
        {
            return await _context.Tarefas.AsNoTracking().OrderByDescending(t => t.DataCadastro).ToListAsync();
        }

        public async Task<IEnumerable<Tarefa>> ObterTarefasPorStatus(Status status)
        {
            return await _context.Tarefas.Where(t => t.Status.Equals(status)).OrderByDescending(t => t.DataCadastro).ToListAsync();
        }

        public void Remover(Guid tarefaId)
        {
            _context.Tarefas.Remove(new Tarefa() { Id = tarefaId });
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
