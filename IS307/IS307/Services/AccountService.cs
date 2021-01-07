using IS307.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS307.Services
{
    public static class AccountService
    {
        public static bool LoginService(LoginModel loginModel)
        {
            return true;
        }


        /// <param name="registerModel"></param>
        /// <returns>
        ///  0 : Success 
        ///  1 : Username already exists
        ///  -1 : Bad request
        /// </returns>
        public static int RegisterService(RegisterModel registerModel)
        {
            return 1;
        }
    }
}
