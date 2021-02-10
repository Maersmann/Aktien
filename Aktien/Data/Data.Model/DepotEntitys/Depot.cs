﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aktien.Data.Model.DepotEntitys
{
    [Table("Depot")]
    public class Depot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public String Bezeichnung { get; set; }
        public List<DepotWertpapier> DepotWertpapier { get; set; }
    }
}