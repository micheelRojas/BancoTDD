using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTDD.Dominio.Contracts
{
   public  interface IUnitOfWork
    {
        ICuentaBancariaRepository CuentaBancariaRepository { get; }
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
