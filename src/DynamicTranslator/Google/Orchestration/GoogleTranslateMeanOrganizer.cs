﻿using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicTranslator.Extensions;
using DynamicTranslator.Orchestrators.Organizers;
using Newtonsoft.Json.Linq;

namespace DynamicTranslator.Google.Orchestration
{
    public class GoogleTranslateMeanOrganizer : AbstractMeanOrganizer
    {
        public override TranslatorType TranslatorType => TranslatorType.Google;

        public override Task<Maybe<string>> OrganizeMean(string text, string fromLanguageExtension)
        {
            var result = text.DeserializeAs<Dictionary<string, object>>();
            var arrayTree = result["sentences"] as JArray;
            var output = arrayTree.GetFirstValueInArrayGraph<string>();
            return Task.FromResult(new Maybe<string>(output));
        }
    }
}
