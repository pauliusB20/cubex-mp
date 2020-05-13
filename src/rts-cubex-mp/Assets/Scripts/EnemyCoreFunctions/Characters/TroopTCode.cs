using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopTCode : MonoBehaviour
{
    [SerializeField] string troopCode;

    private void Start() {
        FindObjectOfType<GameSession>().addEnemyTroopAmount(1);    
    }
    public void setTroopCode(string code)
    {
        this.troopCode = code;
        Debug.Log(gameObject.name + " team code = " + code);
    }
    public string getTroopCode()
    {
        return troopCode;
    }
}
