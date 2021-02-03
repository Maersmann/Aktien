﻿using Aktien.Data.Types;
using GalaSoft.MvvmLight.Messaging;
using Aktien.Logic.Core.Depot;
using Aktien.Logic.Core.Validierung;
using Aktien.Logic.Messages.Base;
using Aktien.Logic.UI.BaseViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aktien.Data.Model.DepotModels;
using Aktien.Data.Model.AktienModels;
using Aktien.Logic.Core.AktieLogic;

namespace Aktien.Logic.UI.DepotViewModels
{
    public class BuyOrderViewModel : ViewModelStammdaten
    {
        private OrderHistory data;
        private BuySell buySell;
        public BuyOrderViewModel()
        {
            SaveCommand = new DelegateCommand(this.ExecuteSaveCommand, this.CanExecuteSaveCommand);

            state = State.Neu;
            data = new OrderHistory();

            KaufTyp = Data.Types.KaufTypes.Kauf;
            OrderTyp = Data.Types.OrderTypes.Normal;
            Anzahl = null;
            Preis = null;
            Datum = DateTime.Now;
            buySell = BuySell.Buy;
        }

        protected override void ExecuteSaveCommand()
        {
            var Depot = new DepotAPI();
            if (buySell.Equals(BuySell.Buy))
            {
                Depot.NeueAktieGekauft(data.Preis, data.Fremdkostenzuschlag, data.Orderdatum, AktieID, data.Anzahl, data.KaufartTyp, data.OrderartTyp);
            }
            else
            {
                Depot.NeueAktieVerkauft(data.Preis, data.Fremdkostenzuschlag, data.Orderdatum, AktieID, data.Anzahl, data.KaufartTyp, data.OrderartTyp);
            }
            Messenger.Default.Send<StammdatenGespeichertMessage>(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Buy-Order erfolgreich gespeichert." }, "BuyOrder");
        }

        public void SetBuySell(BuySell inBuySell)
        {
            buySell = inBuySell;
            this.RaisePropertyChanged("KauftypBez");
            this.RaisePropertyChanged("Titel");
        }


        #region Bindings

        public IEnumerable<KaufTypes> KaufTypes
        {
            get
            {
                return Enum.GetValues(typeof(KaufTypes)).Cast<KaufTypes>();
            }
        }
        public IEnumerable<OrderTypes> OrderTypes
        {
            get
            {
                return Enum.GetValues(typeof(OrderTypes)).Cast<OrderTypes>();
            }
        }
        public KaufTypes KaufTyp
        {
            get { return data.KaufartTyp; }
            set
            {
                if (LoadAktie || (this.data.KaufartTyp != value))
                {
                    this.data.KaufartTyp = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        public OrderTypes OrderTyp
        {
            get { return data.OrderartTyp; }
            set
            {
                if (LoadAktie || (this.data.OrderartTyp != value))
                {
                    this.data.OrderartTyp = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        public double? Anzahl
        {
            get
            {
                return data.Anzahl;
            }
            set
            {
                if (LoadAktie || (this.data.Anzahl != value))
                {
                    ValidateAnzahl(value);
                    this.data.Anzahl = value.GetValueOrDefault();
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public double? Fremdkosten
        {
            get
            {
                return data.Fremdkostenzuschlag;
            }
            set
            {
                if (LoadAktie || (this.data.Fremdkostenzuschlag != value))
                {
                    this.data.Fremdkostenzuschlag = value;
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public double? Preis
        {
            get
            {
                return data.Preis;
            }
            set
            {
                if (LoadAktie || (this.data.Preis != value))
                {
                    ValidateBetrag(value);
                    this.data.Preis = value.GetValueOrDefault();
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public DateTime? Datum
        {
            get
            {
                return data.Orderdatum;
            }
            set
            {
                if (LoadAktie || (!DateTime.Equals(this.data.Orderdatum, value)))
                {
                    ValidateDatum(value);
                    this.data.Orderdatum = value.GetValueOrDefault();
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public String KauftypBez 
        { 
            get 
            {
                if (buySell == BuySell.Buy)
                {
                    return "Kauf Art";
                }
                else
                {
                    return "Verkauf Art";
                }
            } 
        }
        public String Titel
        {
            get
            {
                if (buySell == BuySell.Buy)
                {
                    return "Aktie gekauft";
                }
                else
                {
                    return "Aktie verkauft";
                }
            }
        }
        public int AktieID { get; set; }

        #endregion

        #region Validierung

        private bool ValidateAnzahl(double? inAnzahl)
        {
            var Validierung = new AktieGekauftValidierung();

            bool isValid = Validierung.ValidateAnzahl(inAnzahl, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Anzahl", validationErrors);
            return isValid;
        }

        private bool ValidateBetrag(Double? inBetrag)
        {
            var Validierung = new AktieGekauftValidierung();

            bool isValid = Validierung.ValidateBetrag(inBetrag, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Preis", validationErrors);
            return isValid;
        }

        private bool ValidateDatum(DateTime? value)
        {
            var Validierung = new AktieGekauftValidierung();

            bool isValid = Validierung.ValidateDatum(value, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Datum", validationErrors);
            return isValid;
        }

        #endregion

        public override void Cleanup()
        {
            state = State.Neu;
            data = new OrderHistory();
            KaufTyp = Data.Types.KaufTypes.Kauf;
            OrderTyp = Data.Types.OrderTypes.Normal;
            Anzahl = null;
            Preis = null;
            Datum = DateTime.Now;
            Fremdkosten = null;
        }
    }
}