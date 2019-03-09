﻿using System;
using DynamicTranslator.Configuration.Startup;

namespace DynamicTranslator.SesliSozluk.Configuration
{
    public static class SesliSozlukConfigurationExtensions
    {
        public static ISesliSozlukTranslatorConfiguration UseSesliSozlukTranslate(this ITranslatorModuleConfigurations moduleConfigurations)
        {
            moduleConfigurations.Configurations.ActiveTranslatorConfiguration.AddTranslator(TranslatorType.SesliSozluk);

            return moduleConfigurations.Configurations.Get<ISesliSozlukTranslatorConfiguration>();
        }

        public static void WithConfigurations(this ISesliSozlukTranslatorConfiguration configuration, Action<ISesliSozlukTranslatorConfiguration> creator)
        {
            creator(configuration);
        }
    }
}
