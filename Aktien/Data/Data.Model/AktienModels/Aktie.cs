﻿using Aktien.Data.Model.DepotModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Aktien.Data.Model.AktienModels
{
    [Table("Aktie")]
    public class Aktie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public string ISIN { get; set; }

        public string WKN { get; set; }

        public List<Dividende> Dividenden { get; set; }
        public List<DividendeErhalten> ErhalteneDividenden { get; set; }
        public List<OrderHistory> OrderHistories { get; set; }
    }
}
