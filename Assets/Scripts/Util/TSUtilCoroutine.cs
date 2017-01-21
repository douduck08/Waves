using UnityEngine;
using System.Collections;
using System;

namespace TeamSignal.Utilities
{
	public partial class TSUtil
	{
		static public IEnumerator WaitForFixedUpdate(Action callback)
		{
			yield return new WaitForFixedUpdate();
			if(null != callback)
			{
				callback();
			}
		}
		
		static public IEnumerator WaitForEndOfFrame(Action callback)
		{
			yield return new WaitForEndOfFrame();
			
			if(null != callback)
			{
				callback();
			}
		}
		
		static public IEnumerator WaitForNextFrame(Action callback)
		{
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			
			if(null != callback)
			{
				callback();
			}
		}
		
		static public IEnumerator WaitForSeconds(float sec, Action callback)
		{
			yield return new WaitForSeconds(sec);
			
			if(null != callback)
			{
				callback();
			}
		}
	}
}