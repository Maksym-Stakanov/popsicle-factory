using PopsicleFactory.Core.DTOs.Requests.Base;
using PopsicleFactory.Core.Enums;

namespace PopsicleFactory.Core.DTOs.Requests;

public sealed record PopsiclesListRequestDto(PopsicleSize? Size) : RequestDto;