using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Original Java Authors: Mario Cordova, Adriana Davila, Yul Puma, Yiming Zhao, Erin Vasquez
/// Date First Released: 12/8/2015
/// </summary>
public class Vertex {
    public Vertex[] Neighbors;
    public int id, weight, row, col, previousX, previousY;

    /// <summary>
    /// Create a vertex for our maze graph
    /// </summary>
    /// <param name="weight">This vertex's weight in the graph</param>
    /// <param name="x">This vertex's X position in our maze graph</param>
    /// <param name="y">This vertex's Y position in our maze graph</param>
    /// <param name="id">This vertex's ID in our maze graph</param>
    public Vertex(int weight, int x, int y, int id) {
        this.weight = weight;
        row = x;
        col = y;
        this.id = id;

        previousX = -1;
        previousY = -1;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="currentRow">Currently observed vertex X position</param>
    /// <param name="currentCol">Currently observed vertex Y position</param>
    /// <param name="matrix">Our matrix of vertices</param>
    public void SetNeighbors(int currentRow, int currentCol, Vertex[][] matrix) {

        if (currentRow == 0 && currentCol > 0 && currentCol < matrix.Length - 1) {
            // The current vertex is against the Top [Wall?]

            Neighbors = new Vertex[3];
            Neighbors[0] = matrix[currentRow][currentCol + 1]; // the neighbor vertex to the right
            Neighbors[1] = matrix[currentRow][currentCol]; // the neighbor vertex below
            Neighbors[2] = matrix[currentRow][currentCol - 1]; // the neighbor vertex to the left

        } else if (currentCol == 0 && currentRow > 0 && currentRow < matrix.Length - 1) {
            // Current vertex against the Left wall

            Neighbors = new Vertex[3];
            Neighbors[0] = matrix[currentRow][currentCol + 1]; // the neighbor vertex vertex to the right
            Neighbors[1] = matrix[currentRow - 1][currentCol]; // the neighbor vertex vertex above
            Neighbors[2] = matrix[currentRow + 1][currentCol]; // the neighbor vertex below

        } else if (currentCol == matrix.Length - 1 && currentRow > 0 && currentRow < matrix.Length - 1) {
            // Our vertex is against the Right wall

            Neighbors = new Vertex[3];
            Neighbors[0] = matrix[currentRow][currentCol - 1];// the neighbor vertex left
            Neighbors[1] = matrix[currentRow - 1][currentCol];// the neighbor vertex above
            Neighbors[2] = matrix[currentRow + 1][currentCol];// the neighbor vertex below

        } else if (currentRow == matrix.Length - 1 && currentCol > 0 && currentCol < matrix.Length - 1) {
            // Our vertex is on the Bottom

            Neighbors = new Vertex[3];
            Neighbors[0] = matrix[currentRow][currentCol + 1]; // the neighbor vertex right
            Neighbors[1] = matrix[currentRow][currentCol - 1]; // the neighbor vertex left
            Neighbors[2] = matrix[currentRow - 1][currentCol]; // the neighbor vertex above

        } else if (currentRow == 0 && currentCol == 0) {
            // Our vertex is in the Top Left Corner

            Neighbors = new Vertex[2];
            Neighbors[0] = matrix[currentRow][currentCol + 1]; // the neighbor vertex right
            Neighbors[1] = matrix[currentRow + 1][currentCol]; // the neighbor vertex below

        } else if (currentRow == 0 && currentCol == matrix.Length - 1) {
            // Our vertex is in the Top right corner

            Neighbors = new Vertex[2];
            Neighbors[0] = matrix[currentRow][currentCol - 1]; // the neighbor vertex left
            Neighbors[1] = matrix[currentRow + 1][currentCol]; // the neighbor vertex below

        } else if (currentRow == matrix.Length - 1 && currentCol == 0) {
            // Our vertex is on the Bottom Left Corner

            Neighbors = new Vertex[2];
            Neighbors[0] = matrix[currentRow][currentCol + 1];// the neighbor vertex right
            Neighbors[1] = matrix[currentRow - 1][currentCol];// the neighbor vertex above

        } else if (currentRow == matrix.Length - 1 && currentCol == matrix.Length - 1) {
            // Our vertex is in the Bottom Right Corner

            Neighbors = new Vertex[2];
            Neighbors[0] = matrix[currentRow][currentCol - 1]; // the neighbor vertex left
            Neighbors[1] = matrix[currentRow - 1][currentCol]; // the neighbor vertex above

        } else {
            // Our vertex is in the Middle, not against any wall

            Neighbors = new Vertex[4];
            Neighbors[0] = matrix[currentRow + 1][currentCol]; // the neighbor vertex below
            Neighbors[1] = matrix[currentRow - 1][currentCol]; // the neighbor vertex above
            Neighbors[2] = matrix[currentRow][currentCol + 1]; // the neighbor vertex right
            Neighbors[3] = matrix[currentRow][currentCol - 1]; // the neighbor vertex left
        }

    }

}
