using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Original Java Authors: Mario Cordova, Adriana Davila, Yul Puma, Yiming Zhao, Erin Vasquez
/// Date First Released: 12/8/2015
/// </summary>
public class Maze : MonoBehaviour {

    [SerializeField, Range(1, 100)]
    int MazeSize = 20;

    enum VertexType {Wall, Path, SolvedPath, Exit};



    /// <summary>
    /// a 2d array representing our maze consisting of our maze graph vertices
    /// </summary>
    Vertex[][] MazeMatrix;

    /// <summary>
    /// an array of... ?
    /// </summary>
    Vertex[] MST;

    /// <summary>
    /// an array of visited vertices
    /// </summary>
    bool[] IsVisited;

    /// <summary>
    /// An integer array of the solved maze path
    /// </summary>
    int[][] SolvedMazePath;

    /// <summary>
    /// 
    /// </summary>
    private void Awake() {
        int VertexID = 0; // Set to 0 as our first vertex will be ID 0, incrementing by 1

        MazeMatrix = new Vertex[MazeSize][];
        MST = new Vertex[MazeSize * MazeSize];
        SolvedMazePath = new int[(MazeSize * 2) - 1][];
        IsVisited = new bool[MazeSize * MazeSize];


        // Initialize all vertices as NOT visited
        for (int i = 0; i < MazeSize * MazeSize; i++) {
            IsVisited[i] = false;
        }

        // Initialize our vertices
        for (int i = 0; i < MazeSize; i++) {
            for (int j = 0; j < MazeSize; j++) {
                if (i == 0 && j == 0) {
                    // If we're at the top left corner, set weight to 0 (as in wall?)
                    MazeMatrix[i][j] = new Vertex(0, 0, 0, VertexID);

                } else {
                    // Set our weight to 1 (as in a path?)
                    MazeMatrix[i][j] = new Vertex(1, i, j, VertexID);
                }

                // Increment our vertex ID for the next vertex to be initialized
                VertexID++;
            }
        }

        // For all of our vertices on our maze graph
        for (int i = 0; i < MazeSize; i++) {
            for (int j = 0; j < MazeSize; j++) {

                // Set up the neighbors for this vertex
                MazeMatrix[i][j].SetNeighbors(i, j, MazeMatrix);
            }
        }


    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int[][] generateForGUI() {
        Prim();
        CreatePath();
        SolveMaze(0, 0);

        return SolvedMazePath;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Print() {
        string Output = "";

        for (int i = 0; i < MazeSize; i++) {

            for (int j = 0; j < MazeSize; j++) {
                Output += MazeMatrix[i][j].id + " ";
            }

            Output += "\n";
        }


        Debug.Log(Output);
    }

    /// <summary>
    /// 
    /// </summary>
    public void Prim() {
        MinHeap heap = new MinHeap();
        int count = 0;
        int neighborID;
        Vertex min = null;

        heap.Insert(MazeMatrix[0][0]);

        while (!heap.IsEmpty()) {
            min = heap.Pop();

            IsVisited[min.id] = true;
            MST[count++] = min;

            for (int i = 0; i < min.Neighbors.Length; i++) {
                neighborID = min.Neighbors[i].id;
                //System.out.println(heap.search(neighborID) + " " + isVisited[neighborID]);
                if (heap.Search(neighborID) == false && IsVisited[neighborID] == false) {
                    //System.out.println("YES");
                    min.Neighbors[i].previousX = min.row;
                    min.Neighbors[i].previousY = min.col;
                    heap.Insert(min.Neighbors[i]);
                }
            }
            //System.out.println(heap.getSize());
        }

        IsVisited[min.id] = true;
    }

    /// <summary>
    /// 
    /// </summary>
    public void CreatePath() {
        int dimension = MazeSize;
        int sizeOfMST = dimension * dimension;
        int TargetID;

        for (int i = 0; i < sizeOfMST; i++) {
            TargetID = MST[i].id;

            GetIDCoordinates(TargetID, dimension);
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="dimension"></param>
    private void GetIDCoordinates(int ID, int dimension) {

        for (int i = 0; i < dimension; i++) {

            for (int j = 0; j < dimension; j++) {
                if (MazeMatrix[i][j].id == ID) {
                    SearchAndMark(MazeMatrix[i][j]);
                }
            }

        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="v"></param>
    private void SearchAndMark(Vertex v) {
        int row = (v.row * 2);
        int column = (v.col * 2);
        int oldRow = (v.previousX * 2);
        int oldColumn = (v.previousY * 2);

        // If we got the top left corner, we know it's type 1
        if (row == 0 && column == 0) {
            SolvedMazePath[row][column] = 1;
            return;
        }

        SolvedMazePath[row][column] = 1;

        if (row - oldRow == 0) {
            if (column - oldColumn > 0)
                SolvedMazePath[row][column - 1] = 1;
            else
                SolvedMazePath[row][column + 1] = 1;
        } else {
            if (row - oldRow > 0)
                SolvedMazePath[row - 1][column] = 1;
            else
                SolvedMazePath[row + 1][column] = 1;
        }

    }

    /// <summary>
    /// Output our maze to the console
    /// </summary>
    public void ShowMaze() {
        int dimension = MazeSize;
        string Output = "";

        for (int i = 0; i < (dimension * 2) - 1; i++) {

            for (int j = 0; j < (dimension * 2) - 1; j++) {
                Output += SolvedMazePath[i][j] + " ";
            }

            Output += "\n";
        }

        Debug.Log(Output);

    }

    /// <summary>
    /// Return true if the tile is valid
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    private bool ValidTile(int row, int column) {
        bool result = false;

        if (row >= 0 && row < SolvedMazePath.Length && column >= 0 && column < SolvedMazePath[0].Length) {

            if (SolvedMazePath[row][column] == 1) {
                result = true;
            }

        }

        return result;
    }

    /// <summary>
    /// Solve the maze
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    public bool SolveMaze(int row, int column) {
        bool done = false;

        if (ValidTile(row, column)) {
            SolvedMazePath[row][column] = 2; //tile has already been tried
            if (row == SolvedMazePath.Length - 1 && column == SolvedMazePath.Length - 1) {
                done = true;
            } else {
                done = SolveMaze(row + 1, column); // down
                if (!done) {
                    done = SolveMaze(row, column - 1); //left
                }
                if (!done) {
                    done = SolveMaze(row - 1, column); //up
                }
                if (!done) {
                    done = SolveMaze(row, column + 1); //right
                }
            }
            if (done) {
                SolvedMazePath[row][column] = 3; //3 means path leading to the end
            }
        }
        return done;
    }

    /// <summary>
    /// Output the solved maze to the console
    /// </summary>
    public void PrintSolvedMaze() {
        string Output = "";

        for (int i = 0; i < SolvedMazePath.Length; i++) {

            for (int j = 0; j < SolvedMazePath[0].Length; j++) {
                Output += SolvedMazePath[i][j] + " ";
            }

            Output += "\n";
        }

        Debug.Log(Output);
    }

} // end Maze










