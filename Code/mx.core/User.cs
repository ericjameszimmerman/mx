using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.core
{
    public class User
    {
        public User()
        {
            this.Name = string.Empty;
            this.Email = string.Empty;
        }

        public User(string name, string email)
        {
            this.Name = name;
            this.Email = email;
        }

        public string Name { get; set; }

        public string Email { get; set; }

        public string ID
        {
            get
            {
                return string.Format("{0} <{1}>", this.Name, this.Email);
            }
        }
    }
}
