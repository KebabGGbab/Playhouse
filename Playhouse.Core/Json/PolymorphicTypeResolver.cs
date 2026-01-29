using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Playhouse.Core.Models.BrowserEvents;
using Playhouse.Core.Models.BrowserEvents.Abstractions;

namespace Playhouse.Core.Json
{
    public sealed class PolymorphicTypeResolver : DefaultJsonTypeInfoResolver 
    { 
        public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options) 
        { 
            JsonTypeInfo jsonTypeInfo = base.GetTypeInfo(type, options);
            
            if (jsonTypeInfo.Type.IsAssignableTo(typeof(BrowserEvent)) && jsonTypeInfo.Type.IsAbstract) 
            { 
                jsonTypeInfo.PolymorphismOptions = new() 
                { 
                    TypeDiscriminatorPropertyName = "$BrowserEventType", 
                    IgnoreUnrecognizedTypeDiscriminators = false, 
                    UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization
                };

                if (jsonTypeInfo.Type.IsAssignableTo(typeof(BrowserContextBrowserEvent)))
                {
                    ConfigureBrowserContextType(jsonTypeInfo.PolymorphismOptions.DerivedTypes);
                }
                else if (jsonTypeInfo.Type.IsAssignableTo(typeof(LocatorBrowserEvent)))
                {
                    ConfigureLocatorType(jsonTypeInfo.PolymorphismOptions.DerivedTypes);
                }
                else if (jsonTypeInfo.Type.IsAssignableTo(typeof(PageBrowserEvent)))
                {
                    ConfigurePageType(jsonTypeInfo.PolymorphismOptions.DerivedTypes);
                }
                else
                {
                    ConfigureBrowserEventType(jsonTypeInfo.PolymorphismOptions.DerivedTypes);
                }
            } 

            return jsonTypeInfo; 
        }

        private static void ConfigureBrowserEventType(IList<JsonDerivedType> deriveds)
        {
            ConfigureBrowserContextType(deriveds);
            ConfigureLocatorType(deriveds);
            ConfigurePageType(deriveds);
        }

        private static void ConfigureBrowserContextType(IList<JsonDerivedType> deriveds) 
        {
            deriveds.Add(new JsonDerivedType(typeof(BrowserContextClosedBrowserEvent), "BrowserContextClosed"));
        }

        private static void ConfigureLocatorType(IList<JsonDerivedType> deriveds)
        {
            deriveds.Add(new JsonDerivedType(typeof(LocatorClickBrowserEvent), "LocatorClick"));
        }

        private static void ConfigurePageType(IList<JsonDerivedType> deriveds)
        {
            deriveds.Add(new JsonDerivedType(typeof(PageClosedBrowserEvent), "PageClosed"));
            deriveds.Add(new JsonDerivedType(typeof(PageCreatedBrowserEvent), "PageCreated"));
            deriveds.Add(new JsonDerivedType(typeof(PageGoToBrowserEvent), "PageGoTo"));
        }
    }
}
