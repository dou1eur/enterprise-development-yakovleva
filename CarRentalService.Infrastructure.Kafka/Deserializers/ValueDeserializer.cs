using CarRentalService.Application.Contracts.Rental;
using Confluent.Kafka;
using System.Text.Json;

namespace CarRentalService.Infrastructure.Kafka.Deserializers;

/// <summary>
/// Deserializer for Kafka message values (List of RentalRequest)
/// </summary>
public class ValueDeserializer : IDeserializer<IList<RentalRequest>>
{
    /// <summary>
    /// Deserializes a list of rental requests from byte array
    /// </summary>
    public IList<RentalRequest> Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        if (isNull) return [];
        return JsonSerializer.Deserialize<IList<RentalRequest>>(data) ?? [];
    }
}