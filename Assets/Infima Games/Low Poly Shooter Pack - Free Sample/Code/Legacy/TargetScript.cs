using UnityEngine;
using System.Collections;

public class TargetScript : MonoBehaviour
{
    bool routineStarted = false;

    //Used to check if the target has been hit
    public bool isHit = false;

    [Header("Audio")] public AudioClip upSound;
    public AudioClip downSound;

    [Header("Animations")] public AnimationClip targetUp;
    public AnimationClip targetDown;

    public AudioSource audioSource;

    private Animation _animation;
    private AudioSource _audioSource;

    void Start()
    {
        _animation = gameObject.GetComponent<Animation>();
        _audioSource = audioSource.GetComponent<AudioSource>();

        ToppleTarget(false);
    }

    private void Update()
    {
        //If the target is hit
        if (isHit == true)
        {
            ToppleTarget();
        }
    }

    //Time before the target pops back up
    private IEnumerator DelayTimer(float delay)
    {
        //Wait for random amount of time
        yield return new WaitForSeconds(delay);
        RaiseTarget();
        routineStarted = false;
    }

    public void Raise(float maxRaiseDelay)
    {
        float raiseTime = maxRaiseDelay * Random.value;
        
        Debug.Log("Raising Target " + gameObject.name);
        StartCoroutine(DelayTimer(raiseTime));
    }

    void RaiseTarget(bool doSound = true)
    {
        _animation.clip = targetUp;
        _animation.Play();

        if (doSound)
        {
            //Set the upSound as current sound, and play it
            _audioSource.clip = upSound;
            audioSource.Play();
        }
    }

    void ToppleTarget(bool doSound = true)
    {
        isHit = false;

        _animation.clip = targetDown;
        _animation.Play();

        if (doSound)
        {
            _audioSource.clip = downSound;
            audioSource.Play();
        }
    }
}