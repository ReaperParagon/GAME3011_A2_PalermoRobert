using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LockPickAudioClips
{
    LockFail,
    LockMove,
    LockPin,
    TryLock,
    Unlock
}

public class LockPickingAudioManager : MonoBehaviour
{
    [SerializeField]
    public List<AudioClip> audioClips;

    [SerializeField]
    public AudioSource audioSource;

    public static LockPickingAudioManager instance;

    private void OnEnable()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }    

        instance = this;
    }

    /// Functions ///

    public void PlayAudio(LockPickAudioClips clip, bool loop = false)
    {
        // Check if within audioclips list
        if (!((int)clip >= 0 && (int)clip < audioClips.Count))
            return;

        StopAudio();

        audioSource.clip = audioClips[(int)clip];
        audioSource.loop = loop;

        audioSource.Play();
    }

    public void StopAudio()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }

    public bool GetIsPlayingClip(LockPickAudioClips clip)
    {
        if (audioSource.clip == audioClips[(int)clip] && audioSource.isPlaying)
            return true;

        return false;
    }

}
