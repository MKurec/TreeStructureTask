using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeStructure.Core.Domain
{
    public class User : Entity
    {
        private static List<string> _roles = new ()
        {
            "user", "admin"
        };

        public string Name { get; protected set; }
        public string Role { get; protected set; }
        public string Password { get; protected set; }

        protected User()
        {

        }
        public User(Guid id,  string name,  string password, string role)
        {
            Id = id;
            SetRole(role);
            SetName(name);
            SetPassword(password);
        }
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception($"User can not have an empty first name.");
            }
            Name = name;
        }

        public void SetRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                throw new Exception($"User can not have an empty role.");
            }
            role = role.ToLowerInvariant();
            if (!_roles.Contains(role))
            {
                throw new Exception($"User can not have a role: '{role}'.");
            }
            Role = role;
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception($"User can not have an empty password.");
            }
            Password = password;
        }


    }
}
