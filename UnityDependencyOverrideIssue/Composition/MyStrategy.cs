using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Builder;
using Unity.Injection;
using Unity.Registration;
using Unity.Resolution;
using Unity.Strategies;
using UnityDependencyOverrideIssue.Data;

namespace UnityDependencyOverrideIssue.Composition
{
    public class MyStrategy : BuilderStrategy
    {
        private static int _counter = 0;

        public MyStrategy()
        {
            
        }

        public override void PreBuildUp(ref BuilderContext context)
        {
            // Get the type that is to be constructed
            var typeToBuild = GetTypeToBuild(context);

            if (!IsBuildingLayer(typeToBuild)) { return; }

            if (IsThereAnotherLayerInHierarchy(context))
            {
                // so when an instance of IQueryProcessor is requested, provide an instance that is marked as "Nested"
                var resolvedParameter = new ResolvedParameter<IDrillBit>( CompositionRoot.SecondDrill);
                var dependencyOverride = new DependencyOverride<IDrillBit>(resolvedParameter);

                AddResolverOverrides(ref context, dependencyOverride);
            }

            if (++_counter > 50)
            {
                throw new FileLoadException("Boom");
            }
        }

        private static Type GetTypeToBuild(BuilderContext context)
        {
            Type typeToBuild;
            if (context.Registration is ContainerRegistration registration)
            {
                typeToBuild = registration.Type;
            }
            else
            {
                typeToBuild = context.Type;
            }

            return typeToBuild;
        }

        public bool IsThereAnotherLayerInHierarchy(BuilderContext context)
        {
            var typeOfInterest = typeof(ILayer);
            
            BuilderContext? currentContext = context;
            var hasTypeOfInterest = false;
            while (currentContext.HasValue && !hasTypeOfInterest)
            {
                var typeToBuild = GetTypeToBuild(currentContext.Value);
                hasTypeOfInterest = typeOfInterest.IsAssignableFrom(typeToBuild) && typeof(Crust) != typeToBuild;
                currentContext = GetParentContext(currentContext);
            }

            return hasTypeOfInterest;
        }

        private static bool IsBuildingLayer(Type typeToBuild)
        {
            return typeof(ILayer).IsAssignableFrom(typeToBuild);
        }

        public static void AddResolverOverrides(ref BuilderContext ctx, ResolverOverride theOverride)
        {
            var newOverrides = ctx.Overrides;
            newOverrides = newOverrides == null 
                ? new ResolverOverride[]{theOverride} 
                : newOverrides.Concat(new[] {theOverride}).ToArray();

            ctx.Overrides = newOverrides;
        }

        public static BuilderContext? GetParentContext(BuilderContext? ctx)
        {
            if (ctx == null)
            {
                return null;
            }
            return GetParentContext(ctx.Value);
        }

        public static BuilderContext? GetParentContext(BuilderContext ctx)
        {
            if (IntPtr.Zero == ctx.Parent) return null;

            BuilderContext parentContext;
            unsafe
            {
                parentContext = Unsafe.AsRef<BuilderContext>(ctx.Parent.ToPointer());
            }

            return parentContext;
        }


    }
}