using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefsSO : ScriptableObject
{
    /// <summary>
    /// 切
    /// </summary>
    public AudioClip[] chop;

    /// <summary>
    /// 交付失败
    /// </summary>
    public AudioClip[] deliveryFail;
    
    /// <summary>
    /// 交付成功
    /// </summary>
    public AudioClip[] deliverySuccess;

    /// <summary>
    /// 移动声音
    /// </summary>
    public AudioClip[] footStep;

    /// <summary>
    /// 物体放下
    /// </summary>
    public AudioClip[] objectDrop;
    /// <summary>
    /// 物体拿起
    /// </summary>
    public AudioClip[] objectPickUp;
    /// <summary>
    /// 炉子声音
    /// </summary>
    public AudioClip stoveSizzle;
    
    /// <summary>
    /// 倒垃圾
    /// </summary>
    public AudioClip[] trash;
    /// <summary>
    /// 警告音
    /// </summary>
    public AudioClip[] warning;
}
