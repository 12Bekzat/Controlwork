using System;
using System.Collections.Generic;
using System.Text;

namespace Vkontakte.Models
{
  public class User
  {
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public ICollection<Message> Messages { get; set; } = new List<Message>();
  }
}
