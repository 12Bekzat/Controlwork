using System;
using System.Collections.Generic;
using System.Text;

namespace Vkontakte.Models
{
  public class Message
  {
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public string Value { get; set; }
    public bool IsRead { get; set; }
    public User User { get; set; }
  }
}
