using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D20EventLog.Models
{
  public static class AssemblyBootstrapper
  {
    public static IKernel Bootstrap(IKernel kernel)
    {
      kernel.Bind<IActorModel>().To<ActorModel>();
      kernel.Bind<ISceneModel>().To<SceneModel>();
      return kernel;
    }
  }
}
