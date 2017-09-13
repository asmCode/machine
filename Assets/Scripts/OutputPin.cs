public class OutputPin
{
    public int Id { get; set; }
    public int ElementId { get; set; }
    public float SignalValue { get; set; }
    public bool SignalBoolValue
    {
        get { return SignalValue > 0.0f; }
        set { SignalValue = value ? 1.0f : 0.0f; }
    }
    public bool Resolved { get; set; }
}
