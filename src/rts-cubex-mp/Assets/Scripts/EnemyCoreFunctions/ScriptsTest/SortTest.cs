using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortTest : MonoBehaviour
{
   [SerializeField] List<int> myArray;
   [SerializeField] bool isSorted = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (myArray != null && myArray.Count > 2)
        {
         Sort(myArray);
        }
        
    }
    void Sort(List<int> array)
    {
        var length = array.Count;
        for (int i = length / 2 - 1; i >= 0; i--)
        {
            Heapify(array, length, i);
        }
        for (int i = length - 1; i >= 0; i--)
        {
            int temp = array[0];
            array[0] = array[i];
            array[i] = temp;
            Heapify(array, i, 0);
        }
    }

    //Rebuilds the heap
    void Heapify(List<int> array, int length, int i)
    {
        int largest = i;
        int left = 2 * i + 1;
        int right = 2 * i + 2;
        if (left < length && array[left] > array[largest])
        {
            largest = left;
        }
        if (right < length && array[right] > array[largest])
        {
            largest = right;
        }
        if (largest != i)
        {
            int swap = array[i];
            array[i] = array[largest];
            array[largest] = swap;
            Heapify(array, length, largest);
        }
    }

}
