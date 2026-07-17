

using MazeGame.Models.Structure;

namespace MazeGame.Models.Generation
{
    public interface IMazeGenerator
    {
        void Generate(Maze maze);
    }
}