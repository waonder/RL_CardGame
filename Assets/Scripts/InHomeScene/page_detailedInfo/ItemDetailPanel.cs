﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailPanel : MonoBehaviour
{
    public string id;
    public int hashcode;
    //
    public Image itemImg;
    public Image bgImg;
    public Image frameImg;
#if UNITY_EDITOR
    [ReadOnly]
#endif
    public SDConstants.ItemType itemType;
    public Text itemNameText;
    public Text itemExtraText;
    public Text itemDescText;
    public Button btnToResolve;
    //
    public virtual void BtnTappped()
    {

    }
}
