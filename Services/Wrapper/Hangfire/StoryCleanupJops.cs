using Services.Services.StoriesServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Wrapper.Hangfire
{
    public  class StoryCleanupJops
    {
        private readonly IStoryServices _services;

        public StoryCleanupJops(IStoryServices services)
        {
            _services = services;
        }



        public async Task Execute()
            => await _services.DeleteExprireSotories();
    }
}
