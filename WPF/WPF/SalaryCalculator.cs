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
    public string GetEmploymentType() => employmentType;
    public bool GetIsYouthRelief() => isYouthRelief;

    public double CalculateSocialZus()
    {
        return Math.Round(grossAmount * 0.1371, 2);
    }

    public double CalculateHealthZus()
    {
        double baseForHealth = grossAmount - CalculateSocialZus();
        return Math.Round(baseForHealth * 0.09, 2);
    }

    public double CalculatePitTax()
    {
        if (isYouthRelief)
        {
            return 0.00;
        }
        double baseForPit = grossAmount - CalculateSocialZus();
        return Math.Round(baseForPit * 0.12, 2);
    }

    public double CalculateNetSalary()
    {
        double net = grossAmount - CalculateSocialZus() - CalculateHealthZus() - CalculatePitTax();
        return Math.Round(net, 2);
    }
}