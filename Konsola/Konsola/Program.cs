using System;

public class KalkulatorWynagrodzen
{
    private double kwotaBrutto;
    private string rodzajUmowy;
    private bool czyUlgaDlaMlodych;

    public KalkulatorWynagrodzen(double kwotaBrutto, string rodzajUmowy, bool czyUlgaDlaMlodych)
    {
        UstawKwoteBrutto(kwotaBrutto);
        UstawRodzajUmowy(rodzajUmowy);
        this.czyUlgaDlaMlodych = czyUlgaDlaMlodych;
    }

    // Walidacja danych: sprawdzenie czy kwota brutto jest większa od 0
    public static bool WalidujKwoteBrutto(double kwota)
    {
        return kwota > 0;
    }

    public void UstawKwoteBrutto(double kwota)
    {
        if (WalidujKwoteBrutto(kwota))
        {
            this.kwotaBrutto = kwota;
        }
        else
        {
            throw new ArgumentException("Kwota brutto musi być większa od 0.", nameof(kwota));
        }
    }

    public double PobierzKwoteBrutto() => kwotaBrutto;

    public void UstawRodzajUmowy(string rodzaj)
    {
        if (string.IsNullOrWhiteSpace(rodzaj))
        {
            throw new ArgumentException("Rodzaj umowy nie może być pusty.", nameof(rodzaj));
        }

        this.rodzajUmowy = rodzaj.Trim();
    }

    public string PobierzRodzajUmowy() => rodzajUmowy;

    public bool CzyUlgaDlaMlodych() => czyUlgaDlaMlodych;

    // Obliczanie składek ZUS społecznych (13,71% brutto)
    public double ObliczZusSpoleczny()
    {
        return Math.Round(kwotaBrutto * 0.1371, 2);
    }

    // Obliczanie składki zdrowotnej (9,00% podstawy wymiaru: brutto - ZUS społeczne)
    public double ObliczSkladkeZdrowotna()
    {
        double podstawaZdrowotna = kwotaBrutto - ObliczZusSpoleczny();
        return Math.Round(podstawaZdrowotna * 0.09, 2);
    }

    // Obliczanie zaliczki na podatek PIT (0,00 PLN przy włączonej uldze dla młodych)
    public double ObliczZaliczkePit()
    {
        if (czyUlgaDlaMlodych)
        {
            return 0.00;
        }

        double podstawaPit = kwotaBrutto - ObliczZusSpoleczny();
        return Math.Round(podstawaPit * 0.12, 2);
    }

    // Obliczanie wynagrodzenia netto
    public double ObliczWynagrodzenieNetto()
    {
        double netto = kwotaBrutto - ObliczZusSpoleczny() - ObliczSkladkeZdrowotna() - ObliczZaliczkePit();
        return Math.Round(netto, 2);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== PROGRAM GŁÓWNY - KALKULATOR WYNAGRODZEŃ ===\n");

        // Wariant 1: Umowa o pracę bez ulgi dla młodych
        KalkulatorWynagrodzen kalk1 = new KalkulatorWynagrodzen(5000.0, "Umowa o pracę", false);
        Console.WriteLine($"Wariant 1: {kalk1.PobierzKwoteBrutto()} PLN ({kalk1.PobierzRodzajUmowy()}, bez ulgi)");
        Console.WriteLine($"  - Składki ZUS: {kalk1.ObliczZusSpoleczny()} PLN");
        Console.WriteLine($"  - Składka zdrowotna: {kalk1.ObliczSkladkeZdrowotna()} PLN");
        Console.WriteLine($"  - Podatek PIT: {kalk1.ObliczZaliczkePit()} PLN");
        Console.WriteLine($"  - Wynagrodzenie Netto: {kalk1.ObliczWynagrodzenieNetto()} PLN\n");

        // Wariant 2: Umowa zlecenie z ulgą dla młodych (<26 lat)
        KalkulatorWynagrodzen kalk2 = new KalkulatorWynagrodzen(5000.0, "Umowa zlecenie", true);
        Console.WriteLine($"Wariant 2: {kalk2.PobierzKwoteBrutto()} PLN ({kalk2.PobierzRodzajUmowy()}, z ulgą dla młodych)");
        Console.WriteLine($"  - Składki ZUS: {kalk2.ObliczZusSpoleczny()} PLN");
        Console.WriteLine($"  - Składka zdrowotna: {kalk2.ObliczSkladkeZdrowotna()} PLN");
        Console.WriteLine($"  - Podatek PIT: {kalk2.ObliczZaliczkePit()} PLN (Ulga dla młodych)");
        Console.WriteLine($"  - Wynagrodzenie Netto: {kalk2.ObliczWynagrodzenieNetto()} PLN");

        Console.WriteLine("\nNaciśnij dowolny klawisz, aby zakończyć...");
        Console.ReadKey();
    }
}