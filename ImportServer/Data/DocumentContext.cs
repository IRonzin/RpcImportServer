using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OpenDataViewerOld.Controllers
{
    public class DocumentContext : DbContext
    {
        private readonly static string _connectionString = "Host=25.29.171.129;Port=5432;Database=OpenDataViewerDB;Username=postgres;Password=password";

        public DbSet<Document> Documents { get; set; }

        public DbSet<File> Files { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }
    }

    public class Document
    {
        public int DocumentId { get; set; }
        public string Identifier { get; set; }
        public string Title { get; set; }
        public string Organization { get; set; }
        public string Topic { get; set; }

        public File File { get; set; }
    }

    public class File
    {
        public int FileId { get; set; }
        public string Source { get; set; }
        public string Format { get; set; }
        public string Data { get; set; }

        public int DocumentId { get; set; }
        public Document Document { get; set; }
    }
}
