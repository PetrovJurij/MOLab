using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Core
{
    public class MethodsLoader
    {
        private string _path;

        public MethodsLoader(string path)
        {
            this._path = path;
        }

        public IEnumerable<IOptimisationMethod> Load()
        {
            List<Type> types = new List<Type>();

            foreach (string dll in Directory.GetFiles(_path, "*.dll"))
            {
                types.AddRange(
                    Assembly.LoadFrom(dll).GetTypes()
                    .Where(m => (typeof(IOptimisationMethod)).IsAssignableFrom(m)));
            }

            var instances = new List<IOptimisationMethod>();
            foreach (var type in types)
            {
                instances.Add((IOptimisationMethod)Activator.CreateInstance(type));
            }

            return instances;
        }
    }
}
