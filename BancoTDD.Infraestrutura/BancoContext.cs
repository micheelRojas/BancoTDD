using BancoTDD.Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTDD.Infraestrutura
{
   public  class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options)
            : base(options) { }

        public DbSet<CuentaAhorro> CuentasBancarias { get; set; }
    }
}
