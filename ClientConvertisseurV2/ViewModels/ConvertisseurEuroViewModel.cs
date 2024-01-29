using ClientConvertisseurV2.Models;
using ClientConvertisseurV2.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConvertisseurV2.ViewModels
{
    public class ConvertisseurEuroViewModel : ObservableObject
    {
		private double montantEuro;

		public double MontantEuro
		{
			get { return montantEuro; }
			set 
			{ 
				montantEuro = Math.Round(value, 2); 
				OnPropertyChanged();
			}
		}

		private ObservableCollection<Devise> devises;

		public ObservableCollection<Devise> Devises
		{
			get { return devises; }
			set {
				devises = value;
			}
		}

		private Devise selectedDevise;

		public Devise SelectedDevise
        {
			get { return selectedDevise; }
			set { 
				selectedDevise = value;
                OnPropertyChanged();
            }
		}

		private double montantDevise;

		public double MontantDevise
		{
			get { return montantDevise; }
			set {
				montantDevise = Math.Round(value);
				OnPropertyChanged();
			}
		}

		public IRelayCommand BtnConvert { get;  }

        public ConvertisseurEuroViewModel()
        {
            Devises = new ObservableCollection<Devise>();
            GetDataOnLoadAsync();
			BtnConvert = new RelayCommand(ActionConvert);
        }

		public void ActionConvert()
		{
			if(SelectedDevise == null)
			{
				ShowErrorMessage("Erreur de devise", "Vous devez séléctionner une devise");
				return;
			}
			if(MontantEuro < 0)
			{
				ShowErrorMessage("Erreur de montant", "Vous devez entrer un montant strictement positif");
				return;
			}
			MontantDevise = MontantEuro * SelectedDevise.Taux;
		}

        private async void GetDataOnLoadAsync()
        {
            WSService service = new WSService("http://localhost:44356/api/");
            List<Devise> result = await service.GetDevisesAsync("devises");
            if (result != null)
            {
                foreach (Devise dev in result)
                {
                    Devises.Add(dev);
                }
            }
            else
            {
				ShowErrorMessage("Erreur API", "L'API est indisponible, la liste des devises n'a pas pu être chargée");
            }
        }

        private async void ShowErrorMessage(string title, string message)
        {

            ContentDialog contentDialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = "Ok"
            };

            contentDialog.XamlRoot = App.MainRoot.XamlRoot;


            ContentDialogResult result = await contentDialog.ShowAsync();
        }


    }
}
