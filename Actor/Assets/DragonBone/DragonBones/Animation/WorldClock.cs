// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
namespace DragonBones
{
	public class WorldClock: IAnimatable
	{

	
		public	static WorldClock clock = new WorldClock(-1, 1);
		
	
		private 	bool _dirty;
		private bool _isPlaying;
		private float _time;
		private float _timeScale;
		
		
		
		private  List<IAnimatable> _animatableList = new List<IAnimatable>();

		public WorldClock (float time, float timeScale)
		{
			_dirty = false;
		    _isPlaying = true;
			_time = 0;
			setTimeScale(timeScale);
		}

	
		public	static WorldClock getInstance()
		{
			return clock;
		}
		
		public float getTime() 
		{
			return _time;
		}
		
		public float getTimeScale() 
		{
			return _timeScale;
		}

		public void setTimeScale(float timeScale)
		{
			if (timeScale < 0 || timeScale != timeScale)
			{
				timeScale = 1.0f;
			}
			
			_timeScale = timeScale;
		}


		public bool contains(IAnimatable animatable) 
		{
			return _animatableList.IndexOf(animatable) >=0;
		}
		
		public void add(IAnimatable animatable)
		{
			if (animatable!=null && !contains(animatable))
			{
				_animatableList.Add(animatable);
			}
		}
		
		public void remove(IAnimatable animatable)
		{
			if (animatable==null) { return; }
			

			if (_animatableList.IndexOf(animatable) >=0)
			{
				_animatableList.Remove(animatable);
				//_dirty = true;
			}
		}
		
		public void removeAll()
		{
			_animatableList.Clear();
		}
		
		public void play()
		{
			_isPlaying = true;
		}
		
		public void stop()
		{
			_isPlaying = false;
		}
		
		public void advanceTime(float passedTime)
		{
			if (!_isPlaying)
			{
				return;
			}
			
			if (passedTime < 0 || passedTime != passedTime)
			{
				/*
        passedTime = getTimer() * 0.001f - _time;
        if (passedTime < 0)
        {
            passedTime = 0.f;
        }
        */
				passedTime = 0.0f;
			}
			
			passedTime *= _timeScale;
			_time += passedTime;

			if (_animatableList.Count<=0)
			{
				return;
			}
			
			for (int i = 0; i < _animatableList.Count; ++i)
			{
				if (_animatableList[i]!=null)
				{
					_animatableList[i].advanceTime(passedTime);
				}
			}

			/*
			if (_dirty)
			{
				int curIdx = 0;
				
				for (int i = 0; i < _animatableList.Count; ++i)
				{
					if (_animatableList[i])
					{
						if (curIdx != i)
						{
							_animatableList[curIdx] = _animatableList[i];
							_animatableList[i] = null;
						}
						
						curIdx++;
					}
				}
				
				_animatableList.(curIdx);
				_dirty = false;
			}
			*/


   }
}

}