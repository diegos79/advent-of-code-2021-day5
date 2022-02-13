using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace aocd5
{
    internal class Program
    {
        static void Main()
        {
            string[] inputFile = ReadInputFile("input5.txt"); //reading input data
            List<string[]> result = GetInputStringData(inputFile); //
            var verticalLinesPoints = GetVerticalLines(result);
            var horizontalLinesPoints = GetHorizontalLines(result);
            Console.WriteLine("*************ADVENT OF CODE 2021 *********************\n");
            Console.WriteLine("*************DAY 5 - PART1 AND PART2 *********************\n\n\n");
            /**************PRINTING MATRIX WILL TAKE TIME AND RESOURCES****************************/
            /**************UNCOMMENT TO VIEW COMPUTATIONAL OPERATIONS******************************/
            //Console.WriteLine("-----HORIZONTAL LINES-------------");
            //PrintMatrix(horizontalLinesPoints);
            //Console.WriteLine("-----VERTICAL LINES-------------");
            //PrintMatrix(verticalLinesPoints);
            //Console.WriteLine("---VERT+HORIZ LINES---------------");
            var MatrixVertAndHoriz = MergeTwoMatrices (verticalLinesPoints, horizontalLinesPoints);
            //PrintMatrix(MatrixVertAndHoriz); //Optional Uncomment 
            Console.WriteLine("For the horizontal and vertical lines only...");
            Console.WriteLine("The overlapping points are:..." + CountOverlapPoints(MatrixVertAndHoriz, 2));
            Console.WriteLine("------------------");
            var diagonalLinesPoints = GetDiagonalLines(result); //get points of diagonal lines
            //PrintMatrix(diagonalLinesPoints); //Optional Uncomment
            Console.WriteLine("------------------");
            MatrixVertAndHoriz = MergeTwoMatrices(MatrixVertAndHoriz, diagonalLinesPoints); //merged matrix vert and horiz
            //PrintMatrix (MatrixVertAndHoriz); //Optional Uncomment
            var otherDiagonalLinesPoints = GetOthersDiagonalLines(result); //get points of other diagonal lines
            Console.WriteLine("------------------");
            //PrintMatrix(otherDiagonalLinesPoints); //Optional Uncomment
            Console.WriteLine("------------------");
            var finalMatrix = MergeTwoMatrices(MatrixVertAndHoriz, otherDiagonalLinesPoints);//merged matrix vert+horiz+diag
            //PrintMatrix(finalMatrix); //Optional Uncomment
            Console.WriteLine("For horizontal, vertical and diagonal lines...");
            Console.WriteLine("Overlapping points are:...final result is: " + CountOverlapPoints(finalMatrix, 2));
            Console.ReadLine();
        } //End Main

        /********************************************************************************/
        // Convert input data to list of splitted strings
        public static List<string[]> GetInputStringData (string[] inputFile)
        {
            string[] splitCriteria = { "->", ",", " " };
            List<string[]> result = new List<string[]>();
            for (int i = 0; i < inputFile.Length; i++) //loop for create list of string[]
            {
                result.Add(inputFile[i].Split(splitCriteria, StringSplitOptions.None));
            }
            return result;
        }

        public static string[] ReadInputFile(string path)
        {
            return File.ReadAllLines(path);
        }

        /**************************************************************/
        // Get Others Diagonal Lines different from progressive diagonal lines 
        public static int[,] GetOthersDiagonalLines(List<string[]> result)
        {
            var allPoints1 = (PopulatePoints(result, 0, 1, 500)); //populate points 1 coordinates x1,y1
            var allPoints2 = (PopulatePoints(result, 4, 5, 500)); //populate points 2 coordinates x2,y2
            int[,] resultMatrix = new int[1000, 1000];
            var xEquals = GetCoordEquals(allPoints1, allPoints2, 0); //0 for x1=x2
            var yEquals = GetCoordEquals(allPoints1, allPoints2, 1);//1 for y1=y2
            var diag = GetDiagonalLinesPoints(allPoints1, allPoints2); //diagonal lines
            var otherLines = new List<int>();
            for (int i = 0; i < result.Count; i++) //all the other rows 
            {
                if (!xEquals.Contains(i) && !yEquals.Contains(i) && !diag.Contains(i))
                {
                    otherLines.Add(i);
                }
            }
            
            for (int j = 0; j < otherLines.Count; j++)
            {
                int x1 = allPoints1[otherLines[j], 0];
                int y1 = allPoints1[otherLines[j], 1];
                int x2 = allPoints2[otherLines[j], 0];
                int y2 = allPoints2[otherLines[j], 1];

                if (x1<x2 && y1<y2)
                {
                    while (x1 <= x2 && y1 <= y2)
                    {
                        resultMatrix[x1,y1] += 1;
                        x1++;y1++;
                    }
                }
                else if (x1 <= x2 && y1 >= y2)
                {
                    while (x1 <= x2 && y1 >= y2)
                    {
                        resultMatrix[x1, y1] += 1;
                        x1++; y1--;
                    }
                }
                else if (x1 >= x2 && y1 <= y2)
                {
                    while (x1 >= x2 && y1 <= y2)
                    {
                        resultMatrix[x1, y1] += 1;
                        x1--; y1++;
                    }
                }
                else if (x1 >= x2 && y1 >= y2)
                {
                    while (x1 >= x2 && y1 >= y2)
                    {
                        resultMatrix[x1, y1] += 1;
                        x1--; y1--;
                    }
                }
            }
            return resultMatrix;
        }

        /**************************************************************/
        // Get Diagonal Lines x1 != x2, y1 != y2
        public static int[,] GetDiagonalLines(List<string[]> result)
        {
            var allPoints1 = (PopulatePoints(result, 0, 1, 500)); //populate points 1 coordinates x1,y1
            var allPoints2 = (PopulatePoints(result, 4, 5, 500)); //populate points 2 coordinates x2,y2
            var diag = GetDiagonalLinesPoints(allPoints1, allPoints2);
            int[,] resultMatrix = new int[1000, 1000];

            for (int j = 0; j < diag.Count; j++)
            // DIAGONAL POINTS LONG LINES
            {
                int x1 = allPoints1[diag[j], 0];
                int y1 = allPoints1[diag[j], 1];
                int x2 = allPoints2[diag[j], 0];
                int y2 = allPoints2[diag[j], 1];

                if (x1 <= x2 && y1 <= y2)
                {
                    while (x1 <= x2 && y1 <= y2)
                    {
                        resultMatrix[x1, y1] += 1;
                        x1++; y1++;
                    }
                }
                else if (x1 <= x2 && y1 >= y2)
                {
                    while (x1 <= x2 && y1 >= y2)
                    {
                        resultMatrix[x1, y1] += 1;
                        x1++; y1--;
                    }
                }
                else if (x1 >= x2 && y1 <= y2)
                {
                    while (x1 >= x2 && y1 <= y2)
                    {
                        resultMatrix[x1, y1] += 1;
                        x1--; y1++;
                    }
                }
                else if (x1 >= x2 && y1 >= y2)
                {
                    while (x1 >= x2 && y1 >= y2)
                    {
                        resultMatrix[x1, y1] += 1;
                        x1--; y1--;
                    }
                }
            }
            return resultMatrix;
        }


        /**************************************************************/
        // Concatenate two square matrices of same length
        public static int[,] MergeTwoMatrices (int[,] matr1, int[,] matr2)
        {
            for (int i = 0; i < matr1.GetLength(0); i++)
                for (int j = 0; j < matr2.GetLength(0);j++)
                    matr2[j, i] += matr1[j, i];
            return matr2;
        }

        /**************************************************************/
        // Get Horizontal Lines for y1 = y2
        public static int[,] GetHorizontalLines(List<string[]> result)
        {
            var allPoints1 = (PopulatePoints(result, 0, 1, 500)); //populate points 1 coordinates x1,y1
            var allPoints2 = (PopulatePoints(result, 4, 5, 500)); //populate points 2 coordinates x2,y2

            var yEquals = GetCoordEquals(allPoints1, allPoints2, 1);//1 for y1=y2
            int[,] resultMatrix = new int[1000, 1000];

            for (int j = 0; j < yEquals.Count; j++)
            //loop for track y1=y2 horizontal lines
            {
                int x1 = allPoints1[yEquals[j], 0];
                int y1 = allPoints1[yEquals[j], 1];
                int x2 = allPoints2[yEquals[j], 0];
                int y2 = allPoints2[yEquals[j], 1];

                if (x1 <= x2)
                {
                    while (x1 <= x2) //complete horizontal lines in result matrix (exluding x2)
                    {
                        resultMatrix[x1, y1] += 1;
                        x1++;
                    }
                }
                else if (x1 >= x2)
                {
                    while (x1 >= x2) //complete vertical lines in result matrix (exluding y2)
                    {
                        resultMatrix[x2, y2] += 1;
                        x2++;
                    }
                }

            }
            return resultMatrix;
        }


        /**************************************************************/
        // Get Vertical Lines for x1 = x2
        public static int[,] GetVerticalLines(List<string[]> result)
        {

            var allPoints1 = (PopulatePoints(result, 0, 1, 500)); //populate points 1 coordinates x1,y1
            var allPoints2 = (PopulatePoints(result, 4, 5, 500)); //populate points 2 coordinates x2,y2
            var xEquals = GetCoordEquals(allPoints1, allPoints2, 0); //0 for x1=x2
            int[,] resultMatrix = new int[1000, 1000];

            //get vertical line x1=x2, y1!=y2
            for (int i = 0; i < xEquals.Count; i++)

            //loop for track x1=x2 vertical lines
            {
                int x1 = allPoints1[xEquals[i], 0];//setting points x1
                int y1 = allPoints1[xEquals[i], 1];//setting points y1
                int x2 = allPoints2[xEquals[i], 0];//setting points x2
                int y2 = allPoints2[xEquals[i], 1];//setting points y2

                if (y1 <= y2) //complete vertical lines points
                {
                    while (y1 <= y2) //complete vertical lines in result matrix (exluding y1 and y2)
                    {
                        resultMatrix[x1, y1] += 1;
                        y1++;
                    }
                }
                else if (y1 >= y2) //complete vertical lines points
                {
                    while (y1 >= y2) //complete vertical lines in result matrix (exluding y1 and y2)
                    {
                        resultMatrix[x1, y2] += 1;
                        y2++;
                    }
                }
            }
            return resultMatrix;
        }

        /**************************************************************/
        // Get overlap points over a matrix, for minimum of overlapped points
        public static int CountOverlapPoints(int[,] matrix, int overlapMinPoints)
        {
            int result = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[j, i] >= overlapMinPoints)
                        result++;
                }
            }
            return result;
        }
        /***************************************************************************/
        public static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[j, i] == 0)
                        Console.Write(" .");
                    else
                        Console.Write(" " + matrix[j, i]);
                }
                Console.WriteLine();
            }
        }

        /**********************************************************************************/
        // Get List of rows corresponding to equals coordinates
        public static List<int> GetCoordEquals(int[,] allPoints1, int[,] allPoints2, int xOry)
        //xOry = 0 for xs, xOry = 1 for ys
        {
            List<int> result = new List<int>();
            for (int i = 0; i < allPoints1.GetLength(0); i++)
            {
                if (allPoints1[i, xOry] == allPoints2[i, xOry])
                {
                    result.Add(i);
                }
            }
            return result;
        }

        public static List<int> GetDiagonalLinesPoints(int[,] allPoints1, int[,] allPoints2)
       
        {
            List<int> result = new List<int>();
            for (int i = 0; i < allPoints1.GetLength(0); i++)
            {
                if ((allPoints1[i, 0] == allPoints1[i, 1]) && (allPoints2[i,0] == allPoints2[i,1])) //x1=y1 AND x2=y2
                {
                    result.Add(i);
                }

                if ((allPoints1[i, 0] == allPoints2[i, 1]) && (allPoints2[i, 0] == allPoints1[i, 1])) //x1=y2 AND x2=y1
                {
                    result.Add(i);
                }

            }
            return result;
        }

        /*************************************************************************************/
        // Create coordinates for points x1, y1, x2, y2 from strings list to matrix int[,]
        public static int[,] PopulatePoints(List<string[]> list, int columnCoordX, int columnCoordY, int totalCoord)
        {
            int[,] point = new int[totalCoord, 2]; //matrix of points coordinates (2 column, total input numbers) 
            for (int i = 0; i < list.Count; i++) //loop for convert x1s coordinates to int number
            {
                point[i, 0] = int.Parse(list[i][columnCoordX]);
            }
            for (int i = 0; i < list.Count; i++) //loop for convert y1s coordinates to int number
            {
                point[i, 1] = int.Parse(list[i][columnCoordY]);
            }
            return point;
        }

    }
}

