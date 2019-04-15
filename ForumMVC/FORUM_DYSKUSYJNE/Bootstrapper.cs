using System.Web.Mvc;
using FORUM_DYSKUSYJNE.Contracts;
using FORUM_DYSKUSYJNE.DataAccess;
using FORUM_DYSKUSYJNE.DataAccess.Repositories;
using FORUM_DYSKUSYJNE.Database;
using Microsoft.Practices.Unity;
using Unity.Mvc3;

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
			container.RegisterType<IUnitOfWork, UnitOfWork>();
			container.RegisterType<ISectionRepository, SectionRepository>();
			container.RegisterType<ITopicRepository, TopicRepository>();
			container.RegisterType<IUserRepository, UserRepository>();
			// register all your components with the container here
			// it is NOT necessary to register your controllers

			// e.g. container.RegisterType<ITestService, TestService>();            

			return container;
        }
    }
}