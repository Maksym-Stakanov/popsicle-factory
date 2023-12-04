using PopsicleFactory.Core.DTOs.Responses.Base;
using PopsicleFactory.Core.Enums;

namespace PopsicleFactory.Core.DTOs.Responses;

public sealed record PopsicleResponseDto(Guid Id, string Name, PopsicleSize Size) : ResponseDto;