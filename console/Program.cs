using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Test.TestLib;

namespace console
{
    class Program
    {
        static void Main(string[] args)
        {

            AssemblyName dynamicAssemblyName = new AssemblyName("TestLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            dynamicAssembly =
                AssemblyBuilder.DefineDynamicAssembly(dynamicAssemblyName, AssemblyBuilderAccess.Run);
            dynamicModule = dynamicAssembly.DefineDynamicModule("TestLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            
            CreateTypes(AnotherClass.AllClasses);
            
            if (new AnotherClass().Create() != null)
            {
                Console.WriteLine("success");
            }
        }

        private static void CreateTypes(List<string> types)
        {
            types.ForEach(t => dynamicModule.DefineType(t, TypeAttributes.Class | TypeAttributes.Public).CreateType());
        }

        private static AssemblyBuilder dynamicAssembly;
        private static ModuleBuilder dynamicModule;

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            Console.WriteLine("resolve assembly");
            if (args.Name.Contains("TestLib"))
            {
                Console.WriteLine("resolve assembly: dynamic" + (dynamicAssembly.IsDynamic ? "yes": "no"));
                return dynamicAssembly;
            }

            return null;
        }
    }
}
