using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    private static List<GameObject> obstacles = new List<GameObject>();
    private static List<PoolObjectType> obstaclesTypes = new List<PoolObjectType>();

    private void Update() 
    {
        if (Time.timeScale != 0 && 
        (obstacles.Count == 0 || obstacles.Count < 3 
        && obstacles[obstacles.Count - 1].transform.position.x <= 9))
            StartCoroutine(GetObstacle());
    }

    private IEnumerator GetObstacle()
    {
        var type = (PoolObjectType)Random.Range(0, 9);
        var obstacle = PoolManager.Instance.GetPoolObject(type);
        obstacle.transform.position = new Vector2(17f, -4.78f);
        obstacle.SetActive(true);

        obstacles.Add(obstacle);
        obstaclesTypes.Add(type);
        while (obstacle.transform.position.x > -17f)
            yield return new WaitForSeconds(0);

        PoolManager.Instance.CoolObject(obstacle, type);
        obstacles.Remove(obstacle);
        obstaclesTypes.Remove(type);
    }

    public static void DeleteAllObstacles()
    {
        for (var i = 0; i < obstacles.Count; i++)
        {
            PoolManager.Instance.CoolObject(obstacles[i], obstaclesTypes[i]);
        }

        obstacles.Clear();
        obstaclesTypes.Clear();
    }
}
