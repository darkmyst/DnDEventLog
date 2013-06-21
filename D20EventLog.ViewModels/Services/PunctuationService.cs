using D20EventLog.ViewModels.Stuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D20EventLog.ViewModels.Services
{
  public class PunctuationService : IPunctuationService
  {
    public int SentencesPunctuated { get; protected set; }

    public string Punctuate(string sentence, Punctuation punctuation)
    {
      if (sentence == null)
        return null;

      SentencesPunctuated++;

      string punctuationCharacters = "";

      switch (punctuation)
      {
        case Punctuation.Hesitant:
          punctuationCharacters = "...";
          break;
        case Punctuation.Normal:
          punctuationCharacters = ".";
          break;
        case Punctuation.Questioning:
          punctuationCharacters = "?";
          break;
        case Punctuation.Excited:
          punctuationCharacters = "!";
          break;
        case Punctuation.ReallyExcited:
          punctuationCharacters = "!!";
          break;
        case Punctuation.OverlyExcited:
          punctuationCharacters = "!!!11";
          break;
        default:
          throw new NotImplementedException();
      }

      return string.Format("{0}{1} ({2})", sentence.Trim(), punctuationCharacters, SentencesPunctuated); 
    }
  }
}
