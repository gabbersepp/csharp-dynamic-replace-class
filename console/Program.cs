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
            AppDomain.CurrentDomain.TypeResolve += CurrentDomain_TypeResolve;

            if (new AnotherClass().Create() != null)
            {
                Console.WriteLine("success");
            }

        }

        private static Assembly CurrentDomain_TypeResolve(object sender, ResolveEventArgs args)
        {
            Console.WriteLine("resolve type");
            if (args.Name.Contains("TestLib"))
            {
                dynamicModule.DefineType("Test.TestLib.Class1", TypeAttributes.Class | TypeAttributes.Public).CreateType();
                return dynamicAssembly;
            }

            return null;
        }

        private static AssemblyBuilder dynamicAssembly;
        private static ModuleBuilder dynamicModule;

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            Console.WriteLine("resolve assembly");
            if (args.Name.Contains("TestLib"))
            {
                Console.WriteLine("resolve assembly");
                return dynamicAssembly;
            }

            return null;
        }
    }
}
