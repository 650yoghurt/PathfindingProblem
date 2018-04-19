# PathfindingProblem

Code challenge april 2018.

Task:
- Implement 2D grid based pathfinding.
- Choose whether you want to do so in C++, C# or both

For C++
- in Cpp/src/nap.cpp
- implement Map::ComputePath() 

For C#
- in CSharp/src/Map.cs
- implement Map.ComputePath()

Input Data:
- Static 32x32 Grid as text file
- '#' character means tile is blocked
- '.' character means tile is free
- The supplied grid file file can be assumed to be valid

Execution:
- pathfind.exe <Relative Map Path> <startX> <startY> <endX> <endY>
- e.g.: 
-        pathfind.exe map0.txt 16 5 16 26

Expected result:
- 8 directional movement.
- Prints map to console with marked found path (@). 
- Application terminates 


*Test Maps:*
The Maps folder contains 4 test maps, each which have the following start positions:
- Maps/test0.txt 16 5 16 26


