using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace WPF
{
    public class SalaryRecord
    {
        public string Date { get; set; } = string.Empty;
        public string Gross { get; set; } = string.Empty;
        public string Contract { get; set; } = string.Empty;
        public string Net { get; set; } = string.Empty;
        public string Relief { get; set; } = string.Empty;
    }

    public partial class MainWindow : Window
    {
        public ObservableCollection<SalaryRecord> RecordsList { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            RecordsList = new ObservableCollection<SalaryRecord>();
            dgHistory.ItemsSource = RecordsList;

            RecordsList.Add(new SalaryRecord { Date = "2023-10-27", Gross = "5000,00", Contract = "O pracę", Net = "3500,00", Relief = "Tak" });
            RecordsList.Add(new SalaryRecord { Date = "2023-10-20", Gross = "4500,00", Contract = "Zlecenie", Net = "3150,00", Relief = "Nie" });
            RecordsList.Add(new SalaryRecord { Date = "2023-10-13", Gross = "5500,00", Contract = "O pracę", Net = "3850,00", Relief = "Tak" });
        }

        private void BtnCalculateAndSave_Click(object sender, RoutedEventArgs e)
        {
            string rawInput = txtGross.Text.Replace('.', ',');
            if (!double.TryParse(rawInput, out double grossAmount) || !SalaryCalculator.ValidateGrossAmount(grossAmount))
            {
                MessageBox.Show("Proszę podać poprawną dodatnią kwotę brutto!", "Błąd danych", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string contractType = rbEmploymentContract.IsChecked == true ? "Umowa o pracę" : "Umowa zlecenie";
            bool isYouthRelief = chkYouthRelief.IsChecked ?? false;

            SalaryCalculator calc = new SalaryCalculator(grossAmount, contractType, isYouthRelief);

            double zus = calc.CalculateSocialZus();
            double health = calc.CalculateHealthZus();
            double pit = calc.CalculatePitTax();
            double net = calc.CalculateNetSalary();

            txtNetResultMain.Text = $"Wynagrodzenie netto: {net:N2} PLN";
            txtZusDetails.Text = $"Składki ZUS (społeczne): {zus:N2} PLN";
            txtHealthDetails.Text = $"Składka zdrowotna: {health:N2} PLN";
            txtPitDetails.Text = $"Zaliczka na podatek PIT: {pit:N2} PLN";

            string shortContract = rbEmploymentContract.IsChecked == true ? "O pracę" : "Zlecenie";
            RecordsList.Insert(0, new SalaryRecord
            {
                Date = DateTime.Now.ToString("yyyy-MM-dd"),
                Gross = grossAmount.ToString("N2"),
                Contract = shortContract,
                Net = net.ToString("N2"),
                Relief = isYouthRelief ? "Tak" : "Nie"
            });
        }
    }
}