using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Cidade { get; set; }
        public string Ra { get; set; }
        public string Nome { get; set; }

    }
}
