using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pass_generation.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class PassGenerationController : ControllerBase
    {
        private readonly ILogger<PassGenerationController> _logger;

        public PassGenerationController(ILogger<PassGenerationController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Позволяет получить по заданным параметрам (длина пароля и длина хеша)
        /// выходные данные: пароль, соль и хеш
        /// </summary>
        /// <remarks>
        /// Длина пароля 6-8 символов, кратная 2
        /// Длина соли не может быть меньше одного символа
        /// </remarks>
        /// <param name="pass"></param>
        /// <param name="salt"></param>
        /// <returns></returns>   
        [HttpGet]
        public PasswordData Get(int pass, int salt)
        {
            //создание объекта класса для хранения параметров
            PasswordData data = new PasswordData();

            //обращение к методу для генерации параметров для вышесозданного объекта
            GetPasswordData.Get(pass, salt, data);

            return data;
        }
    }
}
