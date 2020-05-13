using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTest : MonoBehaviour
{
    [SerializeField] int credits = 20000;
    public int getCreditsAmount()
    {
      return credits;
    }
    public void setCreditsAmount(int creditsAmount)
    {
      credits = creditsAmount;
    }
}
