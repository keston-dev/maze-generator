

namespace MazeGame.Models.Structure
{
    public class Maze
    {
        private int rows = 5;

        public int Rows
        {
            get => rows; 
            set { rows = value; ResizeGrid(); }
        } 

        private int columns = 5;

        public int Columns
        {
            get => columns; 
            set { columns = value; ResizeGrid(); }
        } 

        private MazeCell[,] grid;

        public IEnumerable<MazeCell> Cells => grid.Cast<MazeCell>();
        public Maze()
        {
            grid = new MazeCell[rows, columns];
        }


        public MazeCell GetCell(int row, int col) => grid[row, col];

        public void SetCell(int row, int col, MazeCell type) => grid[row, col] = type; 


        
        private void ResizeGrid()
        {
            MazeCell[,] newGrid = new MazeCell[rows, columns];
            if(grid != null)
            {
                int copyRows = Math.Min(rows, grid.GetLength(0)); 
                int copyCols = Math.Min(columns, grid.GetLength(1));
                for(int r = 0; r < copyRows; r++)
                {
                    for(int c = 0; c < copyCols; c++)
                    {
                        newGrid[r, c] = grid[r, c];
                    }
                }
            }
            
            grid = newGrid;
        }

        public void Clear()
        {
            for(int row = 0; row < Rows; row++) {
                for(int column = 0; column < Columns; column++) {
                    SetCell(row, column, MazeCell.Empty);
                }
            }
        }
    }
}