using Data.Entity.Stories;
using Data.Enums.Media;
using Data.Follower;
using Data.Identity;
using Infrastructure.Abstract;
using Infrastructure.Specification.StorySpecifications;
using Infrastructure.Specification.UsersSpecifications;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.FilesServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.StoriesServices
{
    public class StoryServices : IStoryServices
    {
        private readonly IFileServices _fileServices;
        private readonly IUnitOfWork _UOW;
        public StoryServices(IFileServices fileServices, IUnitOfWork uOW)
        {
            _fileServices = fileServices;
            _UOW = uOW;
        }
        public async Task AddStory(int userid, List<StoryMedia> medias)
        {

            var story = new Story
            {
                UserId = userid,
                Media = medias,
                CreatedAt = DateTime.UtcNow,
                ExprieAt = DateTime.UtcNow.AddHours(24)
            };
            await _UOW.Repository<Story>().AddAsync(story);
            await _UOW.Complete();
        }



        public async Task<List<StoryMedia>> StorymedaiProcess(List<IFormFile> files)
        {
            var result = new List<StoryMedia>();

            foreach (var item in files)
            {
                var path = Path.GetExtension(item.FileName.ToLower());
                var type = path == ".mp4" ? MedaiType.Video : MedaiType.Image;
                var folder = type == MedaiType.Video ? "Stories/Videos" : "Stories/Images";
                var url = await _fileServices.UploadImage(item, folder);
                result.Add(new StoryMedia
                {
                    Type = type,
                    Url = url,
                    CreatedAt = DateTime.UtcNow

                });
            }

            return result;


        }
        public async Task DeleteExprireSotories()//Jops 
        {
            var stories = await _UOW.Repository<Story>().GetAllpridicated(s => s.ExprieAt <= DateTime.UtcNow).ToListAsync();

            foreach (var item in stories)
            {
                if (item.Media != null)
                {
                    foreach (var media in item.Media)
                    {
                        await _fileServices.RemoveImage(media.Url);
                    }
                }

            }
            _UOW.Repository<Story>().RemoveRange(stories);
            await _UOW.Complete();
        }

        public IQueryable<User> GetUserStories(int userid)
        {
            //    var stories = _UOW.Repository<User>()
            //        .GetAllpridicated(c => c.Id== userid&&c.Stories.Any(c=>c.ExprieAt>DateTime.Now), new[] { "Stories", "Stories.Media" });

            var specs = new UserWithStorySpecification(userid);
            var stories = _UOW.Repository<User>().GetEntitiesWithSpecs(specs);
            var result = stories.Where(c => c.Id == userid && c.Stories.Any(c => c.ExprieAt > DateTime.Now));
            return result;
        }

        public IQueryable<User> GetFollowingStory(int userid)
        {
            var followingid = _UOW.Repository<Follow>()
                .GetAllpridicated(c => c.FollowerId == userid).Select(c => c.FollowingId);

            var specs = new UserWithStorySpecification(userid,followingid.ToList());
            var stories = _UOW.Repository<User>().GetEntitiesWithSpecs(specs);
            //var result = _UOW.Repository<User>() //before specification pattern 
            //    .GetAllpridicated(c => followingid.Contains(c.Id) 
            //    && c.Stories.Any(c => c.ExprieAt > DateTime.Now), new[] { "Stories", "Stories.Media" });
            return stories;
        }

        public async Task<Story> GetStoryById(int storyid)
        {  
            var specs = new StoryWithUserAndMediaSepcification(storyid);
            var story =await  _UOW.Repository<Story>().GetEntityByIdSepcs(specs);
            return story;
        }

        public async Task<bool> DeleteStory(Story story)
        {
            _UOW.Repository<Story>().Delete(story);
            await _UOW.Complete();
            return true;
        }

        public async Task<bool> DeleteStoryMedia(List<StoryMedia> media)
        {
           _UOW.Repository<StoryMedia>().RemoveRange(media);
            await _UOW.Complete();
            return true;
        }
    }
}
