using Unity;
using Unity.Injection;
using Unity.Lifetime;
using UnityDependencyOverrideIssue.Data;

namespace UnityDependencyOverrideIssue.Composition
{
    class CompositionRoot
    {
        public const string SecondDrill = "SecondDrill";

        private readonly UnityContainer _container;
        public UnityContainer Container => _container;

        public CompositionRoot()
        {
            this._container = new UnityContainer();

            this.Initialise();
        }

        private void Initialise()
        {
            this._container
                .AddNewExtension<MyExtension>()
                .AddNewExtension<Diagnostic>()
                .RegisterType<IDrillBit, PrimaryDrill>()
                .RegisterType<IDrillBit, SecondaryDrill>(SecondDrill)
                .RegisterType<ILayer, Crust>(new TransientLifetimeManager(),
                    new InjectionConstructor(new ResolvedParameter(typeof(IDrillBit)), new ResolvedParameter(typeof(ILayer), "mantle")))
                .RegisterType<ILayer, Mantle>("mantle", new TransientLifetimeManager(),
                    new InjectionConstructor(new ResolvedParameter(typeof(IDrillBit)), new ResolvedParameter(typeof(ILayer), "core")))
                .RegisterType<ILayer, Core>("core")
                .RegisterType<IController, TheController>()
                .RegisterType<IMessageProvider, DefaultMessageProvider>()
                .RegisterType<IMessageProvider, AlternativeMessageProvider>(nameof(AlternativeMessageProvider))
                ;
        }
    }
}
