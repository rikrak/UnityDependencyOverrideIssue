using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using Unity.Injection;
using Unity.Resolution;
using UnityDependencyOverrideIssue.Composition;
using UnityDependencyOverrideIssue.Data;

namespace UnityDependencyOverrideIssue
{
    [TestClass]
    public class DependencyOverrideTests
    {
        [TestMethod]
        public void UseOfDynamicDependencyOverrideAsPartOfStrategy()
        {
            // arrange
            var target = new Composition.CompositionRoot();

            // act
            var actual = target.Container.Resolve<ILayer>();

            // assert
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void DirectUseOfDependencyOverride()
        {
            // arrange
            var target = new Composition.CompositionRoot();
            var resolvedParameter = new ResolvedParameter<IDrillBit>( CompositionRoot.SecondDrill);
            var dependencyOverride = new DependencyOverride<IDrillBit>(resolvedParameter);

            // act
            var actual = target.Container.Resolve<ILayer>(dependencyOverride);

            // assert
            Assert.IsNotNull(actual);
        }
    }

}
