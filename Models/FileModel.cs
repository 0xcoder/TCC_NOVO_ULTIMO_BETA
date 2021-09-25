using System;
using System.ComponentModel.DataAnnotations;

namespace GERENCIADOR_TESTE_TEMPLANTE.Models
{
    public class FileModel
    {
        public  int Id { get; set; } 
        public string Name { get; set; }
        public string FileType { get; set; }
        public string Extension { get; set; }
        public string Description { get; set; }
        public string UploadedBy { get; set; }
        public string status {get;set;}
        //public long idUsuario { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? CreatedOn { get; set; } 
    }
}