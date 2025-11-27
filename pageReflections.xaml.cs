namespace StPeters;

public partial class pageReflections : ContentPage
{
    private const string cSCRIPTUREWEB = "https://www.dailyscripture.net/daily-meditation/";
    private bool bWebViewLoaded;

    public pageReflections()
    {
        //this.Title = "Reflections";
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (bWebViewLoaded) return;

        // show loading state
        loadingIndicator.IsVisible = true;
        loadingIndicator.IsRunning = true;

        // create the WebView only when the page is visible
        WebView wvReflections = new WebView
        {
            Source = cSCRIPTUREWEB,
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Fill
        };

        // hide the loading indicator once navigation completes
        wvReflections.Navigated += (s, e) =>
        {
            loadingIndicator.IsRunning = false;
            loadingIndicator.IsVisible = false;
        };

        // add to the placeholder container
        contentContainer.Content = wvReflections;
        bWebViewLoaded = true;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

    }
} //class pageReflections