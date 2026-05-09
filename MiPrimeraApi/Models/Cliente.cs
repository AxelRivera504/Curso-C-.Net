using System.ComponentModel.DataAnnotations;

namespace MiPrimeraApi.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(250)]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Telefono { get; set; }
        public bool Activo { get; set; } = true;
    }
}

/*
 * Representación en SQL
CREATE TABLE Cliente(
	Id		 INT IDENTITY(1,1),
	Nombre   VARCHAR(100) NOT NULL,
	Email    VARCHAR(250) NOT NULL,
	Telefono VARCHAR(20),
	Activo	 BIT default (1)

	CONSTRAINT Cliente_Id PRIMARY KEY (Id)
);
*/