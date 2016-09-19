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
		public class AnimationState
		{
	
		public enum  FadeState {FADE_BEFORE, FADING, FADE_COMPLETE};
		
		private static List<AnimationState> _pool = new List<AnimationState>();
		public static AnimationState borrowObject()
		{
			if (_pool.Count<=0)
			{
				return new AnimationState();
			}
			
			AnimationState animationState = _pool[_pool.Count - 1];
			_pool.RemoveAt(_pool.Count - 1);
			return animationState;
		}
		public static void returnObject(AnimationState animationState)
		{

			if (_pool.IndexOf(animationState)<0)
			{
				_pool.Add(animationState);
			}
			
			animationState.clear();
		}
		public static void clearObjects()
		{
			for (int i = 0; i < _pool.Count; ++i)
			{
				_pool[i].clear();
				//delete _pool[i];
			}
			
			_pool.Clear();
		}
		
	
		public	bool additiveBlending;
		public bool autoTween;
		public bool autoFadeOut;
		public bool displayControl;
		public bool lastFrameAutoTween;
		public float fadeOutTime;
		public float weight;
		public string name;
		
	
		private 	bool _isPlaying;
		public bool _isComplete;
		private bool _isFadeOut;
		private bool _pausePlayheadInFade;
		private int _currentPlayTimes;
		public int _layer;
		private int _playTimes;
		private int _currentTime;
		private int _currentFrameIndex;
		private int _currentFramePosition;
		private int _currentFrameDuration;
		private int _totalTime;
		private float _time;
		private float _timeScale;
		private float _fadeWeight;
		private float _fadeTotalWeight;
		private float _fadeCurrentTime;
		private float _fadeTotalTime;
		private float _fadeBeginTime;
		public string _group;
		public  FadeState _fadeState;
		
		private List<TimelineState> _timelineStateList = new List<TimelineState>();
		private List<string> _mixingTransforms = new List<string>();
		
		private AnimationData _clip;
		private Armature _armature;


		public AnimationState ()
		{
			_clip = null;
			_armature = null;
		}

	
		public bool getIsComplete() 
		{
			return _isComplete;
		}

		public bool getIsPlaying() 
		{
			return (_isPlaying && !_isComplete);
		}

		public int getCurrentPlayTimes()
		{
			return _currentPlayTimes < 0 ? 0 : _currentPlayTimes;
		}

		public int getLayer() 
		{
			return _layer;
		}

		public float getTotalTime() 
		{
			return _totalTime * 0.001f;
		}

		public float getCurrentWeight() 
		{
			return _fadeWeight * weight;
		}

		public string getGroup() 
		{
			return _group;
		}

		public AnimationData getClip() 
		{
			return _clip;
		}

		public AnimationState setAdditiveBlending(bool value)
		{
			additiveBlending = value;
			return this;
		}

		public AnimationState setAutoFadeOut(bool value, float fadeOutTime_)
		{
			autoFadeOut = value;
			
			if (fadeOutTime_ >= 0)
			{
				fadeOutTime = fadeOutTime_;
			}
			
			return this;
		}

		public AnimationState setWeight(float value)
		{
			weight = value;
			return this;
		}


		public AnimationState setFrameTween(bool autoTween_, bool lastFrameAutoTween_)
		{
			autoTween = autoTween_;
			lastFrameAutoTween = lastFrameAutoTween_;
			return this;
		}
		public int getPlayTimes() 
		{
			return _playTimes;
		}

		public AnimationState setPlayTimes(int playTimes)
		{
			_playTimes = playTimes;
			
			if (Math.Round(_totalTime * 0.001f * _clip.frameRate) < 2)
			{
				_playTimes = playTimes < 0 ? -1 : 1;
			}
			else
			{
				_playTimes = playTimes < 0 ? -playTimes : playTimes;
			}
			
			autoFadeOut = playTimes < 0 ? true : false;
			return this;
		}
		public float getCurrentTime() 
		{
			return _currentTime < 0 ? 0.0f : _currentTime * 0.001f;
		}
		public AnimationState setCurrentTime(float currentTime)
		{
			if (currentTime < 0 || currentTime != currentTime)
			{
				currentTime = 0.0f;
			}
			
			_time = currentTime;
			_currentTime = (int)(_time * 1000.0f);
			return this;
		}
		
		public float getTimeScale() 
		{
			return _timeScale;
		}
		public AnimationState setTimeScale(float timeScale)
		{
			if (timeScale != timeScale)
			{
				timeScale = 1.0f;
			}
			
			_timeScale = timeScale;
			return this;
		}

		public void fadeIn(Armature armature, AnimationData clip, float fadeTotalTime, float timeScale, int playTimes, bool pausePlayhead)
		{
			_armature = armature;
			_clip = clip;
			_pausePlayheadInFade = pausePlayhead;
			_totalTime = _clip.duration;
			autoTween = _clip.autoTween;
			name = _clip.name;
			setTimeScale(timeScale);
			setPlayTimes(playTimes);
			// reset
			_isComplete = false;
			_currentFrameIndex = -1;
			_currentPlayTimes = -1;
			
			if (Math.Round(_totalTime * 0.001f * _clip.frameRate) < 2)
			{
				_currentTime = _totalTime;
			}
			else
			{
				_currentTime = -1;
			}
			
			_time = 0.0f;
			_mixingTransforms.Clear();
			// fade start
			_isFadeOut = false;
			_fadeWeight = 0.0f;
			_fadeTotalWeight = 1.0f;
			_fadeCurrentTime = 0.0f;
			_fadeBeginTime = _fadeCurrentTime;
			_fadeTotalTime = fadeTotalTime * _timeScale;
			_fadeState = FadeState.FADE_BEFORE;
			// default
			_isPlaying = true;
			displayControl = true;
			lastFrameAutoTween = true;
			additiveBlending = false;
			weight = 1.0f;
			fadeOutTime = fadeTotalTime;
			updateTimelineStates();
		}

		public AnimationState fadeOut(float fadeTotalTime, bool pausePlayhead)
		{
			if (!(fadeTotalTime >= 0))
			{
				fadeTotalTime = 0.0f;
			}
			
			_pausePlayheadInFade = pausePlayhead;
			
			if (_isFadeOut)
			{
				if (fadeTotalTime > _fadeTotalTime / _timeScale - (_fadeCurrentTime - _fadeBeginTime))
				{
					return this;
				}
			}
			else
			{
				for (int i = 0; i < _timelineStateList.Count;  ++i)
				{
					_timelineStateList[i].fadeOut();
				}
			}
			
			// fade start
			_isFadeOut = true;
			_fadeTotalWeight = _fadeWeight;
			_fadeState = FadeState.FADE_BEFORE;
			_fadeBeginTime = _fadeCurrentTime;
			_fadeTotalTime = _fadeTotalWeight >= 0 ? fadeTotalTime * _timeScale : 0.0f;
			// default
			displayControl = false;
			return this;
		}
		public AnimationState play()
		{
			_isPlaying = true;
			return this;
		}

		public AnimationState stop()
		{
			_isPlaying = false;
			return this;
		}

		public bool getMixingTransform(string timelineName) 
		{

			return (_mixingTransforms.IndexOf(timelineName)>=0);
		}

		public AnimationState addMixingTransform(string timelineName, bool recursive)
		{
			if (recursive)
			{
				Bone currentBone = null;
				
				// From root to leaf
				for (int i = _armature.getBones().Count; i>=0; i--)
				{
					Bone bone = _armature.getBones()[i];
					string boneName = bone.name;
					
					if (boneName == timelineName)
					{
						currentBone = bone;
					}
					
					if (
						currentBone!= null &&
						(currentBone == bone || currentBone.contains(bone)) &&
						_clip.getTimeline(boneName)!=null &&
						_mixingTransforms.IndexOf (boneName) < 0 
						)
					{
						_mixingTransforms.Add(boneName);
					}
				}
			}
			else if (
				_clip.getTimeline(timelineName)!= null &&
				_mixingTransforms.IndexOf (timelineName) < 0
				)
			{
				_mixingTransforms.Add(timelineName);
			}
			
			updateTimelineStates();
			return this;
		}

		public AnimationState removeMixingTransform(string timelineName, bool recursive)
		{
			if (recursive)
			{
				Bone currentBone = null;
				
				// From root to leaf
				for (int i = _armature.getBones().Count; i>=0; i--)
				{
					Bone bone = _armature.getBones()[i];
					
					if (bone.name == timelineName)
					{
						currentBone = bone;
					}
					
					if (currentBone!=null && (currentBone == bone || currentBone.contains(bone)))
					{

						if (_mixingTransforms.IndexOf(bone.name) >= 0 )
						{
							_mixingTransforms.Remove(bone.name);
						}
					}
				}
			}
			else
			{

				if (_mixingTransforms.IndexOf(timelineName)>=0)
				{
					_mixingTransforms.Remove(timelineName);
				}
			}
			
			updateTimelineStates();
			return this;
		}

		public AnimationState removeAllMixingTransform()
		{
			_mixingTransforms.Clear();
			updateTimelineStates();
			return this;
		}
	
		public bool advanceTime(float passedTime)
		{
			passedTime *= _timeScale;
			advanceFadeTime(passedTime);
			
			if (_fadeWeight!=0)
			{
				advanceTimelinesTime(passedTime);
			}
			
			return _isFadeOut && _fadeState == FadeState.FADE_COMPLETE;
		}

		public void updateTimelineStates()
		{
			for (int i = _timelineStateList.Count; i>0; i--)
			{
				TimelineState timelineState = _timelineStateList[i];
				
				if (_armature.getBone(timelineState.name) == null)
				{
					removeTimelineState(timelineState);
				}
			}
			
			if (_mixingTransforms.Count<=0)
			{
				for (int i = 0; i < _clip.timelineList.Count;  ++i)
				{
					addTimelineState(_clip.timelineList[i].name);
				}
			}
			else
			{
				for (int i = _timelineStateList.Count; i>0; i--)
				{
					TimelineState timelineState = _timelineStateList[i];

 					if (_mixingTransforms.IndexOf(timelineState.name) <0 )
					{
						removeTimelineState(timelineState);
					}
				}
				
				for (int i = 0; i < _mixingTransforms.Count; ++i)
				{
					addTimelineState(_mixingTransforms[i]);
				}
			}
		}

		public void addTimelineState(string timelineName)
		{
			Bone bone = _armature.getBone(timelineName);
			
			if (bone!=null)
			{
				for (int i = 0; i < _timelineStateList.Count;  ++i)
				{
					if (_timelineStateList[i].name == timelineName)
					{
						return;
					}
				}
				
				TimelineState timelineState = TimelineState.borrowObject();
				timelineState.fadeIn(bone, this, _clip.getTimeline(timelineName));
				_timelineStateList.Add(timelineState);
			}
		}

		public void removeTimelineState(TimelineState timelineState)
		{

			if (_timelineStateList.IndexOf(timelineState) >= 0)
			{
				TimelineState.returnObject(timelineState);
				_timelineStateList.Remove(timelineState);
			}
		}

		public void advanceFadeTime(float passedTime)
		{
			bool fadeStartFlg = false;
			bool fadeCompleteFlg = false;
			
			if (_fadeBeginTime >= 0)
			{
				FadeState fadeState = _fadeState;
				_fadeCurrentTime += passedTime < 0 ? -passedTime : passedTime;
				
				if (_fadeCurrentTime >= _fadeBeginTime + _fadeTotalTime)
				{
					// fade complete
					if (_fadeWeight == 1 || _fadeWeight == 0)
					{
						fadeState = FadeState.FADE_COMPLETE;
						
						if (_pausePlayheadInFade)
						{
							_pausePlayheadInFade = false;
							_currentTime = -1;
						}
					}
					
					_fadeWeight = _isFadeOut ? 0.0f : 1.0f;
				}
				else if (_fadeCurrentTime >= _fadeBeginTime)
				{
					// fading
					fadeState = FadeState.FADING;
					_fadeWeight = (_fadeCurrentTime - _fadeBeginTime) / _fadeTotalTime * _fadeTotalWeight;
					
					if (_isFadeOut)
					{
						_fadeWeight = _fadeTotalWeight - _fadeWeight;
					}
				}
				else
				{
					// fade before
					fadeState = FadeState.FADE_BEFORE;
					_fadeWeight = _isFadeOut ? 1.0f : 0.0f;
				}
				
				if (_fadeState != fadeState)
				{
					// _fadeState == FadeState::FADE_BEFORE && (fadeState == FadeState::FADING || fadeState == FadeState::FADE_COMPLETE)
					if (_fadeState == FadeState.FADE_BEFORE)
					{
						fadeStartFlg = true;
					}
					
					// (_fadeState == FadeState::FADE_BEFORE || _fadeState == FadeState::FADING) && fadeState == FadeState::FADE_COMPLETE
					if (fadeState == FadeState.FADE_COMPLETE)
					{
						fadeCompleteFlg = true;
					}
					
					_fadeState = fadeState;
				}
			}
			
			if (fadeStartFlg)
			{
				EventData.EventType eventDataType;
				
				if (_isFadeOut)
				{
					eventDataType = EventData.EventType.FADE_OUT;
				}
				else
				{
					hideBones();
					eventDataType = EventData.EventType.FADE_IN;
				}
				
				if (_armature._eventDispatcher.HasEvent(EventData.typeToString(eventDataType)))
				{
					EventData eventData = EventData.borrowObject(eventDataType);
					eventData.armature = _armature;
					eventData.animationState = this;
					_armature._eventDataList.Add(eventData);
				}
			}
			
			if (fadeCompleteFlg)
			{
				EventData.EventType eventDataType;
				
				if (_isFadeOut)
				{
					eventDataType = EventData.EventType.FADE_OUT_COMPLETE;
				}
				else
				{
					eventDataType = EventData.EventType.FADE_IN_COMPLETE;
				}
				
				if (_armature._eventDispatcher.HasEvent(EventData.typeToString(eventDataType)))
				{
					EventData eventData = EventData.borrowObject(eventDataType);
					eventData.armature = _armature;
					eventData.animationState = this;
					_armature._eventDataList.Add(eventData);
				}
			}
		}


		public void advanceTimelinesTime(float passedTime)
		{
			if (_isPlaying && !_pausePlayheadInFade)
			{
				_time += passedTime;
			}
			
			bool startFlg = false;
			bool completeFlg = false;
			bool loopCompleteFlg = false;
			bool isThisComplete = false;
			int currentPlayTimes = 0;
			int currentTime = (int)(_time * 1000.0f);
			
			if (_playTimes == 0)
			{
				isThisComplete = false;
				currentPlayTimes = (int)(Math.Ceiling(Math.Abs(currentTime) / (float)(_totalTime)));
				currentTime -= (int)(Math.Floor(currentTime / (float)(_totalTime))) * _totalTime;
				
				if (currentTime < 0)
				{
					currentTime += _totalTime;
				}
			}
			else
			{
				int totalTimes = _playTimes * _totalTime;
				
				if (currentTime >= totalTimes)
				{
					currentTime = totalTimes;
					isThisComplete = true;
				}
				else if (currentTime <= -totalTimes)
				{
					currentTime = -totalTimes;
					isThisComplete = true;
				}
				else
				{
					isThisComplete = false;
				}
				
				if (currentTime < 0)
				{
					currentTime += totalTimes;
				}
				
				currentPlayTimes = (int)(Math.Ceiling(currentTime / (float)(_totalTime)));
                
				currentTime -= (int)(Math.Floor(currentTime / (float)(_totalTime))) * _totalTime;
				
				if (isThisComplete)
				{
					currentTime = _totalTime;
				}
			}
			
			if (currentPlayTimes == 0)
			{
				currentPlayTimes = 1;
			}
			
			// update timeline
			_isComplete = isThisComplete;
			float progress = _time * 1000.0f / (float)(_totalTime);
            
			
			for (int i = 0; i < _timelineStateList.Count;  ++i)
			{
				_timelineStateList[i].update(progress);
				_isComplete = _timelineStateList[i]._isComplete && _isComplete;
			}
			
			// update main timeline
			if (_currentTime != currentTime)
			{
				if (_currentPlayTimes != currentPlayTimes)    // check loop complete
				{
					if (_currentPlayTimes > 0 && currentPlayTimes > 1)
					{
						loopCompleteFlg = true;
					}
					
					_currentPlayTimes = currentPlayTimes;
				}
				
				if (_currentTime < 0 && !_pausePlayheadInFade)    // check start
				{
					startFlg = true;
				}
				
				if (_isComplete)    // check complete
				{
					completeFlg = true;
				}
				
				_currentTime = currentTime;
				updateMainTimeline(isThisComplete);
			}
			
			if (startFlg)
			{
				if (_armature._eventDispatcher.HasEvent(EventData.START))
				{
					EventData eventData = EventData.borrowObject(EventData.EventType.START);
					eventData.armature = _armature;
					eventData.animationState = this;
					_armature._eventDataList.Add(eventData);
				}
			}
			
			if (completeFlg)
			{
				if (_armature._eventDispatcher.HasEvent(EventData.COMPLETE))
				{
					EventData eventData = EventData.borrowObject(EventData.EventType.COMPLETE);
					eventData.armature = _armature;
					eventData.animationState = this;
					_armature._eventDataList.Add(eventData);
				}
				
				if (autoFadeOut)
				{
					fadeOut(fadeOutTime, true);
				}
			}
			else if (loopCompleteFlg)
			{
				if (_armature._eventDispatcher.HasEvent(EventData.LOOP_COMPLETE))
				{
					EventData eventData = EventData.borrowObject(EventData.EventType.LOOP_COMPLETE);
					eventData.armature = _armature;
					eventData.animationState = this;
					_armature._eventDataList.Add(eventData);
				}
			}
		}

		public void updateMainTimeline(bool isThisComplete)
		{
			if (_clip.frameList.Count >0)
			{
				Frame prevFrame = null;
				Frame currentFrame = null;
				
				for (int i = 0; i < _clip.frameList.Count;  ++i)
				{
					if (_currentFrameIndex < 0)
					{
						_currentFrameIndex = 0;
					}
					else if (_currentTime < _currentFramePosition || _currentTime >= _currentFramePosition + _currentFrameDuration)
					{
						++_currentFrameIndex;
						
						if (_currentFrameIndex >=  _clip.frameList.Count)
						{
							if (isThisComplete)
							{
								--_currentFrameIndex;
								break;
							}
							else
							{
								_currentFrameIndex = 0;
							}
						}
					}
					else
					{
						break;
					}
					
					currentFrame = _clip.frameList[_currentFrameIndex];
					
					if (prevFrame!=null)
					{
						_armature.arriveAtFrame(prevFrame, this, true);
					}
					
					_currentFrameDuration = currentFrame.duration;
					_currentFramePosition = currentFrame.position;
					prevFrame = currentFrame;
				}
				
				if (currentFrame!=null)
				{
					_armature.arriveAtFrame(currentFrame, this, false);
				}
			}
		}


		public void hideBones()
		{
			for (int i = 0; i < _clip.hideTimelineList.Count;  ++i)
			{
				Bone bone = _armature.getBone(_clip.hideTimelineList[i]);
				
				if (bone!=null)
				{
					bone.hideSlots();
				}
			}
		}


		public void clear()
		{
			// reverse delete
			for (int i = _timelineStateList.Count-1; i>=0; i--)
			{
				TimelineState.returnObject(_timelineStateList[i]);
			}
			
			_timelineStateList.Clear();
			_mixingTransforms.Clear();
			_armature = null;
			_clip = null;
		}



		}
}
