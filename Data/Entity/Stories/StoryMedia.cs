using Data.Entity.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity.Stories
{
    public  class StoryMedia:BaseMedia
    {
        public int StoryId { get; set; }
        public Story Story { get; set; }

    }
}
