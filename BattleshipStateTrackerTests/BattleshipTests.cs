using BattleshipStateTracker;
using Xunit;


namespace BattleshipStateTrackerTests
{
    public class BattleshipTests
    {
        // This method is being used for testing right initialization of grid
        // and placement of ship on the board

        public static BattleshipBoard SetUpBoard(int row, int column, int length, int orientation)
        {
            BattleshipBoard mainBoard = new BattleshipBoard();
            mainBoard.SetBoard(row, column, length, orientation);
            return mainBoard;
        }

        //This method is being used for testing the process of giving response to
        // players attempt and update of grid based on attempts made by the user

        public static Player SetUpPlayer(int x, int y, BattleshipBoard board)
        {
            Player singlePlayer = new Player();
            singlePlayer.SetBattleShipBoard(board);
            singlePlayer.SetCoordinates(x, y);
            return singlePlayer;

        }

        [Fact]
        public void Test1()
        {
            //Arrange
            BattleshipBoard myBoard;
            
            //Act
            myBoard = SetUpBoard(1, 1, 2, 1);
            
            //Assert
            Assert.Equal('*', myBoard.GetGrid()[1, 1]);
        }

        [Fact]
        public void Test2()
        {
            //Arrange
            BattleshipBoard myBoard;

            //Act
            myBoard = SetUpBoard(1, 1, 2, 1);

            //Assert
            Assert.Equal('*', myBoard.GetGrid()[1, 2]);
        }

        [Fact]
        public void Test3()
        {
            //Arrange
            BattleshipBoard myBoard;

            //Act
            myBoard = SetUpBoard(1, 1, 2, 1);

            //Assert
            Assert.Equal('-', myBoard.GetGrid()[0, 2]);
        }

        [Fact]
        public void Test4()
        {
            //Arrange
            BattleshipBoard myBoard;
            Player myPlayer;

            //Act
            myBoard = SetUpBoard(1, 1, 2, 1);
            myPlayer = SetUpPlayer(1, 1, myBoard);

            //Assert
            Assert.Equal('H', myPlayer.GetBattleshipBoard().GetGrid()[1, 1]);
        }

        [Fact]
        public void Test5()
        {
            //Arrange
            BattleshipBoard myBoard;
            Player myPlayer;

            //Act
            myBoard = SetUpBoard(1, 1, 2, 1);
            myPlayer = SetUpPlayer(1, 3, myBoard);

            //Assert
            Assert.Equal('M', myPlayer.GetBattleshipBoard().GetGrid()[1, 3]);
        }

        [Fact]
        public void Test6()
        {
            //Arrange
            BattleshipBoard myBoard;
            Player myPlayer;

            //Act
            myBoard = SetUpBoard(1, 1, 2, 1);
            myPlayer = SetUpPlayer(1, 3, myBoard);

            //Assert
            Assert.Equal('M', myPlayer.GetBattleshipBoard().GetGrid()[1, 3]);
        }
    }
}
