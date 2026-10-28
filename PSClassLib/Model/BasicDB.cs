using System;
using System.Collections.Generic;
using System.Text;

namespace PSClassLib.Model
{
    public class BasicDB
    {

        protected readonly PSDBConnection _db;

        public PSDBConnection Db { get; }

        public BasicDB(PSDBConnection db)
        {
            _db = db;
        }
    }
}
