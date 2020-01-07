﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipDetailPanel : BasicSubMenuPanel
{
    public enum subPanel
    {
        infor,lvUp,fix,
    }
    subPanel _csp = subPanel.infor;
    public subPanel currentSubPanel
    {
        get { return _csp; }
        set
        {
            if (_csp != value)
            {
                refreshPanel(_csp, value);
                _csp = value;
            }
        }
    }
    public SDEquipDetail equipDetail;
    public Transform inforPanel;
    public SDEquipImprove equipImprove;

    public override void whenOpenThisPanel()
    {
        base.whenOpenThisPanel();

    }

    public override void commonBackAction()
    {
        base.commonBackAction();
        homeScene.SubMenuClose();
    }
    public void btnToInfor()
    {
        currentSubPanel = subPanel.infor;
    }
    public void btnToLvUp()
    {
        currentSubPanel = subPanel.lvUp;
        equipImprove.currentImproveKind = SDEquipImprove.ImproveKind.exp;
        equipImprove.InitImprovePanel();
    }
    public void btnToFix()
    {
        currentSubPanel = subPanel.fix;
        equipImprove.currentImproveKind = SDEquipImprove.ImproveKind.fix;
        equipImprove.InitImprovePanel();
    }
    public void refreshPanel(subPanel oldPanel, subPanel newPanel)
    {
        if(newPanel == subPanel.infor)
        {
            UIEffectManager.Instance.showAnimFadeIn(inforPanel);
            UIEffectManager.Instance.hideAnimFadeOut(equipImprove.transform);
        }
        else if (oldPanel == subPanel.infor)
        {
            UIEffectManager.Instance.hideAnimFadeOut(inforPanel);
            UIEffectManager.Instance.showAnimFadeIn(equipImprove.transform);
        }
    }


}