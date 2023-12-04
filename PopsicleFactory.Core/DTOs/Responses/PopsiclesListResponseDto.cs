using PopsicleFactory.Core.DTOs.Responses.Base;

namespace PopsicleFactory.Core.DTOs.Responses;

public sealed record PopsiclesListResponseDto(List<PopsicleResponseDto> Popsicles) : ResponseDto;