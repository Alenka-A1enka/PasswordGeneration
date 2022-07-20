using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pass_generation
{
    /// <summary>
    /// Позволяет хранить все данные, относящиеся к паролю, в объекте класса
    /// </summary>
    public class PasswordData
    {
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Hash { get; set; }
    }
}
