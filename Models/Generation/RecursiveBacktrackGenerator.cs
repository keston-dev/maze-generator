

using MazeGame.Models.Structure;

namespace MazeGame.Models.Generation
{
    public class RecursiveBacktrackGenerator : IMazeGenerator
    {
        public void Generate(Maze maze)
        {
            for(int r = 0; r < maze.Rows; r++)
            {
                for(int c = 0; c < maze.Columns; c++)
                {
                    maze.SetCell(r, c, MazeCell.Wall);
                }
            }
            List<(int, int)> directions = [(-2, 0), (2, 0), (0, -2), (0, 2)];

            Random rng = new();
            int randRow = rng.Next(0, maze.Rows);
            int randCol = rng.Next(0, maze.Columns);

            maze.SetCell(randRow, randCol, MazeCell.Empty);

            (int, int) startCell = (randRow, randCol);
            Stack<(int cellRow, int cellCol)> cells = new();
            HashSet<(int visitRow, int visitCol)> visited = [(randRow, randCol)]; 

            cells.Push(startCell);
            
            while(cells.Count > 0)
            {
                (int currRow, int currCol) = cells.Peek();

                List<(int row, int col)> candidates = new();
                foreach((int rOffset, int colOffset) in directions)
                {
                    int nextRow = currRow + rOffset; 
                    int nextCol = currCol + colOffset;
                    if(nextRow >= 0
                    && nextRow < maze.Rows
                    && nextCol >= 0
                    && nextCol < maze.Columns
                    && !visited.Contains((nextRow, nextCol))
                    )
                    {
                        candidates.Add((nextRow, nextCol));
                    }
                }


                if(candidates.Count > 0)
                {
                    (int nextRow, int nextCol) = candidates[rng.Next(candidates.Count)];

                    int wallRow = (currRow + nextRow) / 2; 
                    int wallCol = (currCol + nextCol) / 2; 

                    maze.SetCell(wallRow, wallCol, MazeCell.Empty);

                    maze.SetCell(nextRow, nextCol, MazeCell.Empty);


                    visited.Add((nextRow, nextCol));
                    cells.Push((nextRow, nextCol));
                } else
                {
                    cells.Pop();
                }
            }
        }
    }
}