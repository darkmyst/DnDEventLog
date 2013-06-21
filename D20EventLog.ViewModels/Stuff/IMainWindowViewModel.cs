using Caliburn.Micro;
using D20EventLog.ViewModels.Services;
using ReactiveUI;
using ReactiveUI.Xaml;
namespace D20EventLog.ViewModels.Stuff
{
  public interface IMainWindowViewModel : IScreen, IReactiveNotifyPropertyChanged
  {
    string NewSentence { get; set; }
    Punctuation NewPunctuation { get; set; }
    Punctuation[] PunctuationOptions { get; }
    ReactiveCollection<ISentenceViewModel> Sentences { get; }    
    IReactiveCommand CreateSentenceCommand { get;  }
  }
}
