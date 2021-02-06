using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Original Java Authors: Mario Cordova, Adriana Davila, Yul Puma, Yiming Zhao, Erin Vasquez
/// Date First Released: 12/8/2015
/// 
/// </summary>
public class MinHeap {
    Vertex[] vector;
    int size;
    int capacity = 1;

    /// <summary>
    /// 
    /// </summary>
    public MinHeap() {
        vector = new Vertex[capacity];
        size = 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="i"></param>
    public void Insert(Vertex i) {

        if (size + 1 == capacity) {
            DoubleUp();
        }

    }

    /// <summary>
    /// Doubles the size of our heap
    /// </summary>
    public void DoubleUp() {
        Vertex[] temp = new Vertex[capacity * 2];

        for (int i = 1; i <= size; i++) {
            temp[i] = vector[i];
        }

        vector = temp;
        capacity *= 2;

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="i"></param>
    /// <param name="j"></param>
    public void Swap(int i, int j) {
        Vertex temp = vector[i];

        vector[i] = vector[j];
        vector[j] = temp;
    }

    /// <summary>
    /// "Pop min from heap"
    /// </summary>
    /// <returns></returns>
    public Vertex Pop() {
        int selection = (int)(1 + Random.value * size); // Math.random() was used in java
        Vertex min = vector[selection];
        vector[selection] = vector[size--];

        // Fix heap
        HeapifyDown(selection);

        // Check size
        if (size == capacity * (1 / 4)) {
            Shrink();
        }

        return min;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Shrink() {
        Vertex[] temp = new Vertex[capacity / 2];

        for (int i = 1; i <= size; i++) {
            temp[i] = vector[i];
        }

        vector = temp;
        capacity /= 2;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="i"></param>
    public void HeapifyUp(int i) {

        while (i > 1 && vector[i].weight < vector[i / 2].weight) {
            Swap(i, i / 2);
            i /= 2;
        }

    }

    /// <summary>
    /// "Fix heap"
    /// </summary>
    /// <param name="i"></param>
    public void HeapifyDown(int i) {
        int iChild, iMin;

        while (i * 2 <= size) {
            iChild = i * 2;

            if (vector[iChild].weight <= vector[iChild + 1].weight)
                iMin = iChild;
            else
                iMin = iChild + 1;

            if (vector[i].weight > vector[iMin].weight) {
                Swap(i, iMin);
                i = iMin;
            } else {
                break;
            }

        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int GetSize() {
        return size;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty() {
        return size == 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool Search(int id) {

        for(int i = 1; i <= size; i++) {

            if(vector[i].id == id) {
                return true;
            }

        }

        return false;
    }

}
