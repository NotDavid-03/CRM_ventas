using System;
using System.ComponentModel.DataAnnotations;

namespace CRM_Nexus_Retail.Models
{
    public class Omni_Cliente
    {
        public int Id_Nexus_Cliente { get; set; }

        [Required(ErrorMessage = "El código de referencia es obligatorio.")]
        [StringLength(20)]
        [Display(Name = "Código de Referencia")]
        public string Codigo_Ref { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(80)]
        [Display(Name = "Nombre")]
        public string Nombre_Pila { get; set; }

        [Required(ErrorMessage = "El apellido paterno es obligatorio.")]
        [StringLength(80)]
        [Display(Name = "Apellido Paterno")]
        public string Apellido_Paterno { get; set; }

        [StringLength(80)]
        [Display(Name = "Apellido Materno")]
        public string Apellido_Materno { get; set; }

        [Required(ErrorMessage = "El correo de contacto es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        [StringLength(150)]
        [Display(Name = "Correo de Contacto")]
        public string Correo_Contacto { get; set; }

        [Phone(ErrorMessage = "El formato del teléfono no es válido.")]
        [StringLength(20)]
        [Display(Name = "Línea Directa")]
        public string Linea_Directa { get; set; }

        [StringLength(250)]
        [Display(Name = "Domicilio Fiscal")]
        public string Domicilio_Fiscal { get; set; }

        [Display(Name = "Fecha de Alta")]
        [DataType(DataType.DateTime)]
        public DateTime Fecha_Alta { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El saldo no puede ser negativo.")]
        [Display(Name = "Saldo de Puntos")]
        public int Saldo_Puntos { get; set; }

        [Display(Name = "Estado de Cuenta")]
        public bool Estado_Cuenta { get; set; }

        [StringLength(500)]
        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }

        // Propiedad calculada: nombre completo
        public string Nombre_Completo
        {
            get
            {
                return string.Format("{0} {1} {2}", Nombre_Pila, Apellido_Paterno, Apellido_Materno).Trim();
            }
        }

        // Propiedad calculada: texto del estado
        public string Texto_Estado
        {
            get
            {
                return Estado_Cuenta ? "Activo" : "Inactivo";
            }
        }
    }
}