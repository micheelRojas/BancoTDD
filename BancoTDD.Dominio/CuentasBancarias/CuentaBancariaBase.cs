using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTDD.Dominio.CuentasBancarias
{
  public abstract class CuentaBancariaBase
    {
        protected List<Movimiento> _movimientos;
        public string Numero { get; private set; }
        public string Nombre { get; private set; }
        public decimal Saldo { get; protected set; }
        protected decimal ValorMinimoConsignacionInicial;
        public CuentaBancariaBase(string numero, string nombre, decimal valorMinimoConsignacionInicial)
        {
            Numero = numero;
            Nombre = nombre;
            ValorMinimoConsignacionInicial = valorMinimoConsignacionInicial;
            _movimientos = new List<Movimiento>();
        }
        public IReadOnlyCollection<Movimiento> Movimientos => _movimientos.AsReadOnly();

        public virtual string Consignar(decimal valorConsignacion, DateTime fecha)
        {
            if (!_movimientos.Any() && valorConsignacion < ValorMinimoConsignacionInicial)
            {
                return "El valor a consignar es incorrecto";
            }

            if (!_movimientos.Any() && valorConsignacion >= ValorMinimoConsignacionInicial)
            {
                AddMovimientoAumenteSaldo(valorConsignacion, fecha, "CONSIGNACION");
                return $"Su Nuevo Saldo es de {Saldo:c2} pesos m/c";
            }
            throw new NotImplementedException();
        }

        public abstract string Retirar(decimal valorRetiro, DateTime fecha);
        protected void AddMovimientoDisminuyeSaldo(decimal valor, DateTime fecha, string tipo)
        {
            AddMovimiento(fecha, tipo, valor);
            Saldo -= valor;
        }

        protected void AddMovimientoAumenteSaldo(decimal valor, DateTime fecha, string tipo)
        {
            AddMovimiento(fecha, tipo, valor);
            Saldo += valor;
        }

        private void AddMovimiento(DateTime fecha, string tipo, decimal valor)
        {
            _movimientos.Add(new Movimiento(cuentaBancaria: this, fecha: fecha, tipo: tipo, valor: valor));
        }
    }
}
