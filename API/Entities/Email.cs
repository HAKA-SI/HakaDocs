using System;

namespace API.Entities
{
    public class Email
    {
        public Email()
        {
            TimeToSend = DateTime.Now;
        }

        public int Id { get; set; }
        public int EmailTypeId { get; set; }
        public EmailType EmailType { get; set; }
        // public int? StudentId { get; set; }
        // public User Student { get; set; }
        public int RecipientId { get; set; }
        public AppUser Recipient { get; set; }
        public string ToAddress { get; set; }
        public string CCAddress { get; set; }
        public string BCCAddress { get; set; }
        public string FromAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime TimeToSend { get; set; }
        public byte StatusFlag { get; set; }
        // public int? OrderId { get; set; }
        // public Order Order { get; set; }
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public int SenderId { get; set; }
        public AppUser Sender { get; set; }
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        public int UpdateUserId { get; set; }
       // public AppUser UpdateUser { get; set; }
       
    }
}