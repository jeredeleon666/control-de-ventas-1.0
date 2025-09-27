using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace control_de_ventas_1._0.Models;

public partial class Ventum
{
    [Key]
    public int CodigoVenta { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Fecha { get; set; }

    public int CodigoProducto { get; set; }

    [ForeignKey("CodigoProducto")]
    [InverseProperty("Venta")]
    public virtual Producto CodigoProductoNavigation { get; set; } = null!;
}
