// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by the Game Data Editor.
//
//      Changes to this file will be lost if the code is regenerated.
//
//      This file was generated from this data file:
//      E:\RL_CardGame_Build\Assets/GameDataEditor/Resources/gde_data.txt
//  </autogenerated>
// ------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.Video;
using System;
using System.Collections.Generic;

using GameDataEditor;

namespace GameDataEditor
{
    public class GDEPlayerData : IGDEData
    {
        static string maxPassLevelKey = "maxPassLevel";
		int _maxPassLevel;
        public int maxPassLevel
        {
            get { return _maxPassLevel; }
            set {
                if (_maxPassLevel != value)
                {
                    _maxPassLevel = value;
					GDEDataManager.SetInt(_key, maxPassLevelKey, _maxPassLevel);
                }
            }
        }

        static string lastPassLevelKey = "lastPassLevel";
		int _lastPassLevel;
        public int lastPassLevel
        {
            get { return _lastPassLevel; }
            set {
                if (_lastPassLevel != value)
                {
                    _lastPassLevel = value;
					GDEDataManager.SetInt(_key, lastPassLevelKey, _lastPassLevel);
                }
            }
        }

        static string maxDurgeonPassLevelKey = "maxDurgeonPassLevel";
		int _maxDurgeonPassLevel;
        public int maxDurgeonPassLevel
        {
            get { return _maxDurgeonPassLevel; }
            set {
                if (_maxDurgeonPassLevel != value)
                {
                    _maxDurgeonPassLevel = value;
					GDEDataManager.SetInt(_key, maxDurgeonPassLevelKey, _maxDurgeonPassLevel);
                }
            }
        }

        static string coinKey = "coin";
		int _coin;
        public int coin
        {
            get { return _coin; }
            set {
                if (_coin != value)
                {
                    _coin = value;
					GDEDataManager.SetInt(_key, coinKey, _coin);
                }
            }
        }

        static string n_coinKey = "n_coin";
		int _n_coin;
        public int n_coin
        {
            get { return _n_coin; }
            set {
                if (_n_coin != value)
                {
                    _n_coin = value;
					GDEDataManager.SetInt(_key, n_coinKey, _n_coin);
                }
            }
        }

        static string addGoldPercKey = "addGoldPerc";
		int _addGoldPerc;
        public int addGoldPerc
        {
            get { return _addGoldPerc; }
            set {
                if (_addGoldPerc != value)
                {
                    _addGoldPerc = value;
					GDEDataManager.SetInt(_key, addGoldPercKey, _addGoldPerc);
                }
            }
        }

        static string allBufPercKey = "allBufPerc";
		int _allBufPerc;
        public int allBufPerc
        {
            get { return _allBufPerc; }
            set {
                if (_allBufPerc != value)
                {
                    _allBufPerc = value;
					GDEDataManager.SetInt(_key, allBufPercKey, _allBufPerc);
                }
            }
        }

        static string newBestLevelKey = "newBestLevel";
		int _newBestLevel;
        public int newBestLevel
        {
            get { return _newBestLevel; }
            set {
                if (_newBestLevel != value)
                {
                    _newBestLevel = value;
					GDEDataManager.SetInt(_key, newBestLevelKey, _newBestLevel);
                }
            }
        }

        static string dimensionKey = "dimension";
		int _dimension;
        public int dimension
        {
            get { return _dimension; }
            set {
                if (_dimension != value)
                {
                    _dimension = value;
					GDEDataManager.SetInt(_key, dimensionKey, _dimension);
                }
            }
        }

        static string damondKey = "damond";
		int _damond;
        public int damond
        {
            get { return _damond; }
            set {
                if (_damond != value)
                {
                    _damond = value;
					GDEDataManager.SetInt(_key, damondKey, _damond);
                }
            }
        }

        static string temple_fighterKey = "temple_fighter";
		public List<int>      temple_fighter;
		public void Set_temple_fighter()
        {
	        GDEDataManager.SetIntList(_key, temple_fighterKey, temple_fighter);
		}
		
