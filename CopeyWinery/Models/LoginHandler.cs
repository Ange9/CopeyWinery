﻿using System;
using System.Linq;
using System.Web;


namespace CopeyWinery.Models
{
    public class LoginHandler
    {
        private User objUser;
        static DB_Entities dB_Entity = new DB_Entities();


        public LoginHandler(User objUser)
        {
            this.objUser = objUser;
        }

        public User GetUser()
        {
                var user = dB_Entity.Users
                                    .Where(a => a.Name.Equals(objUser.Name) 
                                    && a.Password.Equals(objUser.Password))
                                    .FirstOrDefault();
                return user;
        }
    }
}
