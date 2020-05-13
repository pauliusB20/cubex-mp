using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortPoints
{
   //Heap sort algorithm for the energon deposit points
    public void heapSort(List<PointSet> points)
    {
        var length = points.Count;
        for (int i = length / 2 - 1; i >= 0; i--)
        {
            Heapify(points, length, i);
        }
        for (int i = length - 1; i >= 0; i--)
        {
            PointSet temp = points[0];
            points[0] = points[i];
            points[i] = temp;
            Heapify(points, i, 0);
        }
    }

    //Rebuilds the heap
    void Heapify(List<PointSet> points, int length, int i)
    {
        int largest = i;
        int left = 2 * i + 1;
        int right = 2 * i + 2;
        if (left < length && points[left].getDistanceToObject() > points[largest].getDistanceToObject())
        {
            largest = left;
        }
        if (right < length && points[right].getDistanceToObject() > points[largest].getDistanceToObject())
        {
            largest = right;
        }
        if (largest != i)
        {
            PointSet swap = points[i];
            points[i] = points[largest];
            points[largest] = swap;
            Heapify(points, length, largest);
        }
    }

      
}
