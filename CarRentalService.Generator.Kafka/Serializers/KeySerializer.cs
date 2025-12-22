using Confluent.Kafka;
using System.Text.Json;

namespace CarRentalService.Generator.Kafka.Serializers;

/// <summary>
/// Serializer for Kafka message keys
/// </summary>
public class KeySerializer : ISerializer<Guid>
{
    /// <summary>
    /// Serializes a Guid key to byte array
    /// </summary>
    public byte[] Serialize(Guid key, SerializationContext context)
        => JsonSerializer.SerializeToUtf8Bytes(key);
}