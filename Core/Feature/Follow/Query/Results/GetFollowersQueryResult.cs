using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Follow.Query.Results
{
    public  class GetFollowersQueryResult
    {
        public int UserId { get; set; }
        public string UserName {  get; set; }
        public string Bio {  get; set; }
        public string Image {  get; set; }


    }
}
