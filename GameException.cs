using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoVirus
{
    class GameException : Exception
    {
        public GameException(string message) : base(message) { }
    }

    class StateSaveException : GameException
    {
        public StateSaveException(string message) : base(message) { }
    } 
}
