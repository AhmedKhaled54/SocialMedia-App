using Data.Entity.Stories;
using Data.Identity;
using Microsoft.AspNetCore.Http;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.StoriesServices
{
    public  interface IStoryServices
    {
        Task AddStory(int userid,List<StoryMedia> medias);
        Task<List<StoryMedia>> StorymedaiProcess(List<IFormFile> files);
        Task DeleteExprireSotories();

        IQueryable<User> GetUserStories(int userid);
        IQueryable<User>GetFollowingStory(int userid);
        Task<Story>GetStoryById (int storyid);
        Task<bool>DeleteStory (Story story);
        Task<bool >DeleteStoryMedia(List<StoryMedia> media);

    }
}
