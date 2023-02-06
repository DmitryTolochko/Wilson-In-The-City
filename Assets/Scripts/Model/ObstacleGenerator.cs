using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    private static List<GameObject> obstacles = new List<GameObject>();

    private void Update() 
    {
        if (Time.timeScale != 0 && 
        (obstacles.Count == 0 || obstacles.Count < 3 
        && obstacles[obstacles.Count - 1].transform.position.x <= 9))
            StartCoroutine(GetObstacle());
    }

    private IEnumerator GetObstacle()
    {
        var type = (PoolObjectType)UnityEngine.Random.Range(0, 8);
        var obstacle = PoolManager.Instance.GetPoolObject(type);
        obstacle.transform.position = new Vector2(17f, -4.78f);
        obstacle.SetActive(true);

        obstacles.Add(obstacle);
        while (obstacle.transform.position.x > -17f)
            yield return new WaitForSeconds(0);

        PoolManager.Instance.CoolObject(obstacle, type);
        obstacles.Remove(obstacle);
    }

    public static void DeleteAllObstacles()
    {
        var count = obstacles.Count;
        try
        {
        for (var i = 0; i < count; i++)
        {
            var script = obstacles[i].GetComponent<Obstacle>();
            PoolManager.Instance.CoolObject(obstacles[i], script.Type);
        }
        }
        catch (NullReferenceException)
        {
            return;
        }

        obstacles.Clear();
    }
}
