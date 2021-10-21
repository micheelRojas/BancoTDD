using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BancoTDD.Dominio.Contracts;
using BancoTDD.Dominio.CuentasBancarias;


namespace BancoTDD.Infraestrutura
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BancoContext _bancoContext;
        public UnitOfWork(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
            Inicializacion();
        }
        public ICuentaBancariaRepository CuentaBancariaRepository => new CuentaBancariaMemoryRepository(_bancoContext);

        public void BeginTransaction()
        {
            _bancoContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _bancoContext.SaveChanges();
            _bancoContext.Database.CommitTransaction();
        }

        public void Rollback()
        {
            _bancoContext.Database.RollbackTransaction();
        }

        public void Inicializacion()
        {
            if (_bancoContext.CuentasBancarias.Count() == 0)
            {
                _bancoContext.CuentasBancarias.Add(new Dominio.CuentaAhorro("10101", "Cuenta Ahorro 1", "Bogota"));
                _bancoContext.SaveChanges();
            }

        }
    }
}
