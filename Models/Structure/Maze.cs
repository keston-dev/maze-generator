

namespace MazeGame.Models.Structure
{
    public class Maze
    {
        private int _rows = 5;

        public int Rows
        {
            get => _rows; 
            set { _rows = value; ResizeGrid(); }
        } 

        private int _columns = 5;

        public int Columns
        {
            get => _columns; 
            set { _columns = value; ResizeGrid(); }
        } 

        private MazeCell[,] _grid;

        public IEnumerable<MazeCell> Cells => _grid.Cast<MazeCell>();
        public Maze()
        {
            _grid = new MazeCell[_rows, _columns];
        }


        public MazeCell GetCell(int row, int col) => _grid[row, col];

        public void SetCell(int row, int col, MazeCell type) => _grid[row, col] = type; 


        
        private void ResizeGrid()
        {
            var newGrid = new MazeCell[_rows, _columns];
            int copyRows = Math.Min(_rows, _grid.GetLength(0)); 
            int copyCols = Math.Min(_columns, _grid.GetLength(1));
            for(int row = 0; row < copyRows; row++)
            {
                for(int col = 0; col < copyCols; col++)
                {
                    newGrid[row, col] = _grid[row, col];
                }
            }
            
            
            _grid = newGrid;
        }

        public void Clear() => Array.Clear(_grid);
    }
}