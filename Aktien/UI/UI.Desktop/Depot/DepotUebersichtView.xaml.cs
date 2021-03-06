﻿using Aktien.Logic.Messages.DepotMessages;
using Aktien.Logic.Messages.DividendeMessages;
using Aktien.Logic.UI.DepotViewModels;
using Aktien.Logic.UI.DividendeViewModels;
using Aktien.Logic.UI.WertpapierViewModels;
using Aktien.UI.Desktop.Dividende;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Aktien.UI.Desktop.Depot
{
    /// <summary>
    /// Interaktionslogik für DepotUebersichtView.xaml
    /// </summary>
    public partial class DepotUebersichtView : UserControl
    {
        public DepotUebersichtView()
        {
            InitializeComponent();
            Messenger.Default.Register<OpenDividendenUebersichtAuswahlMessage>(this, "DepotUebersicht", m => ReceiveOpenDividendeUebersichtMessage(m));
        }

        public string MessageToken
        {
            set
            {
               
                if (this.DataContext is DepotUebersichtViewModel modelUebersicht)
                {
                    modelUebersicht.MessageToken = value;
                }
            }
        }

        private void ReceiveOpenDividendeUebersichtMessage(OpenDividendenUebersichtAuswahlMessage m)
        {
            var view = new DividendenUebersichtAuswahlView();

            if (view.DataContext is DividendenUebersichtAuswahlViewModel model)
                model.WertpapierID = m.WertpapierID;
            view.ShowDialog();
        }
    }
}
