namespace Bank.Accounts.API;

public class ContactNumber
{
    public string Value { get; set; }


    private ContactNumber(string value)
    {
        //TODO: Add validations
        Value = value;
    }

    public override bool Equals(object? obj)
    {
        var contactNumber = obj as ContactNumber;
        
        if (contactNumber == null)
            throw new ArgumentNullException(nameof(contactNumber));

        return contactNumber.Value == Value;
    }


    public static bool operator ==(ContactNumber left, ContactNumber right)
    {
        if (ReferenceEquals(left, null) && ReferenceEquals(right, null)) return true;
        if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;
        
        return left.Equals(right);
    }
    
    public static bool operator !=(ContactNumber left, ContactNumber right)
    {
        return !(left == right);
    }

    public static ContactNumber Create(string value) => new ContactNumber(value);
}