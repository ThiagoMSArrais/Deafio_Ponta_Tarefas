using AutoMapper;
using Ponta.Tarefas.API.Application.DTO;
using Ponta.Tarefas.Domain.Models;

namespace Ponta.Tarefas.API.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<TarefaDTO, Tarefa>().ReverseMap();
            CreateMap<StatusDTO, Status>().ReverseMap();
        }
    }
}
