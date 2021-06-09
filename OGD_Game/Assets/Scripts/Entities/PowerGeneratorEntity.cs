using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGeneratorEntity : BaseEntity
{
    private int turnCounter = 0;

    protected override void OnRoundStart()
    {
        turnCounter++;
        if(turnCounter == 2)
        {
            PlayerData.Instance.GiveMoney(6);
            UITreeUpdater.Instance.UpdateTrees();
            Debug.Log("Entity: " + name + " gave 6 points.");
            TakeDamage(1);
        }

    }

    protected override void OnRoundEnd()
    {

    }
}