        static string temple_rangerKey = "temple_ranger";
		public List<int>      temple_ranger;
		public void Set_temple_ranger()
        {
	        GDEDataManager.SetIntList(_key, temple_rangerKey, temple_ranger);
		}
		
        static string temple_priestKey = "temple_priest";
		public List<int>      temple_priest;
		public void Set_temple_priest()
        {
	        GDEDataManager.SetIntList(_key, temple_priestKey, temple_priest);
		}
		
        static string temple_casterKey = "temple_caster";
		public List<int>      temple_caster;
		public void Set_temple_caster()
        {
	        GDEDataManager.SetIntList(_key, temple_casterKey, temple_caster);
		}
		
        static string bossKeysKey = "bossKeys";
		public List<int>      bossKeys;
		public void Set_bossKeys()
        {
	        GDEDataManager.SetIntList(_key, bossKeysKey, bossKeys);
		}
		
        static string bossKeyUsedKey = "bossKeyUsed";
		public List<int>      bossKeyUsed;
		public void Set_bossKeyUsed()
        {
	        GDEDataManager.SetIntList(_key, bossKeyUsedKey, bossKeyUsed);
		}
		
        static string propsTeamKey = "propsTeam";
		public List<int>      propsTeam;
		public void Set_propsTeam()
        {
	        GDEDataManager.SetIntList(_key, propsTeamKey, propsTeam);
		}
		

        static string herosOwnedKey = "herosOwned";
		public List<GDEHeroData>      herosOwned;
		public void Set_herosOwned()
        {
	        GDEDataManager.SetCustomList(_key, herosOwnedKey, herosOwned);
		}
		
        static string heroesTeamKey = "heroesTeam";
		public List<GDEunitTeamData>      heroesTeam;
		public void Set_heroesTeam()
        {
	        GDEDataManager.SetCustomList(_key, heroesTeamKey, heroesTeam);
		}
		
        static string materialsKey = "materials";
		public List<GDEAMaterialData>      materials;
		public void Set_materials()
        {
	        GDEDataManager.SetCustomList(_key, materialsKey, materials);
		}
		
        static string equipsOwnedKey = "equipsOwned";
		public List<GDEEquipmentData>      equipsOwned;
		public void Set_equipsOwned()
        {
	        GDEDataManager.SetCustomList(_key, equipsOwnedKey, equipsOwned);
		}
		
        static string propsKey = "props";
		public List<GDEAMaterialData>      props;
		public void Set_props()
        {
	        GDEDataManager.SetCustomList(_key, propsKey, props);
		}
		
        static string TimeTaskListKey = "TimeTaskList";
		public List<GDEtimeTaskData>      TimeTaskList;
		public void Set_TimeTaskList()
        {
	        GDEDataManager.SetCustomList(_key, TimeTaskListKey, TimeTaskList);
		}
		

        public GDEPlayerData(string key) : base(key)
        {
            GDEDataManager.RegisterItem(this.SchemaName(), key);
        }
        public override Dictionary<string, object> SaveToDict()
		{
			var dict = new Dictionary<string, object>();
			dict.Add(GDMConstants.SchemaKey, "Player");
			
            dict.Merge(true, maxPassLevel.ToGDEDict(maxPassLevelKey));
            dict.Merge(true, lastPassLevel.ToGDEDict(lastPassLevelKey));
            dict.Merge(true, maxDurgeonPassLevel.ToGDEDict(maxDurgeonPassLevelKey));
            dict.Merge(true, coin.ToGDEDict(coinKey));
            dict.Merge(true, n_coin.ToGDEDict(n_coinKey));
            dict.Merge(true, addGoldPerc.ToGDEDict(addGoldPercKey));
            dict.Merge(true, allBufPerc.ToGDEDict(allBufPercKey));
            dict.Merge(true, newBestLevel.ToGDEDict(newBestLevelKey));
            dict.Merge(true, dimension.ToGDEDict(dimensionKey));
            dict.Merge(true, damond.ToGDEDict(damondKey));

            dict.Merge(true, temple_fighter.ToGDEDict(temple_fighterKey));
            dict.Merge(true, temple_ranger.ToGDEDict(temple_rangerKey));
            dict.Merge(true, temple_priest.ToGDEDict(temple_priestKey));
            dict.Merge(true, temple_caster.ToGDEDict(temple_casterKey));
            dict.Merge(true, bossKeys.ToGDEDict(bossKeysKey));
            dict.Merge(true, bossKeyUsed.ToGDEDict(bossKeyUsedKey));
            dict.Merge(true, propsTeam.ToGDEDict(propsTeamKey));

            dict.Merge(true, herosOwned.ToGDEDict(herosOwnedKey));
            dict.Merge(true, heroesTeam.ToGDEDict(heroesTeamKey));
            dict.Merge(true, materials.ToGDEDict(materialsKey));
            dict.Merge(true, equipsOwned.ToGDEDict(equipsOwnedKey));
            dict.Merge(true, props.ToGDEDict(propsKey));
            dict.Merge(true, TimeTaskList.ToGDEDict(TimeTaskListKey));
            return dict;
		}

