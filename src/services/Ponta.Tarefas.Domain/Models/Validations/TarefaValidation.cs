using FluentValidation;

namespace Ponta.Tarefas.Domain.Models.Validations
{
    public class TarefaValidation : AbstractValidator<Tarefa>
    {
        public TarefaValidation()
        {
            RuleFor(t => t.Titulo)
                .NotEmpty().WithMessage("O título não pode estar vazio.")
                .Length(2, 100).WithMessage("O título precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(t => t.Titulo)
                .NotEmpty().WithMessage("A descrição não pode estar vazio.")
                .Length(2, 250).WithMessage("A descrição precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
