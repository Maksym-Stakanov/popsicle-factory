using CSharpFunctionalExtensions;
using PopsicleFactory.Core.Enums;
using PopsicleFactory.Core.ValueObjects;

namespace PopsicleFactory.Core.Entities;

public sealed class Popsicle : Entity<Guid>
{
    public PopsicleNameObject Name { get; private set; }
    public PopsicleSize Size { get; private set; }

    public static Popsicle Create(PopsicleNameObject popsicleName, PopsicleSize size)
        => new(Guid.NewGuid(), popsicleName, size);
    private Popsicle(Guid id, PopsicleNameObject name, PopsicleSize size) : base(id)
    {
        Name = name;
        Size = size;
    }

    public PopsicleObject ToObject() => PopsicleObject.Create(this);

    public Result<Popsicle> Update(PopsicleObject popsicleObject)
    {
        Name = popsicleObject.Name;
        Size = popsicleObject.Size;
        
        return Result.Success(this);
    }
    public Result<Popsicle> UpdateName(PopsicleNameObject newName)
    {
        if (newName != Name)
        {
            Name = newName;
        }
        return Result.Success(this);
    }
    public Result<Popsicle> UpdateSize(PopsicleSize newSize)
    {
        if (newSize != Size)
        {
            Size = newSize;
        }
        return Result.Success(this);
    }
}