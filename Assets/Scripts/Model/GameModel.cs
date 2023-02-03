using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : MonoBehaviour
{
    public static IEnumerator StartGameOverRoutine()
    {
        print("GameOver");
        yield return 0;
    }
}
