using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta
{
    class Persona
    {
        public String Name  { get; set; }
        public String Surname { get; set; }
        public String Email { get; set; }

        public Persona(String name, String cognome, String email)
        {
            this.Name = name;
            this.Surname = cognome;
            this.Email = email;
        }
    }
}
