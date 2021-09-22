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
        
        public CuentaBancariaBase(string numero, string nombre)
        {
            Numero = numero;
            Nombre = nombre;
           
            _movimientos = new List<Movimiento>();
        }
        public IReadOnlyCollection<Movimiento> Movimientos => _movimientos.AsReadOnly();

        public virtual string Consignar(decimal valorConsignacion, DateTime fecha)
        {
            if (!_movimientos.Any() && valorConsignacion < 100000)
            {
                return "El valor a consignar es incorrecto";
            }
            throw new NotImplementedException();
        }

        public abstract string Retirar(decimal valorRetiro, DateTime fecha);
    }
}
