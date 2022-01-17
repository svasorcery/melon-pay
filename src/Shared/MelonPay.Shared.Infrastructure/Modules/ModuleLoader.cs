using System.Reflection;
using Microsoft.Extensions.Configuration;
using MelonPay.Shared.Abstractions.Modules;

namespace MelonPay.Shared.Infrastructure.Modules
{
    public static class ModuleLoader
    {
        public static IList<Assembly> LoadAssemblies(IConfiguration configuration)
        {
            const string ModulePart = "MelonPay.Modules.";

            var assemblies = AppDomain.CurrentDomain
                .GetAssemblies()
                .ToList();

            var files = GetModuleFiles(assemblies);

            var disabledModules = GetDisabledModules(files, ModulePart, configuration);
            foreach (var disabledModule in disabledModules)
            {
                files.Remove(disabledModule);
            }

            foreach (var file in files)
            {
                assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(file)));
            }

            return assemblies;
        }

        public static IList<IModule> LoadModules(IEnumerable<Assembly> assemblies)
            => assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(IModule).IsAssignableFrom(x) && !x.IsInterface)
                .OrderBy(x => x.Name)
                .Select(Activator.CreateInstance)
                .Cast<IModule>()
                .ToList();


        private static List<string> GetModuleFiles(IEnumerable<Assembly> assemblies)
        {
            var locations = assemblies
                .Where(x => !x.IsDynamic)
                .Select(x => x.Location)
                .ToArray();

            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Where(x => !locations.Contains(x, StringComparer.InvariantCultureIgnoreCase))
                .ToList();

            return files;
        }

        private static List<string> GetDisabledModules(IEnumerable<string> files, string modulePart, IConfiguration configuration)
        {
            var disabledModules = new List<string>();
            foreach (var file in files)
            {
                if (!file.Contains(modulePart))
                {
                    continue;
                }

                var moduleName = file.Split(modulePart)[1].Split(".")[0].ToLowerInvariant();
                var enabled = configuration.GetValue<bool>($"{moduleName}:module:enabled");
                if (!enabled)
                {
                    disabledModules.Add(file);
                }
            }

            return disabledModules;
        }
    }
}
