using System;
using System.Collections.Generic;

namespace Sc.Blog.Abstractions.ModelBuilders
{
    public interface IModelBuilder<TViewModel> where TViewModel : class
    {
        bool Build(TViewModel viewModel);

        List<Exception> ModelErrors { get; }
    }
}