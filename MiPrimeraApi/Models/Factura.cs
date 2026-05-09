using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiPrimeraApi.Models
{
    public class Factura
    {
        [Key]
        public int Id { get; set; }

        //FK hacia la tabla de cliente
        public int ClienteId { get; set; }

        //propiedad de navegación - EF Core la usa para los joins
        public Cliente Cliente { get; set; }   
        
        public DateTime FechaFactura { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } = "pendiente"; // Pendiente, Pagada, Anulada

    }
}

/*
 * Representación SQL
CREATE TABLE Factura(
	Id				INT IDENTITY(1,1),
	ClienteId		INT NOT NULL,
	FechaFactura	DATETIME NOT NULL,
	Total			DECIMAL(18,2) NOT NUlL,
	Estado			VARCHAR(20) NOT NULL default ('pendiente'),

	CONSTRAINT Factura_Id PRIMARY KEY (Id),
	CONSTRAINT Factura_Cliente_ClienteId FOREIGN KEY (ClienteId) REFERENCES Cliente(Id)
);
*/