using System.Web;

namespace Sc.Blog.Abstractions.ModelBuilders
{
    public interface IArticleModelBuilder<TViewModel> : IModelBuilder<TViewModel> where TViewModel : class
    {
        bool Build(TViewModel viewModel, HttpPostedFileBase file);
    }
}
