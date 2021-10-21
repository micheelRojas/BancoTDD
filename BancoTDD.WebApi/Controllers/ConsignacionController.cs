using BancoTDD.Aplication;
using BancoTDD.Dominio.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BancoTDD.Aplication.ConsignarCommandHandle;

namespace BancoTDD.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsignacionController : ControllerBase
    {
       
        private readonly IUnitOfWork _unitOfWork;

        public ConsignacionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        [HttpPost]
        public ConsignarResponse Post(ConsignarCommand command)
        {
            var service = new ConsignarCommandHandle(_unitOfWork);
            var response = service.Handle(command);
            return response;
        }
    }
}
