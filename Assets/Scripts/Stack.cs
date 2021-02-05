using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Original Java Authors: Mario Cordova, Adriana Davila, Yul Puma, Yiming Zhao, Erin Vasquez
/// Date First Released: 12/8/2015
/// </summary>
public class Stack {
    
    static int size = 50; // was a FINAL variable in java before
    int capacity;
    int[] stack = new int[size];

    /// <summary>
    /// 
    /// </summary>
    /// <param name="val">Int Value to be pushed onto stack</param>
    void Push(int val) {
        stack[size - capacity - 1] = val;
        capacity++;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    int Pop() {
        int a = stack[size - capacity];

        stack[size - capacity] = 0;
        capacity--;

        return a;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    bool IsEmpty() {
        return capacity == 0;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    int Getcapacity() {
        return capacity;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    int GetSize() {
        return size;
    }


    int GetValue(int i) {
        return stack[i];
    }

    /// <summary>
    /// 
    /// </summary>
    void clearCapacity() {
        capacity = 0;
    }

}
