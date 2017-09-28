using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoVirus
{
    class NanoVirus
    {
        private Cell currentCell;
        private bool MustMove;

        public Cell CurrentCell { get { return currentCell; } }


        public NanoVirus(Cell startingCell)
        {
            currentCell = startingCell;
            MustMove = false;
            GameManager.TurnEnd += TurnEndEventHandler;
        }

        private void TurnEndEventHandler(EventArgs e)
        {
            if(currentCell == null)
                MustMove = true;
            if (MustMove || currentCell.Type != Cell.CellType.Tumorous)
            {
                currentCell = GameManager.GetNanoVirusTarget();
                MustMove = false;
            }
            else
            {
                GameManager.DestroyTumor(currentCell);
                MustMove = true;
            }
        }
        

    }
}
