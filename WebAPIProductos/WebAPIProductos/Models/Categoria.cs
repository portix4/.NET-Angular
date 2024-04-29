using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebAPIProductos.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdCategoria { get; set; }
        public string? Descripcion { get; set; }

        [JsonIgnore] // para evitar el nulo del array ciclico de la llamada a la BBDD
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
