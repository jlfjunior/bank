namespace Bank.Accounts.API;

public class TaxDocument
{
    public string Value { get; }

    private TaxDocument(string value)
    {
        //TODO: Add validation
        Value = value;
    }
    
    public static TaxDocument Create(string value) => new (value);

    public override bool Equals(object? obj)
    {
        var other = obj as TaxDocument;

        if (other == null) throw new ArgumentNullException(nameof(other));
        
        return other.Value == Value;
    }

    public static bool operator ==(TaxDocument left, TaxDocument right)
    {
        if (ReferenceEquals(left, null) && ReferenceEquals(right, null)) return true;
        if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;
        
        return left.Equals(right);    
    }
    
    public static bool operator !=(TaxDocument left, TaxDocument right)
    {
        return !(left == right);    
    }
}