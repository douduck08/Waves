using UnityEngine;
using System;

namespace TeamSignal.Utilities
{
	public partial class TSUtil
	{
		static public GameObject Instantiate(GameObject createObj, Transform parent = null, bool worldPosStays = true, bool resetTrans = true)
		{
			if( null == createObj)
			{
				throw new Exception("TSUtil Instantiate createObj null");
			}
			
			GameObject obj = GameObject.Instantiate(createObj) as GameObject;
			Transform trans = obj.transform;
			
			if( null != parent)
			{
				trans.SetParent(parent, worldPosStays);
			}
			
			if(resetTrans)
			{
				trans.localPosition = Vector3.zero;
				trans.localRotation = Quaternion.identity;
				trans.localScale = Vector3.one;
			}
			return obj;
		}
		
		static public GameObject InstantiateForUGUI(GameObject createObj, Transform parent)
		{
			return Instantiate(createObj, parent, false, false);
		}
	}
}