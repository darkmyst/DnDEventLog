using Caliburn.Micro;
using D20EventLog.ViewModels.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D20EventLog.ViewModels.Stuff
{
  public interface ISentenceViewModel : IScreen, IReactiveNotifyPropertyChanged
  {
    string BareSentence { get; set; }
    Punctuation Punctuation { get; set; }
    string RenderedSentence { get; }
  }
}
