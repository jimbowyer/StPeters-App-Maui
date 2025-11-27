using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Text;
using Microsoft.Maui.Devices.Sensors;
using System;
using System.Threading.Tasks;

namespace StPeters
{
    public partial class pageMain : ContentPage
    {
        Color mcolorBack = new Color();
        Color mcolorText = new Color();
        string? mstrSeason;
        string? mstrYearCycle;
        DayOfWeek mDoW;
        

        public pageMain()
        {
            InitializeComponent();

            const string cWEB = "www.st-peters.ca";
            const string cSEARCH = "www.google.com/maps/search/catholic/@";
            const string cDEFQUE = "51.114515,-114.2201127,11z?hl=en-CA";
            GetSeasonVars(ref mstrSeason, ref mcolorBack, ref mcolorText, ref mstrYearCycle);
            mDoW = WhatDay();

            btnReflections.Text = "Reflections - " + DateTime.Now.ToString("m");
            btnReflections.BackgroundColor = mcolorBack;
            btnReflections.TextColor = mcolorText;
            btnReflections.BorderColor = mcolorBack;
            btnReflections.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new pageReflections());
            };

            btnReadings.Text = "Mass Readings - " + " " + mstrSeason + " (" + mstrYearCycle + ")";
            btnReadings.BackgroundColor = mcolorBack;
            btnReadings.TextColor = mcolorText;
            btnReadings.BorderColor = mcolorBack;
            btnReadings.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new pageReadings());
            };

            btnFindMass.BackgroundColor = mcolorBack;
            btnFindMass.TextColor = mcolorText;
            btnFindMass.BorderColor = mcolorBack;
            btnFindMass.Clicked += async (sender, args) =>
            {
                Uri uriMap;
                Location? location = await Geolocation.GetLastKnownLocationAsync();
                if (location != null)
                {
                    string sLong = location.Longitude.ToString();
                    string sLat = location.Latitude.ToString();
                    uriMap = new Uri("https://" + cSEARCH + sLat + "," + sLong + ",13z");
                }
                else
                {
                    //use default position
                    uriMap = new Uri("https://" + cSEARCH + cDEFQUE);
                }

                //open church search in browser:                
                await Launcher.OpenAsync(uriMap);

            };

            btnBulletins.BackgroundColor = mcolorBack;
            btnBulletins.TextColor = mcolorText;
            btnBulletins.BorderColor = mcolorBack;
            btnBulletins.Clicked += async (s, e) =>
            {
                //open parish bulletins page in browser:                
                Uri uriWeb = new Uri("http://" + cWEB);
                await Launcher.OpenAsync(uriWeb);
            };

            btnMysteries.Text = TodaysMysteries(mDoW);
            btnMysteries.BackgroundColor = mcolorBack;
            btnMysteries.TextColor = mcolorText;
            btnMysteries.BorderColor = mcolorBack;
            btnMysteries.Clicked += (sender, args) =>
            {
                //open rosary mysteries page:                
                Navigation.PushAsync(new pageMysteries(mDoW));
            };  


            //~~~tool bar~~~
            ToolbarItem tbMenu = new ToolbarItem
            {
                Text = "about",
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };
            tbMenu.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new pageAbout());
            };
            ToolbarItems.Add(tbMenu);

        } //pageMain ctor

        private void GetSeasonVars(ref string pSeason, ref Color pcolorBack, ref Color pcolorText, ref string pCycle)
        {
            RomanCalendar cal = new RomanCalendar();
            Season seaNow;
            clsChurchYear litCal = new clsChurchYear();

            try
            {
                seaNow = cal.SeasonOf(DateTime.Now);
                pSeason = seaNow.SeasonName;
                pCycle = litCal.GetChurchYear(DateTime.Now).ToString();

                switch (seaNow.SeasonColor)
                {
                    case Season.Colors.Green:
                        pcolorBack = Colors.Green;
                        pcolorText = Colors.White;
                        break;
                    case Season.Colors.Purple:
                        pcolorBack = Colors.Purple;
                        pcolorText = Colors.White;
                        break;
                    case Season.Colors.Red:
                        pcolorBack = Colors.Red;
                        pcolorText = Colors.White;
                        break;
                    case Season.Colors.White:
                        pcolorBack = Colors.White;
                        pcolorText = Colors.Silver;
                        break;
                    default:
                        pcolorBack = Colors.Green;
                        pcolorText = Colors.White;
                        break;
                }
            }
            catch (Exception)
            {
                //we errored - log later - rtn safe
                pSeason = "Undefined Season";
                pcolorBack = Colors.Blue;
                pcolorText = Colors.White;
            }

        } //GetSeasonVars

        private DayOfWeek WhatDay()
        {
            DateTime dteNow = DateTime.Now;
            return dteNow.DayOfWeek;
        }

        private static string TodaysMysteries(DayOfWeek DoW)
        {
            try
            {
                string sReturn = "";

                switch (DoW)
                {
                    case DayOfWeek.Sunday:
                        sReturn = "Today - Glorious Mysteries";
                        break;
                    case DayOfWeek.Monday:
                        sReturn = "Today - Joyful Mysteries";
                        break;
                    case DayOfWeek.Tuesday:
                        sReturn = "Today - Sorrowful  Mysteries";
                        break;
                    case DayOfWeek.Wednesday:
                        sReturn = "Today - Glorious  Mysteries";
                        break;
                    case DayOfWeek.Thursday:
                        sReturn = "Today - Luminous  Mysteries";
                        break;
                    case DayOfWeek.Friday:
                        sReturn = "Today - Sorrowful  Mysteries";
                        break;
                    case DayOfWeek.Saturday:
                        sReturn = "Today - Joyful  Mysteries";
                        break;
                }

                return sReturn;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        } //TodaysMysteries

    } //class pageMain
} //ns
