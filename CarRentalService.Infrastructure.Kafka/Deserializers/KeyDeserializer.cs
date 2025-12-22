using Confluent.Kafka;
using System.Text.Json;

namespace CarRentalService.Infrastructure.Kafka.Deserializers;

/// <summary>
/// Deserializer for Kafka message keys (Guid)
/// </summary>
public class KeyDeserializer : IDeserializer<Guid>
{
    /// <summary>
    /// Deserializes a Guid key from byte array
    /// </summary>
    public Guid Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        => JsonSerializer.Deserialize<Guid>(data);
}