using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IAISpawnHandler 
{
    AIBase Spawn(AIType aiType, Vector3 position);
}
