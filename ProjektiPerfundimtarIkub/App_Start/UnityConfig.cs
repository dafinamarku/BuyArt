using BuyArt.RepositoryContracts;
using BuyArt.RepositoryLayer;
using BuyArt.ServiceContracts;
using BuyArt.ServiceLayer;
using System;

using Unity;

namespace ProjektiPerfundimtarIkub
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ICategoryRepository, CategoriesRepository>();
            container.RegisterType<ICategoriesService, CategoriesService>();
            container.RegisterType<ISubjectsService, SubjectsService>();
            container.RegisterType<ISubjectRepository, SubjectsRepository>();
            container.RegisterType<IStyleRepository, StylesRepository>();
            container.RegisterType<IStylesService, StylesService>();
            container.RegisterType<IUsersRepository, UsersRepository>();
            container.RegisterType<IUsersService, UsersService>();
            container.RegisterType<IArtworkRepository, ArtworksRepository>();
            container.RegisterType<IArtworksService, ArtworksService>();
            container.RegisterType<ICommentRepository, CommentRepository>();
            container.RegisterType<ICommentService, CommentService>();
            container.RegisterType<IOrdersRepository, OrdersRepository>();
            container.RegisterType<IOrdersService, OrdersService>();
            container.RegisterType<ICartRepository, CartRepository>();
            container.RegisterType<ICartService, CartService>();
        }
    }
}