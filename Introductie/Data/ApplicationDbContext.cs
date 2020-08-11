using System;
using System.Collections.Generic;
using System.Text;
using Introductie.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Introductie.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Klant> Klanten { get; set; }
        public DbSet<Afspraak> Afspraken { get; set; }

    }
}
