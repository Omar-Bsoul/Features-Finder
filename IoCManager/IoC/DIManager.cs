using IoCManager.Dependency;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IoCManager.IoC {
    public static class DIManager {
        private static readonly Container container = new Container();

        public static void Register<T>() where T : class {
            container.Register<T>();
        }

        public static T Resolve<T>() where T : class {
            return container.GetInstance<T>();
        }

        public static void RegisterDependencies() {
            container.Options.DefaultScopedLifestyle = new ThreadScopedLifestyle();

            RegisterDependenciesOfType<ITransientDependency>(Lifestyle.Transient);
            RegisterDependenciesOfType<ISingletonDependency>(Lifestyle.Singleton);
            RegisterDependenciesOfType<IDisposableDependency>(Lifestyle.Scoped);

            container.Verify();
        }

        public static void UsingScopedLifeStyle(Action action) {
            using var scope = ThreadScopedLifestyle.BeginScope(container);

            action.Invoke();
        }

        private static void RegisterDependenciesOfType<T>(Lifestyle lifestyle) {
            var tType = typeof(T);
            var types = GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(p => tType.IsAssignableFrom(p) && p.IsClass);

            types.ToList().ForEach(implementation => {
                var baseAbstraction = implementation.GetInterfaces().First(baseAbstraction => {
                    return baseAbstraction.GetInterfaces().Any(abstractionDependencyType => {
                        return abstractionDependencyType.FullName.Equals(tType.FullName);
                    });
                });

                container.Register(baseAbstraction, implementation, lifestyle);
            });
        }

        private static IEnumerable<Assembly> GetAssemblies() {
            var list = new List<string>();
            var stack = new Stack<Assembly>();

            stack.Push(Assembly.GetEntryAssembly());

            do {
                var assembly = stack.Pop();

                yield return assembly;

                foreach (var reference in assembly.GetReferencedAssemblies()) {
                    if (!list.Contains(reference.FullName)) {
                        stack.Push(Assembly.Load(reference));
                        list.Add(reference.FullName);
                    }
                }
            }
            while (stack.Count > 0);
        }
    }
}
