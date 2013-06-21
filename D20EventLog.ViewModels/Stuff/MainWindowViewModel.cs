using Caliburn.Micro.ReactiveUI;
using ReactiveUI;
using System;
using ReactiveUI.Xaml;
using D20EventLog.ViewModels.Factories;

namespace D20EventLog.ViewModels.Stuff
{
  public class MainWindowViewModel : ReactiveScreen, IMainWindowViewModel
  {
    ReactiveCollection<ISentenceViewModel> _Sentences = new ReactiveCollection<ISentenceViewModel>();
    public ReactiveCollection<ISentenceViewModel> Sentences
    {
      get { return _Sentences; }
    }

    string _NewSentence = string.Empty;
    public string NewSentence
    {
      get { return _NewSentence; }
      set { this.RaiseAndSetIfChanged(value); }
    }

    Punctuation _NewPunctuation = Punctuation.Normal;
    public Punctuation NewPunctuation
    {
      get { return _NewPunctuation; }
      set { this.RaiseAndSetIfChanged(value); }
    }

    public Punctuation[] PunctuationOptions
    {
      get { return (Punctuation[])Enum.GetValues(typeof(Punctuation)); }
    }

    public IReactiveCommand CreateSentenceCommand { get; protected set; }

    private ISentenceFactory _sentenceFactory;

    public MainWindowViewModel(ISentenceFactory sentenceFactory)
    {
      _sentenceFactory = sentenceFactory;

      CreateSentenceCommand = new ReactiveCommand(this.WhenAny(x => x.NewSentence, x => !string.IsNullOrEmpty(x.Value)));
      CreateSentenceCommand.Subscribe(_ => CreateSentence());

    }

    private void CreateSentence()
    {
      Sentences.Add(_sentenceFactory.Create(NewSentence, NewPunctuation));
      NewSentence = string.Empty;
      NewPunctuation = Punctuation.Normal;
    }

  }
}