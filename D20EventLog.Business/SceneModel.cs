using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D20EventLog.Models
{
  public interface ISceneModel 
  {
    IEnumerable<IActorModel> Actors { get; }
    int? CurrentInitiative { get; }
    bool IsActive { get; }
  }

  internal class SceneModel : ISceneModel
  {
    public IEnumerable<IActorModel> Actors { get; internal set; }
    public int? CurrentInitiative { get; internal set; }
    public bool IsActive { get; internal set; }

    public SceneModel()
    {
      Actors = Enumerable.Empty<IActorModel>();
      CurrentInitiative = null;
      IsActive = false;
    }
  }
}
