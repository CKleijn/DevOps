namespace Domain.Tools
{
    public static class AssemblyScanner
    {
        public static IEnumerable<Type> GetSubclassesOf<T>()
        {
            var baseType = typeof(T);

            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsClass && !type.IsAbstract && baseType.IsAssignableFrom(type));
        }
    }
}
