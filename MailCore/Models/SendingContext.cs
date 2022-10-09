using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace MailCore.Models
{
    public class SendingContext : DbContext
    {
        /// <summary>
        /// Сущность таблицы записей отправки
        /// </summary>
        public DbSet<RecordSending> RecordsSending { get; set; }

        /// <summary>
        /// Сущность таблицы сообщений
        /// </summary>
        public DbSet<Message> Messages { get; set; }

        /// <summary>
        /// Переопределение метода конфигурации с указанием провайдера Npgsql и строкой подключения
        /// </summary>
        /// <param DbContextOptionsBuilder="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString;
            optionsBuilder.UseNpgsql(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SendingContext).Assembly);
        }
    }
}
