using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AITroopManagerInterface
{
  //Main method for building a structure
  void buildBarracks();
  

  //Generating random number
  void generateRandomNumber();

  //Method for checking if the base has atlaest one brracks structure
  bool requiredBarracksAmount(int amount);

  
  //Method for returning the state of the purchase process (true - is hapening, false - is not happening)
   bool Purchasing();

  //Method for returning the price of the barracks
   int getBarracksPrice();

  //Method for returning the light trooper price
   int getLightTroopUnitPrice();
}
