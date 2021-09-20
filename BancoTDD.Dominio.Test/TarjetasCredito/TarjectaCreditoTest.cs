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
           AND presentará el mensaje. “Su Nuevo Saldo es de $30.000,00 pesos m/c y su cupo es de $ 1.020.000,00 pesos m/c”.
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
    }

    internal class TarjectaCredito
    {
        protected List<Movimiento> _movimientos;
        public string Numero { get; protected set; }
        public string Nombre { get; protected set; }
        public decimal Cupo { get; protected set; }
        public decimal Saldo { get; protected set; }
        public TarjectaCredito(string numero, string nombre, int cupo)
        {
            Numero = numero;
            Nombre = nombre;
            Cupo = cupo;
            _movimientos = new List<Movimiento>();
        }
        public IReadOnlyCollection<Movimiento> Movimientos => _movimientos.AsReadOnly();

        internal string Consignar(decimal valorConsignacion, DateTime fecha)
        {
            if (valorConsignacion <= 0) {
                return "El Valor a consignar o abonar no puede ser menor a cero $0";
            }
            if (valorConsignacion <= Saldo) {
                _movimientos.Add(new Movimiento(tarjertaCredito: this, fecha: fecha, tipo: "RETIRO", valor: valorConsignacion));
                Cupo += valorConsignacion;
                Saldo -= valorConsignacion;
                return $"Su Nuevo Saldo es de {Saldo:c2} pesos m/c y su cupo es de {Cupo:c2} pesos m/c";
            }
            throw new NotImplementedException();
        }
        internal string Retirar(decimal valorRetirar, DateTime fecha)
        {
            if (valorRetirar <= 0)
            {
                return "El Valor a consignar o abonar no puede ser menor a cero $0";
            }
            if(valorRetirar<= Saldo)
            {
                _movimientos.Add(new Movimiento(tarjertaCredito: this, fecha: fecha, tipo: "RETIRO", valor: valorRetirar));
                Cupo -= valorRetirar;
                Saldo += valorRetirar;
                return $"Su Nuevo Saldo es de {Saldo:c2} pesos m/c";
            }
            throw new NotImplementedException();

        }
    }
    
    internal class Movimiento {
        public Movimiento(TarjectaCredito tarjertaCredito, DateTime fecha, string tipo, decimal valor)
        {
            TarjectaCredito = tarjertaCredito;
            Fecha = fecha;
            Tipo = tipo;
            Valor = valor;
        }
        public TarjectaCredito TarjectaCredito { get; private set; }
        public DateTime Fecha { get; private set; }
        public string Tipo { get; private set; }
        public decimal Valor { get; private set; }
    }
}
