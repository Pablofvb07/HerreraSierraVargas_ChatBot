using HerreraSierraVargas_ChatBot.Models;
using Microsoft.EntityFrameworkCore;

namespace HerreraSierraVargas_ChatBot.Data;

public class ChatDbContext : DbContext
{
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) { }

    public DbSet<ChatHistorial> ChatHistorial { get; set; }
}
