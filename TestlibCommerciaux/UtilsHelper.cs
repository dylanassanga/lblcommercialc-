using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestlibCommerciaux
{
    public static class UtilsHelper
    {
        public static bool ClassExists(Assembly assembly, string className)
        {
            return assembly.GetTypes().Any(t => t.Name == className);
        }
        /// <summary>
        /// recupère tous les types (classes, interfaces, structures, etc.) dans l'assembly.
        /// renvoie l'unique type correspondant à la classe 
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="classname"></param>
        /// <returns></returns>

        public static Type GetClassType(Assembly assembly, string classname)
        {
            var test= (from type in assembly.GetTypes()
                    where type.Name == classname
                    select type).SingleOrDefault();
            return test;
        }

    }
}
