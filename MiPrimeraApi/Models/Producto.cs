using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiPrimeraApi.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(80)]
        public string Categoria { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

        [Required]
        public int Stock { get; set; }
    }
}


/*
 * Representación en SQL
CREATE TABLE Producto(
	Id		    INT IDENTITY(1,1),
	Nombre      VARCHAR(100) NOT NULL,
	Categoria   VARCHAR(80) NOT NULL,
	Precio      DECIMAL(18,2) NOT NUlL,
	Stock      INT NOT NUlL,

	CONSTRAINT Producto_Id PRIMARY KEY (Id)
);
*/
