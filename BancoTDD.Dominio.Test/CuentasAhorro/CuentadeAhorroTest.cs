using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
namespace BancoTDD.Dominio.Test.CuentasAhorro
{
    public class CuentadeAhorroTest
    {
        /*
           Escenario: Valor de consignación -1
           H1: COMO Cajero del Banco QUIERO realizar consignaciones a una cuenta de ahorro PARA salvaguardar el dinero.
           Criterio de Aceptación:
           1.2 El valor de la consignación no puede ser menor o igual a 0.
           //El ejemplo o escenario
           Dado El cliente tiene una cuenta de ahorro 
           Número 10001, Nombre “Cuenta ejemplo”, Saldo de 0
           Cuando Va a consignar un valor -1
           Entonces El sistema presentará el mensaje. “El valor a consignar es incorrecto”
            */
        /*
         ENTITY=> CUENTA AHORRO => AGREGADO ROOT
               => MOVIMIENTOS 
         */
        [Test]
        public void NoPuedeConsignarValorDeMenosUno()
        {
            var cuentaAhorro = new CuentaAhorro(numero: "10001", nombre: "Cuenta Ejemplo", ciudad: "Bogota");
            decimal valorConsignacion = -1;
            string respuesta = cuentaAhorro.Consignar(valorConsignacion: valorConsignacion, fecha: new DateTime(2020, 2, 1), ciudad: "Bogota");
            Assert.AreEqual("El valor a consignar es incorrecto", respuesta);
        }
      

        /*
          Escenario: Consignación Inicial Correcta
            HU: Como Usuario quiero realizar consignaciones a una cuenta de ahorro para salvaguardar el 
            dinero.
            Criterio de Aceptación:
           
            1.1 La consignación inicial debe ser mayor o igual a 50 mil pesos
            1.3 El valor de la consignación se le adicionará al valor del saldo aumentará
            Dado El cliente tiene una cuenta de ahorro 
            Número 10001, Nombre “Cuenta ejemplo”, Saldo de 0
            Cuando Va a consignar el valor inicial de 50 mil pesos 
            Entonces El sistema registrará la consignación
            AND presentará el mensaje. “Su Nuevo Saldo es de $50.000,00 pesos m/c”.
         */
        [Test]
        public void PuedeHacerConsignacionInicialCorrecta()
        {
            var cuentaAhorro = new CuentaAhorro(numero: "10001", nombre: "Cuenta Ejemplo", ciudad: "Bogota");
            decimal valorConsignacion = 50000;
            string respuesta = cuentaAhorro.Consignar(valorConsignacion: valorConsignacion, fecha: new DateTime(2020, 2, 1), ciudad: "Bogota");
            Assert.AreEqual(1, cuentaAhorro.Movimientos.Count);//Criterio general
            Assert.AreEqual("Su Nuevo Saldo es de $ 50.000,00 pesos m/c", respuesta);
        }
        /*
       * Escenario: Consignación Inicial Incorrecta
         HU: Como Usuario quiero realizar consignaciones a una cuenta de ahorro para salvaguardar el
         dinero.
          Criterio de Aceptación:
          1.1 La consignación inicial debe ser mayor o igual a 50 mil pesos
          Dado El cliente tiene una cuenta de ahorro con
          Número 10001, Nombre “Cuenta ejemplo”, Saldo de 0
          Cuando Va a consignar el valor inicial de $49.950 pesos
          Entonces El sistema no registrará la consignación
          AND presentará el mensaje. “El valor mínimo de la primera consignación debe ser
          de $50.000 mil pesos. Su nuevo saldo es $0 pesos”. 
       * 
       */
        [Test]
        public void PuedeHacerConsignacionInicialInCorrecta()
        {
            var cuentaAhorro = new CuentaAhorro(numero: "10001", nombre: "Cuenta Ejemplo", ciudad: "Bogota");
            decimal valorConsignacion = 49950;
            string respuesta = cuentaAhorro.Consignar(valorConsignacion: valorConsignacion, fecha: new DateTime(2020, 2, 1), ciudad: "Bogota");
            Assert.AreEqual(0, cuentaAhorro.Movimientos.Count);//Criterio general
            Assert.AreEqual("El valor mínimo de la primera consignación debe ser de $50.000 mil pesos. Su nuevo saldo es $0 pesos", respuesta);
        }

