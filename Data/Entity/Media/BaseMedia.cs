using Data.Enums.Media;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity.Media
{
    public abstract class BaseMedia
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        public MedaiType Type { get; set; } 
        public DateTime CreatedAt { get; set; }= DateTime.Now;
    }
}
