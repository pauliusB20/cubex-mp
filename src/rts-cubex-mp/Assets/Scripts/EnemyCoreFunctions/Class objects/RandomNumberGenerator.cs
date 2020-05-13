using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNumberGenerator
{
    private int randomNumber, previousNumber;

    public int generateRandomNumber(int startingNum, int limit)
    {
       randomNumber = Random.Range(startingNum, limit);
       while (randomNumber == previousNumber)
       {
          randomNumber = Random.Range(startingNum, limit);
       }
       previousNumber = randomNumber;

       return randomNumber;
    }
}
