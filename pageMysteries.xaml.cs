using Microsoft.Maui.Controls;
using System.Reflection;

namespace StPeters;

public partial class pageMysteries : ContentPage
{
	public pageMysteries(DayOfWeek pDoW)
	{
        string sUrl = GetDaysMysteries(pDoW);
        var webView = new WebView();

        InitializeComponent();

        #if ANDROID
                webView.Source = "file:///android_asset/" + sUrl;
        #elif IOS
                string[] parts = sUrl.Split('.');
                webView.Source = Foundation.NSBundle.MainBundle.PathForResource(parts[0], "htm");
        #endif

        Content = webView;

        //~~~tool bar~~~
        ToolbarItem tbMenu = new ToolbarItem
        {
            Text = "Rosary prayers",
            Order = ToolbarItemOrder.Primary,
            Priority = 0
        };
        tbMenu.Clicked += async (s, e) =>
        {
            await Navigation.PushAsync(new pagePrayers()); //Change to PRAYERS page
        };
        ToolbarItems.Add(tbMenu);

    } //ctor

    // Load HTML file from embedded resource
    private string LoadHtmlFromResources(string resourcePath)
    {
        var assembly = Assembly.GetExecutingAssembly();
        using Stream? stream = assembly.GetManifestResourceStream(resourcePath);
        if (stream == null)
            return "<h1>Error: HTML resource not found</h1>";

        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    private string GetDaysMysteries(DayOfWeek DoW)
    {
        //TO DO - need added logic for advent/lent...
        //// (lent sorrowful on sud, advent glorious on sun)
        try
        {
            string sReturn = "";

            switch (DoW)
            {
                case DayOfWeek.Sunday:
                    sReturn = "glorious.htm";
                    break;
                case DayOfWeek.Monday:
                    sReturn = "joyful.htm";
                    break;
                case DayOfWeek.Tuesday:
                    sReturn = "sorrowful.htm";
                    break;
                case DayOfWeek.Wednesday:
                    sReturn = "glorious.htm";
                    break;
                case DayOfWeek.Thursday:
                    sReturn = "luminous.htm";
                    break;
                case DayOfWeek.Friday:
                    sReturn = "sorrowful.htm";
                    break;
                case DayOfWeek.Saturday:
                    sReturn = "joyful.htm";
                    break;
            }

            return sReturn;
        }
        catch (Exception)
        {
            return "";
        }
    } //GetDaysMysteries 

} //class pageMysteries