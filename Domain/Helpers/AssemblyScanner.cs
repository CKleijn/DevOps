using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public static class AssemblyScanner
    {
        public static IEnumerable<T> GetInstancesOfType<T>()
        {
            List<T> instances = new List<T>();
            var assignableType = typeof(T);

            var scanners = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(t => assignableType.IsAssignableFrom(t) && t.IsClass).ToList();

            foreach (Type type in scanners)
                instances.Add((T)Activator.CreateInstance(type)!);

            return instances;
        }
    }

}