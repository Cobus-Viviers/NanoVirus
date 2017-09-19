using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoVirus
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                GameManager.StartGame();
                Console.ReadKey();
            }
            catch (GameException e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
