using System.Diagnostics.CodeAnalysis;

namespace PopsicleFactory.Api.Models.Responses;

[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record PopsiclesListResponse(int Count, PopsicleResponse[] Popsicles);