using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BancoTDD.Dominio.Test.CDTs;


namespace BancoTDD.Dominio.Test.CDTs
{
    class CDTTest
    {
        /*
         * 
         * HU 7.
        Como Usuario quiero realizar consignar mi dinero a mi CDT para ahorrar el dinero sin tener
        acceso al de acuerdo al término definido.
        Criterios de Aceptación
        7.1 El valor de consignación inicial debe ser de mínimo 1 millón de pesos.
        7.2 Sólo se podrá realizar una sola consignación. 
         */
        [Test]
        public void NoPuedeConsignarValorDeMenosUnMillos()
        {
            var cdt = new CDT(numero: "10001", nombre: "Cuenta Ejemplo",termino:"Trimestre",tasa:0.06);
            decimal valorConsignacion = 99999;
            string respuesta = cdt.Consignar(valorConsignacion: valorConsignacion, fecha: new DateTime(2020, 2, 1));
            Assert.AreEqual("Error, la consignacion minima debe ser de $1.000.000,00 pesos m/c", respuesta);
        }
        /*7.1 El valor de consignación inicial debe ser de mínimo 1 millón de pesos.
        7.2 Sólo se podrá realizar una sola consignación.*/
        [Test]
        public void PuedeConsignarCorrecta()
        {
            var cdt = new CDT(numero: "10001", nombre: "Cuenta Ejemplo", termino: "Trimestre", tasa: 0.06);
            decimal valorConsignacion = 1000000;
            string respuesta = cdt.Consignar(valorConsignacion: valorConsignacion, fecha: new DateTime(2020, 2, 1));
            Assert.AreEqual("Su Nuevo Saldo es de $ 1.000.000,00 pesos m/c", respuesta);
        }

       
    }

    internal class CDT
    {

        protected List<Movimiento> _movimientos;
        public string Numero { get; protected set; }
        public string Nombre { get; protected set; }
        public string Termino { get; protected set; }
        public double Tasa { get; protected set; }
        public decimal Saldo { get; protected set; }
        public CDT(string numero, string nombre, string termino, double tasa)
        {
            Numero = numero;
            Nombre = nombre;
            Termino = termino;
            Tasa = tasa;
            _movimientos = new List<Movimiento>();
        }
        public IReadOnlyCollection<Movimiento> Movimientos => _movimientos.AsReadOnly();

        internal string Consignar(decimal valorConsignacion, DateTime fecha)
        {

            if (valorConsignacion < 1000000) {
                return "Error, la consignacion minima debe ser de $1.000.000,00 pesos m/c";
            }
            if (!_movimientos.Any() || MovimientoAnterior(this._movimientos)==null) {
                _movimientos.Add(new Movimiento(cdt: this, fecha: fecha, tipo: "CONSIGNACION", valor: valorConsignacion));
                Saldo += valorConsignacion;
                return $"Su Nuevo Saldo es de {Saldo:c2} pesos m/c";
            }
           

            throw new NotImplementedException();
        }
        internal static string DifenciaMeses(DateTime fechaFin, DateTime fechaInicio) {
            decimal diferencia = Math.Abs((fechaFin.Month - fechaInicio.Month) + 12 * (fechaFin.Year - fechaInicio.Year));
            if (diferencia >= 12) {
                return "Anual";
            }
            else if (diferencia >= 6) {
                return "Semestre";
            }
            else if (diferencia >= 3) {
                return "Trimestre"; 
            }
                return " " ;
        }
        internal Movimiento MovimientoAnterior(List<Movimiento> movimiento)
        {

            int cont = 0;
            Movimiento ultimoMovimiento= null;

            for (int i = 0; i < movimiento.LongCount(); i++)
            {
                if (movimiento[i].Tipo.Equals("Consignacion"))
                    ultimoMovimiento=movimiento[i];
            }
            return ultimoMovimiento;
        }
    }
    internal class Movimiento
    {
        public Movimiento(CDT cdt, DateTime fecha, string tipo, decimal valor)
        {
            CDT = cdt;
            Fecha = fecha;
            Tipo = tipo;
            Valor = valor;
        }
        public CDT CDT{ get; private set; }
        public DateTime Fecha { get; private set; }
        public string Tipo { get; private set; }
        public decimal Valor { get; private set; }
    }
}
