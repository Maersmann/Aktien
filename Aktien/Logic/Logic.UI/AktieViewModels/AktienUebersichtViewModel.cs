﻿using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Aktien.Logic.Messages.Aktie;
using Aktien.Logic.Messages.AktieMessages;
using Aktien.Logic.Messages.Base;
using Aktien.Logic.Messages.DepotMessages;
using Aktien.Logic.Messages.DividendeMessages;
using Aktien.Logic.UI.BaseViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Aktien.Data.Model.WertpapierModels;
using Aktien.Logic.Core.WertpapierLogic;
using Aktien.Logic.Messages.WertpapierMessages;

namespace Aktien.Logic.UI.AktieViewModels
{
    public class AktienUebersichtViewModel : ViewModelBasis
    {
        private ObservableCollection<Wertpapier> alleAktien;

        private Wertpapier selectedAktie;

        public AktienUebersichtViewModel()
        {
            LoadData();
            BearbeitenCommand = new DelegateCommand(this.ExecuteBearbeitenCommand, this.CanExecuteCommand);
            EntfernenCommand = new DelegateCommand(this.ExecuteEntfernenCommand, this.CanExecuteCommand);
            OpenNeueDividendeCommand = new DelegateCommand(this.ExecuteOpenNeueDividendeCommand, this.CanExecuteCommand);
            AddAktieCommand = new RelayCommand(this.ExecuteAddAktieCommand);
        }

        public void LoadData()
        {
            alleAktien = new AktieAPI().LadeAlle();
            this.RaisePropertyChanged("AlleAktien");
        }

        #region Binding
        public Wertpapier SelectedAktie 
        {
            get 
            {
                return selectedAktie; 
            }              
            set
            {
                selectedAktie = value;
                this.RaisePropertyChanged();
                ((DelegateCommand)BearbeitenCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)EntfernenCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)OpenNeueDividendeCommand).RaiseCanExecuteChanged();
                if (selectedAktie != null)
                {
                    Messenger.Default.Send<LoadWertpapierOrderMessage>(new LoadWertpapierOrderMessage { WertpapierID = selectedAktie.ID });
                }
            } 
        }
        public IEnumerable<Wertpapier> AlleAktien 
        {
            get
            {
                return alleAktien;
            }
        }
        public ICommand BearbeitenCommand { get; protected set; }
        public ICommand EntfernenCommand { get; protected set; }
        public ICommand OpenNeueDividendeCommand { get; set; }
        public ICommand AddAktieCommand { get; set; }
        #endregion

        #region Commands
        private bool CanExecuteCommand()
        {
            return selectedAktie!=null;
        }

        private void ExecuteBearbeitenCommand()
        {
            Messenger.Default.Send<Messages.Aktie.OpenAktieStammdatenMessage>(new Messages.Aktie.OpenAktieStammdatenMessage { WertpapierID = selectedAktie.ID, State = Data.Types.State.Bearbeiten });
        }



        private void ExecuteOpenNeueDividendeCommand()
        {
            Messenger.Default.Send<OpenDividendenUebersichtAuswahlMessage>(new OpenDividendenUebersichtAuswahlMessage { WertpapierID = selectedAktie.ID });
        }
        private void ExecuteEntfernenCommand()
        {
            new AktieAPI().Entfernen(selectedAktie);
            Messenger.Default.Send<LoadWertpapierOrderMessage>(new LoadWertpapierOrderMessage { WertpapierID = 0 }, "AktieOrder");
            alleAktien.Remove(SelectedAktie);
            this.RaisePropertyChanged("AlleAktien");     
            Messenger.Default.Send<DeleteAktieErfolgreichMessage>(new DeleteAktieErfolgreichMessage() );
        }

        private void ExecuteAddAktieCommand()
        {
            Messenger.Default.Send<Messages.Aktie.OpenAktieStammdatenMessage>(new Messages.Aktie.OpenAktieStammdatenMessage { State = Data.Types.State.Neu });
        }
        #endregion
    }
}
