using MyVocabulary.UI.Behaviors;

namespace MyVocabulary.UI.Controls;

/// <summary>
/// Image with error placeholder
/// </summary>
public class EImage : Image
{

    public static readonly BindableProperty ErrorPlaceholderProperty =
        BindableProperty.Create(nameof(ErrorPlaceholder), typeof(ImageSource), typeof(EImage), null);

    public ImageSource ErrorPlaceholder
    {
        get => (ImageSource)GetValue(ErrorPlaceholderProperty);
        set => SetValue(ErrorPlaceholderProperty, value);
    }

    public EImage()
    {
        var behavior = new ImageLoadingBehavior();
        behavior.SetBinding(ImageLoadingBehavior.ErrorPlaceholderProperty, new Binding(nameof(ErrorPlaceholder), source: this));
        Behaviors.Add(behavior);
    }

}