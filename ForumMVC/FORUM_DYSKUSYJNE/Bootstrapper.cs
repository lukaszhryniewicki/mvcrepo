using System.Web.Mvc;
using FORUM_DYSKUSYJNE.Core.Contracts;
using FORUM_DYSKUSYJNE.DataAccess;
using FORUM_DYSKUSYJNE.DataAccess.Repositories;
using FORUM_DYSKUSYJNE.Infrastructure.Services;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using FORUM_DYSKUSYJNE.Infrastructure.Data;

namespace FORUM_DYSKUSYJNE
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

			
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();


			container.RegisterType<ForumDatabase, ForumDatabase>();
			container.RegisterType(typeof(IRepository<>), typeof(Repository<>));
			container.RegisterType<IAnnouncementsService, AnnouncementsService>();
			container.RegisterType<IConstantsService, ConstantsService>();
			container.RegisterType<IGroupsService, GroupService>();
			container.RegisterType<IUsersService, UsersService>();
			container.RegisterType<ITopicsService, TopicsServices>();
			container.RegisterType<IPostsService, PostsServices>();
			container.RegisterType<IPrivateMessagesService, PrivateMessagesService>();
			container.RegisterType<IForbiddenWordsService, ForbiddenWordsService>();
			container.RegisterType<ISectionsService, SectionsService>();
			container.RegisterType<ISectionRepository, SectionRepository>();
			container.RegisterType<ITopicRepository, TopicRepository>();
			container.RegisterType<IDataService, DataService>(new PerResolveLifetimeManager());
			container.RegisterType<IUnitOfWork, UnitOfWork>();

			// register all your components with the container here
			// it is NOT necessary to register your controllers

			// e.g. container.RegisterType<ITestService, TestService>();            

			return container;
        }
    }
}