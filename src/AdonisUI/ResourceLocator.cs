using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdonisUI
{
    public class ResourceLocator
    {
        public static Uri ClassicTheme => new Uri("pack://application:,,,/AdonisUI.ClassicTheme;component/Resources.xaml", UriKind.Absolute);

        public static Uri LightColorScheme => new Uri("pack://application:,,,/AdonisUI;component/ColorSchemes/Light.xaml", UriKind.Absolute);

        public static Uri DarkColorScheme => new Uri("pack://application:,,,/AdonisUI;component/ColorSchemes/Dark.xaml", UriKind.Absolute);

        /// <summary>
        /// Removes all resources of AdonisUI from the provided resource dictionary.
        /// </summary>
        /// <param name="rootResourceDictionary">The resource dictionary containing AdonisUI's resources. Expected are the resource dictionaries of the app or window.</param>
        public static void RemoveAdonisResources(ResourceDictionary rootResourceDictionary)
        {
            Uri[] adonisResources = { ClassicTheme };
            ResourceDictionary currentTheme = FindFirstContainedResourceDictionaryByUri(rootResourceDictionary, adonisResources);

            if (currentTheme != null)
            {
                if (!RemoveResourceDictionaryFromResourcesDeep(currentTheme, rootResourceDictionary))
                    throw new Exception("The currently active color scheme was found but could not be removed.");
            }
        }

        /// <summary>
        /// Adds any Adonis theme to the provided resource dictionary.
        /// </summary>
        /// <param name="rootResourceDictionary">The resource dictionary containing AdonisUI's resources. Expected are the resource dictionaries of the app or window.</param>
        public static void AddAdonisResources(ResourceDictionary rootResourceDictionary)
        {
            rootResourceDictionary.MergedDictionaries.Add(new ResourceDictionary { Source = ClassicTheme });
        }

        /// <summary>
        /// Adds a resource dictionary with the specified uri to the MergedDictionaries collection of the <see cref="rootResourceDictionary"/>.
        /// Additionally all child ResourceDictionaries are traversed recursively to find the current color scheme which is removed if found.
        /// </summary>
        /// <param name="rootResourceDictionary">The resource dictionary containing the currently active color scheme. It will receive the new color scheme in its MergedDictionaries. Expected are the resource dictionaries of the app or window.</param>
        /// <param name="colorSchemeResourceUri">The Uri of the color scheme to be set. Can be taken from the <see cref="ResourceLocator"/> class.</param>
        /// <param name="currentColorSchemeResourceUri">Optional uri to an external color scheme that is not provided by AdonisUI.</param>
        public static void SetColorScheme(ResourceDictionary rootResourceDictionary, Uri colorSchemeResourceUri, Uri currentColorSchemeResourceUri = null)
        {
            Uri[] knownColorSchemes = currentColorSchemeResourceUri != null ? new [] { currentColorSchemeResourceUri } : new [] { LightColorScheme, DarkColorScheme};

            ResourceDictionary currentTheme = FindFirstContainedResourceDictionaryByUri(rootResourceDictionary, knownColorSchemes);

            // It is important to add the new resource dictionary before removing the old one.
            // Removing it first would decrease performance significantly because warnings for missing resources are created.
            rootResourceDictionary.MergedDictionaries.Add(new ResourceDictionary { Source = colorSchemeResourceUri });

            if (currentTheme != null)
            {
                if (!RemoveResourceDictionaryFromResourcesDeep(currentTheme, rootResourceDictionary))
                    throw new Exception("The currently active color scheme was found but could not be removed.");
            }
        }

        private static ResourceDictionary FindFirstContainedResourceDictionaryByUri(ResourceDictionary resourceDictionary, Uri[] knownColorSchemes)
        {
            if (knownColorSchemes.Any(scheme => resourceDictionary.Source != null && resourceDictionary.Source.IsAbsoluteUri && resourceDictionary.Source.AbsoluteUri.Equals(scheme.AbsoluteUri)))
                return resourceDictionary;

            if (!resourceDictionary.MergedDictionaries.Any())
                return null;

            return resourceDictionary.MergedDictionaries.FirstOrDefault(d => FindFirstContainedResourceDictionaryByUri(d, knownColorSchemes) != null);
        }

        private static bool RemoveResourceDictionaryFromResourcesDeep(ResourceDictionary resourceDictionaryToRemove, ResourceDictionary rootResourceDictionary)
        {
            if (!rootResourceDictionary.MergedDictionaries.Any())
                return false;

            if (rootResourceDictionary.MergedDictionaries.Contains(resourceDictionaryToRemove))
            {
                rootResourceDictionary.MergedDictionaries.Remove(resourceDictionaryToRemove);
                return true;
            }

            return rootResourceDictionary.MergedDictionaries.Any(dict => RemoveResourceDictionaryFromResourcesDeep(resourceDictionaryToRemove, dict));
        }
    }
}
