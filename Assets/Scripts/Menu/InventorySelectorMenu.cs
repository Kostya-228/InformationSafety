﻿using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class InventorySelectorMenu : MonoBehaviour
{
    [SerializeField] private Invertory monoInventory;
    [SerializeField] private BoxInvertory boxInvertory;


    [SerializeField] private Animation animationBase;
    [SerializeField] private Animation animationBox;

    private bool isBoxOpened;
    private List<AssetItem> toReturn;

    public void OpenCloseMonoInvertory()
    {
        if (!monoInventory.IsOpened)
            animationBase["Invertoty"].speed = 1;
        else{
            animationBase["Invertoty"].speed = -1;
            animationBase["Invertoty"].time = animationBase["Invertoty"].length;
        }
        //animationBase.Play("InvertotyClose");
        // else animationBase.Play("InvertotyOpen");
        animationBase.Play("Invertoty");
        monoInventory.IsOpened = !monoInventory.IsOpened;
    }

    public async Task<List<AssetItem>> OpenBox(List<AssetItem> assetItems)
    {
        ClickController.GlobalEnabled = false;
        boxInvertory.gameObject.SetActive(true);
        boxInvertory.Render(assetItems);
        animationBox["InventoryBox"].speed = 1;
        animationBox.Play("InventoryBox");
        isBoxOpened = true;
        while (isBoxOpened) await Task.Yield();
        return toReturn;
    }

    public async void CloseBox()
    {
        toReturn = boxInvertory.CollectBoxRest();
        isBoxOpened = false;
        animationBox["InventoryBox"].speed = -1;
        animationBox["InventoryBox"].time = animationBox["InventoryBox"].length;
        animationBox.Play("InventoryBox");
        while (animationBox.isPlaying) await Task.Yield();
        boxInvertory.gameObject.SetActive(false);
        ClickController.GlobalEnabled = true;
    }
}
