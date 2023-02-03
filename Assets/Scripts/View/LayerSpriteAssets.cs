using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSpriteAssets : MonoBehaviour
{
    public static LayerSpriteAssets Instance {get; private set;}

    public List<Sprite> CloudsLayerSprites = new List<Sprite>();
    public List<Sprite> CityLayerSprites = new List<Sprite>();
    public List<Sprite> FirstLayerSprites = new List<Sprite>();
    public List<Sprite> SecondLayerSprites = new List<Sprite>();
    public List<Sprite> ThirdLayerSprites = new List<Sprite>();
    public List<Sprite> FourthLayerSprites = new List<Sprite>();

    private void Awake()
    {
        Instance = this;
    }

    public static Sprite GetRandomSpriteOfType(LayerTypes type)
    {
        return type switch 
        {
            LayerTypes.CloudsLayer => Instance.CloudsLayerSprites[Random.Range(0, Instance.CloudsLayerSprites.Count)],
            LayerTypes.FirstLayer => Instance.FirstLayerSprites[Random.Range(0, Instance.FirstLayerSprites.Count)],
            LayerTypes.SecondLayer => Instance.SecondLayerSprites[Random.Range(0, Instance.SecondLayerSprites.Count)],
            LayerTypes.ThirdLayer => Instance.ThirdLayerSprites[Random.Range(0, Instance.ThirdLayerSprites.Count)],
            LayerTypes.FourthLayer => Instance.FourthLayerSprites[Random.Range(0, Instance.FourthLayerSprites.Count)],
        };
    }

    public static Sprite GetAnotherSprite(Sprite current)
    {
        return current == Instance.CityLayerSprites[0] 
        ? Instance.CityLayerSprites[1]
        : Instance.CityLayerSprites[0];
    }
}