        public override void UpdateCustomItems(bool rebuildKeyList)
        {
            if (herosOwned != null)
            {
                for(int x=0;  x<herosOwned.Count;  x++)
                {
                    GDEDataManager.UpdateItem(herosOwned[x], rebuildKeyList);
                    herosOwned[x].UpdateCustomItems(rebuildKeyList);
                }
            }
            if (heroesTeam != null)
            {
                for(int x=0;  x<heroesTeam.Count;  x++)
                {
                    GDEDataManager.UpdateItem(heroesTeam[x], rebuildKeyList);
                    heroesTeam[x].UpdateCustomItems(rebuildKeyList);
                }
            }
            if (materials != null)
            {
                for(int x=0;  x<materials.Count;  x++)
                {
                    GDEDataManager.UpdateItem(materials[x], rebuildKeyList);
                    materials[x].UpdateCustomItems(rebuildKeyList);
                }
            }
            if (equipsOwned != null)
            {
                for(int x=0;  x<equipsOwned.Count;  x++)
                {
                    GDEDataManager.UpdateItem(equipsOwned[x], rebuildKeyList);
                    equipsOwned[x].UpdateCustomItems(rebuildKeyList);
                }
            }
            if (props != null)
            {
                for(int x=0;  x<props.Count;  x++)
                {
                    GDEDataManager.UpdateItem(props[x], rebuildKeyList);
                    props[x].UpdateCustomItems(rebuildKeyList);
                }
            }
            if (TimeTaskList != null)
            {
                for(int x=0;  x<TimeTaskList.Count;  x++)
                {
                    GDEDataManager.UpdateItem(TimeTaskList[x], rebuildKeyList);
                    TimeTaskList[x].UpdateCustomItems(rebuildKeyList);
                }
            }
        }

        public override void LoadFromDict(string dataKey, Dictionary<string, object> dict)
        {
            _key = dataKey;

			if (dict == null)
				LoadFromSavedData(dataKey);
			else
			{
                dict.TryGetInt(maxPassLevelKey, out _maxPassLevel);
                dict.TryGetInt(lastPassLevelKey, out _lastPassLevel);
                dict.TryGetInt(maxDurgeonPassLevelKey, out _maxDurgeonPassLevel);
                dict.TryGetInt(coinKey, out _coin);
                dict.TryGetInt(n_coinKey, out _n_coin);
                dict.TryGetInt(addGoldPercKey, out _addGoldPerc);
                dict.TryGetInt(allBufPercKey, out _allBufPerc);
                dict.TryGetInt(newBestLevelKey, out _newBestLevel);
                dict.TryGetInt(dimensionKey, out _dimension);
                dict.TryGetInt(damondKey, out _damond);

                dict.TryGetIntList(temple_fighterKey, out temple_fighter);
                dict.TryGetIntList(temple_rangerKey, out temple_ranger);
                dict.TryGetIntList(temple_priestKey, out temple_priest);
                dict.TryGetIntList(temple_casterKey, out temple_caster);
                dict.TryGetIntList(bossKeysKey, out bossKeys);
                dict.TryGetIntList(bossKeyUsedKey, out bossKeyUsed);
                dict.TryGetIntList(propsTeamKey, out propsTeam);

                dict.TryGetCustomList(herosOwnedKey, out herosOwned);
                dict.TryGetCustomList(heroesTeamKey, out heroesTeam);
                dict.TryGetCustomList(materialsKey, out materials);
                dict.TryGetCustomList(equipsOwnedKey, out equipsOwned);
                dict.TryGetCustomList(propsKey, out props);
                dict.TryGetCustomList(TimeTaskListKey, out TimeTaskList);
                LoadFromSavedData(dataKey);
			}
		}

