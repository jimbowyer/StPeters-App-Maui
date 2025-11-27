using System;
using System.Reflection;
using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;

namespace StPeters;

public partial class pageAbout : ContentPage
{
    private const string cWEB = "www.st-peters.ca";

    public pageAbout()
    {
        InitializeComponent();

        // set version text from assembly
        //var assembly = typeof(App).Assembly;
        var assembly = typeof(StPeters.App).GetTypeInfo().Assembly;
        var fullName = assembly.FullName ?? string.Empty;
        var assemName = new AssemblyName(fullName);
        string? sVersion = "Version: " + (assemName.Version?.ToString() ?? "1.0.0.0");
        
        lblVersion.Text = "Version: " + sVersion;

        // wire up link button to open browser
        btnLink.Clicked += async (s, e) =>
        {
            try
            {
                await Launcher.OpenAsync(new Uri("http://" + cWEB));
            }
            catch
            {
                // ignore failures to open URL
            }
        };
    }

} //class pageAbout