using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReactiveUI;

namespace DnDEventLog
{
    public class ActorEvent : ReactiveObject
    {
        protected static int nextEventID = 1;

        public ActorEvent()
        {
            EventID = nextEventID++;
        }

        #region EventID - int
        private int _EventID = 0;
        public int EventID
        {
            get { return _EventID; }
            protected set { _EventID = this.RaiseAndSetIfChanged(x => x.EventID, value); }
        }
        #endregion
        
        #region Round - int
        private int _Round = 0;
        public int Round
        {
            get { return _Round; }
            set { _Round = this.RaiseAndSetIfChanged(x => x.Round, value); }
        }
        #endregion

        #region Actor - Actor
        private Actor _Actor = null;
        public Actor Actor
        {
            get { return _Actor; }
            set { _Actor = this.RaiseAndSetIfChanged(x => x.Actor, value); }
        }
        #endregion

        #region EventType - string
        private string _EventType = string.Empty;
        public string EventType
        {
            get { return _EventType; }
            set { _EventType = this.RaiseAndSetIfChanged(x => x.EventType, value); }
        }
        #endregion

        #region EventName - string
        private string _EventName = string.Empty;
        public string EventName
        {
            get { return _EventName; }
            set { _EventName = this.RaiseAndSetIfChanged(x => x.EventName, value); }
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
