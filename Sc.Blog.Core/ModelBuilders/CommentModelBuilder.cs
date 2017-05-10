using AutoMapper;
using Glass.Mapper.Sc;
using Sc.Blog.Abstractions.ModelBuilders;
using Sc.Blog.Abstractions.Repositories;
using Sc.Blog.Model.Model;
using Sc.Blog.Model.ViewModels;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sc.Blog.Core.ModelBuilders
{
    public class CommentModelBuilder : IModelBuilder<CommentViewModel>
    {
        private IRepository<Comment, Guid> _commentRepository;
        private ISitecoreContext _context;
        public List<Exception> ModelErrors { get; }

        public CommentModelBuilder(IRepository<Comment, Guid> commentRepository,
            ISitecoreContext context)
        {
            _context = context;
            _commentRepository = commentRepository;
            ModelErrors = new List<Exception>();
        }
        
        public bool Build(CommentViewModel viewModel)
        {
            try
            {
                var comment = Mapper.Map<Comment>(viewModel);
                var result = _commentRepository.Create(comment);
                if(!result)
                {
                    ModelErrors.Add(_commentRepository.RepositoryErrors.LastOrDefault());
                    return false;
                }

                using (new SecurityDisabler())
                {
                    var item = _context.GetItem<Item>(viewModel.ArticleId);
                    item.Editing.BeginEdit();
                    MultilistField comments = new MultilistField(item.Fields["Comments"]);
                    comments.Add(comment.Id.ToString());
                    item.Editing.EndEdit();
                }
                return true;
            }
            catch (Exception ex)
            {
                ModelErrors.Add(ex);
                return false;
            }
            
        }
    }
}
