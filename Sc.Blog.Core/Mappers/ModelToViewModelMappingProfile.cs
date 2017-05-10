using AutoMapper;
using Glass.Mapper.Sc.Fields;
using Sc.Blog.Model.Model;
using Sc.Blog.Model.ViewModels;

namespace Sc.Blog.Core.Mappers
{
    public class ModelToViewModelMappingProfile : Profile
    {
        public ModelToViewModelMappingProfile()
        {
            CreateMap<ArticleViewModel, Article>()
                .ForMember(a => a.Image,
                                x => x.MapFrom(i => new Image
                                {
                                    MediaId = i.Image.ID.Guid
                                }));

            CreateMap<CommentViewModel, Comment>();
        }
    }
}