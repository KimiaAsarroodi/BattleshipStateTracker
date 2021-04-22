using System;



namespace BattleshipStateTracker
{
    // This class contains all the fields and methods related to creation of
    // the board and allocation of ship on it

    public class BattleshipBoard
    {
        char[,] grid = new char[10, 10];
        public int counter = 0;

        public char[,] GetGrid()
        {
            return grid;
        }

        public void SetGrid(int x, int y, int z)
        {
            if (z == 0)
            {
                grid[x, y] = '-';
            }
            else
            {
                grid[x, y] = '*';
            }
        }

        //The single battleship  on the board will be placed randomly on the board
        // through generation of random starting row and column plus length of
        // ship and orientation (vertical/horizontal) which will be created in
        // this function

        public void Randomize()
        {
            Random random = new Random();
            int randRow = random.Next(0, 10);
            int randCol = random.Next(0, 10);
            int randLen = random.Next(2, 6);
            int randOrientation = random.Next(0, 2);

            SetBoard(randRow, randCol, randLen, randOrientation);
        }

        //This is where the grid 2d array is being initialized and the ship is
        // getting placed on the right spot (with randomly generated numbers)

        public void SetBoard(int shipRow, int shipCol, int shipLength, int shipOrientation)
        {
            counter = shipLength;
            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    SetGrid(i, j, 0);
                }
            }
            for (int k = 0; k < shipLength; k++)
            {
                if (shipOrientation == 0)
                {
                    if ((shipRow + k) <= 9)
                    {
                        SetGrid(shipRow + k, shipCol, 1);
                    }
                    else
                    {
                        counter = 10 - shipRow;
                    }
                }
                else if (shipOrientation == 1)
                {
                    if ((shipCol + k) <= 9)
                    {
                        SetGrid(shipRow, shipCol + k, 1);
                    }
                    else
                    {
                        counter = 10 - shipCol;
                    }
                }
            }
        }

        // This method displays the grid on the console

        public void DisplayBoard()
        {
            int row;
            int column;

            Console.WriteLine("  ¦ 0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("--+--------------------");

            for (row = 0; row <= 9; row++)
            {
                Console.Write((row).ToString() + " ¦ ");

                for (column = 0; column <= 9; column++)
                {
                        Console.Write(grid[column, row] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n");
        }
    }
    // This class covers all the fields and methods which are associated to the
    // player including number of missed and hitted attempts plus the response
    // to each attempt which is being stored in the grid 2d array

    public class Player
    {
        public int hittedCount = 0;
        public int missedCount = 0;
        
        BattleshipBoard battleshipBoard;

        public int GetHittedCount()
        {
            return hittedCount;
        }
        public int GetMissedCount()
        {
            return missedCount;
        }
        // In this method the single player will be asked to give coordinates of
        // the spot he/she wants to attack on the board

        public void GetCoord1inates()
        {
            int x = 0;
            int y = 0;

            Console.WriteLine("Please enter the X coordinate");

            string line = Console.ReadLine();
            int value;

            if (int.TryParse(line, out value))
            {
                x = value;
            }
            else
            {
                Console.WriteLine("Not an integer!");
            }

            Console.WriteLine("Please enter the Y coordinate");

            line = Console.ReadLine();

            if (int.TryParse(line, out value))
            {
                y = value;
            }
            else
            {
                Console.WriteLine("Not an integer!");
            }
            SetCoordinates(x, y);

        }

        //Through this method player's attempt will be processed and the result
        // of attack will be provided to him/her plus the grid will get updated
        // with result of the attempt

        public void SetCoordinates(int x, int y)
        {
            try
            {
                if (battleshipBoard.GetGrid()[x, y].Equals('*'))
                {
                    battleshipBoard.GetGrid()[x, y] = 'H';
                    Console.Clear();
                    Console.WriteLine("You have hitted the battleship!\r\n");
                    hittedCount += 1;
                }
                else if (battleshipBoard.GetGrid()[x, y].Equals('H'))
                {
                    Console.WriteLine("You have already hitted this location");
                }
                else if (battleshipBoard.GetGrid()[x, y].Equals('M'))
                {
                    Console.WriteLine("You have missed this location in your previous attacks");
                }
                else
                {
                    battleshipBoard.GetGrid()[x, y] = 'M';
                    Console.Clear();
                    Console.WriteLine("This shot has been missed!\r\n");
                    missedCount += 1;
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Error: Please enter numbers between 0 and 9. (Inclusive)");
            }

        }


        public BattleshipBoard GetBattleshipBoard()
        {
            return battleshipBoard;
        }
        public void SetBattleShipBoard(BattleshipBoard battleshipBoard)
        {
            this.battleshipBoard = battleshipBoard;
        }
    }

    public class BattleshipMain
    {
        static BattleshipBoard mainBoard;
        static Player singlePlayer;

        // Through this method the game will get setup and instance of both
        // BattleshipBoard and Player will be created plus the board gets
        // initialized with a single randomly placed ship on it

        public static void SetupGame()
        {
            mainBoard = new BattleshipBoard();
            singlePlayer = new Player();
            mainBoard.Randomize();
            singlePlayer.SetBattleShipBoard(mainBoard);
        }

        static void Main(string[] args)
        {
            SetupGame();
            int roundCount = 0;
            Console.Title = "Battleship!";
            Console.WriteLine("Please enter your name");
            string name = System.Console.ReadLine();
            Console.WriteLine("\n");
            Console.WriteLine("Welcome to Battleship Dear " + name);
            Console.WriteLine("\n");

            //The game will continue until all the coordinates containg the battleship
            // get hitted by the player

            while (singlePlayer.GetHittedCount() < mainBoard.counter)
            {
                if (roundCount == 0)
                {
                    Console.WriteLine("Its attack time! Start by choosing the location you want to attack");
                    Console.WriteLine("\n");
                }
                roundCount++;
                singlePlayer.GetCoord1inates();
            }

            Console.WriteLine("Congratulations, " + name + "! You sunked the battleship! Would you like to see the board? (Y/N)\r\n");
            string answer = System.Console.ReadLine();
            bool success = false;

            while (!success)
            {
                if (answer.Equals("Y") || answer.Equals("y"))
                {
                    success = true;
                    mainBoard.DisplayBoard();
                    Console.WriteLine("You missed: " + singlePlayer.GetMissedCount() + " times\r\n");
                    Console.WriteLine("\n");
                    Console.WriteLine("Thanks for playing Battleship. Please press enter to quit.");
                    System.Console.ReadLine();
                }
                else if (answer.Equals("N") || answer.Equals("n"))
                {
                    success = true;
                    Console.WriteLine("You missed: " + singlePlayer.GetMissedCount() + " times\r\n");
                    Console.WriteLine("\n");
                    Console.WriteLine("Thanks for playing Battleship. Please press enter to quit.");
                    System.Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Error: Please enter Y/N ");
                    answer = System.Console.ReadLine();
                }
            }
        }
    }
}