        public override void LoadFromSavedData(string dataKey)
		{
			_key = dataKey;
			
            _maxPassLevel = GDEDataManager.GetInt(_key, maxPassLevelKey, _maxPassLevel);
            _lastPassLevel = GDEDataManager.GetInt(_key, lastPassLevelKey, _lastPassLevel);
            _maxDurgeonPassLevel = GDEDataManager.GetInt(_key, maxDurgeonPassLevelKey, _maxDurgeonPassLevel);
            _coin = GDEDataManager.GetInt(_key, coinKey, _coin);
            _n_coin = GDEDataManager.GetInt(_key, n_coinKey, _n_coin);
            _addGoldPerc = GDEDataManager.GetInt(_key, addGoldPercKey, _addGoldPerc);
            _allBufPerc = GDEDataManager.GetInt(_key, allBufPercKey, _allBufPerc);
            _newBestLevel = GDEDataManager.GetInt(_key, newBestLevelKey, _newBestLevel);
            _dimension = GDEDataManager.GetInt(_key, dimensionKey, _dimension);
            _damond = GDEDataManager.GetInt(_key, damondKey, _damond);

            temple_fighter = GDEDataManager.GetIntList(_key, temple_fighterKey, temple_fighter);
            temple_ranger = GDEDataManager.GetIntList(_key, temple_rangerKey, temple_ranger);
            temple_priest = GDEDataManager.GetIntList(_key, temple_priestKey, temple_priest);
            temple_caster = GDEDataManager.GetIntList(_key, temple_casterKey, temple_caster);
            bossKeys = GDEDataManager.GetIntList(_key, bossKeysKey, bossKeys);
            bossKeyUsed = GDEDataManager.GetIntList(_key, bossKeyUsedKey, bossKeyUsed);
            propsTeam = GDEDataManager.GetIntList(_key, propsTeamKey, propsTeam);

            herosOwned = GDEDataManager.GetCustomList(_key, herosOwnedKey, herosOwned);
            heroesTeam = GDEDataManager.GetCustomList(_key, heroesTeamKey, heroesTeam);
            materials = GDEDataManager.GetCustomList(_key, materialsKey, materials);
            equipsOwned = GDEDataManager.GetCustomList(_key, equipsOwnedKey, equipsOwned);
            props = GDEDataManager.GetCustomList(_key, propsKey, props);
            TimeTaskList = GDEDataManager.GetCustomList(_key, TimeTaskListKey, TimeTaskList);
        }

        public GDEPlayerData ShallowClone()
		{
			string newKey = Guid.NewGuid().ToString();
			GDEPlayerData newClone = new GDEPlayerData(newKey);

            newClone.maxPassLevel = maxPassLevel;
            newClone.lastPassLevel = lastPassLevel;
            newClone.maxDurgeonPassLevel = maxDurgeonPassLevel;
            newClone.coin = coin;
            newClone.n_coin = n_coin;
            newClone.addGoldPerc = addGoldPerc;
            newClone.allBufPerc = allBufPerc;
            newClone.newBestLevel = newBestLevel;
            newClone.dimension = dimension;
            newClone.damond = damond;

            newClone.temple_fighter = new List<int>(temple_fighter);
			newClone.Set_temple_fighter();
            newClone.temple_ranger = new List<int>(temple_ranger);
			newClone.Set_temple_ranger();
            newClone.temple_priest = new List<int>(temple_priest);
			newClone.Set_temple_priest();
            newClone.temple_caster = new List<int>(temple_caster);
			newClone.Set_temple_caster();
            newClone.bossKeys = new List<int>(bossKeys);
			newClone.Set_bossKeys();
            newClone.bossKeyUsed = new List<int>(bossKeyUsed);
			newClone.Set_bossKeyUsed();
            newClone.propsTeam = new List<int>(propsTeam);
			newClone.Set_propsTeam();

            newClone.herosOwned = new List<GDEHeroData>(herosOwned);
			newClone.Set_herosOwned();
            newClone.heroesTeam = new List<GDEunitTeamData>(heroesTeam);
			newClone.Set_heroesTeam();
            newClone.materials = new List<GDEAMaterialData>(materials);
			newClone.Set_materials();
            newClone.equipsOwned = new List<GDEEquipmentData>(equipsOwned);
			newClone.Set_equipsOwned();
            newClone.props = new List<GDEAMaterialData>(props);
			newClone.Set_props();
            newClone.TimeTaskList = new List<GDEtimeTaskData>(TimeTaskList);
			newClone.Set_TimeTaskList();

            return newClone;
		}

