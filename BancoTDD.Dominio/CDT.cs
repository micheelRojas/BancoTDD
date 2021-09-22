using BancoTDD.Dominio.CuentasBancarias;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BancoTDD.Dominio
{
    public class CDT : CuentaBancariaBase
    {


        public string Termino { get; protected set; }
        public double Tasa { get; protected set; }
        public CDT(string numero, string nombre, string termino, double tasa) : base(numero, nombre)
        {
            Termino = termino;
            Tasa = tasa;
        }


        public override string Consignar(decimal valorConsignacion, DateTime fecha)
        {

            if (valorConsignacion < 1000000)
            {
                return "Error, la consignacion minima debe ser de $1.000.000,00 pesos m/c";
            }
            if (!_movimientos.Any() || MovimientoAnteriorConsignacion(this._movimientos) == null)
            {
                _movimientos.Add(new Movimiento(cuentaBancaria: this, fecha: fecha, tipo: "CONSIGNACION", valor: valorConsignacion));
                Saldo += valorConsignacion;
                return $"Su Nuevo Saldo es de {Saldo:c2} pesos m/c";
            }


            throw new NotImplementedException();
        }
        public override string Retirar(decimal valorRetiro, DateTime fecha)
        {
            decimal SaldoTemporal = Math.Round(((Convert.ToDecimal(CalcularTermino(this.Termino, this.Tasa)) + 1) * Saldo), 2);
            if (DifenciaMeses(fecha, MovimientoAnteriorConsignacion(this._movimientos).Fecha) == this.Termino && valorRetiro.Equals(SaldoTemporal) && MovimientoAnteriorRetiro(this._movimientos) == null)
            {
                _movimientos.Add(new Movimiento(cuentaBancaria: this, fecha: fecha, tipo: "RETIRO", valor: valorRetiro));
                Saldo = SaldoTemporal - valorRetiro;
                return $"Su Nuevo Saldo es de {Saldo:c2} pesos m/c";
            }
            return $"Solo puede retirar por el total del saldo: {SaldoTemporal:c2} pesos m/c";

        }
        private static string DifenciaMeses(DateTime fechaFin, DateTime fechaInicio)
        {
            decimal diferencia = Math.Abs((fechaFin.Month - fechaInicio.Month) + 12 * (fechaFin.Year - fechaInicio.Year));
            if (diferencia >= 12)
            {
                return "Anual";
            }
            else if (diferencia >= 6)
            {
                return "Semestre";
            }
            else if (diferencia >= 3)
            {
                return "Trimestre";
            }
            else if (diferencia > 1)
            {
                return "Mensual";
            }
            return " ";
        }
        private Movimiento MovimientoAnteriorConsignacion(List<Movimiento> movimiento)
        {
            Movimiento ultimoMovimiento = null;

            for (int i = 0; i < movimiento.LongCount(); i++)
            {
                if (movimiento[i].Tipo.Equals("CONSIGNACION"))
                    ultimoMovimiento = movimiento[i];
            }
            return ultimoMovimiento;
        }
        private Movimiento MovimientoAnteriorRetiro(List<Movimiento> movimiento)
        {
            Movimiento ultimoMovimiento = null;

            for (int i = 0; i < movimiento.LongCount(); i++)
            {
                if (movimiento[i].Tipo.Equals("RETIRO"))
                    ultimoMovimiento = movimiento[i];
            }
            return ultimoMovimiento;
        }
        private double CalcularTermino(string termino, double tasa)
        {
            double Te = 0.0;
            if (termino.Equals("Mensual"))
            {
                Te = (Math.Pow((1 + tasa), 0.0833333333333333)) - 1;
            }
            if (termino.Equals("Trimestre"))
            {
                Te = (Math.Pow((1 + tasa), 0.25)) - 1;
            }
            if (termino.Equals("Semestre"))
            {
                Te = (Math.Pow((1 + tasa), 0.5)) - 1;
            }
            if (termino.Equals("Anual"))
            {
                Te = (Math.Pow((1 + tasa), 1)) - 1;
            }
            return Te;
        }

    }
}
