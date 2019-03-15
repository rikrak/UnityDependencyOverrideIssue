namespace UnityDependencyOverrideIssue.Data
{
    public interface IDrillBit
    {
        string Name { get; }
    }
    public class PrimaryDrill : IDrillBit
    {
        public string Name => "Primary drill head";
    }
    public class SecondaryDrill : IDrillBit
    {
        public string Name => "Secondary drill head";
    }
    public class TertiaryDrill : IDrillBit
    {
        public string Name => "Tertiary drill head";
    }

}
