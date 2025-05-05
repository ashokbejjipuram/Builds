using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IMPALLibrary
{
    public class ProcParam
    {
        private string _name;
        private DbType _DbType;
        private int _size;
        private object _val;
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        public DbType dbType
        {
            get { return _DbType; }
            set { _DbType = value; }
        }

        public int size
        {
            get { return _size; }
            set { _size = value; }
        }
        public object val
        {
            get { return _val; }
            set { _val = value; }
        }

        public ProcParam(string name, DbType dbType, object val)
        {
            this.name = name;
            this.dbType = dbType;
            this.size = size;
            this.val = val;
        }
    }
}
