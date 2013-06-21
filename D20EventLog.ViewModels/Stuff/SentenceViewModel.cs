using Caliburn.Micro.ReactiveUI;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D20EventLog.ViewModels.Services;

namespace D20EventLog.ViewModels.Stuff
{
  public class SentenceViewModel : ReactiveScreen, ISentenceViewModel
  {
    private IPunctuationService _punctuationService;

    string _BareSentence = null;
    public string BareSentence
    {
      get { return _BareSentence; }
      set { this.RaiseAndSetIfChanged(value); }
    }

    Punctuation _Punctuation = Punctuation.Normal;
    public Punctuation Punctuation
    {
      get { return _Punctuation; }
      set { this.RaiseAndSetIfChanged(value); }
    }    
    
    string _RenderedSentence = null;
    public string RenderedSentence
    {
      get { return _RenderedSentence; }
      protected set { this.RaiseAndSetIfChanged(value); }
    }

    public SentenceViewModel(IPunctuationService punctuationService)
    {
      this._punctuationService = punctuationService;

      // leaky?
      this.WhenAny(vm => vm.BareSentence, vm => vm.Punctuation, (a,b) => true)
        .Subscribe(_ => RenderedSentence = _punctuationService.Punctuate(BareSentence, Punctuation)); 
    }
  }

  

  public enum Punctuation
  {
    Hesitant,
    Normal,
    Questioning,
    Excited,
    ReallyExcited,
    OverlyExcited
  }

}
