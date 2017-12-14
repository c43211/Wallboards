// Copyright (c) Microsoft. All rights reserved.

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Push;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409
//run schtasks /create /tn "RestartApp" /tr "kill MedequipWallboard.exe /F" /sc hourly

namespace IoTBrowser
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            AppCenter.Start("51342041-155d-4cb1-a3f6-9ce8dcea6083", typeof(Analytics));
            AppCenter.Start("51342041-155d-4cb1-a3f6-9ce8dcea6083", typeof(Push));
            this.InitializeComponent();
            var deviceInfo = new Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation();
            var deviceName = deviceInfo.FriendlyName;
            //hide the nav bar
            scvNavBar.Visibility = Visibility.Collapsed;
            //set the address to go to
            String urlString = "http://www.onlinemedequip.com/wallboards/" + deviceName + ".htm";
            webView.Navigate(new Uri(urlString));
        }

        private void Go_Web_Click(object sender, RoutedEventArgs e)
        {
            DoWebNavigate();
        }


        private void Web_Address_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                DoWebNavigate();
            }
        }

        private void DoWebNavigate()
        {
            DismissMessage();

            try
            {
                if (Web_Address.Text.Length > 0)
                {
                    webView.Navigate(new Uri(Web_Address.Text));
                }
                else
                {
                    DisplayMessage("You need to enter a web address.");
                }
            }
            catch (Exception e)
            {
                DisplayMessage("Error: " + e.Message);
            }
        }

        private void DisplayMessage(String message)
        {
            Message.Text = message;
            MessageStackPanel.Visibility = Visibility.Visible;
            webView.Visibility = Visibility.Collapsed;

        }

        private void OnMessageDismiss_Click(object sender, RoutedEventArgs e)
        {
            DismissMessage();
        }

        private void DismissMessage()
        {
            webView.Visibility = Visibility.Visible;
            MessageStackPanel.Visibility = Visibility.Collapsed;
        }
    }
}
