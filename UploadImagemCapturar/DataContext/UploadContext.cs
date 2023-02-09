using Microsoft.EntityFrameworkCore;
using UploadImagemCapturar.Models;
using static System.Net.Mime.MediaTypeNames;

namespace UploadImagemCapturar.DataContext
{
    public class UploadContext : DbContext
    {
        public UploadContext(DbContextOptions<UploadContext> options) : base(options)
        {
        }
        public DbSet<FileUpload> FileUpload { get; set; }
    }
}
