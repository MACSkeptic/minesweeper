using System;
using System.Collections.Generic;
using System.Linq;
using MACSkeptic.Commons.Extensions;
using ThoughtWorks.CodingDojo.MineSweeper.Test.Models;

namespace ThoughtWorks.CodingDojo.MineSweeper.Models
{
    public class RandomBoardGenerator
    {
        public virtual Board Generate(Func<ISizeOfBoard, Board> specs)
        {
            return specs.Invoke(new BoardBuilder());
        }

        private class BoardBuilder : INumberOfBombsOfBoard, IBoardConfig, ISizeOfBoard
        {
            private Board _board;
            private int _howManyBombs;
            private int _size;

            public Board Bombs
            {
                get { return _board; }
            }

            public IBoardConfig With(int howManyBombs)
            {
                _howManyBombs = howManyBombs;
                var trickery = _size*_size;
                var positions = new List<Position>(trickery);

                for (var row = 0; row < _size; row++)
                {
                    for (var column = 0; column < _size; column++)
                    {
                        positions.Add(new Position(row, column));
                    }
                }

                positions.Randomize().Take(_howManyBombs).Each(_board.AddBombAt);

                return this;
            }

            public INumberOfBombsOfBoard WithSize(int size)
            {
                _board = new Board(_size = size);
                return this;
            }
        }
    }
}