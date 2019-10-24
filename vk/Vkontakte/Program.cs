using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Vkontakte.Models;

namespace Vkontakte
{
  class Program
  {
    static void Main(string[] args)
    {
      using (var context = new ChatContext())
      {
        while (true)
        {
          Chat chat = new Chat();
          int chooseTask;
          while (true)
          {
            Console.Clear();
            Console.WriteLine("Выберите пункт: \n1.Создать аккаунт\n2.Выбрать аккаунт");
            if (!int.TryParse(Console.ReadLine(), out chooseTask)) { continue; }

            var checkUser = context.Users.ToList();
            if (checkUser.Count == 2 && chooseTask == 1)
            {
              Console.WriteLine("Нельзя создать третьего пользователя!");
            }
            else if (chooseTask == 2) { break; }
            else if (chooseTask < 1 || chooseTask > 2) { Console.WriteLine("Не Корректно!"); }
          }

          switch (chooseTask)
          {
            case 1:
              Console.WriteLine("Введите имя: ");
              User user = new User();
              user.Name = Console.ReadLine();
              context.Add(user);
              context.SaveChanges();

              Console.WriteLine($"Добро пожаловать во вконтакте {user.Name}!");
              Console.WriteLine("Нажмите Enter что бы подключится к чату");
              Console.ReadLine();
              Console.Clear();

              while (true)
              {
                string messageThird = Console.ReadLine();
                if (string.Equals(messageThird, "/*exit*/")) { break; }
                Message messageValueThird = new Message();
                messageValueThird.User = user;
                messageValueThird.Value = messageThird;
                messageValueThird.IsRead = false;
                context.Add(messageValueThird);
                context.SaveChanges();
                Discuss(chat, user, messageValueThird, context);
                messageValueThird.Id = Guid.NewGuid();
                messageValueThird.IsRead = false;
                context.Add(messageValueThird);
                context.SaveChanges();
                Console.WriteLine("Нажмите Eneter чтобы написать сообщение!");
                Console.ReadLine();
                Console.Clear();
              }
              break;
            case 2:
              var accounts = context.Users.ToList();

              if (accounts == null)
              {
                Console.WriteLine("У вас нету аккаунтов");
                break;
              }

              int i = 0;
              foreach (var account in accounts)
              {
                Console.WriteLine($"{++i}) {account.Name}");
              }

              int chooseAccount;
              while (true)
              {
                if (!int.TryParse(Console.ReadLine(), out chooseAccount))
                {
                  Console.WriteLine("Не корректно!");
                  continue;
                }
                else
                {
                  if (chooseAccount == 1 || chooseAccount == 2) { break; }
                  else if (chooseAccount < 1 || chooseAccount > 2) { Console.WriteLine("Не корректно!"); continue; }
                }
              }

              switch (chooseAccount)
              {
                case 1:
                  Console.Clear();
                  while (true)
                  {
                    string messageFirst = Console.ReadLine();
                    if (string.Equals(messageFirst, "/*exit*/")) { break; }
                    Message messageValueSecond = new Message();
                    messageValueSecond.User = accounts[chooseAccount - 1];
                    messageValueSecond.Value = messageFirst;
                    messageValueSecond.IsRead = false;
                    context.Add(messageValueSecond);
                    context.SaveChanges();
                    Discuss(chat, accounts[chooseAccount - 1], messageValueSecond, context);
                    messageValueSecond.Id = Guid.NewGuid();
                    messageValueSecond.IsRead = false;
                    context.Add(messageValueSecond);
                    context.SaveChanges();
                    Console.WriteLine("Нажмите Eneter чтобы написать сообщение!");
                    Console.ReadLine();
                    Console.Clear();
                  }
                  break;
                case 2:
                  Console.Clear();
                  while (true)
                  {
                    string messageSecond = Console.ReadLine();
                    if (string.Equals(messageSecond, "/*exit*/")) { break; }
                    Message messageValueFirst = new Message();
                    messageValueFirst.User = accounts[chooseAccount];
                    messageValueFirst.Value = messageSecond;
                    messageValueFirst.IsRead = false;
                    context.Add(messageValueFirst);
                    context.SaveChanges();
                    Discuss(chat, accounts[chooseAccount - 1], messageValueFirst, context);
                    messageValueFirst.Id = Guid.NewGuid();
                    messageValueFirst.IsRead = false;
                    context.Add(messageValueFirst);
                    context.SaveChanges();
                    Console.WriteLine("Нажмите Eneter чтобы написать сообщение!");
                    Console.ReadLine();
                    Console.Clear();
                  }
                  break;
              }
              break;
          } // switch
        } // while
      } // using
    } // Main

    private static void Discuss(Chat chat, User user, Message messageValueFirst, ChatContext context)
    {
      chat.Users.Add(user);
      var messages = context.Messages.ToList();
      foreach (var messa in messages)
      {
        if (messa.IsRead == true) { context.Remove(messa); context.SaveChanges(); }
        else
        {
          chat.MessageShow(messa.User, messageValueFirst.Value);
          messageValueFirst = messa;
        }
      }

    } // Discuss
  } // Programm
} // namespace
