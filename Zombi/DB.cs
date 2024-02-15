using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombi
{
    public static class DB
    {
        private static User16Context _context;
        public static User16Context Instance { get
            {
                if( _context == null )
                    _context = new User16Context();
                return _context;
            } }
    }
}
