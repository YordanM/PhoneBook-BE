﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Domain.Models
{
    public class Contact
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }
    }
}
