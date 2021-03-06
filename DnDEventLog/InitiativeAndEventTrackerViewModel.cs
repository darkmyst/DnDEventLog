﻿using System;
using System.Collections.Generic;
using System.Linq;
using ReactiveUI;
using System.Windows.Input;
using ReactiveUI.Xaml;
using System.Windows.Data;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Windows;

namespace DnDEventLog
{
    public class InitiativeAndEventTrackerViewModel : ReactiveObject
    {
        #region Actors - ReactiveCollection<Actor>
        private ReactiveCollection<Actor> _Actors = null;
        public ReactiveCollection<Actor> Actors
        {
            get { return _Actors; }
            set { _Actors = this.RaiseAndSetIfChanged(x => x.Actors, value); }
        }
        #endregion

        #region ActorsViewSource - ListCollectionView
        private ICollectionView _ActorsView = null;
        public ICollectionView ActorsView
        {
            get { return _ActorsView; }
            set { _ActorsView = this.RaiseAndSetIfChanged(x => x.ActorsView, value); }
        }
        #endregion

        #region ActorObservableDisposables - Dictionary<Actor, List<IDisposable>>
        private Dictionary<Actor, List<IDisposable>> _ActorObservableDisposables = new Dictionary<Actor, List<IDisposable>>();
        public Dictionary<Actor, List<IDisposable>> ActorObservableDisposables
        {
            get { return _ActorObservableDisposables; }
            set { _ActorObservableDisposables = this.RaiseAndSetIfChanged(x => x.ActorObservableDisposables, value); }
        }
        #endregion

        #region Events - ReactiveCollection<ActorEvent>
        private ReactiveCollection<ActorEvent> _Events = null;
        public ReactiveCollection<ActorEvent> Events
        {
            get { return _Events; }
            set { _Events = this.RaiseAndSetIfChanged(x => x.Events, value); }
        }
        #endregion

        #region CurrentRound - int
        private int _CurrentRound = 0;
        public int CurrentRound
        {
            get { return _CurrentRound; }
            set { _CurrentRound = this.RaiseAndSetIfChanged(x => x.CurrentRound, value); }
        }
        #endregion

        #region CurrentInitiative - int?
        private int? _CurrentInitiative = null;
        public int? CurrentInitiative
        {
            get { return _CurrentInitiative; }
            set { _CurrentInitiative = this.RaiseAndSetIfChanged(x => x.CurrentInitiative, value); }
        }
        #endregion

        public ICommand AddActorCommand { get; protected set; }
        public ICommand EditActorCommand { get; protected set; }
        public ICommand DeleteActorCommand { get; protected set; }
        public ICommand ResetCombatCommand { get; protected set; }
        public ICommand NextInitiativeCommand { get; protected set; }

        public InitiativeAndEventTrackerViewModel()
        {
            this.ObservableForProperty(x => x.Actors)
                .Value()
                .Subscribe(x =>
            {
                var view = CollectionViewSource.GetDefaultView(Actors);
                view.SortDescriptions.Add(new SortDescription("Initiative", ListSortDirection.Descending));
                ActorsView = view;
            });

            Actors = new ReactiveCollection<Actor>();
            Events = new ReactiveCollection<ActorEvent>();

            InitializeCommands();
        }

        private void InitializeCommands()
        {
            var addActorCommand = new ReactiveCommand();
            addActorCommand.Subscribe(_ => AddActor());
            AddActorCommand = addActorCommand;

            var editActorCommand = new ReactiveCommand();
            editActorCommand.Subscribe(x => EditActor(x as Actor));
            EditActorCommand = editActorCommand;

            var deleteActorCommand = new ReactiveCommand();
            deleteActorCommand.Subscribe(x => DeleteActor(x as Actor));
            DeleteActorCommand = deleteActorCommand;

            var restCombatCommand = new ReactiveCommand();
            restCombatCommand.Subscribe(_ => InitializeCombat());
            ResetCombatCommand = restCombatCommand;

            var nextInitiativeCommand = new ReactiveCommand();
            nextInitiativeCommand.Subscribe(_ => StepToNextInitiative());
            NextInitiativeCommand = nextInitiativeCommand;
        }

