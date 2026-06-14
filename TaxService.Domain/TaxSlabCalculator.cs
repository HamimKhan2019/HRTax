namespace TaxService.Domain;

public record SlabBreakdown(decimal SlabRange, decimal Rate, decimal TaxableAmount, decimal TaxAmount);

public static class TaxSlabCalculator
{
    public static (List<SlabBreakdown> Breakdown, decimal TotalTax) Calculate(decimal annualTaxableIncome)
    {
        var slabs = new (decimal limit, decimal rate)[]
        {
            (350000, 0.00m),
            (100000, 0.05m),
            (400000, 0.10m),
            (500000, 0.15m),
            (500000, 0.20m),
            (decimal.MaxValue, 0.25m)
        };

        var breakdown = new List<SlabBreakdown>();
        decimal remaining = annualTaxableIncome;
        decimal totalTax = 0;

        foreach (var (limit, rate) in slabs)
        {
            if (remaining <= 0) break;
            decimal taxableInThisSlab = Math.Min(remaining, limit);
            decimal taxForSlab = taxableInThisSlab * rate;
            breakdown.Add(new SlabBreakdown(limit, rate * 100, taxableInThisSlab, taxForSlab));
            totalTax += taxForSlab;
            remaining -= taxableInThisSlab;
        }

        return (breakdown, totalTax);
    }
}