        public GDEPlayerData DeepClone()
		{
			GDEPlayerData newClone = ShallowClone();
            newClone.herosOwned = new List<GDEHeroData>();
			if (herosOwned != null)
			{
				foreach(var val in herosOwned)
					newClone.herosOwned.Add(val.DeepClone());
			}
			newClone.Set_herosOwned();
            newClone.heroesTeam = new List<GDEunitTeamData>();
			if (heroesTeam != null)
			{
				foreach(var val in heroesTeam)
					newClone.heroesTeam.Add(val.DeepClone());
			}
			newClone.Set_heroesTeam();
            newClone.materials = new List<GDEAMaterialData>();
			if (materials != null)
			{
				foreach(var val in materials)
					newClone.materials.Add(val.DeepClone());
			}
			newClone.Set_materials();
            newClone.equipsOwned = new List<GDEEquipmentData>();
			if (equipsOwned != null)
			{
				foreach(var val in equipsOwned)
					newClone.equipsOwned.Add(val.DeepClone());
			}
			newClone.Set_equipsOwned();
            newClone.props = new List<GDEAMaterialData>();
			if (props != null)
			{
				foreach(var val in props)
					newClone.props.Add(val.DeepClone());
			}
			newClone.Set_props();
            newClone.TimeTaskList = new List<GDEtimeTaskData>();
			if (TimeTaskList != null)
			{
				foreach(var val in TimeTaskList)
					newClone.TimeTaskList.Add(val.DeepClone());
			}
			newClone.Set_TimeTaskList();
            return newClone;
		}

        public void Reset_maxPassLevel()
        {
            GDEDataManager.ResetToDefault(_key, maxPassLevelKey);

            Dictionary<string, object> dict;
            GDEDataManager.Get(_key, out dict);
            dict.TryGetInt(maxPassLevelKey, out _maxPassLevel);
        }

        public void Reset_lastPassLevel()
        {
            GDEDataManager.ResetToDefault(_key, lastPassLevelKey);

            Dictionary<string, object> dict;
            GDEDataManager.Get(_key, out dict);
            dict.TryGetInt(lastPassLevelKey, out _lastPassLevel);
        }

        public void Reset_maxDurgeonPassLevel()
        {
            GDEDataManager.ResetToDefault(_key, maxDurgeonPassLevelKey);

            Dictionary<string, object> dict;
            GDEDataManager.Get(_key, out dict);
            dict.TryGetInt(maxDurgeonPassLevelKey, out _maxDurgeonPassLevel);
        }

        public void Reset_coin()
        {
            GDEDataManager.ResetToDefault(_key, coinKey);

            Dictionary<string, object> dict;
            GDEDataManager.Get(_key, out dict);
            dict.TryGetInt(coinKey, out _coin);
        }

        public void Reset_n_coin()
        {
            GDEDataManager.ResetToDefault(_key, n_coinKey);

            Dictionary<string, object> dict;
            GDEDataManager.Get(_key, out dict);
            dict.TryGetInt(n_coinKey, out _n_coin);
        }

