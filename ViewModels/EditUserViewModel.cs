using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador.ViewModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Claims = new List<string>();
            Roles = new List<string>();
        }
        public string Id { get; set; }

        [Required(ErrorMessage = "O nome do usuário é obrigatório")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "O nome email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        public string Cidade { get; set; }
        public List<string> Claims { get; set; }
        public IList<string> Roles { get; set; }
    }
}

