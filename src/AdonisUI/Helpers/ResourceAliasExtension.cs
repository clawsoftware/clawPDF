using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Xaml;

namespace AdonisUI.Helpers
{
    internal class ResourceAliasExtension
        : MarkupExtension
    {
        public object ResourceKey { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            IRootObjectProvider rootObjectProvider = (IRootObjectProvider)serviceProvider.GetService(typeof(IRootObjectProvider));
            IDictionary dictionary = rootObjectProvider?.RootObject as IDictionary;
            return dictionary?[ResourceKey];
        }
    }
}