        public void Reset_addGoldPerc()
        {
            GDEDataManager.ResetToDefault(_key, addGoldPercKey);

            Dictionary<string, object> dict;
            GDEDataManager.Get(_key, out dict);
            dict.TryGetInt(addGoldPercKey, out _addGoldPerc);
        }

        public void Reset_allBufPerc()
        {
            GDEDataManager.ResetToDefault(_key, allBufPercKey);

            Dictionary<string, object> dict;
            GDEDataManager.Get(_key, out dict);
            dict.TryGetInt(allBufPercKey, out _allBufPerc);
        }

        public void Reset_newBestLevel()
        {
            GDEDataManager.ResetToDefault(_key, newBestLevelKey);

            Dictionary<string, object> dict;
            GDEDataManager.Get(_key, out dict);
            dict.TryGetInt(newBestLevelKey, out _newBestLevel);
        }

        public void Reset_dimension()
        {
            GDEDataManager.ResetToDefault(_key, dimensionKey);

            Dictionary<string, object> dict;
            GDEDataManager.Get(_key, out dict);
            dict.TryGetInt(dimensionKey, out _dimension);
        }

        public void Reset_damond()
        {
            GDEDataManager.ResetToDefault(_key, damondKey);

            Dictionary<string, object> dict;
            GDEDataManager.Get(_key, out dict);
            dict.TryGetInt(damondKey, out _damond);
        }

        public void Reset_temple_fighter()
        {
	        GDEDataManager.ResetToDefault(_key, temple_fighterKey);

	        Dictionary<string, object> dict;
	        GDEDataManager.Get(_key, out dict);
	        dict.TryGetIntList(temple_fighterKey, out temple_fighter);
        }
		
        public void Reset_temple_ranger()
        {
	        GDEDataManager.ResetToDefault(_key, temple_rangerKey);

	        Dictionary<string, object> dict;
	        GDEDataManager.Get(_key, out dict);
	        dict.TryGetIntList(temple_rangerKey, out temple_ranger);
        }
		
        public void Reset_temple_priest()
        {
	        GDEDataManager.ResetToDefault(_key, temple_priestKey);

	        Dictionary<string, object> dict;
	        GDEDataManager.Get(_key, out dict);
	        dict.TryGetIntList(temple_priestKey, out temple_priest);
        }
		
        public void Reset_temple_caster()
        {
	        GDEDataManager.ResetToDefault(_key, temple_casterKey);

	        Dictionary<string, object> dict;
	        GDEDataManager.Get(_key, out dict);
	        dict.TryGetIntList(temple_casterKey, out temple_caster);
        }
		
        public void Reset_bossKeys()
        {
	        GDEDataManager.ResetToDefault(_key, bossKeysKey);

	        Dictionary<string, object> dict;
	        GDEDataManager.Get(_key, out dict);
	        dict.TryGetIntList(bossKeysKey, out bossKeys);
        }
		
        public void Reset_bossKeyUsed()
        {
	        GDEDataManager.ResetToDefault(_key, bossKeyUsedKey);

	        Dictionary<string, object> dict;
	        GDEDataManager.Get(_key, out dict);
	        dict.TryGetIntList(bossKeyUsedKey, out bossKeyUsed);
        }
		
        public void Reset_propsTeam()
        {
	        GDEDataManager.ResetToDefault(_key, propsTeamKey);

	        Dictionary<string, object> dict;
	        GDEDataManager.Get(_key, out dict);
	        dict.TryGetIntList(propsTeamKey, out propsTeam);
        }
		

