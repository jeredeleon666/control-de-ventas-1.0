using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace control_de_ventas_1._0.Models;

public partial class ControlVentasContext : DbContext
{
    public ControlVentasContext()
    {
    }

    public ControlVentasContext(DbContextOptions<ControlVentasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.CodigoCategoria);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.CodigoProducto);

            entity.HasOne(d => d.CodigoCategoriaNavigation)
                  .WithMany(p => p.Productos)
                  .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.CodigoVenta);

            entity.HasOne(d => d.CodigoProductoNavigation)
                  .WithMany(p => p.Venta)
                  .OnDelete(DeleteBehavior.ClientSetNull);
        });
    }
}