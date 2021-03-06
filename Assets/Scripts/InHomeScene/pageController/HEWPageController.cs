﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameDataEditor;
using System.Linq;
/// <summary>
/// 通用英雄、装备、武器选择项控制类
/// </summary>
public class HEWPageController : MonoBehaviour
{
    public Transform SItem;
    public List<SingleItem> items = new List<SingleItem>();
    public SDConstants.ItemType CurrentType = SDConstants.ItemType.End;
    public int currentHeroHashcode;
    public EquipPosition EquipPos = EquipPosition.End;
    public SDConstants.ConsumableType ConsumableType;
    public int pageIndex;
    public int itemCount;
    public SingleItem[] AllItemSlots;
    //
    SDEquipSelect equipSelect;
    public ScrollRect scrollRect;
    private int singleHeroHeight = 250;
    private int singleEquipHeight = 250;
    [HideInInspector]
    public int PerPageMaxVolume = 48;
    
    [ContextMenu("INIT_ALL_ITEM_SLOTS")]
    public void Init_AllItemSlots()
    {
        AllItemSlots = scrollRect.content.GetComponentsInChildren<SingleItem>();
        PerPageMaxVolume = AllItemSlots.Length;
    }

    public void ResetPage()
    {
        pageIndex = 0;
        for(int i = 0; i < items.Count; i++)
        {
            Destroy(items[i].gameObject);
        }
        items = new List<SingleItem>();
        scrollRectReset();
    }

