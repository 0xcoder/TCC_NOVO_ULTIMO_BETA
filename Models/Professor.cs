using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador.Models
{
    public class Professor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Insira o Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Insira a Disciplina")]
        public string Disciplina { get; set; }

        [Required(ErrorMessage = "*Insira seu RF")]
        public int RF { get; set; }

    }
}
