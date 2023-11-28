namespace SuperCalculator.Application.Models;

public class PayCode
{
    public string Code { get; set; }

    public OteTreatment Treatment { get; set; }

    public static OteTreatment GetOteFromString(string treatment)
    {
        switch (treatment)
        {
            case "OTE":
                return OteTreatment.OTE;
            case "Not OTE":
                return OteTreatment.NotOte;
            default:
                throw new InvalidCastException("Ote Treatment not found");
        }
    }
}
public enum OteTreatment
{
    NotOte,
    OTE
}