using BancoTDD.Dominio.Contracts;
using System;
//using Banco.Domain.Contracts;
namespace BancoTDD.Aplication
{
    public class ConsignarCommandHandle
    {
        private readonly IUnitOfWork _unitOfWork;
        public ConsignarCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ConsignarResponse Handle(ConsignarCommand command)
        {
            _unitOfWork.BeginTransaction();
            var cuentaBancaria = _unitOfWork.CuentaBancariaRepository.Find(command.NumeroCuenta);
            if (cuentaBancaria == null)
            {
                return new ConsignarResponse() { EsValido = false, Mensaje = $"No existe cuenta bancaria con número {command.NumeroCuenta}" };
            }
            var respuesta = cuentaBancaria.Consignar(command.ValorConsignacion, command.Fecha);
            _unitOfWork.CuentaBancariaRepository.Update(cuentaBancaria);
            _unitOfWork.Commit();
            return new ConsignarResponse() { EsValido = true, Mensaje = respuesta };
        }
        public class ConsignarCommand
        {
            public string NumeroCuenta { get; set; }
            public decimal ValorConsignacion { get; set; }
            public DateTime Fecha { get; set; }
        }

        public class ConsignarResponse
        {
            public bool EsValido { get; set; }
            public string Mensaje { get; set; }
        }
    }
}
