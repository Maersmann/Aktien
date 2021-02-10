﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aktien.Logic.Core.Depot.Classes
{
    public class KaufBerechnungen
    { 
        public Double BuyInAktieGekauft(Double inBuyIn, double inAlteAnzahl, double inNeueGesamtAnzahl, double inPreis, double inNeueAnzahl, double? inFremdkosten)
        {
             return ((inBuyIn * inAlteAnzahl) + ((inPreis * inNeueAnzahl) + inFremdkosten.GetValueOrDefault(0))) / inNeueGesamtAnzahl;
        }

        public Double BuyInAktieEntfernt(Double inBuyIn, double inAlteAnzahl, double inNeueGesamtAnzahl, double inPreis, double inNeueAnzahl, double? inFremdkosten)
        {
            return ((inBuyIn * inAlteAnzahl) - ((inPreis * inNeueAnzahl) - inFremdkosten.GetValueOrDefault(0))) / inNeueGesamtAnzahl;
        }
    }
}