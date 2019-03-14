using System;

namespace UnityDependencyOverrideIssue.Data
{
    public interface ILayer
    {
        void DrillDown();
    }

    public interface IDrillBit
    {
        string Name { get; }
    }

    public class Crust : ILayer
    {
        private readonly IDrillBit _drillBit;
        private readonly ILayer _nextLayer;

        public Crust(IDrillBit drillBit, ILayer nextLayer)
        {
            _drillBit = drillBit;
            _nextLayer = nextLayer;
        }

        public void DrillDown()
        {
            Console.WriteLine($"cutting through the Crust with a {_drillBit.Name}");
            
            _nextLayer.DrillDown();
        }
    }

    public class Mantle : ILayer
    {
        private readonly IDrillBit _drillBit;
        private readonly ILayer _nextLayer;

        public Mantle(IDrillBit drillBit, ILayer nextLayer)
        {
            _drillBit = drillBit;
            _nextLayer = nextLayer;
        }

        public void DrillDown()
        {
            Console.WriteLine($"cutting through the Mantle with a {_drillBit.Name}");
            
            _nextLayer.DrillDown();
        }
    }

    public class Core : ILayer
    {
        private readonly IDrillBit _drillBit;

        public Core(IDrillBit drillBit)
        {
            _drillBit = drillBit;
        }

        public void DrillDown()
        {
            Console.WriteLine($"cutting through the Mantle with a {_drillBit.Name}");
        }
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
