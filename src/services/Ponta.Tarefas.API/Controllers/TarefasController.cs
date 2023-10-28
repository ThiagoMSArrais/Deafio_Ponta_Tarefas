using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ponta.Tarefas.API.Application.DTO;
using Ponta.Tarefas.Domain.Interfaces;
using Ponta.Tarefas.Domain.Models;
using Ponta.WebAPI.Core.Controllers;

namespace Ponta.Tarefas.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class TarefasController : MainController
    {
        private readonly ITarefaService _tarefaService;
        private readonly IMapper _mapper;
        private readonly INotificador _notificador;

        public TarefasController(ITarefaService tarefaService, 
                                 IMapper mapper,
                                 INotificador notificador) : base (notificador)
        {
            _tarefaService = tarefaService;
            _mapper = mapper;
            _notificador = notificador;
        }

        [HttpGet("lista-tarefas")]
        public async Task<IActionResult> ObterTarefas()
        {
            return CustomResponse(_mapper.Map<IEnumerable<TarefaDTO>>(await _tarefaService.ObterTarefas()));
        }

        [HttpGet("obter-tarefa-id/{tarefaId}")]
        public async Task<IActionResult> ObterTarefasPorId(Guid tarefaId)
        {
            if (tarefaId == Guid.Empty) return NotFound();

            return CustomResponse(_mapper.Map<TarefaDTO>(await _tarefaService.ObterTarefaPorId(tarefaId)));
        }

        [HttpGet("obter-tarefas-por-status/{statusDTO}")]
        public async Task<IActionResult> ObterTarefasPorStatus(StatusDTO statusDTO)
        {
            if (string.IsNullOrEmpty(statusDTO.Humanize())) return NotFound();

            return CustomResponse(_mapper.Map<IEnumerable<TarefaDTO>>(await _tarefaService.ObterTarefasPorStatus(_mapper.Map<Status>(statusDTO))));
        }

        [HttpPost("nova-tarefa")]
        public async Task<IActionResult> AdicionarTarefa(TarefaDTO tarefaDTO)
        {
            if (!ModelState.IsValid) return CustomResponse(tarefaDTO);

            await _tarefaService.Adicionar(_mapper.Map<Tarefa>(tarefaDTO));

            return CustomResponse();
        }

        [HttpPut("atualizar-tarefa")]
        public async Task<IActionResult> AtualizarTarefa(TarefaDTO tarefaDTO)
        {
            if (!ModelState.IsValid) return CustomResponse(tarefaDTO);

            await _tarefaService.Atualizar(_mapper.Map<Tarefa>(tarefaDTO));

            return CustomResponse(_notificador.ObterNotificacoes());

        }

        [HttpDelete("excluir-tarefa/{tarefaId}")]
        public async Task<IActionResult> ExcluirTarefa(Guid tarefaId)
        {
            if (tarefaId.Equals(Guid.Empty)) return NotFound();

            await _tarefaService.Remover(tarefaId);

            return CustomResponse(_notificador.ObterNotificacoes());
        }
    }
}
