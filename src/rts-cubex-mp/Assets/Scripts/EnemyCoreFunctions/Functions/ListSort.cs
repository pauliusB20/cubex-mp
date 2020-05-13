using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListSort 
{
   //Heap sort algorithm for the energon deposit points
    public void heapSort(List<DepositPoint> points)
    {
        var length = points.Count;
        for (int i = length / 2 - 1; i >= 0; i--)
        {
            Heapify(points, length, i);
        }
        for (int i = length - 1; i >= 0; i--)
        {
            DepositPoint temp = points[0];
            points[0] = points[i];
            points[i] = temp;
            Heapify(points, i, 0);
        }
    }

    //Rebuilds the heap
    void Heapify(List<DepositPoint> points, int length, int i)
    {
        int largest = i;
        int left = 2 * i + 1;
        int right = 2 * i + 2;
        if (left < length && points[left].getDistance() > points[largest].getDistance())
        {
            largest = left;
        }
        if (right < length && points[right].getDistance() > points[largest].getDistance())
        {
            largest = right;
        }
        if (largest != i)
        {
           DepositPoint swap = points[i];
            points[i] = points[largest];
            points[largest] = swap;
            Heapify(points, length, largest);
        }
    }

      
}
