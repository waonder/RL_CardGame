﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameDataEditor;
using System;
public class UseTimeItem : MonoBehaviour
{
    public Image bgImg;
    public Image frame;
    public Text aboveText;
    public Text midText;
    public Text belowText;
    public Transform timeSlider;
    int wholeLastTime;
#if UNITY_EDITOR
    [DisplayName("该任务Id")]
#endif
    public string taskId;
    #region isEmpty
    bool _isempty;
    public bool isEmpty
    {
        get { return _isempty; }
        set { _isempty = value;
            if (emptyPanel)
            {
                emptyPanel.gameObject.SetActive(_isempty);
            }
        }
    }
    public Transform emptyPanel;
    #endregion
    #region isUnlocked
    bool _isunlocked;
    public bool isUnlocked
    {
        get { return _isunlocked; }
        set 
        {
            _isunlocked = value;
            lockedPanel?.gameObject.SetActive(!_isunlocked);
        }
    }
    public Transform lockedPanel;
    #endregion
    #region isFinished
    bool _isfinished;
    public bool isFinished
    {
        get { return _isfinished; }
        set
        {
            _isfinished = value;
            finishedPanel?.gameObject.SetActive(_isfinished);
        }
    }
    public Transform finishedPanel;
    #endregion
    public HospitalPanel HP
    {
        get { return GetComponentInParent<HospitalPanel>(); }
    }
    public Image statusImg;
    //public SDConstants.ItemType type = SDConstants.ItemType.End;

    public virtual void initTimeTask(GDEtimeTaskData task)
    {
        Debug.Log("Start_Init_Time_Task");
        if(task == null)
        {
            showEmptyPanel();return;
        }
        else
        {
            isEmpty = false;
        }
        taskId = task.taskId;
        string startTimeString = task.startTime;
        if (DateTime.TryParse(startTimeString, out DateTime starttime))
        {
            refreshTimeCondition();
        }
    }
    public void showEmptyPanel()
    {
        isEmpty = true;
    }
    public void showLockedPanel()
    {
        isUnlocked = false;
    }

    public virtual void refreshTimeCondition()
    {
        GDEtimeTaskData task = SDDataManager.Instance.GetTimeTaskById(taskId);
        if (task != null && DateTime.TryParse(task.startTime, out DateTime starttime))
        {
            if (!isFinished)
            {
                float useMinute = task.timeType * 1f;
                TimeSpan span = DateTime.Now - starttime;
                float s = Mathf.Min((float)(span.TotalMinutes) / useMinute, 1);
                timeSlider.localScale = new Vector3(s, 1, 1);
                if (s >= 1) { isFinished = true; }
            }
            else
            {
                timeSlider.localScale = Vector3.one;
            }
            

        }
    }

    public virtual void btnTapped()
    {
        if (isUnlocked)
        {
            HP.currentSickBedId = HP.getTaskIdFromIndex(transform.GetSiblingIndex());

            if (!isEmpty)
            {
                if (SDDataManager.Instance.AbandonTimeTask(taskId))
                {
                    showEmptyPanel();
                    HP.initHospital();
                }
                else
                {
                    if (SDDataManager.Instance.FinishTimeTask(taskId))
                    {
                        showEmptyPanel();
                        HP.initHospital();
                    }
                }
            }
            else
            {
                //槽位为空
                HP.initHospital();
            }
        }
        else
        {
            //槽位未解锁

        }
        HP.currentSelectedBedIndex = this.transform.GetSiblingIndex();
    }
}
