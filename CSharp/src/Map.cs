using System;
using System.Text;
using System.Collections;

namespace PathfindingCSharp
{
    public struct Vector2 : IEquatable<Vector2>
    {
        public int x, y;

        public bool Equals(Vector2 other)
        {
            return x == other.x && y == other.y;
        }
    }
    public class Map
    {
        public const char emptyChar = '.';
        public const char blockedChar = '#';
        public const char invalidChar = '?';
        public const char pathChar = '@';

        private char[,] mapPositions;
        Vector2 startPosition;
        Vector2 goalPosition;

        public Map(string[] mapData, Vector2 startPos, Vector2 endPos)
        {
            if (startPos.x < 0 || startPos.x >= mapData.Length || startPos.y < 0 || startPos.y >= mapData.Length)
                throw new Exception("Start position is out of bounds, provided coordinates were: " + startPos.x + "," + startPos.y);

            if (endPos.x < 0 || endPos.x >= mapData.Length || endPos.y < 0 || endPos.y >= mapData.Length)
                throw new Exception("End position is out of bounds, provided coordinates were: " + endPos.x + "," + endPos.y);

            if (mapData[startPos.y][startPos.x] == blockedChar)
                throw new Exception("Start position is invalid, provided coordinates were: " + startPos.x + "," + startPos.y);

            if (mapData[endPos.y][endPos.x] == blockedChar)
                throw new Exception("End position is invalid, provided coordinates were: " + endPos.x + "," + endPos.y);

            startPosition = startPos;
            goalPosition = endPos;

            mapPositions = new char[mapData.Length, mapData.Length];

            for (int i = 0; i < mapData.Length; ++i)
            {
                for (int j = 0; j < mapData[i].Length; ++j)
                {
                    int index = (i * mapData.Length) + j;

                    if (startPos.x == j && startPos.y == i)
                        mapPositions[i, j] = pathChar;
                    else if (endPos.x == j && endPos.y == i)
                        mapPositions[i, j] = pathChar;
                    else
                    {
                        switch (mapData[i][j])
                        {
                            case emptyChar:
                                mapPositions[i, j] = emptyChar;
                                break;

                            case blockedChar:
                                mapPositions[i, j] = blockedChar;
                                break;

                            default:
                                mapPositions[i, j] = invalidChar;
                                break;
                        }
                    }

                }
            }
        }

        private bool IsBlocked(Vector2 position)
        {
            return mapPositions[position.y, position.x] == blockedChar;
        }

        public void DisplayMap()
        {
            Console.Clear();
            Console.WriteLine(SolutionToString());
        }

        public string SolutionToString()
        {
            StringBuilder sb = new StringBuilder(mapPositions.Length + mapPositions.GetLength(0)); //add extra space for the new lines

            for (int i = 0; i < mapPositions.GetLength(0); ++i)
            {
                for (int j = 0; j < mapPositions.GetLength(1); ++j)
                {
                    sb.Append(mapPositions[i, j]);
                }
                sb.Append('\n');
            }

            return sb.ToString();
        }


        private Queue queue;

        public bool ComputePath()
        { 
            var edgeTo = new (int, int)[32,32];
            var visited = new bool[32, 32];
            queue = new Queue();

            queue.Enqueue(new Node { x = startPosition.x, y = startPosition.y });
            visited[startPosition.y, startPosition.x] = true;

            var res = search(edgeTo, visited);

            if (res) drawPath(edgeTo, goalPosition.x, goalPosition.y);;
            Console.WriteLine("Returned: " + res);

            return res;
        }

        public void drawPath((int, int)[,] edgeTo, int x, int y){
            if ((x == startPosition.x && y == startPosition.y)) return;
            mapPositions[y, x] = pathChar;
            var k = edgeTo[y, x];
            drawPath(edgeTo, k.Item1, k.Item2);
        }

        public bool insideBounds(int i, int j){
            return i >= 0 && j >= 0 && i < 32 && j < 32;
        }

        public bool search((int, int)[,] edgeTo, bool[,] visited)
        {
            if (queue.Count == 0) return false;
            Node n = (Node)queue.Dequeue();

            for (int i = -1; i <= 1; i++){
                for (int j = -1; j <= 1; j++){
                    int x = n.x + i;
                    int y = n.y + j;
                    if (x == goalPosition.x && y == goalPosition.y) {
                        Console.WriteLine("Found it!");
                        edgeTo[y, x] = (n.x, n.y);
                        return true;
                    }
                    if (insideBounds(x,y) && mapPositions[y,x] == emptyChar && (!visited[y,x])) {
                        edgeTo[y, x] = (n.x, n.y);
                        visited[y, x] = true;
                        queue.Enqueue(new Node { x = x, y = y });
                    }
                }
            }

            return search(edgeTo, visited);
        }
        public class Node{
            public int x;
            public int y;
        }
    };
}