        /*
         * Escenario: Consignación posterior a la inicial correcta
            HU: Como Usuario quiero realizar consignaciones a una cuenta de ahorro para salvaguardar el
            dinero.
            Criterio de Aceptación:
            1.3 El valor de la consignación se le adicionará al valor del saldo aumentará
            Dado El cliente tiene una cuenta de ahorro con un saldo de 30.000
            Cuando Va a consignar el valor inicial de $49.950 pesos
            Entonces El sistema registrará la consignación
            AND presentará el mensaje. “Su Nuevo Saldo es de $79.950,00 pesos m/c”
         */
        [Test]
        public void PuedeHacerConsignacionPosterioraInicialCorrecta()
        {
            var cuentaAhorro = new CuentaAhorro(numero: "10001", nombre: "Cuenta Ejemplo", ciudad: "Bogota");
            cuentaAhorro.Consignar(valorConsignacion: 50000, fecha: new DateTime(2019, 2, 1), ciudad: "Bogota");
            cuentaAhorro.Retirar(valorRetirar: 20000, fecha: new DateTime(2019, 2, 1));
            decimal valorConsignacion = 49950;
            string respuesta = cuentaAhorro.Consignar(valorConsignacion: valorConsignacion, fecha: new DateTime(2020, 2, 1), ciudad: "Bogota");
            
            Assert.AreEqual(3, cuentaAhorro.Movimientos.Count);//Criterio general
            Assert.AreEqual("Su Nuevo Saldo es de $ 79.950,00 pesos m/c", respuesta);
        }
        /*
         * 
         * Escenario: Consignación posterior a la inicial correcta
            HU: Como Usuario quiero realizar consignaciones a una cuenta de ahorro para salvaguardar el
            dinero.
            Criterio de Aceptación:
            1.4 La consignación nacional (a una cuenta de otra ciudad) tendrá un costo de $10 mil pesos.
            Dado El cliente tiene una cuenta de ahorro con un saldo de 30.000 perteneciente a una
            sucursal de la ciudad de Bogotá y se realizará una consignación desde una sucursal
            de la Valledupar.
            Cuando Va a consignar el valor inicial de $49.950 pesos.
            Entonces El sistema registrará la consignación restando el valor a consignar los 10 mil pesos.
            AND presentará el mensaje. “Su Nuevo Saldo es de $69.950,00 pesos m/c”.
         * */
        [Test]
        public void PuedeHacerConsignacionPosterioraInicialCorrectaNacional()
        {
            var cuentaAhorro = new CuentaAhorro(numero: "10001", nombre: "Cuenta Ejemplo", ciudad:"Bogota");
            cuentaAhorro.Consignar(valorConsignacion: 50000, fecha: new DateTime(2019, 2, 1), ciudad: "Bogota");
            cuentaAhorro.Retirar(valorRetirar: 20000, fecha: new DateTime(2019, 2, 1));
            decimal valorConsignacion = 49950;
            string respuesta = cuentaAhorro.Consignar(valorConsignacion: valorConsignacion, fecha: new DateTime(2020, 2, 1), ciudad: "Valledupar");
           
            Assert.AreEqual(4, cuentaAhorro.Movimientos.Count);//Criterio general
            Assert.AreEqual("Su Nuevo Saldo es de $ 69.950,00 pesos m/c", respuesta);
        }
        /*
         * HU 2.
            Como Usuario quiero realizar retiros a una cuenta de ahorro para obtener el dinero en efectivo
            Criterios de Aceptación
            2.1 El valor a retirar se debe descontar del saldo de la cuenta.
            2.2 El saldo mínimo de la cuenta deberá ser de 20 mil pesos.
            2.3 Los primeros 3 retiros del mes no tendrán costo.
            2.4 Del cuarto retiro en adelante del mes tendrán un valor de 5 mil pesos.

         */

        /*
         *        Criterio de Aceptación:
         *        2.1 El valor a retirar se debe descontar del saldo de la cuenta.
                  2.2 El saldo mínimo de la cuenta deberá ser de 20 mil pesos.
                  2.3 Los primeros 3 retiros del mes no tendrán costo.
            Dado El cliente tiene una cuenta de ahorro con un saldo de 50.000 perteneciente a una
            sucursal de la ciudad de Bogotá y se realiza un retiro.
            Cuando Va a retirat  el valor de $20.000,00 pesos.
            Entonces El sistema registrará el retiro restando el valor del retiro al saldo.
            AND presentará el mensaje. “Su Nuevo Saldo es de $30.000,00 pesos m/c”.
         */
        
