﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using I2.Loc;
using GameDataEditor;
using System;
using System.Net;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.U2D;
using Spine.Unity;
using Spine;
using Unity.Collections;
using Unity.Jobs;

/// <summary>
/// 数据管理类，包括游戏内所有记录数据
/// </summary>
public class SDDataManager : PersistentSingleton<SDDataManager>
{
    public GDEPlayerData PlayerData;
    public GDESettingData SettingData;
    public GDEResidentMovementData ResidentMovementData;

    public int heroNum = 0;
    public int equipNum = 0;
    public int slaveNum = 0;
    public int runeNum = 0;
    public List<string> AllStrs;
    public List<string> AllStrs2;

    public DateTime OpenTime;
    public override void Awake()
    {
        base.Awake();
        if (Instance == this)
        {
            GDEDataManager.Init("gde_data");
            SetupDatas();
            gameObject.AddComponent<ResourceManager>();
        }
    }
    private void Start()
    {
        SetupDatas();
        //StartCoroutine(IELoadFileAsynchronously());
    }
    public void SetupDatas()
    {
        PlayerData = new GDEPlayerData(GDEItemKeys.Player_CurrentPlayer);
        SettingData = new GDESettingData(GDEItemKeys.Setting_Setting);
        PlayerData.achievementData = new GDEAchievementData(GDEItemKeys.Achievement_newAchievement);
        ResidentMovementData
            = new GDEResidentMovementData(GDEItemKeys.ResidentMovement_EmptyResidentMovement);

        if (PlayerData.herosOwned != null)
        {

        }
        if (PlayerData.heroesTeam != null)
        {
            if (PlayerData.heroesTeam.Count == 0)
            {
                for (int i = 0; i < SDConstants.MaxSelfNum; i++)
                {
                    GDEunitTeamData team = new GDEunitTeamData(GDEItemKeys.unitTeam_emptyHeroTeam)
                    {
                        id = string.Empty,
                        goddess = string.Empty,
                        badge = 0,
                    };
                    PlayerData.heroesTeam.Add(team);
                }
                PlayerData.Set_heroesTeam();
            }
            //if(PlayerData.)
        }
        OpenTime = DateTime.Now;


        //setupUnlockNum();
        //setupDecorations();
        //setupEmployers();
        //setupGoods();
        //setupNpcs();
        //setupRelics();
        //setupJobs();
        //setupAchievement();
        //checkCareerUnlocked();
        //if(SDDataManager.Instance.SettingData.)

    }
    #region Infor

    #region GameChapterInfor
    public void addUnlockedChapter()
    {
        //
    }
    #endregion
    #region HeroInfor
    public void simpleAddHero(string id)
    {
        heroNum++;
        //
        add_Item(id);
        //
        GDEHeroData hero = new GDEHeroData(GDEItemKeys.Hero_BasicHero)
        {
            id = id
,
            hashCode = Instance.heroNum
,
            status = 0
,
            starNumUpgradeTimes = 1
,
            exp = 0
        };
        HeroInfo info = getHeroInfoById(id);
        //skill


        Instance.PlayerData.herosOwned.Add(hero);
        Instance.PlayerData.Set_herosOwned();
    }
    /// <summary>
    /// 添加英雄
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public int addHero(string id)
    {
        heroNum++;
        //
        add_Item(id);
        //
        GDEHeroData hero = new GDEHeroData(GDEItemKeys.Hero_BasicHero)
        {
            id = id
,
            hashCode = Instance.heroNum
,
            status = 0
,
            starNumUpgradeTimes = 1
,
            exp = 0
            ,
            teamIdBelongTo = string.Empty
        };
        HeroInfo info = getHeroInfoById(id);
        //sex
        if (info.Sex == CharacterSex.Unknown)
        {
            hero.sex = UnityEngine.Random.Range(1, 3);
        }
        else hero.sex = (int)info.Sex;
        //ral
        RoleAttributeList ral = RoleAttributeList.RandomSet
        (
        new ScopeInt(-15, 15)//三项Barchart
        , new ScopeInt(-5, 5)//四项攻防
        , new ScopeInt(-1, 1)//其他
        , new ScopeInt(-10, 10)//抗性
        );
        hero.RoleAttritubeList = ral.TurnIntoGDEData;
        //skills
        if(!info.HaveExclusiveSkills)
        {
            List<OneSkill> all = SkillDetailsList.WriteOneSkillList(info.ID);
            List<OneSkill> allBs = all.FindAll(x => !x.isOmegaSkill);
            List<OneSkill> allOs = all.FindAll(x => x.isOmegaSkill);
            if (info.Rarity < 2)
            {
                hero.a_skill0 = new GDEASkillData(GDEItemKeys.ASkill_normalAttack)
                {
                    Id = allBs[UnityEngine.Random.Range(0, allBs.Count)].skillId,
                    Lv = 0,
                };
                hero.a_skill1 = null;
            }
            else
            {
                int[] flags = RandomIntger.NumArrayReturn(2,allBs.Count);
                hero.a_skill0 = new GDEASkillData(GDEItemKeys.ASkill_normalAttack)
                {
                    Id = allBs[flags[0]].skillId,
                    Lv = 0,
                };
                hero.a_skill1 = new GDEASkillData(GDEItemKeys.ASkill_normalAttack)
                {
                    Id = allBs[flags[1]].skillId,
                    Lv = 0,
                };
            }
            hero.a_skillOmega = new GDEASkillData(GDEItemKeys.ASkill_normalAttack)
            {
                Id = allOs[UnityEngine.Random.Range(0, allOs.Count)].skillId,
                Lv = 0,
            };
        }
        else
        {
            hero.a_skill0 = new GDEASkillData(GDEItemKeys.ASkill_normalAttack)
            {
                Id = info.Skill0Info.ID,
                Lv = 0,
            };
            hero.a_skill1 = new GDEASkillData(GDEItemKeys.ASkill_normalAttack)
            {
                Id = info.Skill1Info.ID,
                Lv = 0,
            };
            hero.a_skillOmega = new GDEASkillData(GDEItemKeys.ASkill_normalAttack)
            {
                Id = info.SkillOmegaInfo.ID,
                Lv = 0,
            };
        }

        //animImg
        int level = getHeroLevelById(id);
        
        hero.AnimData = new GDEAnimData(GDEItemKeys.Anim_EmptyAnim)
        {
            isRare = false,
        };
        if (info.UseSpineData)
        {
            SkeletonDataAsset dataAsset = info.SpineData.SkeletonData;
            List<string> all = dataAsset.GetSkeletonData(false).Skins
                .Select(x => x.Name).ToList();
            Debug.Log(dataAsset.name + all.Count);
            string s;
            if (string.IsNullOrEmpty(info.SpineData.Skin)) s = "default";
            else s = info.SpineData.Skin;
            Debug.Log("SkinName: " + s );
            Debug.Log(hero.AnimData.skinName);
            if (string.IsNullOrEmpty(info.SpineData.Skin))
            {
                hero.AnimData.isRare = false;
                int gender = hero.sex - 1;
                all = all.FindAll(x => x.Contains("normal") && x.Substring(x.Length - 1) == gender.ToString());
                if (all.Count <= 0) hero.AnimData.skinName = "default";
                else
                {
                    hero.AnimData.skinName = all[UnityEngine.Random.Range(0, all.Count)];
                }
            }
            else
            {
                hero.AnimData.isRare = true;
                hero.AnimData.skinName = null;
            }
            Debug.Log("SkinName: " + info.SpineData.Skin + "___" + hero.AnimData.skinName);
        }
        //
        if (!Instance.PlayerData.herosOwned.Exists(x => x.id == id)
            && info.Rarity >= 2)
        {
            hero.locked = true;
        }
        else hero.locked = false;

        //
        Instance.PlayerData.herosOwned.Add(hero);
        Instance.PlayerData.Set_herosOwned();
        return hero.hashCode;
    }

