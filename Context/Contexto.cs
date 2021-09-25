using Gerenciador.Models;
using GERENCIADOR_TESTE_TEMPLANTE.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador.Context
{
    public class Contexto : IdentityDbContext<ApplicationUser>
    {
        public Contexto(DbContextOptions<Contexto> options): base(options)
        { }
        public DbSet<Aluno> aluno { get; set; }
        public DbSet<Professor> professor { get; set; }
        public DbSet<FileOnDatabaseModel> FilesOnDatabase { get; set; }
    }
}
