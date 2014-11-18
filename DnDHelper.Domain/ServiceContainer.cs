using System.Collections.Generic;
using Ninject;
using Ninject.Parameters;

namespace DnDHelper.Domain
{
    public static class ServiceContainer
    {
        private static readonly IKernel _kernel = new StandardKernel();
        public static IKernel Kernel { get { return _kernel; } }
        public static T GetInstance<T>()
        {
            return Kernel.Get<T>();
        }
        public static T GetInstance<T>(KeyValuePair<string, object> constructorArgument)
        {
            return Kernel.Get<T>(new ConstructorArgument(constructorArgument.Key, constructorArgument.Value));
        }
    }

    
}