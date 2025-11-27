namespace StPeters;

public partial class pageReadings : ContentPage
{
    //Nov 2025 - take advantage of improved web page layout, use directly:
    private const string cMISSALWEB = "https://readings.livingwithchrist.ca/";
    private bool bWebViewLoaded;

    public pageReadings()
	{
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
            Source = cMISSALWEB,
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

} //class pageNew