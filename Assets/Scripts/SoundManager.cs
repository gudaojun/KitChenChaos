using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioClipRefsSO audioClipRefsSo;

    private float volume =1f;
    private void Awake()
    {
        Instance = this;
        volume =PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess+=DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed+=DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut+=CuttingCounter_OnAnyCut;
        Player.Instance.OnPickUpSomething+=Player_OnPickUpSomething;
        BaseCounter.OnAnyobjectPlacedHere+=BaseCounter_OnAnyobjectPlacedHere;
        TrashCounter.OnTransh+=TrashCounter_OnTransh;
    }

    private void TrashCounter_OnTransh(object sender, EventArgs e)
    {
        TrashCounter trashCounter=sender as TrashCounter;
        PlaySound(audioClipRefsSo.objectDrop,trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyobjectPlacedHere(object sender, EventArgs e)
    {
        BaseCounter baseCounter=sender as BaseCounter;
        PlaySound(audioClipRefsSo.objectDrop,baseCounter.transform.position);
    }

    private void Player_OnPickUpSomething(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSo.objectPickUp,Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
    {
        CuttingCounter cuttingCounter=sender as CuttingCounter;;
        PlaySound(audioClipRefsSo.chop,cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter=DeliveryCounter.Instance;
        PlaySound(audioClipRefsSo.deliveryFail,deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter=DeliveryCounter.Instance;
        PlaySound(audioClipRefsSo.deliverySuccess,deliveryCounter.transform.position);
    }
    
    private void PlaySound(AudioClip audioClip, Vector3 position, float volunmeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip,position,volunmeMultiplier*volume);
    }
    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volunmeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[UnityEngine.Random.Range(0,audioClipArray.Length)],position,volunmeMultiplier);
    }

    /// <summary>
    /// 脚步声音
    /// </summary>
    /// <param name="position"></param>
    /// <param name="volume"></param>
    public void PlayFootstepsSound(Vector3 position,float volume=1f)
    {
        PlaySound(audioClipRefsSo.footStep,position,volume);
    }
    
    /// <summary>
    /// 倒计时
    /// </summary>
    /// <param name="position"></param>
    /// <param name="volume"></param>
    public void PlayCountdownSound()
    {
        PlaySound(audioClipRefsSo.warning,Vector3.zero);
    }

    /// <summary>
    /// 警告音
    /// </summary>
    /// <param name="position"></param>
    public void PlayWarningSound(Vector3 position)
    {
        PlaySound(audioClipRefsSo.warning,position);
    }
    public void ChangedVolume()
    {
        volume += .1f;
        if (volume>1)
        {
            volume = 0f;
        }
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME,volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
