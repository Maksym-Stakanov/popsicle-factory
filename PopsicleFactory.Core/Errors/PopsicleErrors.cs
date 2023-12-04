namespace PopsicleFactory.Core.Errors;

public static class PopsicleErrors
{
    public static string EmptyName => "Popsicle name is not specified!";
    public static string NotFound => "Popsicle by specified id not found!";
    public static string AlreadyExists => "Popsicle with specified id already exists!";
    public static string FailedToAddNewPopsicle => "Failed to add new popsicle with specified id!";
    public static string FailedToRemovePopsicle => "Failed to remove existing popsicle with specified id!";
    public static string IncorrectSize => "Incorrect popsicle size in request! Possible popsicle sizes: Big/Medium/Small";
}