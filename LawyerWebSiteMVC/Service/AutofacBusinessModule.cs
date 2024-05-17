using Autofac;
using LawyerWebSiteMVC.Interface;
using LawyerWebSiteMVC.Service;

namespace LawyerWebSiteMVC.Service
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ArticleService>().As<IArticleService>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<LetterService>().As<ILetterService>().InstancePerLifetimeScope();
            builder.RegisterType<SliderService>().As<ISliderService>().InstancePerLifetimeScope();
            builder.RegisterType<AppUserService>().As<IAppUserService>().InstancePerLifetimeScope();
        }
    }
}