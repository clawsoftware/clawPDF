using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemWrapper.Tests
{
    [AttributeUsage(AttributeTargets.All)]
    class AuthorAttribute : Attribute
    {
        public AuthorAttribute(string author, string email)
        {
            this.Author = author;
            this.Email = email;
        }

        public string Author { get; set; }

        public string Email { get; set; }
    }
}
