using System.Diagnostics.CodeAnalysis;
using CSharpFunctionalExtensions;
using PopsicleFactory.Core.Entities;
using PopsicleFactory.Core.Enums;
using PopsicleFactory.Core.Errors;

namespace PopsicleFactory.Core.ValueObjects;

public sealed class PopsicleObject : ValueObject
{
    public Guid Id { get; }
    public PopsicleNameObject Name { get; }
    public PopsicleSize Size { get; }

    public static Result<PopsicleObject> Create(Guid id, string nameStr, string sizeStr)
    {
        var name = PopsicleNameObject.Create(nameStr);
        var size = PopsicleObject.ConvertPopsicleSize(sizeStr);

        return Result.Combine(name, size)
            .Map(() => new PopsicleObject(id, name.Value, size.Value));
    }

    public static PopsicleObject Create(Popsicle popsicle) => new(popsicle.Id, popsicle.Name, popsicle.Size);

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Id;
        yield return Name;
        yield return Size;
    }

    private PopsicleObject(Guid id, PopsicleNameObject popsicleName, PopsicleSize size)
    {
        Id = id;
        Name = popsicleName;
        Size = size;
    }

#pragma warning disable CS8618
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private PopsicleObject()
    {
    }
#pragma warning restore CS8618
    
    public static Result<PopsicleSize> ConvertPopsicleSize(string sizeStr)
    {
        return Enum.TryParse<PopsicleSize>(sizeStr, out var size)
            ? Result.Success(size)
            : Result.Failure<PopsicleSize>(PopsicleErrors.IncorrectSize);
    }
}