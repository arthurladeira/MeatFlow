using MeatFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeatFlow.Data.Context;

/// <summary>
/// Contexto principal do banco de dados da aplicação MeatFlow.
/// </summary>
public class MeatFlowDbContext : DbContext
{
    /// <summary>Inicializa o contexto com as opções fornecidas pelo container de DI.</summary>
    public MeatFlowDbContext(DbContextOptions<MeatFlowDbContext> options) : base(options) { }

    /// <summary>Estados cadastrados.</summary>
    public DbSet<Estado> Estados { get; set; }

    /// <summary>Cidades cadastradas.</summary>
    public DbSet<Cidade> Cidades { get; set; }

    /// <summary>Compradores cadastrados.</summary>
    public DbSet<Comprador> Compradores { get; set; }

    /// <summary>Carnes disponíveis.</summary>
    public DbSet<Carne> Carnes { get; set; }

    /// <summary>Pedidos realizados.</summary>
    public DbSet<Pedido> Pedidos { get; set; }

    /// <summary>Itens de pedido.</summary>
    public DbSet<ItemPedido> ItensPedido { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeatFlowDbContext).Assembly);
    }
}
