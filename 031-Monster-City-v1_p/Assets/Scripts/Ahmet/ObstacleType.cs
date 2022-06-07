using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleType : MonoBehaviour
{
    public obstacleType type;
    public bool working=true;

    public enum obstacleType
    {
        dna,human,halfHuman
    }
    
}
