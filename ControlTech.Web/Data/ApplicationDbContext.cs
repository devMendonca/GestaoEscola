using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

using ControlTech.Web.Models;

namespace ControlTech.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // propriedade que viraram tabelas
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Inscricao> Incricoes { get; set; }
        public DbSet<Estudante> Estudantes { get; set; }
       

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Curso>().ToTable("Curso");
            builder.Entity<Inscricao>().ToTable("Inscricao");
            builder.Entity<Estudante>().ToTable("Estudante");
        }

       

    }
}
