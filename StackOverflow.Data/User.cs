using System;
using System.Collections.Generic;

namespace StackOverflow.Data
{
    public class User
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public List<Question> questions { get; set; }
    }
}
