﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Aktien.Logic.Messages.Aktie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aktien.Logic.Core.Validierung;
using System.Runtime.CompilerServices;
using Prism.Commands;
using System.Windows.Input;
using Aktien.Logic.Messages.Base;
using Aktien.Data.Types;
using Aktien.Logic.UI.BaseViewModels;
using Aktien.Logic.Core.AktieLogic;
using Aktien.Data.Model.AktienModels;

namespace Aktien.Logic.UI.AktieViewModels
{
    public class AktieStammdatenViewModel : ViewModelStammdaten
    {

        private Aktie aktie;



        public AktieStammdatenViewModel():base()
        {
            aktie = new Aktie();
            SaveCommand = new DelegateCommand(this.ExecuteSaveCommand, this.CanExecuteSaveCommand);

            ISIN = "";
            Name = "";
            WKN = "";

            state = State.Neu;
        }


        protected override void ExecuteSaveCommand()
        {
            var api = new AktieAPI();
            if (state.Equals( State.Neu ))
            {            
                try
                { 
                    api.Speichern(new Aktie() { ISIN = aktie.ISIN, Name = aktie.Name, WKN = aktie.WKN });
                    Messenger.Default.Send<StammdatenGespeichertMessage>(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Aktie erfolgreich gespeichert." }, "AktieStammdaten");
                }
                catch( AktieSchonVorhandenException)
                {
                    Messenger.Default.Send<StammdatenGespeichertMessage>(new StammdatenGespeichertMessage { Erfolgreich = false, Message = "Aktie ist schon vorhanden." }, "AktieStammdaten");
                }
            }
            else
            {
                api.Aktualisieren(aktie);
                Messenger.Default.Send<StammdatenGespeichertMessage>(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Aktie erfolgreich aktualisiert." }, "AktieStammdaten");
            } 
        }


        public void Bearbeiten(int inID) 
        { 
            LoadAktie = true;
            var Loadaktie = new AktieAPI().LadeAnhandID(inID);
            aktie = new Aktie
            {
                ID = Loadaktie.ID
            };

            WKN = Loadaktie.WKN;
            Name = Loadaktie.Name;
            ISIN = Loadaktie.ISIN;
            LoadAktie = false;
            state = State.Bearbeiten;
            this.RaisePropertyChanged("ISIN_isEnabled");
             

        }


        public bool ISIN_isEnabled { get{ return state == State.Neu; } }
        public string ISIN
        {
            get { return this.aktie.ISIN; }
            set
            {

                if (LoadAktie || !string.Equals(this.aktie.ISIN, value))
                {
                    ValidateISIN(value);
                    this.aktie.ISIN = value;
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public string Name{
            get { return this.aktie.Name; }
            set
            {               
                if (LoadAktie || !string.Equals(this.aktie.Name, value))
                {
                    ValidateName(value);
                    this.aktie.Name = value;
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public string WKN
        {
            get { return this.aktie.WKN; }
            set
            {

                if (LoadAktie || !string.Equals(this.aktie.WKN, value))
                {
                    this.aktie.WKN = value;
                    this.RaisePropertyChanged();
                }
            }
        }


        #region Validate
        private bool ValidateName(String inName)
        {
            var Validierung = new AktieStammdatenValidierung();

            bool isValid = Validierung.ValidateName(inName, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Name", validationErrors);
            return isValid;
        }

        private bool ValidateISIN(String inISIN)
        {
            var Validierung = new AktieStammdatenValidierung();

            bool isValid = Validierung.ValidateISIN(inISIN, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "ISIN", validationErrors);
            return isValid;
        }
        #endregion

        public override void Cleanup()
        {
            state = State.Neu;
            aktie = new Aktie();
            ISIN = "";
            Name = "";
            WKN = "";
        }

    }
}