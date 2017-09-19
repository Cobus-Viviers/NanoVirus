using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoVirus
{
    class Cell
    {
        public enum CellType
        {
            White,
            Red,
            Tumorous
        }
        private static Random rng = new Random();
        private int infectionCounter;
        private int x;
        private int y;
        private int z;
        private CellType type;
        

        public int X { get { return x; } }
        public int Y { get { return y; } }
        public int Z { get { return z; } }
        public CellType Type { get { return type; } }

        public Cell()
        {
            GameManager.TurnEnd += OnTurnEndEventHandler;
            infectionCounter = 0;
            x = rng.Next(0, 5000);
            y = rng.Next(0, 5000);
            z = rng.Next(0, 5000);
            

            int cellType = rng.Next(1, 101);

            if (cellType <= 5)
                type = CellType.Tumorous;
            else if (cellType <= 30)
                type = CellType.White;
            else
                type = CellType.Red;
        }

        public void Infect()
        {
            type = CellType.Tumorous;
        }

        public override string ToString()
        {
            return string.Format("x: {0}, y: {1}, z: {2}, type: {3}, counter: {4}",
                x, y, z, type, infectionCounter);
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Cell))
                return false;
            obj = (Cell)obj;
            return obj.ToString() == ToString();
        }
        public override int GetHashCode()
        {
            return x | y | z | (int)type;
        }

        private void OnTurnEndEventHandler(EventArgs e)
        {
            if(type == CellType.Tumorous && ++infectionCounter == 5)
            {
                infectionCounter = 0;
                GameManager.InfectCell(this);
            }
        }
    }
}
