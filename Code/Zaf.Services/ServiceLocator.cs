// -----------------------------------------------------------------------
// <copyright file="ServiceLocator.cs" company="disore">
//     Code Project Open License (CPOL) 1.02
//
//     <author>
//         original by disore
//         http://www.codeproject.com/Articles/36745/Showing-Dialogs-When-Using-the-MVVM-Pattern
//
//      Some modifications by Eric J. Zimmerman    
//     </author>
// </copyright>
// -----------------------------------------------------------------------
namespace Zaf.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// A very simple service locator.
    /// </summary>
    public static class ServiceLocator
    {
        /// <summary>
        ///     Dictionary of available services
        /// </summary>
        private static Dictionary<Type, ServiceInfo> services = new Dictionary<Type, ServiceInfo>();

        /// <summary>
        /// Registers a service.
        /// </summary>
        /// <typeparam name="TInterface">The interface type</typeparam>
        /// <typeparam name="TImplemention">The implementation type</typeparam>
        public static void Register<TInterface, TImplemention>() where TImplemention : TInterface
        {
            Register<TInterface, TImplemention>(false);
        }

        /// <summary>
        /// Registers a service as a singleton.
        /// </summary>
        /// <typeparam name="TInterface">The interface type</typeparam>
        /// <typeparam name="TImplemention">The implementation type</typeparam>
        public static void RegisterSingleton<TInterface, TImplemention>() where TImplemention : TInterface
        {
            Register<TInterface, TImplemention>(true);
        }

        /// <summary>
        /// Resolves a service.
        /// </summary>
        /// <typeparam name="TInterface">The interface type</typeparam>
        /// <returns>The service reference</returns>
        public static TInterface Resolve<TInterface>()
        {
            return (TInterface)services[typeof(TInterface)].ServiceImplementation;
        }

        /// <summary>
        /// Registers a service.
        /// </summary>
        /// <typeparam name="TInterface">The interface type</typeparam>
        /// <typeparam name="TImplemention">The implementation type</typeparam>
        /// <param name="isSingleton">true if service is Singleton; otherwise false.</param>
        private static void Register<TInterface, TImplemention>(bool isSingleton) where TImplemention : TInterface
        {
            services.Add(typeof(TInterface), new ServiceInfo(typeof(TImplemention), isSingleton));
        }

        /// <summary>
        /// Class holding service information.
        /// </summary>
        internal class ServiceInfo
        {
            /// <summary>
            ///     The service implementation type
            /// </summary>
            private Type serviceImplementationType;

            /// <summary>
            ///     The service implementation
            /// </summary>
            private object serviceImplementation;

            /// <summary>
            ///     indicates whether the service is a singleton.
            /// </summary>
            private bool isSingleton;

            /// <summary>
            /// Initializes a new instance of the <see cref="ServiceInfo"/> class.
            /// </summary>
            /// <param name="serviceImplementationType">Type of the service implementation.</param>
            /// <param name="isSingleton">Whether the service is a Singleton.</param>
            public ServiceInfo(Type serviceImplementationType, bool isSingleton)
            {
                this.serviceImplementationType = serviceImplementationType;
                this.isSingleton = isSingleton;
            }

            /// <summary>
            /// Gets the service implementation.
            /// </summary>
            public object ServiceImplementation
            {
                get
                {
                    if (this.isSingleton)
                    {
                        if (this.serviceImplementation == null)
                        {
                            this.serviceImplementation = CreateInstance(this.serviceImplementationType);
                        }

                        return this.serviceImplementation;
                    }
                    else
                    {
                        return CreateInstance(this.serviceImplementationType);
                    }
                }
            }

            /// <summary>
            /// Creates an instance of a specific type.
            /// </summary>
            /// <param name="type">The type of the instance to create.</param>
            /// <returns>the created instance</returns>
            private static object CreateInstance(Type type)
            {
                if (services.ContainsKey(type))
                {
                    return services[type].ServiceImplementation;
                }

                ConstructorInfo ctor = type.GetConstructors().First();

                var parameters =
                    from parameter in ctor.GetParameters()
                    select CreateInstance(parameter.ParameterType);

                return Activator.CreateInstance(type, parameters.ToArray());
            }
        }
    }
}
