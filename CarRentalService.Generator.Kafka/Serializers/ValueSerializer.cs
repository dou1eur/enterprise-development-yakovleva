using CarRentalService.Application.Contracts.Rental;
using Confluent.Kafka;
using System.Text.Json;

namespace CarRentalService.Generator.Kafka.Serializers;

/// <summary>
/// Serializer for Kafka message values (List of RentalRequest)
/// </summary>
public class ValueSerializer : ISerializer<IList<RentalRequest>>
{
    /// <summary>
    /// Serializes a list of rental requests to byte array
    /// </summary>
    public byte[] Serialize(IList<RentalRequest> list, SerializationContext context)
        => JsonSerializer.SerializeToUtf8Bytes(list);
}