    public HeroRace getHeroRaceByIndex(int index)
    {
        List<HeroRace> all = Resources.LoadAll<HeroRace>("ScriptableObjects/heroes/heroRace").ToList();
        return all.Find(x => x.Index == index);
    }
    public Sprite heroBoxFrameByRarity(int rarity)
    {
        string n = string.Format("heroBoxFrame{0:D1}", rarity);
        return atlas_rarity.GetSprite(n);
    }
    public string getHeroSkinNameInSkeleton(int hashcode)
    {
        GDEHeroData hero = getHeroByHashcode(hashcode);
        HeroInfo info = getHeroInfoById(hero.id);
        if (hero.AnimData.isRare)
        {
            if (info.UseSpineData)
            {
                return info.SpineData.Skin;
            }
            else { return null; }
        }
        else
        {
            return hero.AnimData.skinName;
        }
    }
    public int getTempleByType(Job job, AttributeData templeType)
    {
        return getTempleByJob(job).AllAttributeData[(int)templeType];
    }
    public RoleAttributeList getTempleByJob(Job job)
    {
        RoleAttributeList RL = new RoleAttributeList();
        if (job == Job.Fighter)
        {
            RL_ChangeByList(RL, PlayerData.temple_fighter);
            return RL;
        }
        else if (job == Job.Ranger)
        {
            RL_ChangeByList(RL, PlayerData.temple_ranger);
            return RL;
        }
        else if (job == Job.Priest)
        {
            RL_ChangeByList(RL, PlayerData.temple_priest);
            return RL;
        }
        else if (job == Job.Caster)
        {
            RL_ChangeByList(RL, PlayerData.temple_caster);
            return RL;
        }
        return RL;
    }
    void RL_ChangeByList(RoleAttributeList RL, List<int> change, bool IsForAD = true)
    {
        if (IsForAD)
        {
            for (int i = 0; i < (int)AttributeData.End; i++)
            {
                if (i < change.Count)
                {
                    RL.AllAttributeData[i] += change[i];
                }
            }
        }
        else
        {
            for (int i = 0; i < (int)StateTag.End; i++)
            {
                if (i < change.Count)
                {
                    RL.AllResistData[i] += change[i];
                }
            }
        }
    }
    public string getHeroIdByHashcode(int Hashcode)
    {
        foreach (GDEHeroData item in PlayerData.herosOwned)
        {
            if (item.hashCode == Hashcode)
            {
                return item.id;
            }
        }
        return string.Empty;
    }
    public int getHeroOriginalBattleForceByHashCode(int Hashcode)
    {
        GDEHeroData hero = getHeroByHashcode(Hashcode);
        //
        ROHeroData heroData = getHeroOriginalDataById(hero.id);
        RoleAttributeList ral = heroData.ExportRAL;
        //
        return ral.BattleForce;
    }
    public List<GDEHeroData> getHerosListOwned()
    {
        return PlayerData.herosOwned;
    }
    public GDEHeroData GetHeroOwnedByHashcode(int hashCode)
    {
        foreach (GDEHeroData h in PlayerData.herosOwned)
        {
            if (h.hashCode == hashCode)
            {
                return h;
            }
        }
        return null;
    }
    public ROHeroData getHeroDataByID(string id, int starNumUpGradeTimes)
    {
        HeroInfo info = getHeroInfoById(id);
        ROHeroData dal = new ROHeroData();
        dal.Info = info;
        dal.starNumUpGradeTimes = starNumUpGradeTimes;
        dal.CRIDmg = 125 + 25 * dal.starNum;
        dal.DmgReduction = 0;
        dal.DmgReflection = 0;
        dal.GoldRate = dal.RewardRate = 0;
        dal.BarChartRegendPerTurn = RoleBarChart.zero;
        return dal;
    }
    public ROHeroData getHeroOriginalDataById(string id)
    {
        HeroInfo info = getHeroInfoById(id);
        ROHeroData dal = new ROHeroData();
        dal.Info = info;
        dal.RALRate = RoleAttributeList.zero;
        return dal;
    }
    public int getHeroStatus(int hashcode)
    {
        int status = 0;
        foreach (GDEHeroData hero in PlayerData.herosOwned)
        {
            if (hero.hashCode == hashcode)
            {
                status = hero.status;
                break;
            }
        }
        return status;
    }
    public void setHeroStatus(int hashcode, int aimStatus)
    {
        foreach (GDEHeroData hero in PlayerData.herosOwned)
        {
            if (hero.hashCode == hashcode)
            {
                hero.status = aimStatus;
                PlayerData.Set_herosOwned();
                break;
            }
        }
    }
    public int getHeroPosInTeamByHashcode(int hashcode)
    {
        int val = 0;
        foreach (GDEHeroData hero in PlayerData.herosOwned)
        {
            if (hero.hashCode == hashcode)
            {
                val = hero.teamPos;
                break;
            }
        }
        return val;
    }
    public int getHeroStarNumByHashcode(int hashcode)
    {
        foreach (GDEHeroData hero in PlayerData.herosOwned)
        {
            if (hero.hashCode == hashcode)
            {
                string id = hero.id;
                int level = getHeroLevelById(id);
                int starupgrade = hero.starNumUpgradeTimes;
                return level + starupgrade;
            }
        }
        return 0;
    }
    public bool consumeHero(int hashcode)
    {
        foreach (GDEHeroData h in PlayerData.herosOwned)
        {
            if (!h.locked && h.hashCode == hashcode)
            {
                consume_Item(h.id, out int left);
                PlayerData.herosOwned.Remove(h);
                PlayerData.Set_herosOwned();
                return true;
            }
        }
        return false;
    }
    /*
    public void addHeroByConsumeHero(string costId)
    {
        HeroInfo costHero = getHeroInfoById(costId);
        if (costHero.LEVEL > 0) return;
        List<HeroInfo> all = getHeroInfoList;
        all = all.FindAll(x => x.LEVEL > 0 && x.Race.Race == costHero.Race.Race);
        //HeroInfo target = all[UnityEngine.Random.Range(0, all.Count)];
        float[] ra = new float[] { 0.6f, 0.3f, 0.1f };
        int le = RandomIntger.Choose(ra)+1;
        all = all.FindAll(x => x.LEVEL == le);
        HeroInfo target = all[UnityEngine.Random.Range(0, all.Count)];
        //return target.ID;
        int hc = addHero(target.ID);
        RoleAttributeList ral = RoleAttributeList.GDEToRAL
            (getHeroByHashcode(hc).RoleAttritubeList);
    }
    */
    #region 根据ID+KEY来产生随机数,一样的种子会出现必然一致的结果
    public static int Rand(int a, int b, int id, int key)
    {
        UnityEngine.Random.State originalSeed = UnityEngine.Random.state;
        UnityEngine.Random.InitState(id + key);
        int fin = UnityEngine.Random.Range(a, b);
        UnityEngine.Random.state = originalSeed;
        return fin;
    }
    //JACK:根据ID+KEY来产生随机数,一样的种子会出现必然一致的结果
    //key用来对ID人物的不同属性区分
    //并且防止以后加入新的属性,其他属性会重新随机造成的问题,特别是名字,如果再次随机,会造成人物名字全部不一致
    //所以名字需要跟随ID非常固定
    static public float Rand(float a, float b, int id, int key)
    {
        UnityEngine.Random.State originalSeed = UnityEngine.Random.state;
        UnityEngine.Random.InitState(id + key);
        float fin = UnityEngine.Random.Range(a, b);
        UnityEngine.Random.state = originalSeed;
        return fin;
    }
    #endregion
    public void addHeroFatigue(int figure, int heroHashcode)
    {
        foreach (GDEHeroData hero in PlayerData.herosOwned)
        {
            if (hero.hashCode == heroHashcode)
            {
                hero.Fatigue += figure;
                if (hero.Fatigue < 0) hero.Fatigue = 0;
                PlayerData.Set_herosOwned();
                break;
            }
        }
    }
    public int getHeroFatigue(int heroHashcode)
    {
        foreach (GDEHeroData hero in PlayerData.herosOwned)
        {
            if (hero.hashCode == heroHashcode)
            {
                return hero.Fatigue;
            }
        }
        return 0;
    }
    public int getHeroMaxFatigue(int heroHashcode)
    {
        foreach (GDEHeroData hero in PlayerData.herosOwned)
        {
            if (hero.hashCode == heroHashcode)
            {
                int lv = hero.lv;
                ROHeroData h = getHeroDataByID(hero.id, hero.starNumUpgradeTimes);
                int maxF = (int)((SDConstants.fatigueBasicNum + lv * 2)
                    * Mathf.Max(h.quality * 1f / 2, 1));
                return maxF;
            }
        }
        return SDConstants.fatigueBasicNum;
    }
    public bool checkHeroFatigueTooHigh(int heroHashcode)
    {
        foreach (GDEHeroData hero in PlayerData.herosOwned)
        {
            if (hero.hashCode == heroHashcode)
            {
                int f = hero.Fatigue;
                int maxF = getHeroMaxFatigue(heroHashcode);
                if (f >= maxF)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public float getHeroFatigueRate(int heroHashcode)
    {
        foreach (GDEHeroData hero in PlayerData.herosOwned)
        {
            if (hero.hashCode == heroHashcode)
            {
                int f = hero.Fatigue;
                int maxF = getHeroMaxFatigue(heroHashcode);
                return Mathf.Min(1, f * 1f / maxF);
            }
        }
        return 0f;
    }
    public void setHeroFatigue(int figure, int heroHashcode)
    {
        foreach (GDEHeroData hero in PlayerData.herosOwned)
        {
            if (hero.hashCode == heroHashcode)
            {
                hero.Fatigue = figure;
                PlayerData.Set_herosOwned();
                break;
            }
        }
    }
    public IEnumerable<GDEHeroData> FindAllHerosById(string id, bool onlyUnlocked = true)
    {
        return PlayerData.herosOwned.FindAll(
            x => x.id == id && (onlyUnlocked ? true : !x.locked)).AsEnumerable();
    }
    public bool checkHeroCanImprove(int hashcode, SDConstants.MaterialType mtype)
    {
        GDEHeroData data = getHeroByHashcode(hashcode);
        ROHeroData ro = getHeroOriginalDataById(data.id);
        if (mtype == SDConstants.MaterialType.exp)
        {
            int lv = getLevelByExp(data.exp);
            int limitLv = heroMaxLvByStar(data.starNumUpgradeTimes + ro.starNum);
            return lv < limitLv;
        }
        else if(mtype == SDConstants.MaterialType.star)
        {
            return data.starNumUpgradeTimes + ro.starNum < SDConstants.UnitMAxStarNum;
        }
        else if(mtype == SDConstants.MaterialType.skill)
        {
            //List<GDEASkillData> all = data.skillsOwned;
            //return all.FindAll(a => a.Lv < SDConstants.SkillMaxGrade).Count > 0;
            return data.a_skill0.Lv < SDConstants.SkillMaxGrade
                || data.a_skill1.Lv < SDConstants.SkillMaxGrade
                || data.a_skillOmega.Lv < SDConstants.SkillMaxGrade;
        }
        else { return false; }
    }
    public List<HeroInfo> getHeroInfoList
    {
        get
        {
            List<HeroInfo> results = AllList_HeroInfo;
            if(results==null)
            {
                results = new List<HeroInfo>();
                HeroInfo[] all = Resources.LoadAll<HeroInfo>("ScriptableObjects/heroes");
                for (int i = 0; i < all.Length; i++)
                {
                    results.Add(all[i]);
                }
            }
            return results;
        }
    }
    public HeroInfo getHeroInfoById(string id)
    {
        HeroInfo[] all = Resources.LoadAll<HeroInfo>("ScriptableObjects/heroes");
        foreach (HeroInfo info in all)
        {
            if (info.ID == id) return info;
        }
        return null;
    }
    public bool CheckHaveHeroById(string id)
    {
        return PlayerData.herosOwned.Exists(x => x.id == id);
    }
    public Job getHeroCareerById(string id)
    {
        List<HeroInfo> list = getHeroInfoList;
        foreach (HeroInfo info in list)
        {
            if (info.ID == id) return info.Career.Career;
        }
        return Job.End;
    }
    public Race getHeroRaceById(string id)
    {
        List<HeroInfo> list = getHeroInfoList;
        foreach (HeroInfo info in list)
        {
            if (info.ID == id) return info.Race.Race;
        }
        return Race.End;
    }
    public int getHeroLevelById(string id)
    {
        List<HeroInfo> list = getHeroInfoList;
        foreach (HeroInfo info in list)
        {
            if (info.ID == id) return info.LEVEL;
        }
        return 0;
    }
    public int getHeroRarityByHashcode(int hashcode)
    {
        HeroInfo info = getHeroInfoById(getHeroByHashcode(hashcode).id);
        return info.Rarity;
    }
    public int getHeroSkeletonById(string id)
    {
        List<HeroInfo> list = getHeroInfoList;
        foreach (HeroInfo info in list)
        {
            if (info.ID == id) return info.Skeleton;
        }
        return 0;
    }
    public GDEHeroData getHeroByHashcode(int hashcode)
    {
        return GetHeroOwnedByHashcode(hashcode);
    }
    public RoleAttributeList RALByDictionary(Dictionary<string, string> s)
    {
        RoleAttributeList RAL = new RoleAttributeList();
        RAL.Hp = getInteger(s["hp"]);
        RAL.Mp = getInteger(s["mp"]);
        RAL.Tp = getInteger(s["tp"]);
        RAL.AT = getInteger(s["at"]);
        RAL.AD = getInteger(s["ad"]);
        RAL.MT = getInteger(s["mt"]);
        RAL.MD = getInteger(s["md"]);
        RAL.Speed = getInteger(s["speed"]);
        RAL.Taunt = getInteger(s["taunt"]);
        RAL.Accur = getInteger(s["accur"]);
        RAL.Evo = getInteger(s["evo"]);
        RAL.Crit = getInteger(s["crit"]);
        RAL.Expect = getInteger(s["expect"]);

        RAL.Bleed_Def = getInteger(s["bleed_def"]);
        RAL.Mind_Def = getInteger(s["mind_def"]);
        RAL.Fire_Def = getInteger(s["fire_def"]);
        RAL.Frost_Def = getInteger(s["frost_def"]);
        RAL.Corrosion_Def = getInteger(s["corrosion_def"]);
        RAL.Hush_Def = getInteger(s["hush_def"]);
        RAL.Dizzy_Def = getInteger(s["dizzy_def"]);
        RAL.Confuse_Def = getInteger(s["confuse_def"]);
        return RAL;
    }
    public int getRoleRAMaxNumPerLv(AttributeData tag, int lv)
    {
        int up = lv / 10 + 1;
        int basicRal = SDConstants.RoleAttritubeMaxNum;
        if (tag == AttributeData.Hp) return (int)(basicRal * 5 * up);
        else if (tag == AttributeData.Mp) return (int)(basicRal * 3.5f * up);
        else if (tag == AttributeData.Tp) return (int)(basicRal * 2.5f * up);
        else if (tag == AttributeData.AD) return (int)(basicRal * 1f * up);
        else if (tag == AttributeData.AT) return (int)(basicRal * 1f * up);
        else if (tag == AttributeData.MD) return (int)(basicRal * 1f * up);
        else if (tag == AttributeData.MT) return (int)(basicRal * 1f * up);
        else if (tag == AttributeData.Speed) return (int)(basicRal * 0.25f * up);
        else if (tag == AttributeData.Accur) return (int)(basicRal * 1f * up);
        else if (tag == AttributeData.Evo) return (int)(basicRal * 1f * up);
        else if (tag == AttributeData.Crit) return (int)(basicRal * 0.25f * up);
        else if (tag == AttributeData.Expect) return (int)(basicRal * 0.5f * up);
        else if (tag == AttributeData.Taunt) return (int)(basicRal * 1f * up);
        else return basicRal * 1 * up;
    }
    public int getRoleSRMaxNumPerLv(int lv)
    {
        int up = lv / 10 + 1;
        int basicRal = SDConstants.RoleAttritubeMaxNum;

        return (int)(basicRal * 0.5f * up);
    }
    public void dressEquipment(int heroHashcode, int itemHashcode, bool isSecondJewelry = false)
    {
        int oldEquipHashcode = 0;
        foreach (GDEHeroData hero in SDDataManager.Instance.PlayerData.herosOwned)
        {
            if (hero.hashCode == heroHashcode)
            {
                GDEEquipmentData equip
                    = SDDataManager.Instance.getEquipmentByHashcode(itemHashcode);
                equip.OwnerHashcode = heroHashcode;
                #region add equip
                int pos = SDDataManager.Instance.getEquipPosById(equip.id);
                if (pos == (int)EquipPosition.Head)
                {
                    oldEquipHashcode
                        = hero.equipHelmet != null ? hero.equipHelmet.hashcode : 0;
                    hero.equipHelmet = equip;
                }
                else if (pos == (int)EquipPosition.Breast)
                {
                    oldEquipHashcode
                        = hero.equipBreastplate != null ? hero.equipBreastplate.hashcode : 0;
                    hero.equipBreastplate = equip;
                }
                else if (pos == (int)EquipPosition.Arm)
                {
                    oldEquipHashcode
                        = hero.equipGardebras != null ? hero.equipGardebras.hashcode : 0;
                    hero.equipGardebras = equip;
                }
                else if (pos == (int)EquipPosition.Leg)
                {
                    oldEquipHashcode
                        = hero.equipLegging != null ? hero.equipLegging.hashcode : 0;
                    hero.equipLegging = equip;
                }
                else if (pos == (int)EquipPosition.Finger)
                {
                    if (!isSecondJewelry)
                    {
                        oldEquipHashcode
                            = hero.jewelry0 != null ? hero.jewelry0.hashcode : 0;
                        hero.jewelry0 = equip;
                    }
                    else
                    {
                        oldEquipHashcode
                            = hero.jewelry1 != null ? hero.jewelry1.hashcode : 0;
                        hero.jewelry1 = equip;
                    }
                }
                else if (pos == (int)EquipPosition.Hand)
                {
                    oldEquipHashcode
                        = hero.equipWeapon != null ? hero.equipWeapon.hashcode : 0;
                    hero.equipWeapon = equip;
                }
                #endregion
                break;
            }
        }
        foreach (GDEEquipmentData e in SDDataManager.Instance.PlayerData.equipsOwned)
        {
            if (e.hashcode == itemHashcode)
            {
                e.OwnerHashcode = heroHashcode;
            }
            if (e.hashcode == oldEquipHashcode && oldEquipHashcode > 0)
            {
                e.OwnerHashcode = 0;
            }
        }
        SDDataManager.Instance.PlayerData.Set_equipsOwned();
    }
    public void disrobeEquipment(int heroHashcode, EquipPosition pos, bool isSecondJPos = false)
    {
        int equipHashcode = 0;
        foreach (GDEHeroData hero in SDDataManager.Instance.PlayerData.herosOwned)
        {
            if (hero.hashCode == heroHashcode)
            {
                if (pos == EquipPosition.Head)
                {
                    equipHashcode = hero.equipHelmet.hashcode;
                    hero.equipHelmet = Instance.equipEmpty();
                }
                else if (pos == EquipPosition.Breast)
                {
                    equipHashcode = hero.equipBreastplate.hashcode;
                    hero.equipBreastplate = Instance.equipEmpty();
                }
                else if (pos == EquipPosition.Arm)
                {
                    equipHashcode = hero.equipGardebras.hashcode;
                    hero.equipGardebras = Instance.equipEmpty();
                }
                else if (pos == EquipPosition.Leg)
                {
                    equipHashcode = hero.equipLegging.hashcode;
                    hero.equipLegging = Instance.equipEmpty();
                }
                else if (pos == EquipPosition.Finger)
                {
                    if (!isSecondJPos)
                    {
                        equipHashcode = hero.jewelry0.hashcode;
                        hero.jewelry0 = Instance.equipEmpty();
                    }
                    else
                    {
                        equipHashcode = hero.jewelry1.hashcode;
                        hero.jewelry1 = Instance.equipEmpty();
                    }
                }
                else if (pos == EquipPosition.Hand)
                {
                    equipHashcode = hero.equipWeapon.hashcode;
                    hero.equipWeapon = Instance.equipEmpty();
                }
                break;
            }
        }
        foreach (GDEEquipmentData e in SDDataManager.Instance.PlayerData.equipsOwned)
        {
            if (e.hashcode == equipHashcode)
            {
                e.OwnerHashcode = 0;
                break;
            }
        }
        SDDataManager.Instance.PlayerData.Set_herosOwned();
        GDEEquipmentData _e
            = SDDataManager.Instance.getHeroEquipmentByPos(heroHashcode, pos, isSecondJPos);
    }
    #region Level-&&-Exp-=>-Caculate
    public int getLevelByExp(int exp)
    {
        int lv = 0;
        int V = 0;
        while (lv < SDConstants.MaxIncreasingExpLevel)
        {
            V += ExpBulkPerLevel(lv);
            if (V <= exp)
            {
                lv++;
            }
            else
            {
                break;
            }
        }
        return lv;
    }
    public int ExpBulkPerLevel(int lv)
    {
        if (lv < 50) return lv * SDConstants.MinExpPerLevel;
        else return 50 * SDConstants.MinExpPerLevel;
    }
    public int getMinExpReachLevel(int lv)
    {
        int V = 0;
        for (int i = 1; i < lv; i++)
        {
            V += ExpBulkPerLevel(i);
        }
        return V;
    }
    public float getExpRateByExp(int exp)
    {
        int currentLv = getLevelByExp(exp);
        int expOld = getMinExpReachLevel(currentLv);
        int expLength = ExpBulkPerLevel(currentLv);
        return (exp - expOld) * 1f / expLength;
    }
    #endregion
    #region Hero_Improve
    public bool checkHeroEnableSkill1ByHashcode(int hashcode)
    {
        GDEHeroData hero = getHeroByHashcode(hashcode);
        return checkHeroEnableSkill1ById(hero.id);
    }
    public bool checkHeroEnableSkill1ById(string id)
    {
        HeroInfo info = getHeroInfoById(id);
        if (info.Rarity < 2) return false;
        else return true;
    }

    public void addExpToHeroByHashcode(int hashcode, int exp = 1)
    {
        foreach (GDEHeroData h in PlayerData.herosOwned)
        {
            if (h.hashCode == hashcode)
            {
                int starNum = getHeroLevelById(h.id) + h.starNumUpgradeTimes;
                h.exp = HeroOverflowExp(h.exp + exp, starNum);
                PlayerData.Set_herosOwned();
                break;
            }
        }
    }
    public void addStarToHeroByHashcode(int hashcode,int num = 1)
    {
        foreach (GDEHeroData h in PlayerData.herosOwned)
        {
            if (h.hashCode == hashcode)
            {
                h.starNumUpgradeTimes += num;
                PlayerData.Set_herosOwned();
                break;
            }
        }
    }
    public void addMainSkillGradeToHeroByHashcode(int hashcode,int num = 1)
    {
        foreach (GDEHeroData h in PlayerData.herosOwned)
        {
            if (h.hashCode == hashcode)
            {
                h.skillLevel += num;

                AddGradesToHeroSkills(hashcode, num);

                PlayerData.Set_herosOwned();
                break;
            }
        }
    }
    public void AddGradesToHeroSkills(int hashcode, int gradeNum)
    {
        GDEHeroData data = getHeroByHashcode(hashcode);
        bool flag = checkHeroEnableSkill1ByHashcode(hashcode);
        int N = Mathf.Min(SkillGradeNumWaitingForImprove(hashcode), gradeNum);
        List<GDEASkillData> allSkills = new List<GDEASkillData>();
        allSkills.Add(data.a_skill0);
        if (flag) allSkills.Add(data.a_skill1);
        allSkills.Add(data.a_skillOmega);
        for(int i = 0; i < N; i++)
        {
            List<GDEASkillData> all = allSkills.FindAll(x => x.Lv < SDConstants.SkillMaxGrade);
            all[UnityEngine.Random.Range(0, all.Count)].Lv++;
            //
            PlayerData.Set_herosOwned();
        }
    }
    
    #endregion
    public int heroMaxLvByStar(int star)
    {
        int limitedLv = 10;
        if (star == 0) limitedLv = 10;
        else if (star == 1) limitedLv = 20;
        else if (star == 2) limitedLv = 30;
        else if (star == 3) limitedLv = 50;
        else if (star == 4) limitedLv = 70;
        else if (star == 5) limitedLv = 100;
        return limitedLv;
    }
    public bool checkHeroExpIfOverflow(int currentExp, int star)
    {
        int limitedLv = heroMaxLvByStar(star);
        int limitedExp = getMinExpReachLevel(limitedLv);
        if (currentExp >= limitedExp) return true;
        return false;
    }
    public int HeroOverflowExp(int oldExp, int star)
    {
        int limitedLv = heroMaxLvByStar(star);
        int limitedExp = getMinExpReachLevel(limitedLv);
        if (oldExp >= limitedExp) return limitedExp;
        return oldExp;
    }
    /// <summary>
    /// likability
    /// </summary>
    /// <param name="hashcode"></param>
    /// <param name="likability"></param>
    public void addLikabilityToHeroByHashcode(int hashcode, int likability = 1)
    {
        foreach (GDEHeroData h in PlayerData.herosOwned)
        {
            if (h.hashCode == hashcode)
            {
                h.likability += likability; break;
            }
        }
    }
    public int getLikeByLikability(int L, out float RateToNext)
    {
        if (L >= SDConstants.MinHeartVolume * 8.5f)
        {
            RateToNext = 0; return 3;
        }
        if (L >= SDConstants.MinHeartVolume * 3.5f)
        {
            RateToNext = (L - SDConstants.MinHeartVolume * 3.5f) * 1f
                / (SDConstants.MinHeartVolume * 5f);
            return 2;
        }
        if (L >= SDConstants.MinHeartVolume)
        {
            RateToNext = (L - SDConstants.MinHeartVolume) * 1f
                / (SDConstants.MinHeartVolume * 2.5f);
            return 1;
        }
        RateToNext = L * 1f / SDConstants.MinHeartVolume;
        return 0;
    }
    public bool getHeroIfLocked(int hashcode)
    {
        GDEHeroData h = getHeroByHashcode(hashcode);
        return h.locked;
    }
    public int getHeroExpPrice(int hashcode)
    {
        GDEHeroData h = getHeroByHashcode(hashcode);
        int _exp = h.exp;
        string id = h.id;
        ROHeroData dal = getHeroDataByID(id, h.starNumUpgradeTimes);
        return (int)((25+_exp) * (1 + 0.2f * dal.quality + 0.2f * dal.starNum));
    }

    //
    #endregion
    #region Hero_Anim_Infor
    public string getRandomImgAddressForAnim(string parent, int skeletonIndex = 0)
    {
        RoleSkeletonData hail = null;

        RoleSkeletonData.SlotRegionPairList list = null;
        foreach (var L in hail.AllEnableList)
        {
            if (L.slot == parent)
            {
                list = L; break;
            }
        }
        if (list == null) return string.Empty;
        //
        int count = list.AllRegionList.Count;
        int selectedIndex = UnityEngine.Random.Range(0, count);
        return list.AllRegionList[selectedIndex].Region;
    }
    public Sprite getSpriteFromAtlas(string atlasAddress, string spriteName)
    {
        //Debug.Log("ATLAS==--=="+atlasAddress + "===---===" + spriteName);
        SpriteAtlas atlas = Resources.Load<SpriteAtlas>("Sprites/AnimImage/" + atlasAddress);
        return atlas.GetSprite(spriteName);
    }
    #endregion
    #region HeroTeamInfor
    public GDEunitTeamData getHeroTeamByTeamId(string id)
    {
        foreach (GDEunitTeamData t in PlayerData.heroesTeam)
        {
            if (t.id == id) return t;
        }
        return null;
    }
    public bool checkHeroOwned(string heroId)
    {
        bool flag = false;
        foreach (GDEHeroData item in PlayerData.herosOwned)
        {
            if (item.id == heroId)
            {
                flag = true; break;
            }
        }
        return flag;
    }
    public void setHeroTeam(string teamId, int index, int hashcode)
    {
        GDEunitTeamData Team = getHeroTeamByTeamId(teamId);
        if (Team == null)
        {
            Team = new GDEunitTeamData(GDEItemKeys.unitTeam_emptyHeroTeam);
            Team.id = teamId;
            PlayerData.heroesTeam.Add(Team);
            PlayerData.Set_heroesTeam();
        }

        foreach (GDEHeroData H in PlayerData.herosOwned)
        {
            if (H.teamIdBelongTo == teamId && H.TeamOrder == index)
            {
                H.teamIdBelongTo = string.Empty;
                H.TeamOrder = 0;
                H.status = 0;
                PlayerData.Set_herosOwned();
                break;
            }
        }

        foreach (GDEHeroData H in PlayerData.herosOwned)
        {

            if (H.hashCode == hashcode)
            {
                H.teamIdBelongTo = teamId; H.TeamOrder = index;
                H.status = 1;
                PlayerData.Set_herosOwned();
                break;
            }
        }
    }
    public void setHeroTeamPos(int hashcode, int newPos)
    {
        int P = newPos % SDConstants.MaxSelfNum;
        foreach (GDEHeroData H in PlayerData.herosOwned)
        {
            if (H.hashCode == hashcode)
            {
                H.teamPos = P;
                PlayerData.Set_herosOwned();
                break;
            }
        }
    }
    public List<GDEunitTeamData> getHeroGroup() { return PlayerData.heroesTeam; }
    public void removeFromTeam(int hashcode)
    {
        foreach (GDEHeroData item in PlayerData.herosOwned)
        {
            if (item.hashCode == hashcode)
            {
                if (item.status == 1)//角色在战斗组
                {
                    removeHeroFromBattleTeam(hashcode);
                }
                else if (item.status == 2)
                {

                }
                else if (item.status == 3)
                {

                }
                else if (item.status == 4)
                {

                }
                break;
            }
        }
    }
    public void removeHeroFromBattleTeam(int hashcode)
    {

        foreach (GDEHeroData hero in PlayerData.herosOwned)
        {
            if (hero.hashCode == hashcode)
            {
                hero.teamIdBelongTo = string.Empty;
                hero.status = 0; break;
            }
        }
    }
    public List<int> getHerosHashcodeFromTeam(string id)
    {
        List<int> list = new List<int>();
        IEnumerable<GDEHeroData> all
            = PlayerData.herosOwned.FindAll(x => x.teamIdBelongTo == id).AsEnumerable();
        foreach (var a in all)
        {
            list.Add(a.hashCode);
        }
        return list;
    }
    public List<GDEHeroData> getHerosFromTeam(string id)
    {
        List<GDEHeroData> list
            = PlayerData.herosOwned.FindAll(x => x.teamIdBelongTo == id).ToList();
        list.Sort((x, y) => 
        {
            return x.TeamOrder.CompareTo(y.TeamOrder);
        });
        if (list.Count > SDConstants.MaxSelfNum)
        {
            List<GDEHeroData> _l = new List<GDEHeroData>();
            for(int i = 0; i < SDConstants.MaxSelfNum; i++)
            {
                _l.Add(list[i]);
            }
            return _l;
        }
        return list;
    }
    public GDEHeroData getHeroFromTeamByOrder(string teamId, int order)
    {
        List<GDEHeroData> all = getHerosFromTeam(teamId);
        return all.Find(x => x.TeamOrder == order);
    }


    public void setTeamName(string teamId, string new_name)
    {
        foreach (GDEunitTeamData t in PlayerData.heroesTeam)
        {
            if (t.id == teamId)
            {
                t.teamName = new_name;
                PlayerData.Set_heroesTeam();
                break;
            }
        }
    }
    public void setTeamGoddess(string teamId, string goddessId)
    {
        foreach (GDEunitTeamData t in PlayerData.heroesTeam)
        {
            if (t.id == teamId)
            {
                t.goddess = goddessId;
                PlayerData.Set_heroesTeam();
                break;
            }
        }
    }
    #endregion
    #region ConsumableInfor
    public Sprite ConsumableIcon(string id)
    {
        return atlas_consumable.GetSprite(id);
    }
    public int addConsumable(string id, int num = 1)
    {
        add_Item(id, num);
        foreach (GDEItemData M in PlayerData.consumables)
        {
            if (M.id == id)
            {
                M.num += num;
                PlayerData.Set_consumables();
                return M.num;
            }
        }
        consumableItem[] allPs = Resources.LoadAll<consumableItem>
            ("ScriptableObjects/Items/Consumables");
        if (allPs.Select(x => x.ID == id).ToList().Count <= 0)
        {
            Debug.Log("不存在该道具"); return 0;
        }

        GDEItemData m = new GDEItemData(GDEItemKeys.Item_MaterialEmpty);
        m.id = id;
        m.num = num;
        PlayerData.consumables.Add(m);
        PlayerData.Set_consumables();
        return m.num;
    }
    public int getConsumableNum(string id)
    {
        GDEItemData item = PlayerData.consumables.Find(x => x.id == id);
        if (item != null)
        {
            return item.num;
        }
        return 0;
    }
    public List<GDEItemData> getConsumablesOwned
    {
        get { return PlayerData.consumables; }
    }
    public bool consumeConsumable(string id, out int residue, int num = 1)
    {
        foreach (GDEItemData m in PlayerData.consumables)
        {
            if (m.id == id)
            {
                if (m.num < num)
                {
                    residue = m.num;
                    return false;
                }
                else
                {
                    m.num -= num;
                    consume_Item(id, out int left, num);
                    if (m.num <= 0)
                    {
                        PlayerData.consumables.Remove(m);
                    }
                    PlayerData.Set_consumables();
                    residue = m.num;
                    return true;
                }
            }
        }
        residue = 0;
        return false;
    }
    public List<consumableItem> AllConsumableList
    {
        get 
        {
            if(AllList_ConsumableItem != null) 
            {
                return AllList_ConsumableItem;
            }
            consumableItem[] all = Resources.LoadAll<consumableItem>
                ("ScriptableObjects/Items/Consumables");
            return all.ToList();
        }
    }
    public consumableItem getConsumableItemById(string id)
    {
        consumableItem[] all = Resources.LoadAll<consumableItem>
            ("ScriptableObjects/Items/Consumables");
        foreach (consumableItem item in all)
        {
            if (item.ID == id) return item;
        }
        return null;
    }
    /// <summary>
    /// getConsumableById替补
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public consumableItem getConsumableById(string id)
    {
        return getConsumableItemById(id);
    }
    public GDEItemData getConsumeableDataById(string id)
    {
        foreach (GDEItemData data in getConsumablesOwned)
        {
            if (data.id == id) return data;
        }
        return null;
    }
    public string getConsumableStrById(string id)
    {
        consumableItem item = getConsumableItemById(id);
        if (item != null) return item.SpecialStr;
        return string.Empty;
    }

    public bool checkIfHaveOpKey(SDConstants.MaterialType type, out string keyId)
    {
        consumableItem key = AllConsumableList.Find
            (x =>
            {
                string s = x.SpecialStr.ToLower();
                string t = type.ToString().ToLower();
                return s.Contains(t) && x.MaterialType == SDConstants.MaterialType.key;
            });
        if (key)
        {
            string ID = key.ID;
            keyId = ID;
            if (PlayerData.consumables.Exists(x => x.id == ID)) return true;
        }else keyId = string.Empty;
        return false;
    }
    #endregion
    #region PropInfor
    public List<GDEItemData> getPropsOwned
    {
        get
        {
            return getConsumablesOwned.FindAll(x =>
            {
                consumableItem item = getConsumableItemById(x.id);
                if (item == null) return false;
                return item.isProp;
            });
        }
    }
    public bool checkIfPropIsTaken(string id)
    {
        foreach (GDEItemData p in PlayerData.propsTeam)
        {
            if (p.id == id) return true;
        }
        return false;
    }
    public int propTakenVolume(string id)
    {
        consumableItem p = getConsumableById(id);
        if (!p.isProp) return 0;
        int level = p.LEVEL;
        if (level == 0) return 10;
        else if (level == 1) return 5;
        else if (level == 2) return 3;
        else if (level == 3) return 3;
        else if (level == 4) return 1;
        else if (level == 5) return 1;
        return 1;
    }
    public void unlockNewPropTeamSlot(int unlockNum = 1)
    {
        if (PlayerData.propsTeam.Count < SDConstants.BagMaxVolume)
        {
            for (int i = 0; i < unlockNum; i++)
            {
                GDEItemData D = new GDEItemData(GDEItemKeys.Item_MaterialEmpty)
                {
                    id = string.Empty,
                    num = 0,
                };
                D.index = PlayerData.propsTeam.Count;
                PlayerData.propsTeam.Add(D);
                PlayerData.Set_propsTeam();
            }
        }
    }
    #endregion
    #region MaterialInfor
    public List<consumableItem> allRaws
    {
        get
        {
            consumableItem[] all = Resources.LoadAll<consumableItem>
                ("ScriptableObjects/Items/Consumables");
            List<consumableItem> results = new List<consumableItem>();
            for(int i = 0; i < all.Length; i++)
            {
                if(all[i].MaterialType == SDConstants.MaterialType.raw)
                {
                    results.Add(all[i]);
                }
            }
            return results;
        }
    }
    public List<GDEItemData> getMaterialsOwned
    {
        get {
            return getConsumablesOwned.FindAll(x =>
                {
                    consumableItem item = getConsumableItemById(x.id);
                    if (item == null) return false;
                    return !item.isProp;
                });
        }
    }
    public string getMaterialNameById(string id)
    {
        List<consumableItem> list = AllConsumableList;
        foreach(consumableItem item in list)
        {
            if (item.ID == id) return item.NAME;
        }
        return string.Empty;
    }
    public SDConstants.MaterialType getMaterialTypeById(string id)
    {
        consumableItem item = getConsumableById(id);
        if (item)
        {
            return item.MaterialType;
        }
        return SDConstants.MaterialType.end;
    }
    public int getMaterialLevelById(string id)
    {
        List<consumableItem> list = AllConsumableList;
        foreach (var m in list)
        {
            if (m.ID == id)
            {
                return m.LEVEL;
            }
        }
        return 0;
    }
    public int getMaterialWeightById(string id)
    {
        if(AllConsumableList.Exists(x=>x.ID == id))
        {
            consumableItem item = getConsumableItemById(id);
            return 5 - item.LEVEL;
        }
        return 1;
    }


    public bool checkConsumableIfProp(string id)
    {
        consumableItem item = getConsumableById(id);
        if (item)
        {
            return item.isProp;
        }
        return false;
    }
    #region MaterialFunction
    public int getFigureFromMaterial(consumableItem item)
    {
        if (item.MaterialType == SDConstants.MaterialType.exp
            || item.MaterialType == SDConstants.MaterialType.likability
            || item.MaterialType == SDConstants.MaterialType.equip_exp)
        {
            return getInteger(item.SpecialStr);
        }
        return 0;
    }
    public int getFigureFromMaterial(string itemId)
    {
        consumableItem item = getConsumableById(itemId);
        return getFigureFromMaterial(item);
    }
    public string getMaterialSpecialStr(string id)
    {
        consumableItem item = getConsumableById(id);
        if (item) return item.SpecialStr;
        return string.Empty;
    }
    #region consumableItem_materialType_skill
    public Job consumableItemSkill_FixCareer(string itemId)
    {
        string str = getMaterialSpecialStr(itemId);
        string career = str.Split('_')[0];
        if(career == "any") { return Job.End; }
        for(int i = 0; i < (int)Job.End; i++)
        {
            Job _j = (Job)i;
            if(_j.ToString().ToLower() == career.ToLower())
            {
                return _j;
            }
        }
        return Job.End;
    }
    #endregion
    #endregion
    #endregion

    #region 基础货币信息
    void refreshDataInHomeScene()
    {

    }
    #region Gold_Infor
    public int AddCoin(int val)
    {
        int number = PlayerData.coin;
        PlayerData.coin = number + val;
        
        return PlayerData.coin;
    }
    public bool ConsumeCoin(int val)
    {
        int number = PlayerData.coin;
        if (number >= val)
        {
            PlayerData.coin = number - val;
            return true;
        }
        else
        {
            Debug.Log("操作无法执行，金币数量不足");
            return false;
        }
    }
    public int GetCoin() { return PlayerData.coin; }

    public int getGoldPerc()
    {
        return getGoldPercOrigin();
    }
    public int getGoldPercOrigin() { return PlayerData.addGoldPerc; }
    public void addGoldPerc(int val) { PlayerData.addGoldPerc += val; }

    #endregion
    #region Damond_Infor
    public int AddDamond(int val)
    {
        int num = PlayerData.damond;
        PlayerData.damond = num + val;
        return PlayerData.damond;
    }
    public bool ConsumeDamond(int val)
    {
        int num = PlayerData.damond;
        if (num >= val)
        {
            PlayerData.damond = num-val;
            return true;
        }
        else
        {
            Debug.Log("操作无法执行，钻石数量不足");
            return false;
        }
    }
    public int GetDamond() { return PlayerData.damond; }
    #endregion
    #region JianCai_Infor
    public int AddJiancai(int val)
    {
        int num = PlayerData.JianCai;
        PlayerData.JianCai = num + val;
        return PlayerData.JianCai;
    }
    public bool ConsumeJiancai(int val)
    {
        int num = PlayerData.JianCai;
        if (num >= val)
        {
            PlayerData.JianCai = num - val;
            return true;
        }
        else
        {
            Debug.Log("操作无法执行，建筑升级材料不足");
            return false;
        }
    }
    public int getJiancai() { return PlayerData.JianCai; }
    #endregion
    #endregion
    #region LevelInfor
    public void SetNewBestLevel(int val)
    {
        if (SDGameManager.Instance.gameType == SDConstants.GameType.Normal)
        {
            if (val > PlayerData.newBestLevel)
            {
                PlayerData.newBestLevel = val;
            }
        }
    }
    public int GetNewBestLevel()
    {
        return PlayerData.newBestLevel;
    }
    public int GetMaxPassSection()
    {
        if (SDGameManager.Instance.gameType == SDConstants.GameType.Normal)
        {
            return PlayerData.maxPassSection;
        }
        else if (SDGameManager.Instance.gameType == SDConstants.GameType.Hut)
        {

        }
        if (SDGameManager.Instance.gameType == SDConstants.GameType.Dungeon)
        {
            return PlayerData.maxDurgeonPassLevel;
        }
        return PlayerData.maxPassSection;
    }
    public int getDimension() { return PlayerData.dimension; }
    public List<GDESectionData> getSectionHistoryList()
    {
        return PlayerData.finishSectionsList;
    }
    public GDESectionData getSectionHistoryByIndex(int index)
    {
        return PlayerData.finishSectionsList.Find(x => x.Index == index);
    }
    public bool SaveSectionHistory(int sectionIndex, int remark)
    {
        if (GetMaxPassSection() + 1 < sectionIndex) return false;
        GDESectionData data = new GDESectionData(GDEItemKeys.Section_nullSection)
        {
            Index = sectionIndex,
            remark = remark,
        };
        PlayerData.finishSectionsList.Add(data);
        PlayerData.finishSectionsList.Sort((x, y) =>
        {
            return x.Index.CompareTo(y.Index);
        });
        PlayerData.Set_finishSectionsList();
        return true;
    }
    #endregion
    #region DataListResult
    #region sprite_atlas
    public List<SpriteAtlas> AllAtlas = new List<SpriteAtlas>();
    public SpriteAtlas[] atlas_equip_list = new SpriteAtlas[(int)EquipPosition.End];
    public SpriteAtlas atlas_battleBg;
    public SpriteAtlas atlas_rarity;
    public SpriteAtlas atlas_consumable;
    public SpriteAtlas atlas_ralAndSstate;
    public SpriteAtlas atlas_UI;
    #endregion
    public void ReadAtlas()
    {
        AllAtlas = Resources.LoadAll<SpriteAtlas>("Sprites/atlas").ToList();
        //atlas_equip_list
        for(int i = 0; i < (int)EquipPosition.End; i++)
        {
            SpriteAtlas atlas = AllAtlas.Find(x => x.name.ToLower().Contains
            (((EquipPosition)i).ToString().ToLower()));
            if (atlas != null) atlas_equip_list[i] = atlas;
        }
        //atlas-battleBg
        atlas_battleBg = AllAtlas.Find(x => x.name.Contains("battleBg"));
        atlas_rarity = AllAtlas.Find(x => x.name.Contains("rarity"));
        atlas_consumable = AllAtlas.Find(x => x.name == "atlas_consumable");
        atlas_ralAndSstate = AllAtlas.Find(x => x.name == "atlas_ral&sstate");
        atlas_UI = AllAtlas.Find(x => x.name == "atlas_ui");
    }

    #region ScriptableObjects
    public List<HeroInfo> AllList_HeroInfo;
    public List<EquipItem> AllList_EquipItem;
    public List<consumableItem> AllList_ConsumableItem;
    public List<EnemyInfo> AllList_EnemyInfo;
    public List<RuneItem> AllList_RuneItem;
    public List<GoddessInfo> AllList_GoddessInfo;
    public void ReadAllSOs()
    {
        AllList_HeroInfo = Resources.LoadAll<HeroInfo>
            ("ScriptableObjects/heroes").ToList();
        AllList_EquipItem = Resources.LoadAll<EquipItem>
            ("ScriptableObjects/items/Equips").ToList();
        AllList_ConsumableItem = Resources.LoadAll<consumableItem>
            ("ScriptableObjects/Items/Consumables").ToList();
        AllList_EnemyInfo = Resources.LoadAll<EnemyInfo>
            ("ScriptableObjects/enemies").ToList();
        AllList_RuneItem = Resources.LoadAll<RuneItem>
            ("ScriptableObjects/Items/Runes").ToList();
        AllList_GoddessInfo = Resources.LoadAll<GoddessInfo>
            ("ScriptableObjects/goddess").ToList();
    }
    #endregion

    public Sprite GetIconInRAL(AttributeData ad)
    {
        string n = "ral_" + ad.ToString().ToLower();
        return atlas_ralAndSstate.GetSprite(n);
    }
    public Sprite GetIconInRAL(StateTag st)
    {
        string n = "ral_" + st.ToString().ToLower();
        return atlas_ralAndSstate.GetSprite(n);
    }
    public void ResetDatas()
    {
        PlayerPrefs.DeleteAll();
        GDEDataManager.ClearSaved();
    }
    public int getInteger(string s)
    {
        int tmp = 0;
        if (s != null && s != "")
        {
            if (int.TryParse(s, out _)) { tmp = int.Parse(s); }
        }
        return tmp;
    }
    public SDConstants.ItemType getItemTypeById(string id)
    {
        string Sign = id.ToCharArray()[0].ToString().ToUpper();
        if (Sign == "M") return SDConstants.ItemType.Consumable;
        else if (Sign == "D") return SDConstants.ItemType.Enemy;
        else if (Sign == "E") return SDConstants.ItemType.Equip;
        else if (Sign == "H") return SDConstants.ItemType.Hero;
        else if (Sign == "G") return SDConstants.ItemType.Goddess;
        else if (Sign == "R") return SDConstants.ItemType.Rune;
        else if (Sign == "N") return SDConstants.ItemType.NPC;

        return SDConstants.ItemType.End;
    }

    #region SkeletonAnimation
    public List<SkeletonDataAsset> AllSkeletonAssets;
    public List<SkeletonDataAsset> AllHeroAssets;
    #endregion
    public void ReadAllSkeletonAssets()
    {
        AllSkeletonAssets = Resources.LoadAll<SkeletonDataAsset>("Spine").ToList();
        Debug.Log(AllSkeletonAssets.Count + "_SkeletonData");
        AllHeroAssets = AllSkeletonAssets.FindAll(x => x.name.Contains("role"));
        Debug.Log(AllHeroAssets.Count + "_Hero_SkeletonData");
    }
    #endregion
    #region HeroAlarPoolInfor
    public List<HeroAltarPool> GetAllHeroAltarPoolList
    {
        get
        {
            return Resources.LoadAll<HeroAltarPool>("ScriptableObjects/pools").ToList();
        }
    }
    public HeroAltarPool GetHeroAltarPoolById(string id)
    {
        return GetAllHeroAltarPoolList.Find(x => x.ID == id);
    }
    public List<GDEHeroAltarPoolData> GetAllHeroPool()
    {
        return PlayerData.AltarPoolList;
    }
    public GDEHeroAltarPoolData GetHeroPoolDataById(string id)
    {
        return GetAllHeroPool().Find(x => x.ID == id);
    }
    public HeroInfo AltarInOnePool(float[] Possibilities, string poolId, bool MustS = false)
    {
        int L = RandomIntger.Choose(Possibilities);
        GDEHeroAltarPoolData PoolData = GetHeroPoolDataById(poolId);
        PoolData.AltarTimes++;
        PlayerData.Set_AltarPoolList();
        if(PoolData.AltarTimes>= 10)
        {
            if (PoolData.GetSNum == 0) L = 3;
        }
        HeroAltarPool Pool = GetHeroAltarPoolById(poolId);
        List<int> LEVELList = Pool.HeroList.Select(x => x.LEVEL).ToList();
        LEVELList.Sort();
        if (MustS) L = LEVELList.Max();
        if (!LEVELList.Contains(L))
        {
            if (L < LEVELList.Count)
            {
                L = LEVELList[L];
            }
            else 
            {
                L = LEVELList[UnityEngine.Random.Range(0, LEVELList.Count)];
            } 
        }
        if (L >= 3)
        {
            PoolData.GetSNum++;
            PlayerData.Set_AltarPoolList();
        }
        List<HeroInfo> list = Pool.HeroList.FindAll(x => x.LEVEL == L);
        List<HeroInfo> _list = Pool.HeroesUsingSpecialPossibility.FindAll(x => x.LEVEL == L);
        if (_list.Count > 0)
        {
            float R = UnityEngine.Random.Range(0, 1f);
            if (R < 0.5f)
            {
                return _list[UnityEngine.Random.Range(0, _list.Count)];
            }
        }
        return list[UnityEngine.Random.Range(0, list.Count)];
    }
    #endregion
    #region Goddess_Infor
    public List<GoddessInfo> AllGoddessList
    {
        get
        {
            if (AllList_GoddessInfo != null) return AllList_GoddessInfo;
            return Resources.LoadAll<GoddessInfo>
                ("ScriptableObjects/goddess").ToList();
        }
    }
    public GDEgoddessData getGDEGoddessDataById(string goddessId)
    {
        foreach (GDEgoddessData g in PlayerData.goddessOwned)
        {
            if (g.id == goddessId)
            {
                return g;
            }
        }
        return null;
    }
    public GoddessInfo getGoddessInfoById(string id)
    {
        List<GoddessInfo> All = AllGoddessList;
        for(int i = 0; i < All.Count; i++)
        {
            if (All[i].ID == id) return All[i];
        }
        return null;
    }
    public bool addGoddess(GoddessInfo info)
    {
        foreach (GDEgoddessData g in PlayerData.goddessOwned)
        {
            if (g.id == info.ID)
            {
                g.attitube.agile = info.GoddessAtti.Agile;
                g.attitube.stamina = info.GoddessAtti.Stamina;
                g.attitube.recovery = info.GoddessAtti.Recovery;
                g.attitube.leader = info.GoddessAtti.Leader;
                g.index = info.Index;
                PlayerData.Set_goddessOwned();
                return false;
            }
        }
        //
        GDEgoddessData data = new GDEgoddessData(GDEItemKeys.goddess_baseGoddess)
        {
            id = info.ID,
            star = 0,
            volume = 0,
            exp = 0,
            rune0 = 0,
            rune1 = 0,
            rune2 = 0,
            rune3 = 0,
            index = info.Index,
            UseTeamId = new List<int>(),
        };
        data.attitube.agile = info.GoddessAtti.Agile;
        data.attitube.stamina = info.GoddessAtti.Stamina;
        data.attitube.recovery = info.GoddessAtti.Recovery;
        data.attitube.leader = info.GoddessAtti.Leader;
        PlayerData.goddessOwned.Add(data);
        PlayerData.Set_goddessOwned();
        return true;
    }
    public void addExpToGoddess(string goddessId,int exp)
    {
        foreach(GDEgoddessData goddess in PlayerData.goddessOwned)
        {
            if(goddess.id == goddessId)
            {
                goddess.exp += exp;
                PlayerData.Set_goddessOwned();
                break;
            }
        }
    }
    public List<GDEgoddessData> getAllGoddesses()
    {
        List<GDEgoddessData> all = PlayerData.goddessOwned;
        return all;
    }
    public List<GDEgoddessData> getAllGoddessesUnLocked()
    {
        List<GDEgoddessData> all = PlayerData.goddessOwned;
        all = all.FindAll(x => 
        {
            string id = x.id;
            return CheckIfHaveGoddessById(id);
        });
        return all;
    }

    #region Integrity-&&-Volume-=>-Caculate
    public bool IncreaseGoddessVolume(string id,int vol = 1)
    {
        foreach(GDEgoddessData g in PlayerData.goddessOwned)
        {
            if(g.id == id)
            {
                g.volume += vol;
                PlayerData.Set_goddessOwned();
                return true;
            }
        }
        return false;
    }
    public int getIntegrityByVolume(int volume, int quality)
    {
        int integrity = 0;
        int V = 0;
        while(integrity < SDConstants.goddessMaxIntegrity)
        {
            V += VolumeBulkPerIntegrity(integrity, quality);
            if (V < volume)
            {
                integrity++;
            }
            else
            {
                break;
            }
        }
        return integrity;
    }
    public bool CheckIfHaveGoddessById(string id)
    {
        GDEgoddessData GD = SDDataManager.Instance.PlayerData.goddessOwned.Find(x=>x.id == id);
        if (GD == null) return false;
        GoddessInfo info = getGoddessInfoById(id);
        if (info == null) return false;
        int integrity = getIntegrityByVolume(GD.volume, info.Quality);
        if (integrity < 1) return false;
        return true;
    }
    public int VolumeBulkPerIntegrity(int integrity, int quality)
    {
        int result = SDConstants.MinVolumePerIntegrity * (integrity + 1) * (5 + quality * 2);
        return result;
    }
    public int getMinVolumeReachIntegrity(int integrity,int quality)
    {
        int V = 0;
        for(int i = 0; i < integrity; i++)
        {
            V += VolumeBulkPerIntegrity(i,quality);
        }
        return V;
    }
    public float getRateAppraochIntegrity(int volume, int quality)
    {
        int integrity = getIntegrityByVolume(volume, quality);

        int vOld = 0; 
        vOld = getMinVolumeReachIntegrity(integrity, quality);
        int minV = VolumeBulkPerIntegrity(integrity, quality);
        return (volume - vOld) * 1f / minV;
    }
    #endregion

    #endregion
    #region TimeTask_Infor
    public bool haveTimeTaskByTaskId(string taskId, out GDEtimeTaskData task)
    {
        task = PlayerData.TimeTaskList.Find(x => x.taskId == taskId);
        return task != null;
    }
    public bool AddTimeTask(SDConstants.timeTaskType taskType,int Hashcode
        ,string itemId
        ,string taskId = null)
    {
        string TaskId = taskId;
        GDEtimeTaskData taskData;

        if (string.IsNullOrEmpty(TaskId))
        {
            string taskNB = "TT_" + taskType.ToString();
            List<GDEtimeTaskData> allFix = PlayerData.TimeTaskList.FindAll(x => x.taskId.Contains(taskNB));
            TaskId = taskNB + "#" + string.Format("{0:D2}", allFix.Count);
        }
        else if (haveTimeTaskByTaskId(TaskId, out taskData))
        {
            if (!string.IsNullOrEmpty(taskData.startTime))
            {
                DateTime starttime = Convert.ToDateTime(taskData.startTime);
                TimeSpan interval = DateTime.Now - starttime;
                if (interval.Seconds < taskData.timeType * 60)
                {
                    Debug.Log("已存在该任务且未完成，添加失败");
                    return false;
                }
            }
        }
        //
        taskData = new GDEtimeTaskData(GDEItemKeys.timeTask_emptyTimeTask)
        {
            taskId = taskId,
            itemHashcode = Hashcode,
            itemId = itemId,
            startTime = DateTime.Now.ToString(),
        };
        if (taskType == SDConstants.timeTaskType.HOSP)
        {
            int maxF = getHeroMaxFatigue(taskData.itemHashcode);
            GDEHeroData hero = getHeroByHashcode(Hashcode);
            ROHeroData _hero = getHeroOriginalDataById(hero.id);
            int mainTimeType = (hero.starNumUpgradeTimes + _hero.starNum + _hero.quality)
                * 2 + 10;

            int status = hero.status;
            if (status == 2)
            {
                setHeroStatus(Hashcode, 3);
                taskData.oldData = status;
                taskData.timeType = mainTimeType;
                Debug.Log("添加英雄从濒死到康复的治疗任务,hashcode: " + Hashcode
                    + " TaskId: " + TaskId);
                PlayerData.TimeTaskList.Add(taskData);
                PlayerData.Set_TimeTaskList();
                return true;
            }
            else if (status != 3)
            {
                float fatigueRate = getHeroFatigueRate(Hashcode);
                if (fatigueRate > 0.1f)//存在明显疲劳
                {
                    setHeroStatus(Hashcode, 3);
                    taskData.oldData = status;
                    taskData.timeType = (int)(mainTimeType * fatigueRate * 0.8f);
                    Debug.Log("添加英雄从疲劳到康复的治疗任务,hashcode: " + Hashcode
                        + " TaskId: " + TaskId);
                    PlayerData.TimeTaskList.Add(taskData);
                    PlayerData.Set_TimeTaskList();
                    return true;
                }
                else//太轻松不需要治疗
                {
                    Debug.Log("英雄疲劳度太低,不能添加任务");
                    return false;
                }
            }
            else
            {
                Debug.Log("英雄状态(status)不符合要求");
                return false;
            }
        }
        else if (taskType == SDConstants.timeTaskType.FACT)
        {
            consumableItem M = getConsumableById(itemId);
            SDConstants.MaterialType mt = M.MaterialType;
            int mainTimeType = (M.LEVEL + 1) * 2;
            GDENPCData slave = SDDataManager.Instance.GetNPCOwned(Hashcode);
            if (slave != null)
            {
                int lv = getLevelByExp(slave.exp);
                int like = getLikeByLikability(slave.likability, out float rate);
                mainTimeType = Mathf.Max(2, mainTimeType - lv);
                if (mt == SDConstants.MaterialType.exp)
                {
                    taskData.oldData = slave.workPower0;
                }
                else if (mt == SDConstants.MaterialType.equip_exp)
                {
                    taskData.oldData = slave.workPower1;
                }
                else
                {
                    taskData.oldData = slave.workPower2;
                }
            }
            else
            {
                taskData.oldData = 0;
            }
            taskData.timeType = mainTimeType;
            PlayerData.TimeTaskList.Add(taskData);
            PlayerData.Set_TimeTaskList();
            return true;
        }
        else
        {
            Debug.Log("不存在该任务类型");
            return false;
        }
    }
    public bool AbandonTimeTask(string taskId)
    {
        GDEtimeTaskData task = PlayerData.TimeTaskList.Find(x => x.taskId == taskId);
        if (task!=null && DateTime.TryParse(task.startTime, out DateTime starttime))
        {
            if (taskId.Contains("TT_HOSP"))
            {
                TimeSpan span = DateTime.Now - starttime;
                if (span.Minutes * 1f / task.timeType < 0.2f)
                {
                    //确认没能进行足够时间来进行恢复，timetask未完成
                    foreach (GDEtimeTaskData T in PlayerData.TimeTaskList)
                    {
                        if (T.taskId == taskId)
                        {
                            PlayerData.TimeTaskList.Remove(T);
                            PlayerData.Set_TimeTaskList();
                            setHeroStatus(task.itemHashcode, task.oldData);
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
    public bool FinishTimeTask(string taskId)
    {
        GDEtimeTaskData task = PlayerData.TimeTaskList.Find(x => x.taskId == taskId);
        if (task != null && DateTime.TryParse(task.startTime, out DateTime starttime))
        {
            TimeSpan span = DateTime.Now - starttime;
            if (taskId.Contains("TT_HOSP"))
            {
                bool flag = false;
                if (task.oldData != 2)//该角色之前并未遭受重创
                {
                    float s = span.Minutes * 1f / task.timeType;
                    if (s >= 0.2f)
                    {
                        //确认已进行恢复最小时间
                        int getHeroF = getHeroFatigue(task.itemHashcode);
                        setHeroFatigue((int)(getHeroF * (1f-s)), task.itemHashcode);
                        flag = true;
                    }
                }
                else//该角色之前遭受重创
                {
                    float s = span.Minutes * 1f / task.timeType;
                    if (s >= 1)
                    {
                        //重创角色只能使用timetype时间
                        setHeroFatigue(0, task.itemHashcode);
                        flag = true;
                    }
                }
                if (flag)
                {
                    foreach (GDEtimeTaskData T in PlayerData.TimeTaskList)
                    {
                        if (T.taskId == taskId)
                        {
                            PlayerData.TimeTaskList.Remove(T);
                            PlayerData.Set_TimeTaskList();
                            setHeroStatus(task.itemHashcode, 0);
                            return true;
                        }
                    }
                }
            }
            else if (taskId.Contains("TT_FACT"))
            {
                foreach (GDEtimeTaskData T in PlayerData.TimeTaskList)
                {
                    if (T.taskId == taskId)
                    {
                        TimeSpan leaveT = TimeSpan.FromMinutes(span.Minutes % task.timeType);
                        int num = span.Minutes / task.timeType;
                        addConsumable(task.itemId, num);
                        T.startTime = (DateTime.Now - leaveT).ToString();
                        PlayerData.Set_TimeTaskList();
                        string MN = getMaterialNameById(task.itemId);
                        PopoutController.CreatePopoutMessage("获得 " + MN + " X " + num, 50);
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public GDEtimeTaskData GetTimeTaskById(string taskId)
    {
        return PlayerData.TimeTaskList.Find(x => x.taskId == taskId);
    }
    public bool ChangeNPCInFactoryAssemblyLine(int NPCHashcode,string taskId)
    {
        foreach(GDEtimeTaskData T in PlayerData.TimeTaskList)
        {
            if(T.taskId == taskId && T.itemHashcode != NPCHashcode)
            {
                T.itemHashcode = NPCHashcode;
                PlayerData.Set_TimeTaskList();
                return true;
            }
        }
        return false;
    }
    #endregion
    #region Task_Info

    #endregion
    #region Achievement_Infor
    public GDEAchievementData getAchievementData()
    {
        return PlayerData.achievementData;
    }
    public void addAchievementDataByType(string type, int num = 1)
    {
        GDEAchievementData data = PlayerData.achievementData;
        if (type == "login")
        {
            data.login += num;
        }
        else if(type == "kill_fodder")
        {
            data.killFodder += num;
        }
        else if (type == "kill_normal")
        {
            data.killNormalEnemy += num;
        }
        else if(type == "kill_elite")
        {
            data.killElite += num;
        }
        else if (type == "kill_boss")
        {
            data.killBoss += num;
        }
        else if(type == "kill_god")
        {
            data.killGod += num;
        }
        else if (type == "forgeEquip")
        {
            data.forgeEquip += num;
        }
        else if (type == "forgeProp")
        {
            data.forgeProp += num;
        }
        else if (type == "useProp")
        {
            data.useProp += num;
        }
        else if (type == "ownHero")
        {
            data.ownHero += num;
        }
        else if (type == "ownHeroFightForce")
        {
            if (num >= data.ownHeroFightForce)
            {
                data.ownHeroFightForce = num;
            }
        }
        else if (type == "ownEquip")
        {
            data.ownEquip += num;
        }
        else if (type == "finishTarget")
        {
            data.finishTarget += num;
        }
        else if (type == "earnGold")
        {
            if (num <= int.MaxValue - data.earnCoin)
            {
                data.earnCoin += num;
            }
        }
        else if (type == "consumeCoin")
        {
            data.consumeCoin += num;
        }
        else if (type == "passedNum_level")
        {
            data.passedNum_level += num;
        }
        else if(type == "heroDie")
        {
            data.heroDie += num;
        }
    }
    public List<GDEItemData> GetAllEnemiesPlayerSaw
    {
        get { return PlayerData.achievementData.EnemiesGet; }
    }
    public void AddKillingDataToAchievement(string enemyId)
    {
        List<GDEItemData> All = GetAllEnemiesPlayerSaw;
        if(All.Exists(x=>x.id == enemyId))
        {
            foreach(GDEItemData d in PlayerData.achievementData.EnemiesGet)
            {
                if(d.id == enemyId) 
                {
                    d.num++;
                    PlayerData.achievementData.Set_EnemiesGet();
                    break; 
                }
            }
        }
        else
        {
            GDEItemData newE = new GDEItemData(GDEItemKeys.Item_MaterialEmpty)
            {
                id = enemyId,num=1,index = 0,
            };
            PlayerData.achievementData.EnemiesGet.Add(newE);
            PlayerData.achievementData.Set_EnemiesGet();
        }
    }
    #endregion
    #region Equipments_Infor
    #region getEquipedEquipmentByPosInHashcode
    public GDEEquipmentData getHeroEquipHelmet(int heroHashcode)
    {
        foreach (GDEHeroData hero in PlayerData.herosOwned)
        {
            if (hero.hashCode == heroHashcode)
            {
                if (hero.equipHelmet != null)
                {
                    bool exit = false;
                    foreach (GDEEquipmentData a in PlayerData.equipsOwned)
                    {
                        if (a.hashcode == hero.equipHelmet.hashcode)
                        {
                            exit = true;
                        }
                    }
                    if (exit)
                    {
                        return hero.equipHelmet;
                    }
                    else
                    {
                        hero.equipHelmet.id = string.Empty;
                        hero.equipHelmet.equipType = 0;
                        hero.equipHelmet.lv = 0;
                        hero.equipHelmet.equipBattleForce = 0;
                        hero.equipHelmet.hashcode = 0;
                        hero.equipHelmet.num = 0;
                        return hero.equipHelmet;
                    }
                }
            }
        }
        return null;
    }
    public GDEEquipmentData getHeroEquipBreastplate(int heroHashcode)
    {
        foreach (GDEHeroData hero in PlayerData.herosOwned)
        {
            if (hero.hashCode == heroHashcode)
            {
                if (hero.equipBreastplate != null)
                {
                    bool exit = false;
                    foreach (GDEEquipmentData a in PlayerData.equipsOwned)
                    {
                        if (a.hashcode == hero.equipBreastplate.hashcode)
                        {
                            exit = true;
                        }
                    }
                    if (exit)
                    {
                        return hero.equipBreastplate;
                    }
                    else
                    {
                        hero.equipBreastplate.id = string.Empty;
                        hero.equipBreastplate.equipType = 0;
                        hero.equipBreastplate.lv = 0;
                        hero.equipBreastplate.equipBattleForce = 0;
                        hero.equipBreastplate.hashcode = 0;
                        hero.equipBreastplate.num = 0;
                        return hero.equipBreastplate;
                    }
                }
            }
        }
        return null;
    }
    public GDEEquipmentData getHeroEquipGardebras(int heroHashcode)
    {
        foreach (GDEHeroData hero in PlayerData.herosOwned)
        {
            if (hero.hashCode == heroHashcode)
            {
                if (hero.equipGardebras != null)
                {
                    bool exit = false;
                    foreach (GDEEquipmentData a in PlayerData.equipsOwned)
                    {
                        if (a.hashcode == hero.equipGardebras.hashcode)
                        {
                            exit = true;
                        }
                    }
                    if (exit)
                    {
                        return hero.equipGardebras;
                    }
                    else
                    {
                        hero.equipGardebras.id = string.Empty;
                        hero.equipGardebras.equipType = 0;
                        hero.equipGardebras.lv = 0;
                        hero.equipGardebras.equipBattleForce = 0;
                        hero.equipGardebras.hashcode = 0;
                        hero.equipGardebras.num = 0;
                        return hero.equipGardebras;
                    }
                }
            }
        }
        return null;
    }
    public GDEEquipmentData getHeroEquipLegging(int heroHashcode)
    {
        foreach (GDEHeroData hero in PlayerData.herosOwned)
        {
            if (hero.hashCode == heroHashcode)
            {
                if (hero.equipLegging != null)
                {
                    bool exit = false;
                    foreach (GDEEquipmentData a in PlayerData.equipsOwned)
                    {
                        if (a.hashcode == hero.equipLegging.hashcode)
                        {
                            exit = true;
                        }
                    }
                    if (exit)
                    {
                        return hero.equipLegging;
                    }
                    else
                    {
                        hero.equipLegging.id = string.Empty;
                        hero.equipLegging.equipType = 0;
                        hero.equipLegging.lv = 0;
                        hero.equipLegging.equipBattleForce = 0;
                        hero.equipLegging.hashcode = 0;
                        hero.equipLegging.num = 0;
                        return hero.equipLegging;
                    }
                }
            }
        }
        return null;
    }
    public GDEEquipmentData getHeroEquipJewelry(int heroHashcode, bool isSecondJewelry = false)
    {
        foreach (GDEHeroData hero in PlayerData.herosOwned)
        {
            if (hero.hashCode == heroHashcode)
            {
                if (!isSecondJewelry)
                {
                    if (hero.jewelry0 != null)
                    {
                        bool exit = false;
                        foreach (GDEEquipmentData a in PlayerData.equipsOwned)
                        {
                            if (a.hashcode == hero.jewelry0.hashcode)
                            {
                                exit = true;
                            }
                        }
                        if (exit)
                        {
                            return hero.jewelry0;
                        }
                        else
                        {
                            hero.jewelry0.id = string.Empty;
                            hero.jewelry0.equipType = 0;
                            hero.jewelry0.lv = 0;
                            hero.jewelry0.equipBattleForce = 0;
                            hero.jewelry0.hashcode = 0;
                            hero.jewelry0.num = 0;
                            return hero.jewelry0;
                        }
                    }
                }
                else
                {
                    if (hero.jewelry1 != null)
                    {
                        bool exit = false;
                        foreach (GDEEquipmentData a in PlayerData.equipsOwned)
                        {
                            if (a.hashcode == hero.jewelry1.hashcode)
                            {
                                exit = true;
                            }
                        }
                        if (exit)
                        {
                            return hero.jewelry1;
                        }
                        else
                        {
                            hero.jewelry1.id = string.Empty;
                            hero.jewelry1.equipType = 0;
                            hero.jewelry1.lv = 0;
                            hero.jewelry1.equipBattleForce = 0;
                            hero.jewelry1.hashcode = 0;
                            hero.jewelry1.num = 0;
                            return hero.jewelry1;
                        }
                    }
                }
            }
        }
        return null;
    }
    public GDEEquipmentData getHeroWeapon(int heroHashcode)
    {
        foreach (GDEHeroData hero in PlayerData.herosOwned)
        {
            if (hero.hashCode == heroHashcode)
            {
                if (hero.equipWeapon != null)
                {
                    bool exit = getAllOwnedEquips().Exists(x 
                        => x.hashcode == hero.equipWeapon.hashcode && x.id == hero.equipWeapon.id);
                    if (exit)
                    {
                        return hero.equipWeapon;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        return null;
    }
    public GDEEquipmentData getHeroEquipmentByPos
        (int heroHashcode, EquipPosition pos, bool isSecondJPos = false)
    {
        switch (pos)
        {
            case EquipPosition.Head: return Instance.getHeroEquipHelmet(heroHashcode);
            case EquipPosition.Breast: return Instance.getHeroEquipBreastplate(heroHashcode);
            case EquipPosition.Arm: return Instance.getHeroEquipGardebras(heroHashcode);
            case EquipPosition.Leg: return Instance.getHeroEquipLegging(heroHashcode);
            case EquipPosition.Finger:
                if (!isSecondJPos) return Instance.getHeroEquipJewelry(heroHashcode);
                else return Instance.getHeroEquipJewelry(heroHashcode, true);
            case EquipPosition.Hand:
                return Instance.getHeroWeapon(heroHashcode);
            default: return null;
        }
    }
    public GDEEquipmentData equipEmpty()
    {
        GDEEquipmentData e = new GDEEquipmentData(GDEItemKeys.Equipment_EquipEmpty);
        e.hashcode = e.OwnerHashcode = e.equipType = e.equipBattleForce = e.index
            = e.num = e.lv = 0;
        e.id = string.Empty;
        return e;
    }
    #endregion
    public Sprite equipPosIcon(EquipPosition pos)
    {
        string n = "pos_" + pos.ToString().ToLower();
        return atlas_rarity.GetSprite(n);
    }
    public GDEEquipmentData getEquipmentByHashcode(int itemHashcode)
    {
        List<GDEEquipmentData> all = SDDataManager.Instance.PlayerData.equipsOwned;
        foreach (GDEEquipmentData e in all)
        {
            if (e.hashcode == itemHashcode)
            {
                return e;
            }
        }
        return null;
    }
    public List<GDEEquipmentData> getAllOwnedEquips()
    {
        return PlayerData.equipsOwned;
    }
    public List<GDEEquipmentData> getOwnedEquipsByPos(EquipPosition Pos, bool listOrder = false)
    {
        List<GDEEquipmentData> equips = SDDataManager.Instance.PlayerData.equipsOwned;
        List<GDEEquipmentData> outData = new List<GDEEquipmentData>();
        for (int i = 0; i < equips.Count; i++)
        {
            GDEEquipmentData e = equips[i];
            if (SDDataManager.Instance.getEquipPosById(e.id) == (int)Pos)
            {
                outData.Add(e);
            }
        }
        if (listOrder)
        {
            List<GDEEquipmentData> equipsListOrder = outData;
            equipsListOrder.Sort((x,y)=> 
            {
                int l_x = getEquipRarityById(x.id);
                int l_y = getEquipRarityById(y.id);
                return -l_x.CompareTo(l_y);
            });
            return equipsListOrder;
        }
        return outData;
    }
    public List<GDEEquipmentData> GetPosOwnedEquipsByCareer(EquipPosition Pos
        ,string heroId, bool listOrder = false)
    {
        List<GDEEquipmentData> allEquips = SDDataManager.Instance.getOwnedEquipsByPos(Pos, listOrder);
        if (Pos == EquipPosition.Hand)
        {
            List<WeaponRace> heroCanUse = getHeroInfoById(heroId).WeaponRaceList;
            List<GDEEquipmentData> AllEs = allEquips.FindAll
                (x => 
                {
                    if (string.IsNullOrEmpty(heroId)) return true;
                    WeaponRace race = GetEquipItemById(x.id).WeaponRace;
                    return heroCanUse.Contains(race);
                });
            return AllEs;
        }
        else
        {
            return allEquips;
        }
    }

    public List<EquipItem> AllEquipList
    {
        get
        {
            List<EquipItem> results = AllList_EquipItem;
            if (results == null)
            {
                results = new List<EquipItem>();
                EquipItem[] all = Resources.LoadAll<EquipItem>
                   ("ScriptableObjects/items/Equips");
                for (int i = 0; i < all.Length; i++)
                {
                    results.Add(all[i]);
                }
            }
            return results;
        }
    }
    public EquipItem GetEquipItemById(string id)
    {
        List<EquipItem> all = AllEquipList;
        foreach(EquipItem item in all)
        {
            if (item.ID == id) return item;
        }
        return null;
    }

    public Sprite GetEquipIconById(string id)
    {
        EquipItem item = GetEquipItemById(id);
        return item.IconFromAtlas;
    }
    public int getEquipPosById(string id)
    {
        List<EquipItem> all = AllEquipList;
        foreach(EquipItem item in all)
        {
            if(item.ID == id)
            {
                return (int)item.EquipPos;
            }
        }
        return (int)EquipPosition.End;
    }
    /// <summary>
    /// same as LEVEL for equip
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public int getEquipRarityById(string id)
    {
        List<EquipItem> all = AllEquipList;
        foreach (EquipItem item in all)
        {
            if (item.ID == id)
            {
                return item.LEVEL;
            }
        }
        return 0;
    }
    public int getEquipBaiscBattleForceById(string id)
    {
        EquipItem item = GetEquipItemById(id);
        return item.BattleForce;
    }
    public int getEquipBattleForceByHashCode(int itemHashcode)
    {
        GDEEquipmentData equip = SDDataManager.Instance.getEquipmentByHashcode(itemHashcode);
        int basic = SDDataManager.Instance.getEquipBaiscBattleForceById(equip.id);
        int level = equip.lv;
        int flag = (int)(basic * (1 + level * 0.15f));
        return flag;
    }
    public string getEquipNameByHashcode(int itemHashcode)
    {
        GDEEquipmentData e = SDDataManager.Instance.getEquipmentByHashcode(itemHashcode);
        string id = e.id;
        EquipItem item = GetEquipItemById(id);
        if (item) return item.NAME;
        return SDGameManager.T("无此装备");
    }
    public bool checkEquipFixIfSuccess(int hashcode)
    {
        foreach (GDEEquipmentData e in PlayerData.equipsOwned)
        {
            if (e.hashcode == hashcode)
            {
                int quality = e.quality;
                if (quality >= SDConstants.equipMaxQuality) return false;
                float rate = Mathf.Min(1
                    , (SDConstants.equipMaxQuality - quality - 1) * 0.2f + 0.25f);
                if (UnityEngine.Random.Range(0, 1) < rate)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public int getEquipQualityByHashcode(int hashcode)
    {
        foreach (GDEEquipmentData e in PlayerData.equipsOwned)
        {
            if (e.hashcode == hashcode)
            {
                return e.quality;
            }
        }
        return 0;
    }
    public bool PromoteEquipQuality(int hashcode, int promote = 1)
    {
        foreach (GDEEquipmentData e in PlayerData.equipsOwned)
        {
            if (e.hashcode == hashcode)
            {
                if (e.quality < SDConstants.equipMaxQuality)
                {
                    e.quality += promote;
                    return true;
                }
            }
        }
        return false;
    }

    public void LvupEquipByHashcode(int hashcode, int figure = 1)
    {
        foreach (GDEEquipmentData equip in PlayerData.equipsOwned)
        {
            if (equip.hashcode == hashcode)
            {
                equip.lv += figure;
                PlayerData.Set_equipsOwned();
                break;
            }
        }
    }


    public void addEquip(GDEEquipmentData equip)
    {
        Instance.equipNum++;
        //
        add_Item(equip.id);
        //
        equip.hashcode = Instance.equipNum;
        equip.OwnerHashcode = 0;
        Instance.PlayerData.equipsOwned.Add(equip);
        Instance.PlayerData.Set_equipsOwned();
    }
    public void addEquip(string id)
    {
        Instance.equipNum++;
        //
        add_Item(id);
        //
        int level = GetEquipItemById(id).LEVEL;
        GDEEquipmentData e = new GDEEquipmentData(GDEItemKeys.Equipment_EquipEmpty)
        {
            id = id,
            hashcode = Instance.equipNum,
            OwnerHashcode = 0,
            locked = level<2?false:true,
            lv = 0,
            num = 1,
            index = 0,
            quality = UnityEngine.Random.Range(0, SDConstants.equipMaxQuality),
            initialQuality = UnityEngine.Random.Range(0, 1),
        };
        Instance.PlayerData.equipsOwned.Add(e);
        Instance.PlayerData.Set_equipsOwned();
    }
    public bool consumeEquip(int hashcode)
    {
        foreach (GDEEquipmentData E in PlayerData.equipsOwned)
        {
            if (E.hashcode == hashcode && !E.locked)
            {
                PlayerData.equipsOwned.Remove(E);
                PlayerData.Set_equipsOwned();
                consume_Item(E.id, out int leftAmount);
                return true;
            }
        }
        Debug.Log("对象(装备#" + hashcode + ")不存在或已被锁定");
        return false;
    }
    public IEnumerable<GDEEquipmentData> FindAllArmorsById(string id, bool onlyUnlocked = true)
    {
        return PlayerData.equipsOwned.FindAll(x => x.id == id
        && (onlyUnlocked ? true : !x.locked)).AsEnumerable();
    }
    #endregion
    #region residentMovementInfor
    public RoleAttributeList BuffFromRace(RoleAttributeList basic, Race r)
    {
        RoleAttributeList ral = new RoleAttributeList();
        //人类
        if (r == Race.Human)
        {

        }
        //精灵在夜间战斗力上升，白天速度降低
        else if (r == Race.Elf)
        {
            if (ResidentMovementData.CurrentDayNightId == 1)//夜间
            {
                ral.AT = (int)(basic.read(AttributeData.AT) * 0.1f);
                ral.MT = (int)(basic.read(AttributeData.MT) * 0.1f);
                ral.Speed = (int)(basic.read(AttributeData.Speed) * 0.2f);
                ral.Accur = (int)(basic.read(AttributeData.Accur) * 0.2f);
            }
            else
            {
                ral.Speed = -(int)(basic.read(AttributeData.Speed) * 0.1f);
            }
        }
        //龙裔基础能力周期性增强
        else if (r == Race.Dragonborn)
        {
            ActionBarManager abm = FindObjectOfType<ActionBarManager>();
            if (abm)
            {
                int flag = abm.CurrentRoundNum % 4;
                flag = flag <= 2 ? flag : 4 - flag;
                ral.AT = (int)(basic.read(AttributeData.AT) * flag * 1f / 50);
                ral.AD = (int)(basic.read(AttributeData.AD) * flag * 1f / 50);
                ral.MT = (int)(basic.read(AttributeData.MT) * flag * 1f / 50);
                ral.MD = (int)(basic.read(AttributeData.MT) * flag * 1f / 50);
            }
        }

        return ral;
    }
    public RoleAttributeList BuffFromDaynight(RoleAttributeList basic)
    {
        RoleAttributeList ral = new RoleAttributeList();
        if (ResidentMovementData.CurrentDayNightId == 1)//夜间
        {
            ral.Accur = -basic.read(AttributeData.Accur);
        }
        else
        {

        }
        return ral;
    }
    #endregion
    #region Skill_Infor
    public OneSkill getOwnedSkillById(string skillId, int heroHashcode)
    {
        if (string.IsNullOrEmpty(skillId)) return null;
        List<OneSkill> all = getSkillListByHashcode(heroHashcode);
        return all.Find(x => x.skillId == skillId);
    }
    public OneSkill getSkillByHeroId(string skillId, string heroId)
    {
        HeroInfo info = getHeroInfoById(heroId);
        List<OneSkill> all
            = SkillDetailsList.WriteOneSkillList(heroId);
        foreach (OneSkill s in all)
        {
            if (s.skillId == skillId)
            {
                return s;
            }
        }
        return null;
    }
    public List<OneSkill> getSkillListByHashcode(int _hashcode)
    {
        GDEHeroData data = getHeroByHashcode(_hashcode);
        List<OneSkill> All = SkillDetailsList.WriteOneSkillList(data.id);
        //
        All = All.FindAll(x =>
        {
        if (x.skillId == data.a_skill0.Id)
        {
            x.lv = data.a_skill0.Lv; return true;
        }
        else if (data.a_skill1 != null && x.skillId == data.a_skill1.Id)
            {
                x.lv = data.a_skill1.Lv;return true;
            }
            else if(x.skillId == data.a_skillOmega.Id)
            {
                x.lv = data.a_skillOmega.Lv;return true;
            }
            else return false;
        });
        All.Sort((x, y) => 
        {
            return x.isOmegaSkill.CompareTo(y.isOmegaSkill);
        });
        return All;
    }
    public skillInfo getDeployedSkillId(int skillPos, int heroHashcode)
    {
        GDEHeroData data = getHeroByHashcode(heroHashcode);
        HeroInfo info = getHeroInfoById(data.id);
        if (skillPos == 0) return info.Skill0Info;
        else if (skillPos == 1) return info.Skill1Info;
        else if (skillPos == 2) return info.SkillOmegaInfo;
        return null;
    }
    public bool CheckIfHaveThisSkill(string skillId, int heroHashcode)
    {
        GDEHeroData hero = getHeroByHashcode(heroHashcode);
        HeroInfo info = getHeroInfoById(hero.id);
        List<OneSkill> list = SkillDetailsList.WriteOneSkillList(hero.id);
        return list.Exists(x => x.skillId == skillId);
    }
    public int SkillGradeNumWaitingForImprove(int hashcode)
    {
        GDEHeroData data = getHeroByHashcode(hashcode);
        int a = 0;
        if(data.a_skill0 != null && !string.IsNullOrEmpty(data.a_skill0.Id))
        {
            Debug.Log("S0:" + data.a_skill0.Lv);
            a += SDConstants.SkillMaxGrade - data.a_skill0.Lv;
        }
        if (data.a_skill1 != null && !string.IsNullOrEmpty(data.a_skill1.Id))
        {
            Debug.Log("S1:" + data.a_skill1.Lv);
            a += SDConstants.SkillMaxGrade - data.a_skill1.Lv;
        }
        if (data.a_skillOmega != null && !string.IsNullOrEmpty(data.a_skillOmega.Id))
        {
            Debug.Log("SOmega:" + data.a_skillOmega.Lv);
            a += SDConstants.SkillMaxGrade - data.a_skillOmega.Lv;
        }
        return a;
    }
    #endregion
    #region Building_Infor
    public bool LvUpBuilding(string id, int level = 1)
    {
        foreach (GDEtownBuildingData B in PlayerData.buildingsOwned)
        {
            if (B.id == id)
            {
                B.level += level;
                PlayerData.Set_buildingsOwned();
            }
        }
        return false;
    }
    #endregion
    #region Enemy_Infor
    public ROEnemyData getEnemyDataById(string id)
    {
        ROEnemyData d = new ROEnemyData();
        EnemyInfo enemy = SDDataManager.Instance.getEnemyInfoById(id);
        if (enemy)
        {
            d.Info = enemy;
            d.CRIDmg = 175;
            d.DmgReduction = 0;
            d.DmgReflection = 0;
            d.dropCoins = 5 * enemy.EnemyRank.Index + 5;
            return d;
        }
        return null;
    }
    public List<EnemyInfo> AllEnemyList
    {
        get 
        {
            if (AllList_EnemyInfo!=null) return AllList_EnemyInfo;
            List<EnemyInfo> results = new List<EnemyInfo>();
            EnemyInfo[] all = Resources.LoadAll<EnemyInfo>
                ("ScriptableObjects/enemies");
            for(int i = 0; i < all.Length; i++)
            {
                results.Add(all[i]);
            }
            return results;
        }
    }
    public EnemyInfo getEnemyInfoById(string id)
    {
        return AllEnemyList.Find(x => x.ID == id);
    }
    public EnemyRank getEnemyRankByIndex(int race)
    {
        EnemyRank[] allranks = Resources.LoadAll<EnemyRank>("");
        foreach (EnemyRank rank in allranks)
        {
            if (rank.Index == race) return rank;
        }
        return null;
    }
    #endregion
    #region Rune_Infor
    public List<RuneItem> AllRuneList
    {
        get
        {
            if (AllList_RuneItem != null) return AllList_RuneItem;
            RuneItem[] all = Resources.LoadAll<RuneItem>
                ("ScriptableObjects/Items/Runes");
            return all.ToList();
        }
    }
    public RuneItem getRuneItemById(string id)
    {
        return AllRuneList.Find(x => x.ID == id);
    }
    public List<GDERuneData> getAllRunesOwned
    {
        get 
        {
            List<GDERuneData> list = PlayerData.RunesOwned;
            bool flag = list.GroupBy(x => x.Hashcode).Where(x => x.Count() > 1).Count() > 0;
            if (flag)
            {
                for(int i = 0; i < list.Count; i++)
                {
                    list[i].Hashcode = i + 1;
                }
                PlayerData.RunesOwned = list;
                PlayerData.Set_RunesOwned();
            }
            return PlayerData.RunesOwned; 
        }
    }
    public GDERuneData getRuneOwnedByHashcode(int hashcode)
    {
        return PlayerData.RunesOwned.Find(x => x.Hashcode == hashcode);
    }
    public bool getRuneEquippedByPosAndGoddess(int pos, string ownerId, out GDERuneData data)
    {
        GDEgoddessData goddess = getGDEGoddessDataById(ownerId);
        if(pos == 0)
        {
            GDERuneData _data = getRuneOwnedByHashcode(goddess.rune0);
            if (_data != null)
            {
                data = _data;
                return true;
            }
        }
        else if(pos == 1)
        {
            GDERuneData _data = getRuneOwnedByHashcode(goddess.rune1);
            if (_data != null)
            {
                data = _data;
                return true;
            }
        }
        else if(pos == 2)
        {
            GDERuneData _data = getRuneOwnedByHashcode(goddess.rune2);
            if (_data != null)
            {
                data = _data;
                return true;
            }
        }
        else if(pos == 3)
        {
            GDERuneData _data = getRuneOwnedByHashcode(goddess.rune3);
            if (_data != null)
            {
                data = _data;
                return true;
            }
        }
        data = null;return false;
    }
    public bool checkRuneEquippedByGoddess(int hashcode,string ownerId,out int pos)
    {
        GDEgoddessData goddess = getGDEGoddessDataById(ownerId);
        if (goddess == null)
        {
            pos = 0;return false;
        }
        if (goddess.rune0 == hashcode)
        {
            pos = 0; return true;
        }
        else if (goddess.rune1 == hashcode)
        {
            pos = 1; return true;
        }
        else if (goddess.rune2 == hashcode)
        {
            pos = 2; return true;
        }
        else if(goddess.rune3 == hashcode)
        {
            pos = 3;return true;
        }
        pos = 0;
        return false;
    }
    public void AddRune(string runeId)
    {
        Instance.runeNum++;
        add_Item(runeId);
        RuneItem item = getRuneItemById(runeId);
        if (item)
        {
            GDERuneData _rune = new GDERuneData(GDEItemKeys.Rune_RuneEmpty)
            {
                id = runeId,
                Hashcode = Instance.runeNum,
                posInOwner = 0,
                ownerId = string.Empty,
                quality = item.Quality,
                level = 0,
                initalQuality = 0,
                locked = false,
                //
                attitube = new GDEgoddessAttiData
                    (GDEItemKeys.goddessAtti_emptyGAtti)
                {
                    stamina = UnityEngine.Random.Range(0, 2) + item.Atti.Stamina,
                    agile = UnityEngine.Random.Range(0, 2) + item.Atti.Agile,
                    recovery = UnityEngine.Random.Range(0, 2) + item.Atti.Recovery,
                    leader = UnityEngine.Random.Range(0, 2) + item.Atti.Leader,
                }
            };
            PlayerData.RunesOwned.Add(_rune);
            PlayerData.Set_RunesOwned();
        }
    }
    public bool ConsumeRune(int hashcode)
    {
        foreach(GDERuneData rune in PlayerData.RunesOwned)
        {
            if(rune.Hashcode == hashcode)
            {
                PlayerData.RunesOwned.Remove(rune);
                PlayerData.Set_RunesOwned();
                consume_Item(rune.id, out int leftamount);
                return true;
            }
        }
        return false;
    }
    public void addRuneToGoddessSlot(int runeHashcode,string goddessId, int pos)
    {
        GDERuneData rune = getRuneOwnedByHashcode(runeHashcode);
        int oldRune = 0;
        if (rune != null)
        {
            //从原始所有者上卸下
            if (PlayerData.goddessOwned.Exists(x => x.id == rune.ownerId))
            {
                foreach (GDEgoddessData g in PlayerData.goddessOwned)
                {
                    if (g.id == rune.ownerId)
                    {
                        if (g.rune0 == runeHashcode) { g.rune0 = 0; }
                        if (g.rune1 == runeHashcode) { g.rune1 = 0; }
                        if (g.rune2 == runeHashcode) { g.rune2 = 0; }
                        if (g.rune3 == runeHashcode) { g.rune3 = 0; }
                        PlayerData.Set_goddessOwned();
                        break;
                    }
                }
            }
            //装至目标身上
            foreach (GDEgoddessData goddess in PlayerData.goddessOwned)
            {
                if(goddess.id == goddessId)
                {
                    if(pos == 0)
                    {
                        oldRune = goddess.rune0;
                        goddess.rune0 = runeHashcode;
                    }
                    else if(pos == 1)
                    {
                        oldRune = goddess.rune1;
                        goddess.rune1 = runeHashcode;
                    }
                    else if(pos == 2)
                    {
                        oldRune = goddess.rune2;
                        goddess.rune2 = runeHashcode;
                    }
                    else if(pos == 3)
                    {
                        oldRune = goddess.rune3;
                        goddess.rune3 = runeHashcode;
                    }
                    PlayerData.Set_goddessOwned();
                    break;
                }
            }
            //修改原装备信息
            foreach(GDERuneData R in PlayerData.RunesOwned)
            {
                if(R.Hashcode == oldRune)
                {
                    R.ownerId = string.Empty;
                    PlayerData.Set_RunesOwned();
                    break;
                }
            }
            //修改新装备信息
            foreach (GDERuneData _r in PlayerData.RunesOwned)
            {
                if (_r.Hashcode == runeHashcode)
                {
                    _r.ownerId = goddessId;
                    break;
                }
            }
        }
    }
    public bool checkRuneStatus(int hashcode)
    {
        GDERuneData rune = getRuneOwnedByHashcode(hashcode);
        if (rune!=null && !string.IsNullOrEmpty(rune.ownerId))
        {
            if(PlayerData.goddessOwned.Exists(x=>x.id == rune.ownerId))
            {
                return true;
            }
        }
        return false;
    }
    public bool lvUpRune(int hashcode, int levelUp = 1)
    {
        GDERuneData rune = getRuneOwnedByHashcode(hashcode);
        if (rune.level + levelUp > SDConstants.RuneMaxLevel) return false;
        RuneItem r = getRuneItemById(rune.id);
        int coinWill = getCoinWillImproveCost(rune.level,rune.quality,levelUp);
        if (PlayerData.coin < coinWill) return false;
        //
        foreach(GDERuneData R in PlayerData.RunesOwned)
        {
            if(R.Hashcode == hashcode)
            {
                R.level += levelUp;
                R.attitube = chooseAttiElementToImprove(r,R, levelUp);
                PlayerData.coin -= coinWill;
                PlayerData.Set_RunesOwned();
                return true;
            }
        }
        return false;
    }
    GDEgoddessAttiData chooseAttiElementToImprove(RuneItem r,GDERuneData R,int levelUp)
    {
        GDEgoddessAttiData GA = R.attitube;
        int up = (int)((R.quality + 1) * levelUp);
        if (r.AttiType == GoddessAttiType.agile) GA.agile += up;
        else if (r.AttiType == GoddessAttiType.stamina) GA.stamina += up;
        else if (r.AttiType == GoddessAttiType.recovery) GA.recovery += up;
        else if (r.AttiType == GoddessAttiType.leader) GA.leader += up;
        return GA;
    }
    public int getCoinWillImproveCost(int level, int quality, int levelUp = 1)
    {
        int result = 0;
        for(int i = 0; i < levelUp; i++)
        {
            result += (level + i + 1) 
                * SDConstants.BaseCoinSillImproveCost * (quality + 1);
        }
        return result;
    }
    public bool CheckIfCanComposeToCreateNewRune(GDERuneData rune0,GDERuneData rune1,GDERuneData rune2
        ,out string newRuneId)
    {
        if (rune0 != null && rune1 != null && rune2 != null)
        {
            int[] quals = new int[] { rune0.quality, rune1.quality, rune2.quality };
            int wholeQ = rune0.quality + rune1.quality + rune2.quality;
            RuneItem[] all = Resources.LoadAll<RuneItem>("ScriptableObjects/Items/Runes");
            List<RuneItem> rewards = new List<RuneItem>();
            if(quals.Min() == 0)
            {
                //0---1---2
                int d = 1;
                float r = SDConstants.composeBasicFigure;
                for(int i = 0; i < wholeQ; i++)
                {
                    r *= SDConstants.composeChangeFigure;
                }
                if(UnityEngine.Random.Range(0,1f) >= r)
                {
                    d = 2;
                }
                //
                for (int i = 0; i < all.Length; i++)
                {
                    if (all[i].Quality < d)
                    {
                        rewards.Add(all[i]);
                    }
                }
            }
            else if(quals.Min() == 1)
            {
                //3---4---5
                int d = 1;
                float r = SDConstants.composeBasicFigure;
                for (int i = 0; i < wholeQ-3; i++)
                {
                    r *= SDConstants.composeChangeFigure;
                }
                if (UnityEngine.Random.Range(0, 1f) >= r)
                {
                    d = 2;
                }
                //
                for (int i = 0; i < all.Length; i++)
                {
                    if (all[i].Quality >= d)
                    {
                        rewards.Add(all[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < all.Length; i++)
                {
                    if (all[i].Quality > 1)
                    {
                        rewards.Add(all[i]);
                    }
                }
            }
            RuneItem RI = rewards[UnityEngine.Random.Range(0, rewards.Count)];

            newRuneId = RI.ID;
            return true;
        }
        else
        {
            newRuneId = string.Empty;return false;
        }
    }
    public GoddessAttritube GetGoddessAttiByGDE(GDEgoddessAttiData atti)
    {
        if (atti == null) return GoddessAttritube.zero;
        GoddessAttritube GA = new GoddessAttritube
            (atti.agile,atti.stamina,atti.recovery,atti.leader);
        return GA;
    }
    #endregion
    #region Item_Infor(全物品记录)
    private int add_Item(string id, int amount = 1)
    {
        foreach (GDEItemData I in PlayerData.ItemsOwned)
        {
            if (I.id == id)
            {
                I.num += amount;
                PlayerData.Set_ItemsOwned();
                //
                TriggerManager.Instance.WhenGetItem(id);
                //
                return I.num;
            }
        }
        GDEItemData D = new GDEItemData(GDEItemKeys.Item_MaterialEmpty)
        {
            id = id
            ,
            num = amount
        };
        PlayerData.ItemsOwned.Add(D);
        PlayerData.Set_ItemsOwned();
        //
        TriggerManager.Instance.WhenGetItem(id);
        //
        return D.num;
    }
    private bool consume_Item(string id, out int leftAmonut, int amount = 1)
    {
        foreach (GDEItemData D in PlayerData.ItemsOwned)
        {
            if (D.id == id)
            {
                bool flag;
                if (D.num < amount)
                {
                    D.num = 0;
                    leftAmonut = 0;
                    flag = false;
                }
                else
                {
                    D.num -= amount;
                    leftAmonut = D.num;
                    flag = true;
                }
                if (D.num <= 0)
                {
                    PlayerData.ItemsOwned.Remove(D);
                }
                PlayerData.Set_ItemsOwned();
                //
                if(flag) TriggerManager.Instance.WhenLoseItem(id);
                //
                return flag;
            }
        }
        leftAmonut = 0;
        return false;
    }

    public void AddItem(string id , int amount = 1)
    {
        if (id.Contains("_"))
        {
            string Sign = id.Split('_')[0];
            if (Sign == "H")
            {
                addHero(id);
            }
            else if (Sign == "A")
            {
                addEquip(id);
            }
            else if (Sign == "M")
            {
                addConsumable(id, amount);
            }
        }
        else
        {
            addConsumable(id, amount);
        }
    }

    public bool LoseItem(string id, int amount = 1)
    {
        if (CheckIfHaveItemById(id) && amount > 0)
        {
            if (id.Contains("_"))
            {
                string Sign = id.Split('_')[0];
                if (Sign == "H")
                {
                    for(int i = 0; i < amount; i++)
                    {
                        foreach (var HD in FindAllHerosById(id))
                        {
                            consumeHero(HD.hashCode);break;
                        }
                    }
                    return true;
                }
                else if(Sign == "A")
                {
                    for (int i = 0; i < amount; i++)
                    {
                        foreach (var ED in FindAllArmorsById(id))
                        {
                            consumeEquip(ED.hashcode); break;
                        }
                    }
                    return true; 
                }
                else if(Sign == "M")
                {
                    consumeConsumable(id, out int left, amount);
                    return true;
                }
            }
        }
        return false;
    }

    public bool CheckIfHaveItemById(string id)
    {
        foreach (GDEItemData I in PlayerData.ItemsOwned)
        {
            if (I.id == id)
            {
                return true;
            }
        }
        return false;
    }
    public int GetItemAmount(string id)
    {
        foreach (GDEItemData I in PlayerData.ItemsOwned)
        {
            if (I.id == id)
            {
                return I.num;
            }
        }
        return 0;
    }


    public int HeroBattleForce(int hashcode)
    {
        int h = getHeroOriginalBattleForceByHashCode(hashcode);
        for(int i = 0; i < (int)EquipPosition.End; i++)
        {
            h += getEquipBattleForceByHashCode(hashcode);
        }
        return h;
    }
    #endregion
    #region NPC_Infor
    public void AddSlave(string enemyId)
    {
        slaveNum++;
        GDENPCData slave = new GDENPCData(GDEItemKeys.NPC_noone)
        {
            id = enemyId,
            hashcode = slaveNum,
            workingInBuliding=true,
            ShowInBag=true,
            exp=0,
            likability=0,
        };
        PlayerData.NPCList.Add(slave);
        PlayerData.Set_NPCList();
    }
    public GDENPCData GetNPCOwned(int hashcode)
    {
        return PlayerData.NPCList.Find(x => x.hashcode == hashcode);
    }
    #endregion


    public int getNumFromId(string id)
    {
        if (id.Contains("#")) return getInteger(id.Split('#')[1]);
        else return getInteger(id);
    }
    public string rarityString(int quality)
    {
        string n = SDGameManager.T("N");
        if (quality == 1) { n = SDGameManager.T("R"); }
        else if (quality == 2) { n = SDGameManager.T("SR"); }
        else if (quality == 3) { n = SDGameManager.T("SSR"); }
        return n;
    }
    public Sprite raritySprite(int quality)
    {
        return atlas_rarity.GetSprite(rarityString(quality).ToLower());
    }
    public Sprite baseFrameSpriteByRarity(int quality)
    {
        string n = string.Format("frame{0:D1}", quality);
        return atlas_rarity.GetSprite(n);
    }
    public Sprite baseBgSpriteByRarity(int quality)
    {
        string n = string.Format("itemBg{0:D1}", quality);
        return atlas_rarity.GetSprite(n);
    }
    public Sprite heroFrameSpriteByRarity(int quality)
    {
        string n = string.Format("heroFrame{0:D1}", quality);
        return atlas_rarity.GetSprite(n);
    }
    public Sprite heroBgSpriteByRarity(int quality)
    {
        string n = string.Format("heroBg{0:D1}", quality);
        return atlas_rarity.GetSprite(n);
    }
    public Sprite heroRaceBgIcon(Race race)
    {
        string n = "heroRaceBg_" + race.ToString().ToLower();
        return atlas_UI.GetSprite(n);
    }
    #endregion
}


public class ROHelp
{
    public static List<Dictionary<string,string>> ConvertCsvListToDictWithAttritubes
        (List<string[]> list)
    {
        List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();
        if (list.Count <= 1) return results;
        string[] atrs = list[0];//0处存放索引，1处开始存放内容
        for(int i = 1; i < list.Count; i++)
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            string[] sing = list[i];
            for(int k = 0; k < atrs.Length; k++)
            {
                d[atrs[k]] = sing[k];
            }
            results.Add(d);
        }
        return results;
    }
    public static SDConstants.AOEType AOE_TYPE(string data)
    {
        for(int i = 0; i < (int)SDConstants.AOEType.End; i++)
        {
            if(data.ToLower() == ((SDConstants.AOEType)i).ToString().ToLower())
            {
                return (SDConstants.AOEType)i;
            }
        }
        return SDConstants.AOEType.None;
    }
    public static StateTag STATE_TAG(string s)
    {
        for (int i = 0; i < (int)StateTag.End; i++)
        {
            if (s.ToLower() == ((StateTag)i).ToString().ToLower())
            {
                return (StateTag)i;
            }
        }
        return StateTag.End;
    }
    public static AttributeData AD_TAG(string s)
    {
        for (int i = 0; i < (int)AttributeData.End; i++)
        {
            if (s.ToLower() == ((AttributeData)i).ToString().ToLower())
            {
                return (AttributeData)i;
            }
        }
        return AttributeData.End;
    }
    public static bool CheckStringIsADElseST(string s,out AttributeData ad,out StateTag st)
    {
        ad = AD_TAG(s);st = STATE_TAG(s);
        if (ad == AttributeData.End && st != StateTag.End)
        {
            return false;
        }
        else return true;
    }
    public static EquipPosition EQUIP_POS(string s)
    {
        for(int i = 0; i < (int)EquipPosition.End; i++)
        {
            if(s.ToLower() == ((EquipPosition)i).ToString().ToLower())
            {
                return (EquipPosition)i;
            }
        }
        return EquipPosition.End;
    }
    public static Job getJobByString(string s)
    {
        for (int i = 0; i < (int)Job.End; i++)
        {
            if (s.ToLower() == ((Job)i).ToString().ToLower()) return (Job)i;
        }
        return Job.End;
    }

    public static List<T> RandomList<T>(List<T> originalList)
    {
        int length = originalList.Count;
        List<T> oldList = originalList;
        List<T> list = new List<T>();
        while (list.Count < length)
        {
            int a = UnityEngine.Random.Range(0, oldList.Count);
            if (!list.Contains(oldList[a]))
            {
                list.Add(oldList[a]);
                oldList.RemoveAt(a);
            }
        }
        return list;
    }
}

