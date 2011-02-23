namespace ThoughtWorks.CodingDojo.MineSweeper.Models
{
    public interface INumberOfBombsOfBoard
    {
        IBoardConfig With(int howManyBombs);
    }
}