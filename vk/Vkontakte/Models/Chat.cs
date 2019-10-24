using System;
using System.Collections.Generic;
using System.Text;

namespace Vkontakte.Models
{
  public class Chat
  {
    public ICollection<User> Users { get; set; } = new List<User>();

    public void MessageShow(User user, string message)
    {
      Console.WriteLine($"{user.Name}: {message}");
    }
  }
}
