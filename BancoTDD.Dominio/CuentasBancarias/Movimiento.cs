using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTDD.Dominio.CuentasBancarias
{
    public class Movimiento
    {
        public Movimiento()
        {

        }
        public Movimiento(CuentaBancaria cuentaBancaria, DateTime fecha, string tipo, decimal valor)
        {
            CuentaBancaria = cuentaBancaria;
            Fecha = fecha;
            Tipo = tipo;
            Valor = valor;
        }
        public int Id { get; private set; }
        public CuentaBancaria CuentaBancaria { get; private set; }
        public DateTime Fecha { get; private set; }
        public string Tipo { get; private set; }
        public decimal Valor { get; private set; }
    }
}
