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
    public class ConvertisseurDeviseVM : ObservableObject
    {
		private double montantDevise;

		public double MontantDevise
		{
			get { return montantDevise; }
			set {
				montantDevise = Math.Round(value, 2);
				OnPropertyChanged();
			}
		}

		private ObservableCollection<Devise> devises;

		public ObservableCollection<Devise> Devises
		{
			get { return devises; }
			set { devises = value; }
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

		private double montantEuro;

		public double MontantEuro
		{
			get { return montantEuro; }
			set {
				montantEuro = Math.Round(value, 2);
				OnPropertyChanged();
			}
		}

        public IRelayCommand BtnConvert { get; }

        public ConvertisseurDeviseVM()
        {
            Devises = new ObservableCollection<Devise>();
            GetDataOnLoadAsync();

            BtnConvert = new RelayCommand(ActionConvert);
        }

        private void ActionConvert()
        {
            if(SelectedDevise == null)
            {
                ShowErrorMessage("Erreur Devise", "Vous devez sélectionner une devise");
                return;
            }
            if(MontantDevise < 0)
            {
                ShowErrorMessage("Erreur Montant", "Vous devez séléctionner un montant à convertir");
                return;
            }
            MontantEuro = MontantDevise / SelectedDevise.Taux;
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
