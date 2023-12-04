using PopsicleFactory.Core.DTOs.Requests.Base;

namespace PopsicleFactory.Core.DTOs.Requests;

public sealed record UpdatePopsicleSizeRequestDto(Guid Id, string Size) : RequestDto;