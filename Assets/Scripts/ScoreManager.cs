using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    [SerializeField] private List<Sprite> collectibles;

    [SerializeField] private GameObject collectiblesUI;

    public void ShowCollectibles(bool show){
        collectiblesUI.SetActive(show);
    }

    public void AddCollectible(string collectibleName)
    {
        switch (collectibleName.Substring(0,12))
        {
            case "Collectible1":
                CreateImage(1,-70, new Vector2(200,100));
                break;
            case "Collectible2":
                CreateImage(2,-70, new Vector2(200,100));
                break;
            case "Collectible3":
                CreateImage(3,-70, new Vector2(200,200));
                break;
            case "Collectible4":
                CreateImage(4,-70, new Vector2(200,100));
                break;
            case "Collectible5":
                CreateImage(5,-70, new Vector2(250,150));
                break;
            default:
                break;
        }
    }

    private void CreateImage(int collectibleNumber, int xRange, Vector2 size){
        int collectiblesAlreadyPicked = collectiblesUI.transform.Find(collectibleNumber.ToString()).childCount;
        GameObject newCollectibleUI = new GameObject();
        Image newCollectibleImage = newCollectibleUI.AddComponent<Image>();
        newCollectibleImage.sprite = collectibles[collectibleNumber-1];
        newCollectibleUI.GetComponent<RectTransform>().SetParent(collectiblesUI.transform.Find(collectibleNumber.ToString()));
        newCollectibleUI.GetComponent<RectTransform>().sizeDelta = size;
        newCollectibleUI.name = "Collectible"+collectibleNumber+"Image";
        newCollectibleUI.GetComponent<RectTransform>().anchoredPosition = Vector2.zero + new Vector2(xRange,0) * collectiblesAlreadyPicked;
    }
}
