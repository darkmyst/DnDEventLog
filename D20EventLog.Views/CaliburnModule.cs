using Caliburn.Micro;
using Ninject.Modules;

namespace D20EventLog.Views
{
  public class CaliburnModule : NinjectModule
  {
    public override void Load()
    {
      this.Kernel.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
      this.Kernel.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();
    }
  }
}