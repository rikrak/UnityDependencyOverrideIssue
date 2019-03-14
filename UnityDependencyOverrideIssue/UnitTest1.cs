using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using UnityDependencyOverrideIssue.Data;

namespace UnityDependencyOverrideIssue
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // arrange
            var target = new Composition.CompositionRoot();

            // act
            ILayer actual = null;
            try
            {
                actual = target.Container.Resolve<ILayer>();
            }
            catch (StackOverflowException e)
            {
                Console.WriteLine(e);
                Assert.Fail("Stack overflow!");
            }

            // assert
            Assert.IsNotNull(actual);

        }
    }

}
