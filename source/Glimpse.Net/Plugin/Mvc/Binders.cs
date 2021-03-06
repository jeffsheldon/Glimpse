﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Glimpse.Net.Extensibility;
using Glimpse.Net.Extensions;
using Glimpse.Net.Plumbing;
using System;

namespace Glimpse.Net.Plugin.Mvc
{
    [GlimpsePlugin(ShouldSetupInInit = true)]
    internal class Binders:IGlimpsePlugin
    {
        public string Name
        {
            get { return "Binding"; }
        }

        public object GetData(HttpApplication application)
        {
            var store = application.Context.BinderStore();

            var table = new List<object[]> { new[] { "Ordinal", "Model Binder", "Property/Parameter", "Type", "Attempted Value Providers", "Attempted Value", "Culture", "Raw Value" } };

            var ordinal = 0;

            foreach (var boundProperty in store.Properties)
            {
                var providers = new List<object[]>{new []{"Provider", "Successful"}};
                providers.AddRange(boundProperty.NotFoundIn.Select(valueProvider => new[] {valueProvider.GetType().ToString(), "False"}));

                if (boundProperty.FoundIn != null)
                    providers.Add(new[]{boundProperty.FoundIn.GetType().ToString(), "True", "selected"});

                table.Add(new [] {  ordinal++,
                                    boundProperty.ModelBinderType.ToString(),
                                    string.IsNullOrEmpty(boundProperty.MemberOf) ? boundProperty.Name : boundProperty.MemberOf + "." + boundProperty.Name, 
                                    boundProperty.Type.ToString(), 
                                    providers, 
                                    boundProperty.AttemptedValue, 
                                    boundProperty.Culture != null ? boundProperty.Culture.DisplayName:null,
                                    boundProperty.RawValue,
                                    string.IsNullOrEmpty(boundProperty.MemberOf) ? "":"quiet"
                });
            }

            return table;
        }

        public void SetupInit(HttpApplication application)
        {
            GlimpsePipelineInitiation.ModelBinderProviders();

            GlimpsePipelineInitiation.ModelBinders();

            GlimpsePipelineInitiation.ValueProviders();
        }
    }
}
