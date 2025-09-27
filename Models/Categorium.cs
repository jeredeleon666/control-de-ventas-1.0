using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace control_de_ventas_1._0.Models;

public partial class Categorium
{
    [Key]
    public int CodigoCategoria { get; set; }

    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("CodigoCategoriaNavigation")]
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
