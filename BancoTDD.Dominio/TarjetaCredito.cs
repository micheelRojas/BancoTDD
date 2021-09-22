using BancoTDD.Dominio.CuentasBancarias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTDD.Dominio
{
    public class TarjectaCredito:CuentaBancariaBase
    {
        
        public decimal Cupo { get; protected set; }
       
        public TarjectaCredito(string numero, string nombre, decimal cupo) : base(numero, nombre)
        {
           
            Cupo = cupo;
           
        }
       
        public override string Consignar(decimal valorConsignacion, DateTime fecha)
        {
            if (valorConsignacion <= 0)
            {
                return "El Valor a consignar o abonar no puede ser menor a cero $0";
            }
            if (valorConsignacion <= Saldo)
            {
                _movimientos.Add(new Movimiento(cuentaBancaria: this, fecha: fecha, tipo: "CONSIGNACION", valor: valorConsignacion));
                Cupo += valorConsignacion;
                Saldo -= valorConsignacion;
                return $"Su Nuevo Saldo es de {Saldo:c2} pesos m/c y su cupo es de {Cupo:c2} pesos m/c";
            }
            if (valorConsignacion > Saldo)
            {
                return $"El Valor a consignar no puede ser mayor al saldo {Saldo:c2} pesos m/c";
            }
            throw new NotImplementedException();
        }
        public  override string Retirar(decimal valorRetirar, DateTime fecha)
        {
            if (valorRetirar <= 0)
            {
                return "El Valor a retirar no puede ser menor o igual a cero $0";
            }
            if (valorRetirar <= Cupo)
            {
                _movimientos.Add(new Movimiento(cuentaBancaria: this, fecha: fecha, tipo: "RETIRO", valor: valorRetirar));
                Cupo -= valorRetirar;
                Saldo += valorRetirar;
                return $"Su Nuevo Saldo es de {Saldo:c2} pesos m/c y su cupo es de {Cupo:c2} pesos m/c";
            }
            if (valorRetirar > Cupo)
            {
                return $"El Valor a retirar no puede ser mayor al cupo disponible {Cupo:c2} pesos m/c";
            }
            throw new NotImplementedException();

        }
    }
}
