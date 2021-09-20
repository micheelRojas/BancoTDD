using BancoTDD.Dominio.CuentasBancarias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTDD.Dominio
{
    public class CuentaCorriente : CuentaBancariaBase
    {
        public decimal Sobregiro { get; private set; }

        public CuentaCorriente(string numero, string nombre,string ciudad, decimal sobregiro) : base(numero, nombre,ciudad)
        {
            Sobregiro = -sobregiro;
        }

        public override string Retirar(decimal valorRetiro, DateTime fecha)
        {
            var nuevoSaldoTemporal = Saldo - valorRetiro - valorRetiro * 4 / 1000;
            if (nuevoSaldoTemporal > Sobregiro)
            {
                Saldo = nuevoSaldoTemporal;
                return $"Su Nuevo Saldo es de {Saldo:c2} pesos m/c";
            }
            throw new NotImplementedException();
        }
    }
}
