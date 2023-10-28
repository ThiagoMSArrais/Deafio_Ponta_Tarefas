using Ponta.Tarefas.Domain.Notificacoes;

namespace Ponta.Tarefas.Domain.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
