using Data.Enums.Notifacation;
using Data.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public NotifacationType Type { get; set; }
        public bool IsRead  { get; set; }=false;
        public DateTime CreatedAt { get; set; }=DateTime.UtcNow;
        public int RecipientId { get; set; }
        public int? SenderId { get; set; }
        public User Recipient { get; set; }
        public User? Sender {  get; set; }




    }
}
