using ClientConvertisseurV2.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ClientConvertisseurV2.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConvertisseurDevise : Page
    {
        public ConvertisseurDevise()
        {
            this.InitializeComponent();
            this.DataContext = App.Current.Services.GetService<ConvertisseurDeviseVM>();
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            NavigationViewItem nav = args.SelectedItemContainer as NavigationViewItem;
            if (nav.Content.ToString() == "Euro to Devise")
                Frame.Navigate(typeof(ConvertisseurEuroPage));
            else
                Frame.Navigate(typeof(ConvertisseurDevise));
        }
    }
}
