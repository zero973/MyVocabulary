namespace MyVocabulary.UI.Behaviors;

public class ImageLoadingBehavior : Behavior<Image>
{

    public static readonly BindableProperty ErrorPlaceholderProperty =
        BindableProperty.Create(nameof(ErrorPlaceholder), typeof(FontImageSource), typeof(ImageLoadingBehavior));

    public FontImageSource ErrorPlaceholder
    {
        get => GetValue(ErrorPlaceholderProperty) as FontImageSource;
        set => SetValue(ErrorPlaceholderProperty, value);
    }

    protected override void OnAttachedTo(Image image)
    {
        base.OnAttachedTo(image);
        image.Loaded += OnImageFailed;
    }

    protected override void OnDetachingFrom(Image image)
    {
        base.OnDetachingFrom(image);
        image.Loaded -= OnImageFailed;
    }

    private async void OnImageFailed(object sender, EventArgs e)
    {
        var image = sender as Image;
        var uriImage = image!.Source as UriImageSource;

        if (uriImage == null || !uriImage!.Uri.IsAbsoluteUri)
        {
            image.Source = ErrorPlaceholder;
            return;
        }

        var res = await image!.Source.GetPlatformImageAsync(image.Handler!.MauiContext!);
        if (res == null)
        {
            image.Source = ErrorPlaceholder;
        }
    }
}