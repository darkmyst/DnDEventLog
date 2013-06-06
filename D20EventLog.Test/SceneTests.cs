using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject.MockingKernel.Moq;
using Ninject;

namespace D20EventLog.Models.Test
{
  [TestClass]
  public class SceneTests
  {
    private MoqMockingKernel _Kernel;

    [TestMethod]
    public void NewSceneHasNoActorsAndZeroInitiative()
    {
      ISceneModel scene = _Kernel.Get<ISceneModel>();
      Assert.AreEqual(0, scene.Actors.Count());
      Assert.AreEqual(null, scene.CurrentInitiative);
      Assert.IsFalse(scene.IsActive);
    }

    
    [TestInitialize]
    public void TestInitialize()
    {
      _Kernel = (MoqMockingKernel)D20EventLog.Models.AssemblyBootstrapper.Bootstrap(new MoqMockingKernel());
    }

    [TestCleanup]
    public void TestCleanup()
    {
      _Kernel.Dispose();
      _Kernel = null;
    }

  }
}
