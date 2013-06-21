using ReactiveUI;
using System.Windows.Media;

namespace DnDEventLog
{
    public class ActorEffect : ReactiveObject
    {
        #region Name - string
        private string _Name = string.Empty;
        public string Name
        {
            get { return _Name; }
            set { _Name = this.RaiseAndSetIfChanged(x => x.Name, value); }
        }
        #endregion

        #region Code - string
        private string _Code = string.Empty;
        public string Code
        {
            get { return _Code; }
            set { _Code = this.RaiseAndSetIfChanged(x => x.Code, value); }
        }
        #endregion

        #region BackgroundColor - Brush
        private Brush _BackgroundColor = Brushes.Transparent;
        public Brush BackgroundColor
        {
            get { return _BackgroundColor; }
            set { _BackgroundColor = this.RaiseAndSetIfChanged(x => x.BackgroundColor, value); }
        }
        #endregion        

        #region ForegroundColor - Brush
        private Brush _ForegroundColor = Brushes.Black;
        public Brush ForegroundColor
        {
            get { return _ForegroundColor; }
            set { _ForegroundColor = this.RaiseAndSetIfChanged(x => x.ForegroundColor, value); }
        }
        #endregion

        #region Source - string
        private string _Source = string.Empty;
        public string Source
        {
            get { return _Source; }
            set { _Source = this.RaiseAndSetIfChanged(x => x.Source, value); }
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
