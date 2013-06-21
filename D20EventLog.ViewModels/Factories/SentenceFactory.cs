using D20EventLog.ViewModels.Stuff;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D20EventLog.ViewModels.Factories
{
  public class SentenceFactory : ISentenceFactory
  {
    private IKernel _kernel;

    public SentenceFactory(IKernel kernel)
    {
      this._kernel = kernel;
    }

    public ISentenceViewModel Create(string sentence, Punctuation punctuation)
    {
      var sentenceVM = this._kernel.Get<ISentenceViewModel>();
      sentenceVM.Punctuation = punctuation;
      sentenceVM.BareSentence = sentence;
      return sentenceVM;
    }
  }
}
