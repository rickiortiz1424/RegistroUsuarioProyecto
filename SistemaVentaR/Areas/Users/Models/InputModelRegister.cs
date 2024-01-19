using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVentaR.Areas.Users.Models
{
    public class InputModelRegister
    {
        [Required(ErrorMessage = "El Campo Nombre es obligatorio.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El Campo Apellido es obligatorio.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El Campo nid es obligatorio.")]
        public string NID { get; set; }

        [Required(ErrorMessage = "El campo telefono es obligatorio.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{2})[-. ]?([0-9]{5})$", ErrorMessage = "El formato telefono ingresado no es válido.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage ="El Campo correo electronico es obligatorio")]
        [EmailAddress(ErrorMessage ="El Campo correo electronico no es una dirrecion de correo electronico valido")]
        public string Email { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage ="El campo contraseña es obligatorio.")]
        [StringLength(100, ErrorMessage = "El campo de caracteres de {0} debe de ser al menos {2}.", MinimumLength =6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Seleccione un rol")]
        public string Role { get; set; }

        public string ID { get; set; }

        public int Id { get; set; }

        public byte[] Image { get; set; }

        public IdentityUser IdentityUser { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

    }
}
