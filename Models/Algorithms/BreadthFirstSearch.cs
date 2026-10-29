using System.Runtime.CompilerServices;
using MazeGame.Models.Structure;

namespace MazeGame.Models.Algorithms
{
    public class BreadthFirstSearch : IPathfindingAlgorithm
    {
        public string Name => "BFS (Breadth-First Search)";

        public IEnumerable<AlgorithmStep> Solve(Maze maze, CancellationToken token)
        {
            (int rootRow, int rootCol) = (0, 0);

            for(int r = 0; r < maze.Rows; r++)
            {
                for(int c = 0; c < maze.Columns; c++)
                {
                    if(maze.GetCell(r, c) == MazeCell.Start)
                    {
                        (rootRow, rootCol) = (r, c);
                    }
                }
            }

            bool isFinished = false;

            List<(int, int)> directions = [(-1, 0), (1, 0) , (0, -1), (0, 1)];
            Queue<AlgorithmStep> steps = new Queue<AlgorithmStep>();

            AlgorithmStep root = new(rootRow, rootCol, AlgorithmState.Frontier);
            
            Dictionary<(int childRow, int childCol), (int parentRow, int parentCol)> pathTrace = new(); 
            HashSet<(int visitRow, int visitCol)> visited = [(root.Row, root.Col)]; 
            List<(int, int)> outputPath = new();
            
            steps.Enqueue(root);
            yield return root with { State = AlgorithmState.Frontier };
            while(steps.Count > 0 && !isFinished)
            {
                var step = steps.Dequeue();
                yield return step with { State = AlgorithmState.Visited };
                foreach((int rOffset, int colOffset) in directions)
                {
                    int nextRow = step.Row + rOffset; 
                    int nextCol = step.Col + colOffset;

                    if(nextRow >= 0 // are we in row range
                    && nextRow < maze.Rows // are we in row bounds
                    && nextCol >= 0  // are we in col range
                    && nextCol < maze.Columns // are we in col bounds
                    && maze.GetCell(nextRow, nextCol) != MazeCell.Wall // check if this is walkable
                    )
                    {
                        if(maze.GetCell(nextRow, nextCol) == MazeCell.End)
                        {
                            // we're done, find path from end -> start 
                            AlgorithmStep newStep = new(nextRow, nextCol, AlgorithmState.Visited);
                            if(!visited.Contains((nextRow, nextCol)))
                            {
                                visited.Add((newStep.Row, newStep.Col));
                                pathTrace.Add((newStep.Row, newStep.Col), (step.Row, step.Col));
                            }

                            (int, int) current = (nextRow, nextCol);

                            while(current != (rootRow, rootCol))
                            {   
                                outputPath.Add(current);
                                if(pathTrace.TryGetValue((current.Item1, current.Item2), out var pathValue))
                                {
                                    current = pathValue; 
                                }
                            }

                            isFinished = true;
                        }

                        if (visited.Contains((nextRow, nextCol))) continue;
                        
                        AlgorithmStep next = new(nextRow, nextCol, AlgorithmState.Frontier);
                        pathTrace.Add((next.Row, next.Col), (step.Row, step.Col));
                        steps.Enqueue(next);
                        yield return new AlgorithmStep(next.Row, next.Col, AlgorithmState.Frontier);
                        visited.Add((next.Row, next.Col));
                    }
                }
            }


            outputPath.Reverse();
            
            foreach((int, int) pathNode in outputPath)
            {
                yield return new AlgorithmStep(pathNode.Item1, pathNode.Item2, AlgorithmState.Path);
            }
        }

        
    }



}