using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCustoViagem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaPedagios : ContentPage
    {
        private App PropriedadesApp;
        public ListaPedagios()
        {
            InitializeComponent();

            PropriedadesApp = (App)Application.Current;

            lst_lista_pedagios.ItemsSource = PropriedadesApp.ListaPedagios;

            if(PropriedadesApp.ListaPedagios.Count == 0)
            {
                lst_lista_pedagios.IsVisible = false;
                lbl_msg_lista_vazia.IsVisible = true;
            }
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                MenuItem menuItem = sender as MenuItem;

                var pedagio_selecionado = (Model.Pedagio)menuItem.BindingContext;

                if (await DisplayAlert("Tem certeza?", "Deseja remover este pedagio?", "Sim", "Não"))
                {
                    PropriedadesApp.ListaPedagios.RemoveAll(i => i.NroPedagio == pedagio_selecionado.NroPedagio);

                    lst_lista_pedagios.ItemsSource = new List<Model.Pedagio>();

                    lst_lista_pedagios.ItemsSource = PropriedadesApp.ListaPedagios;
                }
            }catch (Exception ex)
            {
                await DisplayAlert("Ops", "Ocorreu um erro: " + ex.Message, "OK");
            }

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                var total = PropriedadesApp.ListaPedagios.Sum(i => i.Valor).ToString("C");

                await DisplayAlert("Resultado Final", String.Format("O total dos pedágios é {0}", total), "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", "Ocorreu um erro: " + ex.Message, "OK");
            }

        }
    }
}