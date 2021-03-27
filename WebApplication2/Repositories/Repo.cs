using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Repositories
{
    public class Repo
    {
        public ChatDbContext _db;
        public Repo(ChatDbContext db)
        {
            _db = db;
        }
        public MessagesViewModel addMessage(Messages model)
        {
            _db.Message.Add(model);
            _db.SaveChanges();
            MessagesViewModel messagesView = new MessagesViewModel() { 
            
            Chatid = model.Chatid,
            CreatedDate = model.CreatedDate,
            Message = model.Message,
            MessageId = model.MessageId,
            SenderId = model.SenderId,
            SenderName = _db.Users.FirstOrDefault(x=>x.UserId.Equals(model.SenderId)).UserName,
            userImage = _db.Users.FirstOrDefault(x=>x.UserId.Equals(model.SenderId)).ImageUrl,
            };
            return messagesView;
        }
        public List<MessagesViewModel> RetriveMessages()
        {
            return (from m in _db.Message
                    join u in _db.Users
                    on m.SenderId equals u.UserId
                    select new MessagesViewModel() {
                    Chatid = m.Chatid,
                    SenderId = u.UserId,
                    CreatedDate = m.CreatedDate,
                    Message = m.Message,
                    MessageId = m.MessageId,
                    SenderName = u.UserName,
                    userImage = u.ImageUrl
                    }).ToList() ;
        }
        public Users AddUser(Users model)
        {
            _db.Users.Add(model);
            _db.SaveChanges();
            return model;
        }
        public Users GetUserbyEmailAndPassword(Users model)
        {

            return _db.Users.FirstOrDefault(x => x.Email.Equals(model.Email) && x.Password.Equals(model.Password));
        }
    }
}
