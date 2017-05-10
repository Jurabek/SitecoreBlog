using AutoMapper;
using Sc.Blog.Abstractions.Facades;
using Sc.Blog.Abstractions.ModelBuilders;
using Sc.Blog.Abstractions.Repositories;
using Sc.Blog.Model.Model;
using Sc.Blog.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Sc.Blog.Common.Constants;

namespace Sc.Blog.Core.ModelBuilders
{
    
    public class ArticleModelBuilder : IArticleModelBuilder<ArticleViewModel>
    {
        private IRepository<Article, Guid> _repository;
        private IMediaUploadFacade _mediaUploadFacade;

        public List<Exception> ModelErrors { get; }

        public ArticleModelBuilder(IRepository<Article, Guid> repository,
            IMediaUploadFacade mediaUploadFacade)
        {
            _repository = repository;
            _mediaUploadFacade = mediaUploadFacade;
            ModelErrors = new List<Exception>();
        }

        public bool Build(ArticleViewModel viewModel, HttpPostedFileBase file)
        {
            if (file != null)
            {
                viewModel.Image = _mediaUploadFacade.CreateMedaiItem(file.InputStream,
                    file.FileName, Folders.MediaLibrary.Images.Blog);
            }
            return Build(viewModel);
        }

        public bool Build(ArticleViewModel viewModel)
        {
            var model = Mapper.Map<Article>(viewModel);
            var result = _repository.Create(model);
            if (!result)
            {
                ModelErrors.Add(_repository.RepositoryErrors.LastOrDefault());
                return false;
            }
            return true;
        }
    }
}
