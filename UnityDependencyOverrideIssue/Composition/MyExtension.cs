using Unity.Builder;
using Unity.Extension;

namespace UnityDependencyOverrideIssue.Composition
{
    public class MyExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            this.Context.Strategies.Add(new MyStrategy(), UnityBuildStage.PreCreation);
        }
    }
}