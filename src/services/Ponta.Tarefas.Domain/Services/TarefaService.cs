using Microsoft.AspNetCore.Http;
using Ponta.Tarefas.Domain.Interfaces;
using Ponta.Tarefas.Domain.Models;
using Ponta.Tarefas.Domain.Models.Validations;
using System.Security.Claims;

namespace Ponta.Tarefas.Domain.Services
{
    public class TarefaService : BaseService, ITarefaService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private readonly ITarefaRepository _tarefaRepository;

        public TarefaService(INotificador notificador,
                             ITarefaRepository tarefaRepository,
                             IHttpContextAccessor httpContextAccessor) : base(notificador)
        {
            _tarefaRepository = tarefaRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Adicionar(Tarefa tarefa)
        {
            if (!ExecutarValidacao(new TarefaValidation(), tarefa)) return;

            tarefa.UsuarioId = ObterUsuarioIdCorrente();

            await _tarefaRepository.Adicionar(tarefa);

            await _tarefaRepository.UnitOfWork.Commit();
        }

        public async Task Atualizar(Tarefa tarefa)
        {
            if (!ExecutarValidacao(new TarefaValidation(), tarefa)) return;

            if (!await ValidarTarefaPertenceAoUsuario(tarefa.Id))
            {
                Notificar("Autorização de atualização negada, somente o proprietário da tarefa poderá atualiza-lo!");
                return;
            }

            _tarefaRepository.Atualizar(tarefa);

            await _tarefaRepository.UnitOfWork.Commit();
        }

        public async Task<Tarefa> ObterTarefaPorId(Guid tarefaId)
        {
            return await _tarefaRepository.ObterTarefaPorId(tarefaId);
        }

        public async Task<Guid> ObterUsuarioIdDaTarefa(Guid tarefaId)
        {
            return await _tarefaRepository.ObterUsuarioIdDaTarefa(tarefaId);
        }

        public async Task<IEnumerable<Tarefa>> ObterTarefas()
        {
            return await _tarefaRepository.ObterTarefas();
        }

        public async Task<IEnumerable<Tarefa>> ObterTarefasPorStatus(Status status)
        {
            return await _tarefaRepository.ObterTarefasPorStatus(status);
        }

        public async Task Remover(Guid tarefaId)
        {
            if (!await ValidarTarefaPertenceAoUsuario(tarefaId))
            {
                Notificar("Autorização de exclusão negada, somente o proprietário da tarefa poderá excluí-lo!");
                return;
            }

            _tarefaRepository.Remover(tarefaId);

            await _tarefaRepository.UnitOfWork.Commit();
        }

        private Guid ObterUsuarioIdCorrente()
        {   
            return Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        private async Task<bool> ValidarTarefaPertenceAoUsuario(Guid tarefaId)
        {
            Guid usuarioCorrente = ObterUsuarioIdCorrente();
            Guid usuarioPropietarioTarefa = await ObterUsuarioIdDaTarefa(tarefaId);

            return usuarioPropietarioTarefa.Equals(usuarioCorrente);
            
        }

        public void Dispose()
        {
            _tarefaRepository?.Dispose();
        }
    }
}
