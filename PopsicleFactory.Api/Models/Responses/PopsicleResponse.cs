using System.Diagnostics.CodeAnalysis;
using PopsicleFactory.Core.Enums;

namespace PopsicleFactory.Api.Models.Responses;

[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record PopsicleResponse(string Name, PopsicleSize Size, string Url);