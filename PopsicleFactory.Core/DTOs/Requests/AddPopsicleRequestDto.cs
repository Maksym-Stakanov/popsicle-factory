using PopsicleFactory.Core.DTOs.Requests.Base;

namespace PopsicleFactory.Core.DTOs.Requests;

public sealed record AddPopsicleRequestDto(string Name, string Size) : RequestDto;