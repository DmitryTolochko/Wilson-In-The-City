using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItemsGenerator : MonoBehaviour
{
    private static List<GameObject> items = new List<GameObject>();
    private bool timeElapsed = true;

    private void Update() 
    {
        if (Time.timeScale != 0 && 
        timeElapsed && 
        (items.Count == 0 || items.Count < 2 
        && items[items.Count - 1].transform.position.x <= Random.Range(8, 14)))
            StartCoroutine(GetItem());
    }

    private IEnumerator GetItem()
    {
        StartCoroutine(Wait());
        var num = Random.Range(0, 10);
        
        var type = num == 0 && GameModel.ScoreCurrent > 20 ? PoolObjectType.Crystal : PoolObjectType.Money;
        var item = PoolManager.Instance.GetPoolObject(type);
        item.transform.localPosition = new Vector2(17f, -4.78f);
        item.SetActive(true);

        items.Add(item);
        var script = item.GetComponent<CollectableItem>();
        while (!(script.IsInvisible || script.IsCollected))
            yield return new WaitForSeconds(0);

        if (script.IsCollected)
        {
            GameModel.CrystalsCountCurrent += script.Type == PoolObjectType.Crystal ? 1 : 0;
            GameModel.MoneyCountCurrent += script.Type == PoolObjectType.Money ? 1 : 0;
        }

        script.IsCollected = false;
        script.IsRaised = false;
        script.IsInvisible = false;
        PoolManager.Instance.CoolObject(item, type);
        items.Remove(item);
    }

    public static void DeleteAllItems()
    {
        var count = items.Count;
        for (var i = 0; i < count; i++)
        {
            var script = items[i].GetComponent<CollectableItem>();
            script.IsRaised = false;
            script.IsInvisible = false;
            script.IsCollected = false;
            PoolManager.Instance.CoolObject(items[i], script.Type);
        }

        items.Clear();
    }

    private IEnumerator Wait()
    {
        timeElapsed = false;
        yield return new WaitForSeconds(Random.Range(0, 8));
        timeElapsed = true;
    }
}