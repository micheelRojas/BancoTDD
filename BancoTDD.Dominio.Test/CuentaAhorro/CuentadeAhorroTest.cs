using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BancoTDD.Dominio.Test.CuentaAhorro
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
            cuentaAhorro.Retirar(valorRetirar: 20000, fecha: new DateTime(2019, 2, 1), ciudad: "Bogota");
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
            cuentaAhorro.Retirar(valorRetirar: 20000, fecha: new DateTime(2019, 2, 1), ciudad: "Bogota");
            decimal valorConsignacion = 49950;
            string respuesta = cuentaAhorro.Consignar(valorConsignacion: valorConsignacion, fecha: new DateTime(2020, 2, 1), ciudad: "Valledupar");
           
            Assert.AreEqual(3, cuentaAhorro.Movimientos.Count);//Criterio general
            Assert.AreEqual("Su Nuevo Saldo es de $ 69.950,00 pesos m/c", respuesta);
        }
    }
    //Agregado
    internal class CuentaAhorro
    {
        public string Numero { get; private set; }//encapsulamiento // guardar la integridad
        public string Nombre { get; private set; }
        public decimal Saldo { get; private set; }
        public string Ciudad { get; private set; }

        private List<Movimiento> _movimientos;

        public IReadOnlyCollection<Movimiento> Movimientos => _movimientos.AsReadOnly();

        public CuentaAhorro(string numero, string nombre, string ciudad)
        {
            Numero = numero;
            Nombre = nombre;
            Ciudad = ciudad;
            _movimientos = new List<Movimiento>();
        }

        internal string Consignar(decimal valorConsignacion, DateTime fecha, string ciudad)
        {
            if (valorConsignacion < 0)
            {
                return "El valor a consignar es incorrecto";
            }
            if (!_movimientos.Any() && valorConsignacion >= 50000 && Ciudad.Equals(ciudad))
            {
                _movimientos.Add(new Movimiento(cuentaAhorro: this, fecha: fecha, tipo: "CONSIGNACION", valor: valorConsignacion));
                Saldo += valorConsignacion;

                return $"Su Nuevo Saldo es de {Saldo:c2} pesos m/c";
            }
            if (!_movimientos.Any() && valorConsignacion < 50000 && Ciudad.Equals(ciudad)) {
                return "El valor mínimo de la primera consignación debe ser de $50.000 mil pesos. Su nuevo saldo es $0 pesos";
            }
            if (_movimientos.Any() && Ciudad.Equals(ciudad))
            {
                
                _movimientos.Add(new Movimiento(cuentaAhorro: this, fecha: fecha, tipo: "CONSIGNACION", valor: valorConsignacion));
                Saldo += valorConsignacion;
                return $"Su Nuevo Saldo es de {Saldo:c2} pesos m/c";
            }
            if (_movimientos.Any() && !Ciudad.Equals(ciudad))
            {
                decimal costoNacional = 10000;
               
                _movimientos.Add(new Movimiento(cuentaAhorro: this, fecha: fecha, tipo: "CONSIGNACION", valor: valorConsignacion));
                Saldo += (valorConsignacion-costoNacional);
                return $"Su Nuevo Saldo es de {Saldo:c2} pesos m/c";
            }
            throw new NotImplementedException();
        }
        internal string Retirar(decimal valorRetirar, DateTime fecha, string ciudad)
        {
            if (valorRetirar < 0)
            {
                return "El valor a retirar es incorrecto";
            }
           
                _movimientos.Add(new Movimiento(cuentaAhorro: this, fecha: fecha, tipo: "RETIRO", valor: valorRetirar));
                Saldo -= valorRetirar;
                return $"Su Nuevo Saldo es de {Saldo:c2} pesos m/c";

        }
    }

    internal class Movimiento
    {
        public Movimiento(CuentaAhorro cuentaAhorro, DateTime fecha, string tipo, decimal valor)
        {
            CuentaAhorro = cuentaAhorro;
            Fecha = fecha;
            Tipo = tipo;
            Valor = valor;
        }

        public CuentaAhorro CuentaAhorro { get; private set; }
        public DateTime Fecha { get; private set; }
        public string Tipo { get; private set; }
        public decimal Valor { get; private set; }
      
    }

}
