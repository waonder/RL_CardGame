﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameDataEditor;

public class PhysicalBuffPanel : BasicSubMenuPanel
{
    public override void whenOpenThisPanel()
    {
        base.whenOpenThisPanel();
    }
    public override void commonBackAction()
    {
        base.commonBackAction();
        homeScene.SubMenuClose();
    }

}
