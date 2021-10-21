using BancoTDD.Dominio.CuentasBancarias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTDD.Dominio.Contracts
{
    public interface ICuentaBancariaRepository
    {
        CuentaBancaria Find(string numero);
        void Update(CuentaBancaria cuentaBancaria);
    }
}
