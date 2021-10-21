using BancoTDD.Dominio;
using BancoTDD.Dominio.Contracts;
using BancoTDD.Dominio.CuentasBancarias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTDD.Infraestrutura
{
    public class CuentaBancariaMemoryRepository : ICuentaBancariaRepository
    {
        private readonly BancoContext _bancoContext;
        public CuentaBancariaMemoryRepository(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public CuentaBancaria Find(string numero)
        {
            var cuentaBancaria = _bancoContext.CuentasBancarias.FirstOrDefault(t => t.Numero == numero);
            return cuentaBancaria;
        }

        public void Update(CuentaBancaria cuentaBancaria)
        {
            _bancoContext.CuentasBancarias.Update(cuentaBancaria as CuentaAhorro);
        }
    }
}