    public void ResetPageWithoutDestroy()
    {
        pageIndex = 0;
        for (int i = 0; i < items.Count; i++)
        {
            items[i].SetSelfAsBg();
        }
        items = new List<SingleItem>();
        scrollRectReset();
    }
    public void Select_None()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].isSelected = false;
        }
    }
    public void scrollRectReset()
    {
        scrollRect.content.anchoredPosition = Vector2.zero;

    }
    /// <summary>
    /// 构建角色房当前拥有物件
    /// </summary>
    /// <param name="type"></param>
    /// <param name="ePos"></param>
    public void ItemsInit(SDConstants.ItemType type)
    {
        Debug.Log("不同类型列表会进行刷新");
        itemCount = 0;
        pageIndex = 0;
        CurrentType = type;
        if (type == SDConstants.ItemType.Hero)
        {
            SDConstants.HeroSelectType st = SDGameManager.Instance.heroSelectType;
            if (st == SDConstants.HeroSelectType.All)
            {
                ShowHeroesOwned();
            }
            else if (st == SDConstants.HeroSelectType.Battle)
            {
                ShowHeroesOwnedToBattle();
            }
            else if (st == SDConstants.HeroSelectType.Mission)
            {

            }
            else if (st == SDConstants.HeroSelectType.Recruit)
            {

            }
            else if (st == SDConstants.HeroSelectType.Wake)
            {

            }
            else if(st == SDConstants.HeroSelectType.Dying)
            {
                ShowHeroesownedNeedHospital();
            }
        }
        else if(type == SDConstants.ItemType.Equip)
        {
            if (EquipPos != EquipPosition.End)
            {
                showOnePosEquipsOwned(EquipPos);
            }
            else
            {
                showAllEquipsOwned();
            }
        }
        else if(type == SDConstants.ItemType.Consumable)
        {
            if(ConsumableType == SDConstants.ConsumableType.material)
            {
                showMaterialOwned();
            }
            else if(ConsumableType == SDConstants.ConsumableType.prop)
            {
                showPropsOwned(true);
            }
            else
            {
                showConsumableOwned(true);
            }
        }
        else if(type == SDConstants.ItemType.Goddess)
        {

        }
        else if(type == SDConstants.ItemType.Badge)
        {

        }
        else if(type == SDConstants.ItemType.Enemy)
        {
            showEnemyInIllustrate();
        }
        else if(type == SDConstants.ItemType.Quest)
        {

        }
        else if(type == SDConstants.ItemType.Rune)
        {
            showRuneOwned();
        }
        else if(type == SDConstants.ItemType.NPC)
        {
            showNPCOwned();
        }
        //
        foreach(SingleItem s in items)
        {
            s.type = CurrentType;
        }
    }
    public void ItemsInit(SDConstants.ItemType type,EquipPosition pos)
    {
        EquipPos = pos;
        ItemsInit(type);
    }
    public void ItemsInit(SDConstants.ItemType type,SDConstants.ConsumableType ctype)
    {
        ConsumableType = ctype;ItemsInit(type);
    }

    public void showEmptyItem(SDConstants.ItemType Type)
    {
        Transform s = Instantiate(SItem) as Transform;
        s.transform.SetParent(scrollRect.content);
        s.transform.localScale = Vector3.one;
        s.gameObject.SetActive(true);
        SingleItem _s = s.GetComponent<SingleItem>();
        _s.sourceController = this;
        _s.index = -1;
        _s.isEmpty = true;
    }

    #region itemInitContent
    #region HeroClass
    public void ShowHeroesOwned()
    {
        ResetPage();
        List<GDEHeroData> heroes = SDDataManager.Instance.PlayerData.herosOwned;
        //int index = 0;
        itemCount = heroes.Count;
        //int maxHeroNum = SDConstants.EquipNumPerPage;
        for(int i = 0; i < itemCount; i++)
        {
            Transform s = Instantiate(SItem) as Transform;
            s.transform.SetParent(scrollRect.content);
            s.transform.localScale = Vector3.one;
            s.gameObject.SetActive(true);
            SingleItem _s = s.GetComponent<SingleItem>();
            _s.sourceController = this;
            _s.index = i;
            _s.initHero(heroes[i]);
            items.Add(_s);
        }
    }
    public void ShowHeroesOwnedToBattle()
    {
        ResetPage();

        List<GDEHeroData> heroes = SDDataManager.Instance.PlayerData.herosOwned;
        itemCount = heroes.Count;
        //if (itemCount <= 0) equipSelect.refreshEmptyEquipPanel(true);
        //else equipSelect.refreshEmptyEquipPanel(false);
        for (int i = 0; i < itemCount; i++)
        {
            Transform s = Instantiate(SItem) as Transform;
            s.transform.SetParent(scrollRect.content);
            s.transform.localScale = Vector3.one;
            s.gameObject.SetActive(true);
            SingleItem _s = s.GetComponent<SingleItem>();
            _s.initBattleHero(heroes[i]);
            _s.sourceController = this;
            _s.index = i;
            if (heroes[i].status == 1)
            {
                _s.isSelected = true;
            }
            else
            {
                _s.isSelected = false;
            }

            items.Add(_s);
        }
    }
    public void ShowHeroesownedNeedHospital()
    {
        ResetPage();
        List<GDEHeroData> heroes = SDDataManager.Instance.PlayerData.herosOwned;
        List<GDEHeroData> results = new List<GDEHeroData>();
        //
        for(int i = 0; i < heroes.Count; i++)
        {
            
            if (heroes[i].status == 2 || heroes[i].status == 3)//该角色已遭重创(包括正在救治中)
            {
                results.Add(heroes[i]);
            }
        }
        for(int i = 0; i < heroes.Count; i++)
        {
            if(heroes[i].status!=2&& heroes[i].status != 3)//该角色未遭重创且不在治疗室中
            {
                results.Add(heroes[i]);
            }
        }
        //
        for (int i = 0; i < results.Count; i++)
        {
            Transform s = Instantiate(SItem) as Transform;
            s.transform.SetParent(scrollRect.content);
            s.transform.localScale = Vector3.one;
            s.gameObject.SetActive(true);
            SingleItem _s = s.GetComponent<SingleItem>();
            _s.sourceController = this;
            _s.index = i;
            _s.initInjuriedHero(results[i]);
            items.Add(_s);
        }
    }
    #endregion
    #region EquipClass
    public void showAllEquipsOwned()
    {
        List<GDEEquipmentData> allEquips = SDDataManager.Instance.getAllOwnedEquips();
        //int heroHashcode = SDDataManager.Instance.getherohash
        itemCount = allEquips.Count;
        for(int i = 0; i < itemCount; i++)
        {
            Transform s = Instantiate(SItem) as Transform;
            s.transform.SetParent(scrollRect.content);
            s.transform.localScale = Vector3.one;
            s.gameObject.SetActive(true);
            SingleItem _s = s.GetComponent<SingleItem>();
            //_s.initEquip(allEquips[i].equipId, allEquips[i].upLv);
            _s.sourceController = this;
            _s.index = i;
            _s.initEquip(allEquips[i]);
            items.Add(_s);
        }
    }
    public void showOnePosEquipsOwned(EquipPosition pos)
    {
        ResetPageWithoutDestroy();
        //ResetPage();
        List<GDEEquipmentData> allEquips;
        if (currentHeroHashcode>0&& SDDataManager.Instance.getHeroIdByHashcode
            (currentHeroHashcode) != null)
        {
            string id = SDDataManager.Instance.getHeroIdByHashcode(currentHeroHashcode);
            allEquips = SDDataManager.Instance.GetPosOwnedEquipsByCareer
                (pos, id,true);
        }
        else
        {
            allEquips = SDDataManager.Instance.getOwnedEquipsByPos(pos,true);
        }

        itemCount = allEquips.Count;
        if (itemCount <= 0)
        {
            foreach (SingleItem s in AllItemSlots)
            {
                s.SetSelfAsBg();
            }
        }
        int endNumInVolume = Mathf.Max(0, itemCount - PerPageMaxVolume * pageIndex);
        int startIndex = PerPageMaxVolume * pageIndex;

        for(int i = 0; i < PerPageMaxVolume; i++)
        {
            if (i >= endNumInVolume)
            {
                AllItemSlots[i].SetSelfAsBg();
                continue;
            }
            else
            {
                SingleItem _s = AllItemSlots[i];
                _s.sourceController = this;
                _s.initEquip(allEquips[i + startIndex]);
                items.Add(_s);
            }
        }
    }
    #endregion
    #region GoddessClass
    public void showGoddessOwned(bool onlyOwned = true)
    {
        ResetPage();
        List<GDEgoddessData> goddesses = new List<GDEgoddessData>();
        if (onlyOwned)
        {
            goddesses = SDDataManager.Instance.PlayerData.goddessOwned;
            itemCount = goddesses.Count;
            for (int i = 0; i < itemCount; i++)
            {
                Transform s = Instantiate(SItem) as Transform;
                s.transform.SetParent(scrollRect.content);
                s.transform.localScale = Vector3.one;
                s.gameObject.SetActive(true);
                SingleItem _s = s.GetComponent<SingleItem>();
                _s.sourceController = this;
                _s.index = i;
                _s.initGoddess(goddesses[i]);
                items.Add(_s);
            }
        }
    }
    #endregion
    #region PropClass
    public void showPropsOwned(bool showTaken = false)
    {
        ResetPage();
        List<GDEItemData> props = SDDataManager.Instance.getPropsOwned;
        itemCount = props.Count;
        for(int i = 0; i < itemCount; i++)
        {
            Transform s = Instantiate(SItem) as Transform;
            s.transform.SetParent(scrollRect.content);
            s.transform.localScale = Vector3.one;
            s.gameObject.SetActive(true);
            SingleItem _s = s.GetComponent<SingleItem>();
            _s.sourceController = this;
            _s.index = i;
            _s.initConsumable(props[i], showTaken);
            items.Add(_s);
        }
    }
    #endregion
    #region MaterialClass
    public void showMaterialOwned()
    {
        ResetPage();
        List<GDEItemData> ms = SDDataManager.Instance.getMaterialsOwned;
        itemCount = ms.Count;
        for(int i = 0; i < itemCount; i++)
        {
            Transform s = Instantiate(SItem) as Transform;
            s.transform.SetParent(scrollRect.content);
            s.transform.localScale = Vector3.one;
            s.gameObject.SetActive(true);
            SingleItem _s = s.GetComponent<SingleItem>();
            _s.sourceController = this;
            _s.index = i;
            _s.initConsumable(ms[i]);
            items.Add(_s);
        }
    }
    #endregion
    #region ConsumableClass
    public void showConsumableOwned(bool showTaken = false)
    {
        ResetPage();
        List<GDEItemData> props = SDDataManager.Instance.PlayerData.consumables;
        itemCount = props.Count;
        for (int i = 0; i < itemCount; i++)
        {
            Transform s = Instantiate(SItem) as Transform;
            s.transform.SetParent(scrollRect.content);
            s.transform.localScale = Vector3.one;
            s.gameObject.SetActive(true);
            SingleItem _s = s.GetComponent<SingleItem>();
            _s.sourceController = this;
            _s.index = i;
            _s.initConsumable(props[i], showTaken);
            items.Add(_s);
        }
    }
    #endregion
    #region RuneClass
    public void showRuneOwned()
    {
        ResetPage();
        List<GDERuneData> runes = SDDataManager.Instance.getAllRunesOwned;

        itemCount = runes.Count;
        for(int i = 0; i < itemCount; i++)
        {
            Transform s = Instantiate(SItem) as Transform;
            s.transform.SetParent(scrollRect.content);
            s.transform.localScale = Vector3.one;
            s.gameObject.SetActive(true);
            SingleItem _s = s.GetComponent<SingleItem>();
            _s.sourceController = this;
            _s.index = i;
            _s.initRuneInPage(runes[i]);
            items.Add(_s);
        }
    }
    #endregion
    #region NPCInBagClass
    public void showNPCOwned(bool cantShowInBag = false)
    {
        ResetPage();
        List<GDENPCData> baseall = SDDataManager.Instance.PlayerData.NPCList;
        if (!cantShowInBag)
        {
            baseall = baseall.FindAll(x => x.ShowInBag);
        }
        itemCount = baseall.Count;
        for(int i = 0; i < itemCount; i++)
        {
            Transform s = Instantiate(SItem) as Transform;
            s.transform.SetParent(scrollRect.content);
            s.transform.localScale = Vector3.one;
            SingleItem _s = s.GetComponent<SingleItem>();
            _s.sourceController = this;

            items.Add(_s);
        }
    }
    #endregion
    #region EnemyClass
    public void showEnemyInIllustrate()
    {
        ResetPageWithoutDestroy();
        List<GDEItemData> all = SDDataManager.Instance.GetAllEnemiesPlayerSaw;
        itemCount = all.Count;
        if (itemCount <= 0)
        {
            foreach(SingleItem s in AllItemSlots) { s.SetSelfAsBg(); }
        }
        int endNumInVolume = Mathf.Max(0, itemCount - PerPageMaxVolume * pageIndex);
        int startIndex = PerPageMaxVolume * pageIndex;

        for (int i = 0; i < PerPageMaxVolume; i++)
        {
            if (i >= endNumInVolume)
            {
                AllItemSlots[i].SetSelfAsBg();
                continue;
            }
            else
            {
                SingleItem _s = AllItemSlots[i];
                _s.sourceController = this;
                _s.initEnemy(all[i + startIndex]);
                items.Add(_s);
            }
        }
    }
    #endregion
    #endregion

    #region dropInitContent
    /// <summary>
    /// 构建战利品列表
    /// </summary>
    /// <param name="type"></param>
    public void DropItemsInit(SDConstants.ItemType type)
    {
        Debug.Log("获取当前所有战利品信息");
        itemCount = 0;
        CurrentType = type;
        if (CurrentType == SDConstants.ItemType.End)//显示全部
        {

        }
        else
        {

        }
    }
    public void showAllDropItems()
    {
        GameController GameC = GetComponentInParent<GameController>();
        itemCount = GameC.allDropsGet.Count;
        for(int i = 0; i < itemCount; i++)
        {
            GDEItemData M = GameC.allDropsGet[i];
            Transform s = Instantiate(SItem) as Transform;
            s.transform.SetParent(scrollRect.content);
            s.transform.localScale = Vector3.one;
            s.gameObject.SetActive(true);
            SingleItem _s = s.GetComponent<SingleItem>();
            _s.sourceController = this;
            _s.index = i;
            _s.initDrop(M);
            items.Add(_s);
        }
    }
    #endregion
}
