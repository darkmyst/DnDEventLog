using D20EventLog.Common;
using D20EventLog.ViewModels.Stuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D20EventLog.ViewModels.Factories
{
  public interface ISentenceFactory : IFactory
  {
    ISentenceViewModel Create(string sentence, Punctuation punctuation);
  }
}
