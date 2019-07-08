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
            var dynamicModule = dynamicAssembly.DefineDynamicModule("TestLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            var modelType = dynamicModule.DefineType("Test.TestLib.Class1", TypeAttributes.Class).CreateType();

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_ResourceResolve;

            new AnotherClass().Create();
        }

        private static AssemblyBuilder dynamicAssembly;

        private static Assembly CurrentDomain_ResourceResolve(object sender, ResolveEventArgs args)
        {
            Console.WriteLine(("resolve"));
            if (args.Name.Contains("TestLib"))
            {
                Console.WriteLine("resolve assembly");
                return dynamicAssembly;
            }

            return null;
        }
    }
}
