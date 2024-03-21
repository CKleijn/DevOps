namespace Domain.Tools
{
    public static class AssemblyScanner
    {
        public static IEnumerable<T> GetInstancesOfType<T>()
        {
            List<T> instances = new();
            var assignableType = typeof(T);

            var scanners = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(t => assignableType.IsAssignableFrom(t) && t.IsClass)
                .ToList();

            foreach (Type type in scanners)
            {
                instances.Add((T)Activator.CreateInstance(type)!);
            }

            return instances;
        }

        public static IEnumerable<Type> GetSubclassesOf<T>()
        {
            var baseType = typeof(T);

            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsClass && !type.IsAbstract && baseType.IsAssignableFrom(type));
        }
    }
}
