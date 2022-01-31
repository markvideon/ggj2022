using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BufferedState<S, T> : MonoBehaviour
  where S : MonoBehaviour
  where T : Frame
{
  private List<T> buffer = new List<T>();
  private static int _bufferSize;
  public Action<T> OnRead;
  public Action OnWrite;

  private float _lastSampledAt;

  private static GameClock _clock;
  private float nominalMillisBetweenSamples = 0f;

  private Action onToggle;
  
  #region Getters
  public static int bufferSize
  {
    get { Assert.IsTrue(_bufferSize > 0); return _bufferSize; }
    private set { _bufferSize = value;  }
  }
  
  private bool _bufferUtilised { get { return buffer.Count == _bufferSize; } }

  public int percentageBufferUtilised {
    get { return (int) (100f * (buffer.Count / _bufferSize)); } 
  }
  
  public bool canReadBuffer
  {
    get { return buffer.Count > 0; }
  }
  #endregion

  // Last-in last-out operation
  private void Add(T nextFrame)
  {
    buffer.Add(nextFrame);
    
    if (_bufferUtilised)
    {
      buffer.RemoveAt(0);
    }
  }
  
  // Wouldn't recommend using C# Timers unless you're certain you can observe
  // any errors.
  public void FixedUpdate()
  {
    if (!_clock.inStasis)
    {
      float timeSinceLastSampleMillis = 1000f * (Time.time - _lastSampledAt);
      // Time to read should be based on the clock flowRate. 
      bool sampleThisTime = _clock.flowRate * timeSinceLastSampleMillis > nominalMillisBetweenSamples;

      bool timeToRead = _clock.flow == FlowDirection.backward && 
          sampleThisTime;
      bool timeToWrite = _clock.flow == FlowDirection.forward &&
          sampleThisTime;

      if (timeToRead || timeToWrite)
      {
        // record new sample time
        _lastSampledAt = Time.time;
      }
      
      // Note: Any logging is better placed inside the OnRead & 
      // OnWrite callbacks for a given class. Log statements
      // here will print out for [all] BufferedStates.
      if (timeToWrite)
      {
        Assert.IsNotNull(OnWrite);
        OnWrite();
      } else if (timeToRead)
      {
        Assert.IsNotNull(OnRead);

        if (canReadBuffer)
        {
          int lastIdx = buffer.Count - 1;
          T consumedFrame = buffer[lastIdx];
          buffer.RemoveAt(lastIdx);
          OnRead(consumedFrame);
        }
        else
        {
          //_clock.SetDirection(FlowDirection.forward);
        }
      }
    }
  }

  public void Record(T nextFrame)
  {
    //Debug.Log("Adding frame: " + buffer.Count + "/" + buffer.Count);
    Add(nextFrame);
  }

  public void SetOnRead(Action<T> OnRead)
  {
    this.OnRead = OnRead;
  }

  public static void SetBarState(int nextSize)
  {
    _bufferSize = nextSize;
  }

  public void SetOnWrite(Action OnWrite)
  {
    this.OnWrite = OnWrite;
  }

  public void Start()
  {
    _clock = FindObjectOfType<GameClock>();
    Assert.IsNotNull(_clock);
  }
}