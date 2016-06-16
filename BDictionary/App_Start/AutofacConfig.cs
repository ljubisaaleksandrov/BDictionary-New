using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using BDictionary.Business;
using BDictionary.Domain;
using BDictionary.UI.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDictionary.UI
{
    public class AutofacConfig
    {
        public static void Configure()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            Type t = typeof(IQuestionCategoryService);
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies().Single(a => a.GetName().Name == "BDictionary.Business")).AsImplementedInterfaces();
            builder.RegisterType<BDictionaryEntities>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.Register<IMapper>(c => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<QuestionAnswer, QuestionAnswerViewModel>();
                cfg.CreateMap<QuestionAnswerViewModel, QuestionAnswer>();

                cfg.CreateMap<QuestionCategory, QuestionCategoryViewModel>();
                cfg.CreateMap<QuestionCategoryViewModel, QuestionCategory>();

            }).CreateMapper());

            var container = builder.Build();
            var mvcResolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(mvcResolver);
        }
    }
}