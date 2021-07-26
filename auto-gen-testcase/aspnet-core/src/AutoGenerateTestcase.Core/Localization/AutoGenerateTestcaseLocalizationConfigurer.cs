using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace AutoGenerateTestcase.Localization
{
    public static class AutoGenerateTestcaseLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(AutoGenerateTestcaseConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(AutoGenerateTestcaseLocalizationConfigurer).GetAssembly(),
                        "AutoGenerateTestcase.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
