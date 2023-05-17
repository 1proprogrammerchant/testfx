namespace QuickType
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Welcome
    {
        [JsonProperty("sdk")]
        public Sdk Sdk { get; set; }

        [JsonProperty("tools")]
        public Tools Tools { get; set; }

        [JsonProperty("msbuild-sdks")]
        public MsbuildSdks MsbuildSdks { get; set; }
    }

    public partial class MsbuildSdks
    {
        [JsonProperty("Microsoft.DotNet.Arcade.Sdk")]
        public string MicrosoftDotNetArcadeSdk { get; set; }

        [JsonProperty("Microsoft.DotNet.Helix.Sdk")]
        public string MicrosoftDotNetHelixSdk { get; set; }

        [JsonProperty("MSBuild.Sdk.Extras")]
        public string MsBuildSdkExtras { get; set; }
    }

    public partial class Sdk
    {
        [JsonProperty("version")]
        public string Version { get; set; }
    }

    public partial class Tools
    {
        [JsonProperty("vs")]
        public Vs Vs { get; set; }

        [JsonProperty("dotnet")]
        public string Dotnet { get; set; }

        [JsonProperty("runtimes")]
        public Runtimes Runtimes { get; set; }
    }

    public partial class Runtimes
    {
        [JsonProperty("dotnet")]
        public string[] Dotnet { get; set; }
    }

    public partial class Vs
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("components")]
        public string[] Components { get; set; }
    }

    public partial class Welcome
    {
        public static Welcome FromJson(string json) => JsonConvert.DeserializeObject<Welcome>(json, QuickType.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Welcome self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
