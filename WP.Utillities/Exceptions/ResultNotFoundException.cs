﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Utillities.Exceptions
{
    public class ResultNotFoundException : Exception
    {
        public ResultNotFoundException() {}
        public ResultNotFoundException(string Message) : base(Message) {}
        public ResultNotFoundException(string Message, string path) {}
       
    }
}
