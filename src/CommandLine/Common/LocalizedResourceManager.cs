using System.Globalization;
using System.Resources;
using System.Threading;

namespace NuGet
{
    internal static class LocalizedResourceManager
    {
        private static readonly ResourceManager _resourceManager = new ResourceManager("NuGet.NuGetResources", typeof(LocalizedResourceManager).Assembly);

        public static string GetString(string resourceName)
        {
            var culture = GetLanguageName();
            return _resourceManager.GetString(resourceName + '_' + culture, CultureInfo.InvariantCulture) ??
                   _resourceManager.GetString(resourceName, CultureInfo.InvariantCulture);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "the convention is to used lower case letter for language name.")]        
        /// <summary>
        /// Returns the 3 letter language name used to locate localized resources.
        /// </summary>
        /// <returns>the 3 letter language name used to locate localized resources.</returns>
        public static string GetLanguageName()
        {
            var culture = Thread.CurrentThread.CurrentUICulture;
            while (!culture.IsNeutralCulture)
            {
                if (culture.Parent == culture)
                {
                    break;
                }

                culture = culture.Parent;
            }

            return culture.ThreeLetterWindowsLanguageName.ToLowerInvariant();
        }
        
        public static readonly string InstallCommandPackageRestoreConsentNotFound =
            "Package restore is disabled. To enable it, edit the NuGet.config file and set packageRestore to true. You can also enable package restore by setting the environment variable 'EnableNuGetPackageRestore' to 'true'.";
        
        public static readonly string RestoreCommandPackageRestoreOptOutMessage =
            "Restoring NuGet packages...\n" +
            "To prevent NuGet from downloading packages, edit the NuGet.config file and set packageRestore to false.";
    }
}