        public void Reset_herosOwned()
		{
			GDEDataManager.ResetToDefault(_key, herosOwnedKey);

			Dictionary<string, object> dict;
			GDEDataManager.Get(_key, out dict);

			dict.TryGetCustomList(herosOwnedKey, out herosOwned);
			herosOwned = GDEDataManager.GetCustomList(_key, herosOwnedKey, herosOwned);

			herosOwned.ForEach(x => x.ResetAll());
		}
        public void Reset_heroesTeam()
		{
			GDEDataManager.ResetToDefault(_key, heroesTeamKey);

			Dictionary<string, object> dict;
			GDEDataManager.Get(_key, out dict);

			dict.TryGetCustomList(heroesTeamKey, out heroesTeam);
			heroesTeam = GDEDataManager.GetCustomList(_key, heroesTeamKey, heroesTeam);

			heroesTeam.ForEach(x => x.ResetAll());
		}
        public void Reset_materials()
		{
			GDEDataManager.ResetToDefault(_key, materialsKey);

			Dictionary<string, object> dict;
			GDEDataManager.Get(_key, out dict);

			dict.TryGetCustomList(materialsKey, out materials);
			materials = GDEDataManager.GetCustomList(_key, materialsKey, materials);

			materials.ForEach(x => x.ResetAll());
		}
        public void Reset_equipsOwned()
		{
			GDEDataManager.ResetToDefault(_key, equipsOwnedKey);

			Dictionary<string, object> dict;
			GDEDataManager.Get(_key, out dict);

			dict.TryGetCustomList(equipsOwnedKey, out equipsOwned);
			equipsOwned = GDEDataManager.GetCustomList(_key, equipsOwnedKey, equipsOwned);

			equipsOwned.ForEach(x => x.ResetAll());
		}
        public void Reset_props()
		{
			GDEDataManager.ResetToDefault(_key, propsKey);

			Dictionary<string, object> dict;
			GDEDataManager.Get(_key, out dict);

			dict.TryGetCustomList(propsKey, out props);
			props = GDEDataManager.GetCustomList(_key, propsKey, props);

			props.ForEach(x => x.ResetAll());
		}
        public void Reset_TimeTaskList()
		{
			GDEDataManager.ResetToDefault(_key, TimeTaskListKey);

			Dictionary<string, object> dict;
			GDEDataManager.Get(_key, out dict);

			dict.TryGetCustomList(TimeTaskListKey, out TimeTaskList);
			TimeTaskList = GDEDataManager.GetCustomList(_key, TimeTaskListKey, TimeTaskList);

			TimeTaskList.ForEach(x => x.ResetAll());
		}

        public void ResetAll()
        {
             #if !UNITY_WEBPLAYER
             GDEDataManager.DeregisterItem(this.SchemaName(), _key);
             #else

            GDEDataManager.ResetToDefault(_key, temple_fighterKey);
            GDEDataManager.ResetToDefault(_key, temple_rangerKey);
            GDEDataManager.ResetToDefault(_key, temple_priestKey);
            GDEDataManager.ResetToDefault(_key, temple_casterKey);
            GDEDataManager.ResetToDefault(_key, bossKeysKey);
            GDEDataManager.ResetToDefault(_key, bossKeyUsedKey);
            GDEDataManager.ResetToDefault(_key, maxPassLevelKey);
            GDEDataManager.ResetToDefault(_key, lastPassLevelKey);
            GDEDataManager.ResetToDefault(_key, maxDurgeonPassLevelKey);
            GDEDataManager.ResetToDefault(_key, herosOwnedKey);
            GDEDataManager.ResetToDefault(_key, materialsKey);
            GDEDataManager.ResetToDefault(_key, coinKey);
            GDEDataManager.ResetToDefault(_key, n_coinKey);
            GDEDataManager.ResetToDefault(_key, addGoldPercKey);
            GDEDataManager.ResetToDefault(_key, allBufPercKey);
            GDEDataManager.ResetToDefault(_key, newBestLevelKey);
            GDEDataManager.ResetToDefault(_key, dimensionKey);
            GDEDataManager.ResetToDefault(_key, equipsOwnedKey);
            GDEDataManager.ResetToDefault(_key, propsKey);
            GDEDataManager.ResetToDefault(_key, propsTeamKey);
            GDEDataManager.ResetToDefault(_key, heroesTeamKey);
            GDEDataManager.ResetToDefault(_key, damondKey);
            GDEDataManager.ResetToDefault(_key, TimeTaskListKey);

            Reset_herosOwned();
            Reset_heroesTeam();
            Reset_materials();
            Reset_equipsOwned();
            Reset_props();
            Reset_TimeTaskList();

            #endif

            Dictionary<string, object> dict;
            GDEDataManager.Get(_key, out dict);
            LoadFromDict(_key, dict);
        }
    }
}