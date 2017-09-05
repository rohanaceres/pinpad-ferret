using Castle.Windsor;

namespace Ferret.Core.DependencyInjection
{
    static public class IoC
    {
        /// <summary>
        /// Inversion of Control and Dependency Injection container.
        /// </summary>
        public static IWindsorContainer Container { get; set; } 
            = new WindsorContainer();
    }
}
