using Data.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public  class PrivateMessage
    {
       
        public int Id { get; set; } 
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int SendId { get; set; }
        public User Sender  { get; set; }
        public int RecivedId { get; set; }
        public User Recived { get; set; }
        public bool IsRead { get; set; }=false;


    }
}
