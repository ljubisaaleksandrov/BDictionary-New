using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using BDictionary.BDictionary.UI.Models.Questions;
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
                cfg.CreateMap<QuestionCategory, QuestionCategoryViewModel>()
                                                                .ForMember(dest => dest.QuestionAnswerType, opt => opt.MapFrom(src => src.AnswerType))
                                                                .ForMember(dest => dest.Parent, opt => opt.MapFrom(src => src.QuestionCategory2.Name));
                cfg.CreateMap<QuestionCategoryViewModel, QuestionCategory>();

                cfg.CreateMap<QuestionAnswer, QuestionAnswerViewModel>();
                cfg.CreateMap<QuestionAnswerViewModel, QuestionAnswer>();

                cfg.CreateMap<Question, QuestionViewModel>()
                                                .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src.QuestionAnswer.Value))
                                                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.QuestionCategory.Name));
                cfg.CreateMap<QuestionViewModel, Question>();
                cfg.CreateMap<Question, QuestionListEntryViewModel>()
                                                .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src.QuestionAnswer.Value))
                                                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.QuestionCategory.Name));
                cfg.CreateMap<QuestionListEntryViewModel, Question>();

            }).CreateMapper());

            var container = builder.Build();
            var mvcResolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(mvcResolver);
        }
    }
}