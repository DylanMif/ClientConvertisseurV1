using ClientConvertisseurV1.Models;
using ClientConvertisseurV1.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ClientConvertisseurV1.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConvertisseurEuroPage : Page, INotifyPropertyChanged
    {
        private double montantEuro;

        public double MontantEuro
        {
            get { return montantEuro; }
            set {
                montantEuro = value;
                OnPropertyChanged(nameof(montantEuro));
            }
        }

        private double montantDevise;
        public double MontantDevise
        {
            get { return montantDevise; }
            set
            {
                montantDevise = value;
                OnPropertyChanged(nameof(montantDevise));
            }
        }


        private ObservableCollection<Devise> devises;

        public ObservableCollection<Devise> Devises
        {
            get { return devises; }
            set {
                devises = value;
                OnPropertyChanged(nameof(devises));
            }
        }

        private Devise selectedDevise;

        public Devise SelectedDevise
        {
            get { return selectedDevise; }
            set {
                selectedDevise = value; 
                OnPropertyChanged(nameof(selectedDevise));
            }
        }


        public ConvertisseurEuroPage()
        {
            this.InitializeComponent();
            this.DataContext = this;
            GetDataOnLoadAsync();
        }

        private async void GetDataOnLoadAsync()
        {
            WSService service = new WSService("http://localhost:44356/api/");
            List<Devise> result = await service.GetDevisesAsync("devises");
            if(result != null)
            {
                Devises = new ObservableCollection<Devise>(result);
                cbDevise.ItemsSource = Devises;
                cbDevise.DisplayMemberPath = "NomDevise";
            }
        }

        private void btConvert_Click(object sender, RoutedEventArgs e)
        {
            MontantDevise = MontantEuro * SelectedDevise.Taux;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
