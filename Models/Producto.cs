using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace control_de_ventas_1._0.Models;

[Table("Producto")]
public partial class Producto
{
    [Key]
    public int CodigoProducto { get; set; }

    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    public int CodigoCategoria { get; set; }

    [ForeignKey("CodigoCategoria")]
    [InverseProperty("Productos")]
    public virtual Categorium CodigoCategoriaNavigation { get; set; } = null!;

    [InverseProperty("CodigoProductoNavigation")]
    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
