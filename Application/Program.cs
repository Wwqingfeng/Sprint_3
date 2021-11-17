using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Application.SDK;
using TestAttributes.SDK;


namespace Application {
    class Program {

        //static List<IPlugin> Interface_plugins = null;

        static List<Type> attribute_plugins = null;


        static void Main(string[] args) {

            //Interface_plugins = interface_readExtensions();
            attribute_plugins = readExtensionAttribute();
    
            Console.WriteLine(attribute_plugins.Count + " plug-in(s) found");


            methodInvode();



            //foreach (var plugin in plugins) {
            //    Console.WriteLine(plugin.title);
            //    plugin.doSomething();
            //}

                



            //plugins = readPlugins();
        }

        private static void methodInvode() {

            foreach (Type t in attribute_plugins) {

                var testMethods = from m in t.GetMethods()
                                  where m.GetCustomAttributes(false).Any(a => a is TestAttribute)
                                  select m;

                object testInstace = Activator.CreateInstance(t);

                foreach (MethodInfo mInfo in testMethods) {
                    Console.Write($"Running test on {mInfo.Name}: => ");
                    mInfo.Invoke(testInstace, new object[0]);
                }
                    //mInfo.Invoke(testInstace, null);



                //var testInstance = Activator.CreateInstance(t) as TestAttribute;
                //pluginList.Add(testInstance);
            }

        }

        private static List<Type> readExtensionAttribute() {
            var pluginList = new List<Type>();

            // 1 read the file
            var files = Directory.GetFiles("extension", "*.dll");


            foreach (var file in files) {
                List<Assembly> tempAssemblyList = new List<Assembly>();

                Assembly assembly = Assembly.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), file));                
                tempAssemblyList.Add(assembly);

                foreach(Assembly a in tempAssemblyList) {
                    Type[] allTypes = a.GetTypes();
                    foreach(Type type in allTypes) {

                        if ((type.GetCustomAttribute(typeof(TestAttribute))) is TestAttribute) {
                            pluginList.Add(type);
                            Console.WriteLine("attribute is " + type.Name);

                        }
                    }
                }

      
                

                //var pluginTypes2 = from t in assembly.GetExecutingAssembly().GetTypes()
                //                  where t.GetCustomAttributes(false).Any(a => a is TestAttribute)
                //                  select t;



                //foreach (Type t in a) {

                //    var testMethod = from m in t.GetMethods()
                //                     where m.GetCustomAttributes(false).Any(a => a is TestAttribute)
                //                     select m;

                //    var testInstance = Activator.CreateInstance(t) as TestAttribute;
                //    pluginList.Add(testInstance);
                //} 
                
            }


            return pluginList;
        }

        static List<IPlugin> interface_readExtensions() {

            var pluginList = new List<IPlugin> ();

            var files = Directory.GetFiles("extension", "*.dll");

            foreach(var file in files) {
                var assembly = Assembly.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), file));

                var pluginTypes = assembly.GetTypes().Where(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsInterface).ToArray();

                foreach(var plugin in pluginTypes) {
                    var pluginInstance = Activator.CreateInstance(plugin) as IPlugin;

                    pluginList.Add(pluginInstance);
                }
            }

            return pluginList;
        }
    }
}
