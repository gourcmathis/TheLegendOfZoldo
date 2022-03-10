using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : MonoBehaviour
{   
    public Animator playerTakingDamage;

    public void getHurt(){
        playerTakingDamage.SetTrigger("PlayerHurt");
    }
}
