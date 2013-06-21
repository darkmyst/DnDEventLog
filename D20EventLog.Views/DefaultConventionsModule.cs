using Ninject.Modules;
using Ninject.Extensions.Conventions;
using D20EventLog.Common;
using System.IO;
using System.Reflection;
using D20EventLog.ViewModels;
using D20EventLog.Models;
using System;
using D20EventLog.ViewModels.Stuff;
using System.Diagnostics;
using Ninject;

namespace D20EventLog.Views
{
  public class DefaultConventionsModule : NinjectModule
  {
    public override void Load()
    {
      this.Kernel.Bind(scanner =>
        scanner
          .FromThisAssembly()
          .SelectAllClasses()
          .Join.FromAssemblyContaining(typeof(MainWindowViewModel), typeof(UserModel), typeof(IService))
          .SelectAllClasses()
          .Where(type => IsServiceType(type))
          .BindDefaultInterface()
          .Configure(binding => binding.InSingletonScope()));
      
      this.Kernel.Bind(scanner =>
        scanner
          .FromThisAssembly()
          .SelectAllClasses()
          .Join.FromAssemblyContaining(typeof(MainWindowViewModel), typeof(UserModel), typeof(IService))
          .SelectAllClasses()
          .Where(type => !IsServiceType(type))
          .BindDefaultInterface()
          .Configure(binding => binding.InTransientScope()));      
    }

    private static bool IsServiceType(Type type)
    {
      return typeof(IService).IsAssignableFrom(type) == true;
    }
  }
}