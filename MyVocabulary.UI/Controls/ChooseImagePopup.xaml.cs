using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;

namespace MyVocabulary.UI.Controls;

public partial class ChooseImagePopup : Popup
{

    public string ImageUrl { get; private set; } = string.Empty;

    public ChooseImagePopup(string? curImage)
	{
		InitializeComponent();
        ImageUrl = curImage ?? string.Empty;
    }

    private async void ConfirmPopup(object sender, EventArgs e)
    {
        ImageUrl = UrlEntry.Text?.Trim() ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(ImageUrl))
        {
            var result = Uri.TryCreate(ImageUrl, UriKind.Absolute, out Uri uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!result)
            {
                await Toast.Make("Photo URL must be a valid web address (starting with http:// or https://)", 
                    ToastDuration.Long).Show();
                return;
            }
        }

        Close(ImageUrl);
    }

    private void ClosePopup(object sender, EventArgs e) => Close(ImageUrl);

}