        [Test]
        public void PuedeHacerRetiroCorrecto()
        {
            var cuentaAhorro = new CuentaAhorro(numero: "10001", nombre: "Cuenta Ejemplo", ciudad: "Bogota");
            cuentaAhorro.Consignar(valorConsignacion: 50000, fecha: new DateTime(2019, 2, 1), ciudad: "Bogota");
            
            decimal valorRetirar = 20000;
            string respuesta = cuentaAhorro.Retirar(valorRetirar: valorRetirar, fecha: new DateTime(2019, 2, 1));

            Assert.AreEqual(2, cuentaAhorro.Movimientos.Count);//Criterio general
            Assert.AreEqual("Su Nuevo Saldo es de $ 30.000,00 pesos m/c", respuesta);
        }
        /*
        *        Criterio de Aceptación:
        *        2.1 El valor a retirar se debe descontar del saldo de la cuenta.
                 2.2 El saldo mínimo de la cuenta deberá ser de 20 mil pesos.
           Dado El cliente tiene una cuenta de ahorro con un saldo de 50,000 perteneciente a una
           sucursal de la ciudad de Bogotá y se realiza un retiro.
           Cuando Va a retirat  el valor de $30.050,00 pesos.
           Entonces El sistema  no registrará el retiro.
           AND presentará el mensaje. “El Saldo de la cuenta es inferior a $20.000,00 m/c”.
        */
        
        [Test]
        public void PuedeHacerRetiroInCorrecto()
        {
            var cuentaAhorro = new CuentaAhorro(numero: "10001", nombre: "Cuenta Ejemplo", ciudad: "Bogota");
            cuentaAhorro.Consignar(valorConsignacion: 50000, fecha: new DateTime(2019, 2, 1), ciudad: "Bogota");
            
            decimal valorRetirar = 30050;
            string respuesta = cuentaAhorro.Retirar(valorRetirar: valorRetirar, fecha: new DateTime(2019, 2, 1));

            Assert.AreEqual(1, cuentaAhorro.Movimientos.Count);//Criterio general
            Assert.AreEqual("El Saldo de la cuenta es inferior a $20.000,00 m/c", respuesta);
        }


        /* 2.1 El valor a retirar se debe descontar del saldo de la cuenta.
            2.2 El saldo mínimo de la cuenta deberá ser de 20 mil pesos.
         *  2.4 Del cuarto retiro en adelante del mes tendrán un valor de 5 mil pesos.
         *   Dado El cliente tiene una cuenta de ahorro con un saldo de 50.000 perteneciente a una
           sucursal de la ciudad de Bogotá y se realiza un retiro.
           Cuando Va a retirat  el valor de $20.000,00 pesos por cuarta ves.
           Entonces El sistema  registrará el retiro restando el valor del retiro al saldo menos los 5000 pesos.
           AND presentará el mensaje. “Su Nuevo Saldo es de $25.000,00 pesos m/c”.
       
         */
        [Test]
        public void PuedeHacerRetiroCuartoRetirodelMesCorrecto()
        {
            var cuentaAhorro = new CuentaAhorro(numero: "10001", nombre: "Cuenta Ejemplo", ciudad: "Bogota");
            cuentaAhorro.Consignar(valorConsignacion: 100000, fecha: new DateTime(2019, 2, 1), ciudad: "Bogota");
            cuentaAhorro.Retirar(valorRetirar: 20000, fecha: new DateTime(2019, 2, 1));
            cuentaAhorro.Retirar(valorRetirar: 15000, fecha: new DateTime(2019, 2, 1));
            cuentaAhorro.Retirar(valorRetirar: 15000, fecha: new DateTime(2019, 2, 1));

            decimal valorRetirar = 20000;
            string respuesta = cuentaAhorro.Retirar(valorRetirar: valorRetirar, fecha: new DateTime(2019, 2, 1));

            Assert.AreEqual(6, cuentaAhorro.Movimientos.Count);//Criterio general
            Assert.AreEqual("Su Nuevo Saldo es de $ 25.000,00 pesos m/c", respuesta);
        }


    }
  

}
