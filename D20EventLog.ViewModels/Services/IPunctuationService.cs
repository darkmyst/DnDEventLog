using D20EventLog.Common;
using D20EventLog.ViewModels.Stuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D20EventLog.ViewModels.Services

{
  public interface IPunctuationService : IService
  {
    int SentencesPunctuated { get; }
    string Punctuate(string sentence, Punctuation punctuation);
  }
}
