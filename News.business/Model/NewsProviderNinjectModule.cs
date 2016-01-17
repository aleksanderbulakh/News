using News.business.Interfaces;
using News.business.Provider;
using Ninject.Modules;
using System;

namespace News.business.Model
{
    public class NewsProviderNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<INewsProvider>().To<NewsProvider>();
        }
    }
}
