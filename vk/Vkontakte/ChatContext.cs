using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Vkontakte.Models;

namespace Vkontakte
{
  public class ChatContext : DbContext
  {
    public ChatContext()
    {
      Database.EnsureCreated();
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Message> Messages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer("Server=A-305-07;Database=Chat;Trusted_Connection=True");
    }
  }
}
