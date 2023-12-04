using System.Diagnostics.CodeAnalysis;
using CSharpFunctionalExtensions;
using PopsicleFactory.Core.Errors;

namespace PopsicleFactory.Core.ValueObjects;

public sealed class PopsicleNameObject : ValueObject
{
    public string Value { get; }

    public static Result<PopsicleNameObject> Create(string popsicleName)
        => string.IsNullOrEmpty(popsicleName)
            ? Result.Failure<PopsicleNameObject>(PopsicleErrors.EmptyName)
            : Result.Success(new PopsicleNameObject(popsicleName));

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }

    private PopsicleNameObject(string popsicleName) => Value = popsicleName;

    [SuppressMessage("ReSharper", "UnusedMember.Local")]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private PopsicleNameObject()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public override string ToString() => Value;
}