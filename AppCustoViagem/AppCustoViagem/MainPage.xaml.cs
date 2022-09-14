using AppCustoViagem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppCustoViagem
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private App PropriedadesApp;

        public MainPage()
        {
            InitializeComponent();

            PropriedadesApp = (App)Application.Current;
        }

        private async void BtnPedagio_Clicked(object sender, EventArgs e)
        {
            try
            {
                int qtn_pedagios = PropriedadesApp.ListaPedagios.Count;

                PropriedadesApp.ListaPedagios.Add(new Pedagio
                {
                    NroPedagio = qtn_pedagios + 1,
                    Valor = Convert.ToDecimal(etyValorP.Text)
                });

                await DisplayAlert("Adicionado!", "Veja na lista de pedagios", "Ok");

                etyValorP.Text = "";
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", "Ocorreu um erro: " + ex.Message, "OK");
            }

        }

        private async void BtnCalcular_Clicked(object sender, EventArgs e)
        {
            try
            {
                decimal valor_total_pedagios = PropriedadesApp.ListaPedagios.Sum(item => item.Valor);


                if (string.IsNullOrEmpty(etyDistancia.Text))
                    throw new Exception("Por favor, preencha a distancia.");

                if (string.IsNullOrEmpty(etyConsumo.Text))
                    throw new Exception("Por favor, preencha o consumo do veículo.");

                if (string.IsNullOrEmpty(etyValorC.Text))
                    throw new Exception("Por favor, preencha o valor do combustível.");



                decimal consumo = Convert.ToDecimal(etyConsumo.Text);
                decimal preco_combustivel = Convert.ToDecimal(etyValorC.Text);
                decimal distancia = Convert.ToDecimal(etyDistancia.Text);

                decimal consumo_carro = (distancia / consumo) * preco_combustivel;

                decimal custo_total = consumo_carro + valor_total_pedagios;

                string origem = etyOrigem.Text;
                string destino = etyDestino.Text;

                string mensagem = string.Format("Viagem  de {0} a {1} custará {2}", origem, destino, custo_total.ToString("C"));

                await DisplayAlert("Resultado Final", mensagem, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("OPs", "Ocorreu um erro: " + ex.Message, "OK");
            }
        }

        private async void BtnLimpar_Clicked(object sender, EventArgs e)
        {
            try
            {
                bool confirmar = await DisplayAlert("Tem certeza?", "Limpar todos os dados?", "Sim", "Não");

                if (confirmar)
                {
                    etyConsumo.Text = "";
                    etyDestino.Text = string.Empty;
                    etyDistancia.Text = "";
                    etyOrigem.Text = "";
                    etyValorC.Text = "";
                    etyValorP.Text = "";

                    PropriedadesApp.ListaPedagios.Clear();
                }

            }catch (Exception ex)
            {
                await DisplayAlert("OPS", ex.Message, "OK");
            }

        }


        private async void btnListaPedagio_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new ListaPedagios());
            } catch(Exception ex)
            {
                await DisplayAlert("Ops", "Ocorreu um erro: " + ex.Message, "OK");
            }
        }
    }
    }
