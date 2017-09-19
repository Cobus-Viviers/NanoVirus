using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoVirus
{
    public delegate void TurnEndEventHandler(EventArgs e);

    static class GameManager
    {
        private const int NUMBER_OF_CELLS = 100;
        private static Random rng;

        static List<Cell> RedBloodCells;
        static List<Cell> WhiteBloodCells;
        static List<Cell> TumorousCells;
        private static int DestroyedCellCount;

        public static NanoVirus Virus;

        public static event TurnEndEventHandler TurnEnd;
        public static void OnTurnEnd()
        {
            if (TurnEnd != null)
                TurnEnd(EventArgs.Empty);
        }

        static GameManager()
        {
            RedBloodCells = new List<Cell>();
            WhiteBloodCells = new List<Cell>();
            TumorousCells = new List<Cell>();
            rng = new Random();
        }

        public static void StartGame()
        {
            for (int i = 0; i < NUMBER_OF_CELLS; i++)
            {
                Cell cell = new Cell();
                switch (cell.Type)
                {
                    case Cell.CellType.Red: RedBloodCells.Add(cell);
                        break;
                    case Cell.CellType.White: WhiteBloodCells.Add(cell);
                        break;
                    case Cell.CellType.Tumorous: TumorousCells.Add(cell);
                        break;
                }
            }
            Virus = new NanoVirus(RedBloodCells[rng.Next(RedBloodCells.Count)]);
            DestroyedCellCount = 0;
            PlayGame();
        }
        
        private static Cell FindClosest(List<Cell> CellList, Cell SourceCell)
        {
                Cell target = CellList[0];
                double minDistance = DistanceCalculator.CalculateDistance(target, SourceCell);
                foreach (Cell cell in CellList)
                {
                    double thisDistance = DistanceCalculator.CalculateDistance(SourceCell, cell);
                    if (thisDistance < minDistance)
                    {
                        minDistance = thisDistance;
                        target = cell;
                    }
                }
                return target;
        }

        public static void InfectCell(Cell infectionSource)
        {
            Cell target = null;
            if (RedBloodCells.Count > 0)
            {
                target = FindClosest(RedBloodCells, infectionSource);
                RedBloodCells.Remove(target);
            }
            else if(WhiteBloodCells.Count > 0)
            {
                target = FindClosest(WhiteBloodCells, infectionSource);
                WhiteBloodCells.Remove(target);
            }
            if(target != null)
            {
                target.Infect();
                TumorousCells.Add(target);
            }
        }

        public static void DestroyTumor(Cell tumorCell)
        {
            if (tumorCell.Type != Cell.CellType.Tumorous)
                return;
            TumorousCells.Remove(tumorCell);
            DestroyedCellCount++;
        }

        public static Cell GetNanoVirusTarget()
        {
            if (WhiteBloodCells.Count == 0 && RedBloodCells.Count == 0)
                return null;

            int CellNumber = rng.Next(NUMBER_OF_CELLS - DestroyedCellCount);
            Cell cell = null;

            if (CellNumber < RedBloodCells.Count)
            {
                cell = RedBloodCells[CellNumber];
            }
            else if(CellNumber < WhiteBloodCells.Count + RedBloodCells.Count)
            {
                cell = WhiteBloodCells[CellNumber - RedBloodCells.Count];
            }
            else
            {
                CellNumber = CellNumber - WhiteBloodCells.Count - RedBloodCells.Count;
                cell = TumorousCells[CellNumber];
            }

            if (DistanceCalculator.CalculateDistance(Virus.CurrentCell, cell) > 5000)
                return GetNanoVirusTarget();
            else
                return cell;
        }

        private static void PlayGame()
        {
            bool IsRunning = true;
            int Round = 0;
            TextWriter Writer = new TextWriter("GameState.txt");
            do
            {
                if (WhiteBloodCells.Count == 0 && RedBloodCells.Count == 0)
                    IsRunning = false;

                Round++;
                foreach (string line in GetGameState(Round))
                    Console.WriteLine(line);

                OnTurnEnd();

                try
                {
                    Writer.WriteToFile(GetGameState(Round));
                }
                catch (Exception)
                {
                    throw new StateSaveException("Could not save the game state!");
                }

            } while (IsRunning);
        }

        private static string[] GetGameState(int round)
        {
            string[] state = new string[5];
            state[0] = "==================Round"+round+ "==================";
            state[1] = "Redblood Cells: " + RedBloodCells.Count;
            state[2] = "Whiteblood Cells: " + WhiteBloodCells.Count;
            state[3] = "Tumor Cells: "+ TumorousCells.Count;
            state[4] = "Destroyed: " + 
                (NUMBER_OF_CELLS - RedBloodCells.Count - WhiteBloodCells.Count - TumorousCells.Count);
            return state;
        }
       
    }
}
