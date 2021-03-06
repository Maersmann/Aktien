﻿using Aktien.Data.Model.WertpapierEntitys;
using Aktien.Data.Model.DepotEntitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Aktien.Data.Model.OptionEntitys;

namespace Aktien.Data.Infrastructure
{
    public class Repository : DbContext
    {

        public DbSet<Wertpapier> Wertpapiere { get; set; }
        public DbSet<Dividende> Dividenden { get; set; }
        public DbSet<OrderHistory> OrderHistories { get; set; }
        public DbSet<Depot> Depots { get; set; }
        public DbSet<DepotWertpapier> AktienInDepots { get; set; }
        public DbSet<DividendeErhalten> ErhaltendeDividenden { get; set; }
        public DbSet<Einnahme> Einnahmen { get; set; }
        public DbSet<Ausgabe> Ausgaben { get; set; }
        public DbSet<Konvertierung> Konvertierungen { get; set; }
        public DbSet<ETFInfo> ETFInfos { get; set; }

        public Repository() : base() { this.Database.Migrate(); }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(VersionContextConnection.GetDatabaseConnectionstring());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
