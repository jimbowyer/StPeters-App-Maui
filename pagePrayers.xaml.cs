using Microsoft.Maui.Controls;
using System.Reflection;

namespace StPeters;

public partial class pagePrayers : ContentPage
{
	public pagePrayers()
	{
		InitializeComponent();

        var webView = new WebView();

        #if ANDROID
            const string cURL = "prayers.htm";
            webView.Source = "file:///android_asset/" + cURL;
        #elif IOS               
                webView.Source = Foundation.NSBundle.MainBundle.PathForResource("prayers", "htm");
        #endif

        Content = webView;
    } //ctor

} //class pagePrayers