using MeatFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeatFlow.Data.Mappings;

/// <summary>
/// Configuração do mapeamento Fluent API da entidade <see cref="Cidade"/>.
/// </summary>
public class CidadeMap : IEntityTypeConfiguration<Cidade>
{
    /// <summary>Aplica as configurações de mapeamento da entidade para o banco de dados.</summary>
    public void Configure(EntityTypeBuilder<Cidade> builder)
    {
        builder.ToTable("cidade");

        builder.HasKey(x => x.IdtCidade);

        builder.Property(x => x.IdtCidade)
            .HasColumnName("IDT_CIDADE")
            .IsRequired();

        builder.Property(x => x.IdtEstado)
            .HasColumnName("IDT_ESTADO")
            .IsRequired();

        builder.Property(x => x.NomeCidade)
            .HasColumnName("nome_cidade")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.DatCriacao)
            .HasColumnName("dat_criacao")
            .IsRequired();

        builder.Property(x => x.DatAtualizacao)
            .HasColumnName("dat_atualizacao")
            .IsRequired();

        builder.HasOne(x => x.Estado)
            .WithMany(x => x.Cidades)
            .HasForeignKey(x => x.IdtEstado)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Compradores)
            .WithOne(x => x.Cidade)
            .HasForeignKey(x => x.IdtCidade)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
