using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyFight : MonoBehaviour
{
    // Start is called before the first frame update
    public void punchFonction()
    {
        AnimatorManager.Instance.punchFalse();
        PlayerMove.Instance.lifeLose();

    } 
    public void kick()
    {
        AnimatorManager.Instance.kickFalse();
        bossFight.Instance.lifeLose();
    }
}
