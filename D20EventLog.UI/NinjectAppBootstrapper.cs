using Caliburn.Micro;
using D20EventLog.Lib;
using D20EventLog.ViewModels;
using D20EventLog.ViewModels.Interfaces;
using Ninject;
using Ninject.Extensions.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace D20EventLog.Views
{
  public class NinjectAppBootstrapper : BootstrapperBase
  {
        public NinjectAppBootstrapper()
            : this(true)
        {
        }

        public NinjectAppBootstrapper(bool useApplication)
            : base(useApplication)
        {
            this.Start();
        }

        protected StandardKernel Kernel { get; private set; }

        protected override void BuildUp(object instance)
        {
            this.Kernel.Inject(instance);
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            yield return Assembly.GetAssembly(typeof(NinjectAppBootstrapper));
        }

        protected override void Configure()
        {
            this.Kernel = new StandardKernel();
            this.Kernel.Load(this.SelectAssemblies());
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            this.DisplayRootViewFor(typeof(IMainWindowViewModel));
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            this.Kernel.Dispose();

            base.OnExit(sender, e);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return this.Kernel.GetAll(service);
        }

        protected override object GetInstance(Type service, string key)
        {
            return string.IsNullOrEmpty(key) ? this.Kernel.Get(service) : this.Kernel.Get(service, key);
        }
    }
}