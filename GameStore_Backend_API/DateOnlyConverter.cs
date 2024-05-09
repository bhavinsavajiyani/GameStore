using System.Text.Json;
using System.Text.Json.Serialization;

namespace GameStore_Backend_API;

public sealed class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private const string Format = "yyyy-MM-dd";

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		return DateOnly.Parse(reader.GetString()!);
	}

	public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
	{
		var isoDate = value.ToString("O");
		writer.WriteStringValue(isoDate);
	}
}
