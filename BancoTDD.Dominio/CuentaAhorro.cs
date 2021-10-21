using BancoTDD.Dominio.CuentasBancarias;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BancoTDD.Dominio
{
    public class CuentaAhorro:CuentaBancaria
    {
        public string Ciudad { get; private set; }
        public CuentaAhorro(string numero, string nombre, string ciudad) : base(numero, nombre,50000)
        {
            Ciudad = ciudad;
        }
        
        public  string Consignar(decimal valorConsignacion, DateTime fecha, string ciudad)
        {
            if (valorConsignacion < 0)
            {
                return "El valor a consignar es incorrecto";
            }
            if (!_movimientos.Any() && valorConsignacion >= 50000 && Ciudad.Equals(ciudad))
            {
                _movimientos.Add(new Movimiento(cuentaBancaria: this, fecha: fecha, tipo: "CONSIGNACION", valor: valorConsignacion));
                Saldo += valorConsignacion;

                return $"Su Nuevo Saldo es de {Saldo:c2} pesos m/c";
            }
            if (!_movimientos.Any() && valorConsignacion < 50000 && Ciudad.Equals(ciudad))
            {
                return "El valor mínimo de la primera consignación debe ser de $50.000 mil pesos. Su nuevo saldo es $0 pesos";
            }
            if (_movimientos.Any() && Ciudad.Equals(ciudad))
            {
                AddMovimientoAumenteSaldo(valorConsignacion, fecha, "CONSIGNACION");
                return $"Su Nuevo Saldo es de {Saldo:c2} pesos m/c";
            }
            if (_movimientos.Any() && !Ciudad.Equals(ciudad))
            {
                decimal costoNacional = 10000;
                AddMovimientoAumenteSaldo(valorConsignacion, fecha, "CONSIGNACION");
                AddMovimientoDisminuyeSaldo(costoNacional, fecha, "COSTOCONSIGNACION");
                return $"Su Nuevo Saldo es de {Saldo:c2} pesos m/c";
            }
            throw new NotImplementedException();
        }

       
        public override string Retirar(decimal valorRetirar, DateTime fecha)
        {
            if (valorRetirar < 0)
            {
                return "El valor a retirar es incorrecto";
            }
            decimal SaldoTemporal = Saldo - valorRetirar;
            if (SaldoTemporal < 20000)
            {
                return "El Saldo de la cuenta es inferior a $20.000,00 m/c";

            }
            
            if (SaldoTemporal >= 20000 && Saldo > valorRetirar && CantidaMovientosMes(this._movimientos, fecha) < 3)
            {
                AddMovimientoDisminuyeSaldo(valorRetirar, fecha, "RETIRO");
                
                return $"Su Nuevo Saldo es de {Saldo:c2} pesos m/c";
            }
            if (SaldoTemporal >= 25000 && CantidaMovientosMes(this._movimientos, fecha) >= 3)
            {
                AddMovimientoDisminuyeSaldo(valorRetirar, fecha, "RETIRO");
                AddMovimientoDisminuyeSaldo(5000, fecha, "COSTORETIRO");
                return $"Su Nuevo Saldo es de {Saldo:c2} pesos m/c";

            }


            throw new NotImplementedException();

        }
        private int CantidaMovientosMes(List<Movimiento> movimiento, DateTime fecha)
        {
            int cont = 0;

            for (int i = 0; i < movimiento.LongCount(); i++)
            {
                if (movimiento[i].Fecha.Year == fecha.Year && movimiento[i].Fecha.Month == fecha.Month && movimiento[i].Tipo.Equals("RETIRO"))
                    cont++;
            }
            return cont;
        }
    }
}
