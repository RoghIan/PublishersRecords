using System.Text.Json.Serialization;

namespace API.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ReportingAs
    {
        Publisher = 1,
        AuxiliaryPioneer,
        RegularPioneer
    }
}
