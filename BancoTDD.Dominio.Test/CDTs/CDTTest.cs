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

        /*
         * HU 8.
            Como Usuario quiero el retiro de mi dinero de mi CDT al finalizar el Término establecido para
            recuperar el dinero depositado.
            Criterios de Aceptación
            8.1 Los retiros sólo se podrán realizar una vez haya finalizado el término del CDT.
            8.2 Al realizar el retiro se le liquidará un interés de acuerdo a la tasa definida y plazo de termino.
            8.3 El valor a retirar se reduce del saldo del CDT.
         */

        /*
         *  Dado El cliente tiene una tarjeta de CDT a termino trimestral con un saldo de 1 millon de pesos 
         Cuando va a reirar $1.014.673,85 pesos pasado los 3 meses .
         Entonces El sistema registrará el retiro se le liquidará un interés de acuerdo a la tasa definida de 3% y plazo de termino de trimestre.
         AND presentará el mensaje. “Su Nuevo Saldo es de $ 0,00 pesos m/c"
         */
        [Test]
        public void PuedeRetirarCorrecta()
        {
            var cdt = new CDT(numero: "10001", nombre: "Cuenta Ejemplo", termino: "Trimestre", tasa: 0.06);
            decimal valorConsignacion = 1000000;
            cdt.Consignar(valorConsignacion: valorConsignacion, fecha: new DateTime(2020, 2, 1));

            double valorRetiro = 1014673.85;
            string respuesta = cdt.Retirar(valorRetiro: Convert.ToDecimal(valorRetiro), fecha: new DateTime(2020, 5, 1));
            
            Assert.AreEqual("Su Nuevo Saldo es de $ 0,00 pesos m/c", respuesta);
        }
        //Solo puede retirar por el total del saldo: {SaldoTemporal:c2
        /*
         *  Dado El cliente tiene una tarjeta de CDT a termino trimestral con un saldo de 1 millon de pesos 
         Cuando va a reirar $800.000,00 pesos pasado los 3 meses .
         Entonces El sistema registrará el retiro se le liquidará un interés de acuerdo a la tasa definida de 3% y plazo de termino de trimestre.
         AND presentará el mensaje. “Solo puede retirar por el total del saldo: $ 1,014,673.85 m/c"
         */
        [Test]
        public void PuedeRetirarInCorrecta()
        {
            var cdt = new CDT(numero: "10001", nombre: "Cuenta Ejemplo", termino: "Trimestre", tasa: 0.06);
            decimal valorConsignacion = 1000000;
            cdt.Consignar(valorConsignacion: valorConsignacion, fecha: new DateTime(2020, 2, 1));

            double valorRetiro = 800000;
            string respuesta = cdt.Retirar(valorRetiro: Convert.ToDecimal(valorRetiro), fecha: new DateTime(2020, 5, 1));

            Assert.AreEqual("Solo puede retirar por el total del saldo: $ 1.014.673,85 pesos m/c", respuesta);
        }
    }
}
