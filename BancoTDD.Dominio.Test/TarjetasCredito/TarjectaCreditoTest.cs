using BancoTDD.Dominio.CuentasBancarias;
using BancoTDD.Dominio.Test.TarjetasCredito;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTDD.Dominio.Test.TarjetasCredito
{
    class TarjectaCreditoTest
    {

        /*
         * HU 5.
            Como Usuario quiero realizar consignaciones (abonos) a una Tarjeta Crédito para abonar al saldo
            del servicio.
            Criterios de Aceptación
            5.1 El valor a abono no puede ser menor o igual a 0.
            5.2 El abono podrá ser máximo el valor del saldo de la tarjeta de crédito.
            5.3 Al realizar un abono el cupo disponible aumentará con el mismo valor que el valor del abono
            y reducirá de manera equivalente el saldo.  
         */
         //5.1 El valor a abono no puede ser menor o igual a 0.
        [Test]
        public void PuedeHacerConsignacionCero()
        {
            var tarjetaCredito = new TarjectaCredito(numero: "10001", nombre: "Cuenta Ejemplo",cupo:1000000);

            decimal valorConsignacion = 0;
            string respuesta = tarjetaCredito.Consignar(valorConsignacion: valorConsignacion, fecha: new DateTime(2020, 2, 1));
            
            Assert.AreEqual(0, tarjetaCredito.Movimientos.Count);//Criterio general
            Assert.AreEqual("El Valor a consignar o abonar no puede ser menor a cero $0", respuesta);
        }
        /*
         *  5.2 El abono podrá ser máximo el valor del saldo de la tarjeta de crédito.
            5.3 Al realizar un abono el cupo disponible aumentará con el mismo valor que el valor del abono
            y reducirá de manera equivalente el saldo. 

           Dado El cliente tiene una tarjeta de credito con un saldo de 50.000  y un cupo de 1.000.000.
           Cuando va a consignar $20.000,00 pesos.
           Entonces El sistema  registrará el abono,  restando el valor del abono al saldo y aumentando el cupo.
           AND presentará el mensaje. “Su Nuevo Saldo es de $30.000,00 pesos m/c y su cupo es de $ 970.000,00  pesos m/c”.
         */
        [Test]
        public void PuedeHacerConsignacionCorrecta()
        {
            var tarjetaCredito = new TarjectaCredito(numero: "10001", nombre: "Cuenta Ejemplo", cupo: 1000000);
            tarjetaCredito.Retirar(valorRetirar: 50000, fecha: new DateTime(2020, 2, 1));


            decimal valorConsignacion = 20000;
            string respuesta = tarjetaCredito.Consignar(valorConsignacion: valorConsignacion, fecha: new DateTime(2020, 2, 1));

            Assert.AreEqual(2, tarjetaCredito.Movimientos.Count);//Criterio general
            Assert.AreEqual("Su Nuevo Saldo es de $ 30.000,00 pesos m/c y su cupo es de $ 970.000,00 pesos m/c", respuesta);
        }
        /*
             *  5.2 El abono podrá ser máximo el valor del saldo de la tarjeta de crédito.
                5.3 Al realizar un abono el cupo disponible aumentará con el mismo valor que el valor del abono
                y reducirá de manera equivalente el saldo. 

               Dado El cliente tiene una tarjeta de credito con un saldo de 50.000  y un cupo de 1.000.000.
               Cuando va a consignar $50.050,00 pesos.
               Entonces El sistema no registrará el abono.
               AND presentará el mensaje. “El Valor a consignar no puede ser mayor al saldo $ 50.000,00 pesos m/c”.
             */
        [Test]
        public void PuedeHacerConsignacionInCorrecta()
        {
            var tarjetaCredito = new TarjectaCredito(numero: "10001", nombre: "Cuenta Ejemplo", cupo: 1000000);
            tarjetaCredito.Retirar(valorRetirar: 50000, fecha: new DateTime(2020, 2, 1));


            decimal valorConsignacion = 50050;
            string respuesta = tarjetaCredito.Consignar(valorConsignacion: valorConsignacion, fecha: new DateTime(2020, 2, 1));

            Assert.AreEqual(1, tarjetaCredito.Movimientos.Count);//Criterio general
            Assert.AreEqual("El Valor a consignar no puede ser mayor al saldo $ 50.000,00 pesos m/c", respuesta);
        }

        /*
         *HU 6.
            Como Usuario quiero realizar retiros (avances) a una cuenta de crédito para retirar dinero en
            forma de avances del servicio de crédito.
            Criterios de Aceptación
            6.1 El valor del avance debe ser mayor a 0
            6.2 Al realizar un avance se debe reducir el valor disponible del cupo con el valor del avance.
            6.3 Un avance no podrá ser mayor al valor disponible del cupo.
         * 
         */
        /*
         * 6.1 El valor del avance debe ser mayor a 0. 
         */
        [Test]
        public void PuedeHacerRetiroCero()
        {
            var tarjetaCredito = new TarjectaCredito(numero: "10001", nombre: "Cuenta Ejemplo", cupo: 1000000);

            decimal valorRetiro = 0;
            string respuesta = tarjetaCredito.Retirar(valorRetirar: valorRetiro, fecha: new DateTime(2020, 2, 1));

            Assert.AreEqual(0, tarjetaCredito.Movimientos.Count);//Criterio general
            Assert.AreEqual("El Valor a retirar no puede ser menor o igual a cero $0", respuesta);
        }
        /*
         * 6.2 Al realizar un avance se debe reducir el valor disponible del cupo con el valor del avance.
           6.3 Un avance no podrá ser mayor al valor disponible del cupo
           Dado El cliente tiene una tarjeta de credito con un cupo de 1.000.000.
           Cuando va a reirar $100.000,00 pesos.
           Entonces El sistema  registrará el retiro,  restando el valor del retiro  al cupo y aumentando el saldo en la misma cantidad del retiro.
           AND presentará el mensaje. “Su Nuevo Saldo es de $100.000,00 pesos m/c y su cupo es de $ 900.000,00 pesos m/c"
         */
        [Test]
        public void PuedeHacerRetiroCorrecto()
        {
            var tarjetaCredito = new TarjectaCredito(numero: "10001", nombre: "Cuenta Ejemplo", cupo: 1000000);

            decimal valorRetiro = 100000;
            string respuesta = tarjetaCredito.Retirar(valorRetirar: valorRetiro, fecha: new DateTime(2020, 2, 1));

            Assert.AreEqual(1, tarjetaCredito.Movimientos.Count);//Criterio general
            Assert.AreEqual("Su Nuevo Saldo es de $ 100.000,00 pesos m/c y su cupo es de $ 900.000,00 pesos m/c", respuesta);
        }
        /*
       * 6.2 Al realizar un avance se debe reducir el valor disponible del cupo con el valor del avance.
         6.3 Un avance no podrá ser mayor al valor disponible del cupo
         Dado El cliente tiene una tarjeta de credito con un cupo de 1.000.000.
         Cuando va a reirar $1.000.050,00 pesos.
         Entonces El sistema no registrará el retiro.
         AND presentará el mensaje. “El Valor a retirar no puede ser mayor al cupo disponible $1.000.000,00 pesos m/c"
       */
        [Test]
        public void PuedeHacerRetiroInCorrecto()
        {
            var tarjetaCredito = new TarjectaCredito(numero: "10001", nombre: "Cuenta Ejemplo", cupo: 1000000);

            decimal valorRetiro = 1000050;
            string respuesta = tarjetaCredito.Retirar(valorRetirar: valorRetiro, fecha: new DateTime(2020, 2, 1));

            Assert.AreEqual(0, tarjetaCredito.Movimientos.Count);//Criterio general
            Assert.AreEqual("El Valor a retirar no puede ser mayor al cupo disponible $ 1.000.000,00 pesos m/c", respuesta);
        }
    }
}
