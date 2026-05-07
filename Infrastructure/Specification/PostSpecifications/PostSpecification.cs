using Data.Entity.Posts;
using Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specification.PostSpecifications
{
    public  class PostSpecification
    {
        private string _Search;
        public string Search
        {
            get => _Search;
            set => _Search = value.Trim().ToLower();
        }

        public List<int>FollowingId { get; set; }   
        public int userid { get; set; }

        public int? PostMedia { get; set; }
        

    }
}
