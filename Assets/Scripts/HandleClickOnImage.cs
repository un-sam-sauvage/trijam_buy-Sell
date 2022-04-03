using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleClickOnImage : MonoBehaviour
{
    public List<Tiles> tilesPool;
    public GameObject obj;
    public List<Image> images;

    public void Start(){
        foreach (Image image in images)
        {
            Tiles tile = tilesPool[Random.Range(0,tilesPool.Count)];
            image.sprite = tile.icon;
            image.gameObject.GetComponent<WhichObjectContainsThisImage>().obj = tile.obj;
        }
    }
    public void OnClick(WhichObjectContainsThisImage objClicked){
        GetComponent<HousePlacer>().isHouseSelected = true;
        obj = objClicked.obj;
        for (int i = 1; i < images.Count; i++)
        {
            images[i-1].sprite = images[i].sprite;
            images[i-1].GetComponent<WhichObjectContainsThisImage>().obj = images[i].GetComponent<WhichObjectContainsThisImage>().obj;
        }
        Tiles tile = tilesPool[Random.Range(0,tilesPool.Count)];
        images[images.Count-1].sprite = tile.icon;
        images[images.Count-1].GetComponent<WhichObjectContainsThisImage>().obj = tile.obj;
    }
}

[System.Serializable]
public class Tiles{
    public Sprite icon;
    public GameObject obj;
}
