using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Shared.Configuration
{
    public class JsonConfigModule : Autofac.Module
    {
        private readonly string _configurationFilePath;
        private readonly string _sectionNameSuffix;

        public JsonConfigModule(string configurationFilePath, string sectionNameSuffix = "Settings")
        {
            _configurationFilePath = configurationFilePath;
            _sectionNameSuffix = sectionNameSuffix;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(new JsonConfig(_configurationFilePath, _sectionNameSuffix))
                .As<IJsonConfig>()
                .SingleInstance();

            var settings = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Name.EndsWith(_sectionNameSuffix, StringComparison.InvariantCulture))
                .ToList();

            settings.ForEach(type =>
            {
                builder.Register(c => c.Resolve<IJsonConfig>().LoadSection(type))
                    .As(type)
                    .SingleInstance();
            });
        }
    }
}
