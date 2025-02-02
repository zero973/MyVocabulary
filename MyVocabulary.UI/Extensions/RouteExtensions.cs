namespace MyVocabulary.UI.Extensions;

public static class RouteExtensions
{
    public static async Task GoBack(this Shell shell) => await shell.GoToAsync("..");
}