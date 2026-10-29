using MazeGame.Models.Structure;

namespace MazeGame.Models.Algorithms
{
    public interface IPathfindingAlgorithm
    {
        string Name { get; }
        IEnumerable<AlgorithmStep> Solve(Maze maze, CancellationToken token);
    }
    public record AlgorithmStep(int Row, int Col, AlgorithmState State);
}