using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Ponta.Tarefas.Domain.Models;

namespace Ponta.Tarefas.Infra.Data.Mappings
{
    public class TarefaMapping : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Titulo)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(t => t.Descricao)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(t => t.DataCadastro)
                .IsRequired();

            builder.ToTable("Tarefas");
        }
    }
}
