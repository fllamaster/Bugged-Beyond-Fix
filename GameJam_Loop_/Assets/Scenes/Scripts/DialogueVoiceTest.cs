using UnityEngine;
using System.Collections;
using System.Linq;

public class DialogueVoiceTest : MonoBehaviour
{
    public AudioSource audioSource;           // Источник звука
    public AudioClip[] blipSounds;            // Список звуков
    public float delayBetweenBlips = 0.2f;    // Интервал между звуками
    public bool randomOrder = false;          // Включить рандомный порядок?
    public bool playOnStart = true;           // Запускать при старте?

    void Start()
    {
        if (playOnStart)
            StartCoroutine(LoopBlips());
    }

    IEnumerator LoopBlips()
    {
        while (true)
        {
            AudioClip[] clipsToPlay = blipSounds;

            if (randomOrder)
                clipsToPlay = blipSounds.OrderBy(x => Random.value).ToArray();

            foreach (AudioClip clip in clipsToPlay)
            {
                audioSource.PlayOneShot(clip);
                yield return new WaitForSeconds(delayBetweenBlips);
            }
        }
    }

    // Вызывается извне, если нужно вручную запустить проигрывание
    public void PlayNow()
    {
        StopAllCoroutines();
        StartCoroutine(LoopBlips());
    }

    public void Stop()
    {
        StopAllCoroutines();
    }
}
