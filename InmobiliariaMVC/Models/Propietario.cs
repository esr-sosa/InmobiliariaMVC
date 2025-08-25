using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaMVC.Models
{
	public class Propietario
	{
		[Key]//esto indica que es clave primaria
		[Display(Name = "Código Int.")]
		public int IdPropietario { get; set; }
		[Required]//no null
		public string Nombre { get; set; }
		[Required]
		public string Apellido { get; set; }
		[Required]
		public string Dni { get; set; }
		[Display(Name = "Teléfono")]//solo afecta como se muestra en la vista
		public string Telefono { get; set; }
		[Required, EmailAddress]
		public string Email { get; set; }
		[Required(ErrorMessage = "La clave es obligatoria"), DataType(DataType.Password)]// en la view se muestra como contraseña
		public string Clave { get; set; }

		public override string ToString()
		{
			//return $"{Apellido}, {Nombre}";
			//return $"{Nombre} {Apellido}";
			var res = $"{Nombre} {Apellido}";
			if(!String.IsNullOrEmpty(Dni)) {
				res += $" ({Dni})";
			}
			return res;
		}
	}
}