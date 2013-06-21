using Ninject.Modules;
using Ninject.Extensions.Conventions;
using D20EventLog.Lib;

namespace D20EventLog.Views
{
  public class DefaultConventionsModule : NinjectModule
  {
    public override void Load()
    {
      this.Kernel.Bind(x => x
                .FromThisAssembly()
                .SelectAllClasses()
                .Join.FromAssembliesInPath(".")
                .SelectAllClasses()
                .InheritedFrom<IService>()
                .BindDefaultInterface()
                .Configure(b => b.InSingletonScope()));

      this.Kernel.Bind(x => x
                .FromThisAssembly()
                .SelectAllClasses()
                .Join.FromAssembliesInPath(".")
                .SelectAllClasses()
                .Where(t => typeof(IService).IsAssignableFrom(t) == false)
                .BindDefaultInterface()
                .Configure(b => b.InTransientScope()));
    }
  }
}