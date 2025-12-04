using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Application.Contracts.Common;

/// <summary>
/// Generic collection response DTO
/// </summary>
/// <typeparam name="T">Type of items in the collection</typeparam>
public sealed record CollectionResponse<T>(
    /// <summary>
    /// List of items
    /// </summary>
    List<T> Items
);