        private void InitializeCombat()
        {
            CurrentRound = 0;
            CurrentInitiative = null;
            foreach (var actor in Actors.ToList())
            {
                if (actor.IsPlayer)
                {
                    actor.Initiative = 0;
                    actor.HasInitiative = false;
                }
                else
                    Actors.Remove(actor);
            }
        }

        private void StepToNextInitiative()
        {
            if (Actors == null || !Actors.Any())
            {
                CurrentInitiative = 0;
                CurrentRound = 0;
                return;
            }

            foreach (var actor in Actors)
                actor.HasInitiative = false;

            var actorsWithInitiative =
                Actors.Where(x => x.IsEnabled)
                        .GroupBy(x => x.Initiative)
                        .OrderByDescending(x => x.Key)
                        .SkipWhile(x => x.Key >= CurrentInitiative)
                        .FirstOrDefault();

            if (actorsWithInitiative == null || !actorsWithInitiative.Any())
            {
                CurrentRound++;
                actorsWithInitiative =
                    Actors.Where(x => x.IsEnabled)
                            .GroupBy(x => x.Initiative)
                            .OrderByDescending(x => x.Key)
                            .FirstOrDefault();
            }

            if (actorsWithInitiative == null || !actorsWithInitiative.Any())
            {
                CurrentInitiative = 0;
                CurrentRound = 0;
                return;
            }

            if (CurrentRound == 0) CurrentRound++;
            CurrentInitiative = actorsWithInitiative.Key;
            foreach (var actor in actorsWithInitiative)
            {
                actor.HasInitiative = true;
                actor.IsDelayed = false;
            }
        }

        private void SetActorInitiativeToCurrent(Actor actor)
        {
            if (CurrentInitiative.HasValue)
            {
                actor.Initiative = CurrentInitiative.Value;
                RefreshActor(actor);
            }
        }

        private int _ActorNum = 0;
        private void AddActor()
        {
            var actor = new Actor { Name = string.Format("Actor {0}", ++_ActorNum), Initiative = 0, HasInitiative = false, IsEnabled = true, IsDelayed = false, Notes = string.Empty };
            var editActorWindow = new EditActor(actor);
            editActorWindow.ShowDialog();
            if (!string.IsNullOrWhiteSpace(actor.Name))
            {
                Actors.Add(actor);
                if (actor.Initiative == CurrentInitiative)
                    actor.HasInitiative = true;

                var enabledDisposable = actor.ObservableForProperty(x => x.IsEnabled).Subscribe(x => RefreshActor(x.Sender));
                var delayedDisposable = actor.ObservableForProperty(x => x.IsDelayed).Where(x => !x.Value).Subscribe(x => SetActorInitiativeToCurrent(x.Sender));

                if (!ActorObservableDisposables.ContainsKey(actor))
                    ActorObservableDisposables.Add(actor, new List<IDisposable> { enabledDisposable, delayedDisposable });
                else if (ActorObservableDisposables[actor] == null)
                    ActorObservableDisposables[actor] = new List<IDisposable> { enabledDisposable, delayedDisposable };
                else
                {
                    ActorObservableDisposables[actor].Add(enabledDisposable);
                    ActorObservableDisposables[actor].Add(delayedDisposable);
                }

                ActorsView.Refresh();
            }
        }

        private void EditActor(Actor actor)
        {
            if (actor != null)
            {
                var editActorWindow = new EditActor(actor);
                editActorWindow.ShowDialog();
                if (actor.Initiative == CurrentInitiative)
                    actor.HasInitiative = true;
                ActorsView.Refresh();
            }
        }

        private void DeleteActor(Actor actor)
        {
            if (actor != null 
                && MessageBox.Show(string.Format("Are you sure you want to delete {0}", actor.Name), 
                                   string.Format("Delete {0}?", actor.Name),
                                   MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Actors.Remove(actor);
                ActorsView.Refresh();

                if (ActorObservableDisposables.ContainsKey(actor))
                {
                    foreach (var disposable in ActorObservableDisposables[actor])
                        disposable.Dispose();
                    ActorObservableDisposables.Remove(actor);
                }
            }
        }

        private void RefreshActor(Actor actor)
        {
            if (actor.IsDelayed || !actor.IsEnabled)
                actor.HasInitiative = false;
            else if (actor.Initiative == CurrentInitiative)
                actor.HasInitiative = true;

            ActorsView.Refresh();
        }
    }
}
