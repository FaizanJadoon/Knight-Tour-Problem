using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsTour
{
    class Program
    {
        private const int Row = 8;
        private const int Col = 8;
        private const char GridSymbol = '#';
        private const char MoveSymbol = 'X';
        private static int[] startPosition = new int[2];
        private static char[,] moves = new char[Row,Col];
        private static readonly int[] PossibleMovesX = new int[] {-1, 1, -2, 2, 1, -1, 2, -2}; 
        private static readonly int[] PossibleMovesY = new int[] {2, 2, 1, 1, -2, -2, -1, -1};
        private static int hMove;
        private static int vMove;
        private static int roundMoves = 0;
        private static int bestMoves = 0;
        
        static void Main(string[] args)
        {
            bool isOver = false;
            Random rand = new Random();

            //get starting coordinates
            
            startPosition[0] = rand.Next(0, Row);
            startPosition[1] = rand.Next(0, Col);
            //PopulateArray();
            moves[startPosition[0], startPosition[1]] = MoveSymbol;
            DisplayGrid();
            //run N number of loops
            bool gameOver = false;
            int counter = 0;
            int bestRoundCounter = 0;

            while (!gameOver && counter < 100000)
            {
                PopulateArray();//re-set array
                isOver = false;
                while (!isOver)
                {
                    isOver = MakeMove();
                }

                counter++;
                gameOver = areBlocksFilled();
                if (gameOver)
                {
                    DisplayGrid();
                    Console.WriteLine("It took {0} tries to win the game", counter);
                }
                else if (roundMoves > bestMoves)
                {
                    bestRoundCounter = counter;
                    bestMoves = roundMoves;
                    DisplayGrid();
                }
            }

            if (!gameOver)
            {
                Console.WriteLine("No success. The best score was {0} moves reached on round # {1}", bestMoves, bestRoundCounter);
            }
            Console.ReadKey();
        }

        public static bool areBlocksFilled()
        {
            roundMoves = 0;
            bool blocksAreaFilled = true;
            for (int i = 0; i < Row; i++)
            {
                for (int c = 0; c < Col; c++)
                {
                    if (moves[i, c] != GridSymbol)
                        roundMoves++;
                    else
                    {
                        blocksAreaFilled = false;
                    }
                }
            }
            return blocksAreaFilled;
        }

        public static bool MakeMove()
        {
            for (int i = 0; i < 8; i++)
            {
                if (!getMove())
                {
                    return true;
                }
            }

            return false;
        }

        public static bool getMove()
        {
            Random rand = new Random();
            int badMoveCounter = 0;
            int[] badMoves = new int[]{-1,-1,-1,-1,-1,-1,-1,-1}; //capture values of bad moves

            while (badMoveCounter < 8)
            {
                int randomMove = rand.Next(0, 8); //pick randomly from possible moves
                hMove = startPosition[0] + PossibleMovesX[randomMove];
                vMove = startPosition[1] + PossibleMovesY[randomMove];

                if (((hMove < Row && hMove >= 0) && (vMove < Col && vMove >= 0)) &&
                    (moves[hMove,vMove] != MoveSymbol))
                {
                    moves[hMove, vMove] = MoveSymbol;
                    DisplayGrid();
                    startPosition[0] = hMove;
                    startPosition[1] = vMove;
                    return true;
                   
                }
                else
                {
                    //check if the move is already in BadMoves
                    //if it is, it means it was already made, so do not add it anymore
                    bool moveAlreadyMade = false;
                    for (int i = 0; i < 8; i++)
                    {
                        if (randomMove == badMoves[i])
                        {
                            moveAlreadyMade = true;
                        }
                    }

                    if (!moveAlreadyMade)
                    {
                        badMoves[badMoveCounter] = randomMove;
                        badMoveCounter++;
                    }
                }
            }

            return false;
        }

        public static void DisplayGrid()
        {
            Console.WriteLine("*******************************************");
            Console.Write(" ");
            for (int i = 65; i < Col + 65; i++)
                Console.Write(Convert.ToChar(i));

            Console.WriteLine();
            for (int r = 0; r < Row; r++)
            {
                Console.Write(r + 1);
                for (int c = 0; c < Col; c++)
                {
                    Console.Write(moves[r,c]);
                }
                Console.WriteLine();
            }
        }

        public static void PopulateArray()
        {
            for (int i = 0; i < Row; i++)
            {
                for (int c = 0; c < Col; c++)
                {
                    moves[i, c] = GridSymbol;
                }
            }
        }

    }
}
