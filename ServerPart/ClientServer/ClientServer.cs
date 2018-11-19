using ServerPart.DataBase;
using ServerPart.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerPart.ClientServer
{
    public enum MessageType
    {
        Error = 0,
        Authentification = 1,
        Message = 2,
        ListMessages = 3
    }
    public static class ClientServer
    {
        private static char[] splitArr = { ',' };
        public static string ParseMessage(string message)
        {
            string[] spType = message.Split(splitArr, 2);
            MessageType mt = (MessageType)Int32.Parse(spType[0]);
            switch (mt)
            {
                case MessageType.Error:
                    {
                        return "";
                    }
                case MessageType.Authentification:
                    {
                        string[] lp = spType[1].Split(splitArr, 2);
                        if (lp.Length < 2) return WriteResponse(MessageType.Error, "Неправильный формат аутентификационных данных");
                        var context = new Context();
                        Console.WriteLine("UsersCount="+context.Users.ToList().Count);
                        string NickName = lp[0];
                        var user = context.Users.Where(u => u.Nickname== NickName).FirstOrDefault();
                        if (user == null) return WriteResponse(MessageType.Error, "Неверное имя пользователя");
                        if (user.Password != lp[1]) return WriteResponse(MessageType.Error, "Неверный пароль");
                        return WriteResponse(MessageType.Authentification, "OK");
                    }
                case MessageType.Message:
                    {
                        string[] mr = spType[1].Split(splitArr, 3);
                        if (mr.Length < 3) return WriteResponse(MessageType.Error, "Неверное количество параметров сообщения");
                        var context = new Context();
                        string msg = mr[0];
                        string userName = mr[1];
                        string recepientName = mr[2];
                        if (msg==""|| userName==""|| recepientName=="") return WriteResponse(MessageType.Error, "Неверный формат сообщения");
                        var user = context.Users.Where(u => u.Nickname == userName).FirstOrDefault();
                        var recepient = context.Users.Where(u => u.Nickname == recepientName).FirstOrDefault();
                        if (user == null) return WriteResponse(MessageType.Error, "Имя отправителя задано неверно");
                        if (recepient == null) return WriteResponse(MessageType.Error, "Имя получателя задано неверно");

                        context.Messages.Add(new Message() { UserMessage = msg, Recipient = recepient, RecipientId = recepient.Id, Sender = user, SenderId = user.Id, isRead = false });
                        context.SaveChanges();
                        return WriteResponse(MessageType.Message, "Сообщение успешно добавлено");
                    }
                case MessageType.ListMessages:
                    {
                        string userName = spType[1];
                        List<string> resultMessges = new List<string>();
                        //if (userName == "") return WriteResponse(MessageType.Error, "Отсутствует имя пользователя для запрашиваемого списка сообщений");
                        var context = new Context();
                        var user = context.Users.Where(u => u.Nickname == userName).FirstOrDefault();
                        if (user == null) return WriteResponse(MessageType.Error, "Отсутствует имя пользователя для запрашиваемого списка сообщений");
                        foreach(var msg in user.ReceivedMessages)
                        {
                            var m = context.Entry(msg).Entity;
                            resultMessges.Add(m.Recipient.Nickname);
                            if (!(bool)msg.isRead)
                            {
                                msg.isRead = true;
                                resultMessges.Add("* " + msg.UserMessage);
                                context.Entry(msg).State = EntityState.Modified;
                            }
                            else
                                resultMessges.Add(msg.UserMessage);
                        }
                        context.Entry(user).State = EntityState.Modified;
                        context.SaveChanges();
                        return WriteResponse(MessageType.ListMessages, resultMessges.ToArray());
                    }
                default:
                    {
                        return WriteResponse(MessageType.Error, "Неизвестная ошибка");
                    }
            }
        }

        public static string WriteResponse(MessageType mt, params string[] data)
        {
            List<string> message = new List<string>();
            message.Add(((int)mt).ToString());
            message.AddRange(data);
            return String.Join(",", message);
        }



    }
}

