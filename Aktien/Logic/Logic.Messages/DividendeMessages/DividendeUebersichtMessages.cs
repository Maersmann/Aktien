﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Messages.DividendeMessages
{
    public class OpenDividendeUebersichtMessage
    {
        public int AktieID { get; set; }
    }
    public class LoadDividendeFuerAktieMessage
    {
        public int AktieID { get; set; }
    }
}
