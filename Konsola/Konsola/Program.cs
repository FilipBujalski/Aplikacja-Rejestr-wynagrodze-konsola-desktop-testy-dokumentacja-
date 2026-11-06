using System;

public class SalaryCalculator
{
    private double grossAmount;
    private string employmentType;
    private bool isYouthRelief;

    public SalaryCalculator(double grossAmount, string employmentType, bool isYouthRelief)
    {
        SetGrossAmount(grossAmount);
        this.employmentType = employmentType;
        this.isYouthRelief = isYouthRelief;
    }

    // Walidacja danych: sprawdzenie czy kwota brutto jest większa od 0
    public static bool ValidateGrossAmount(double amount)
    {
        return amount > 0;
    }

    public void SetGrossAmount(double amount)
    {
        if (ValidateGrossAmount(amount))
        {
            this.grossAmount = amount;
        }
        else
        {
            throw new ArgumentException("Kwota brutto musi być większa od 0.");
        }
    }

    public double GetGrossAmount() => grossAmount;

    // Obliczanie składek ZUS społecznych (13,71% brutto)
    public double CalculateSocialZus()
    {
        return Math.Round(grossAmount * 0.1371, 2);
    }

    // Obliczanie składki zdrowotnej (9,00% podstawy wymiaru: brutto - ZUS społeczne)
    public double CalculateHealthZus()
    {
        double baseForHealth = grossAmount - CalculateSocialZus();
        return Math.Round(baseForHealth * 0.09, 2);
    }

    // Obliczanie zaliczki na podatek PIT
    public double CalculatePitTax()
    {
        if (isYouthRelief)
        {
            return 0.00;
        }
        double baseForPit = grossAmount - CalculateSocialZus();
        return Math.Round(baseForPit * 0.12, 2);
    }

    // Obliczanie wynagrodzenia netto
    public double CalculateNetSalary()
    {
        double net = grossAmount - CalculateSocialZus() - CalculateHealthZus() - CalculatePitTax();
        return Math.Round(net, 2);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== PROGRAM GŁÓWNY - KALKULATOR WYNAGRODZEŃ ===\n");

        // Wariant 1: Umowa o pracę bez ulgi dla młodych
        SalaryCalculator calc1 = new SalaryCalculator(5000.0, "Umowa o pracę", false);
        Console.WriteLine($"Wariant 1: {calc1.GetGrossAmount()} PLN (Umowa o pracę, bez ulgi)");
        Console.WriteLine($"  - Składki ZUS: {calc1.CalculateSocialZus()} PLN");
        Console.WriteLine($"  - Składka zdrowotna: {calc1.CalculateHealthZus()} PLN");
        Console.WriteLine($"  - Podatek PIT: {calc1.CalculatePitTax()} PLN");
        Console.WriteLine($"  - Wynagrodzenie Netto: {calc1.CalculateNetSalary()} PLN\n");

        // Wariant 2: Umowa zlecenie z ulgą dla młodych (<26 lat)
        SalaryCalculator calc2 = new SalaryCalculator(5000.0, "Umowa zlecenie", true);
        Console.WriteLine($"Wariant 2: {calc2.GetGrossAmount()} PLN (Umowa zlecenie, z ulgą dla młodych)");
        Console.WriteLine($"  - Składki ZUS: {calc2.CalculateSocialZus()} PLN");
        Console.WriteLine($"  - Składka zdrowotna: {calc2.CalculateHealthZus()} PLN");
        Console.WriteLine($"  - Podatek PIT: {calc2.CalculatePitTax()} PLN (Ulga dla młodych)");
        Console.WriteLine($"  - Wynagrodzenie Netto: {calc2.CalculateNetSalary()} PLN");

        Console.WriteLine("\nNaciśnij dowolny klawisz, aby zakończyć...");
        Console.ReadKey();
    }
}