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
        && items[items.Count - 1].transform.position.x <= Random.Range(9, 14)))
            StartCoroutine(GetItem());
    }

    private IEnumerator GetItem()
    {
        StartCoroutine(Wait());
        // var num = Random.Range(0, 10);
        
        // var type = num == 0 && GameModel.ScoreCurrent > 20 ? PoolObjectType.Crystal : PoolObjectType.Money;
        // type = num > 6 ? PoolObjectType.Clock : type;
        var type = GetItemType();
        var item = PoolManager.Instance.GetPoolObject(type);
        item.transform.localPosition = new Vector2(17f, 0f);
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

    private PoolObjectType GetItemType()
    {
        if (GameModel.ScoreCurrent <= 100)
            return PoolObjectType.Money;
        
        if (GameModel.ScoreCurrent > 100 && GameModel.ScoreCurrent <= 200)
        {
            return Random.Range(0, 10) == 0 ? PoolObjectType.Crystal : PoolObjectType.Money;
        }

        var num = Random.Range(1, 11);

        if (num > 8)
            return PoolObjectType.Clock;
        else if (num == 1)
            return PoolObjectType.Crystal;
        else
            return PoolObjectType.Money;
    }
}
