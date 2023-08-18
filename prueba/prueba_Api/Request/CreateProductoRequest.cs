using System.ComponentModel.DataAnnotations;

namespace prueba_Api.Request
{
    public class CreateProductoRequest
    {
        [Required(ErrorMessage = "El {0}  es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El {0} es obligatorio")]
        [MaxLength(300, ErrorMessage = "La longitud maxima del {0} es {1}")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El {0}  es obligatorio")]
        public double Salario { get; set; }
        [Required(ErrorMessage = "El {0}  es obligatorio")]
        public int Edad { get; set; }
    }
}
