using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PruebaExperticker.Shared;
using System.Reflection;
using Microsoft.Data.Sqlite;

namespace PruebaExperticker.Server.DbConext
{
    public class MiDbContext : DbContext
    {
        public virtual DbSet<Persona> Personas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=TestDatabase.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            using (SqliteConnection con = new SqliteConnection("Data Source=TestDatabase.db;"))
            using (Microsoft.Data.Sqlite.SqliteCommand command = con.CreateCommand())
            {
                con.Open();
                command.CommandText = "SELECT name FROM sqlite_master WHERE name='Personas'";
                var name = command.ExecuteScalar();

                if (name != null && name.ToString() == "Personas")
                    return;
                // acount table not exist, create table and insert 
                command.CommandText = "CREATE TABLE Personas (Nombre VARCHAR(50),Apellidos VARCHAR(50),DNI VARCHAR(10) PRIMARY KEY,Sexo VARCHAR(10), FechaNacimiento TEXT, Direccion VARCHAR(50),Pais VARCHAR(50), CodigoPostal int)";
                command.ExecuteNonQuery();
            }
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map table names
            modelBuilder.Entity<Persona>().ToTable("Personas", "test");
            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.DNI);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
