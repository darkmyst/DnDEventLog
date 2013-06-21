using ReactiveUI;

namespace DnDEventLog
{
    public class Actor : ReactiveObject
    {
        #region Name - string
        private string _Name = string.Empty;
        public string Name
        {
            get { return _Name; }
            set { _Name = this.RaiseAndSetIfChanged(x => x.Name, value); }
        }
        #endregion        

        #region IsPlayer - bool
        private bool _IsPlayer = false;
        public bool IsPlayer
        {
            get { return _IsPlayer; }
            set { _IsPlayer = this.RaiseAndSetIfChanged(x => x.IsPlayer, value); }
        }
        #endregion

        #region Initiative - int
        private int _Initiative = 0;
        public int Initiative
        {
            get { return _Initiative; }
            set { _Initiative = this.RaiseAndSetIfChanged(x => x.Initiative, value); }
        }
        #endregion

        #region HasInitiative - bool
        private bool _HasInitiative = false;
        public bool HasInitiative
        {
            get { return _HasInitiative; }
            set { _HasInitiative = this.RaiseAndSetIfChanged(x => x.HasInitiative, value); }
        }
        #endregion

        #region IsEnabled - bool
        private bool _IsEnabled = true;
        public bool IsEnabled
        {
            get { return _IsEnabled; }
            set { _IsEnabled = this.RaiseAndSetIfChanged(x => x.IsEnabled, value); }
        }
        #endregion

        #region IsDelayed - bool
        private bool _IsDelayed = false;
        public bool IsDelayed
        {
            get { return _IsDelayed; }
            set { _IsDelayed = this.RaiseAndSetIfChanged(x => x.IsDelayed, value); }
        }
        #endregion
        

        #region Effects - ReactiveCollection<Effect>
        private ReactiveCollection<ActorEffect> _Effects = new ReactiveCollection<ActorEffect>();
        public ReactiveCollection<ActorEffect> Effects
        {
            get { return _Effects; }
            set { _Effects = this.RaiseAndSetIfChanged(x => x.Effects, value); }
        }
        #endregion

        #region Notes - string
        private string _Notes = string.Empty;
        public string Notes
        {
            get { return _Notes; }
            set { _Notes = this.RaiseAndSetIfChanged(x => x.Notes, value); }
        }
        #endregion        
    }
}
