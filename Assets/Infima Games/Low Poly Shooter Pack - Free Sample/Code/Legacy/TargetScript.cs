using UnityEngine;
using System.Collections;

public class TargetScript : MonoBehaviour, IShootable
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
    private TargetTrigger _targetTrigger;
    bool isDown = false;

    void Start()
    {
        _animation = gameObject.GetComponent<Animation>();
        _audioSource = audioSource.GetComponent<AudioSource>();

        ToppleTarget(false);
    }

    private void Update()
    {
        if (isHit && !isDown)
        {
            _targetTrigger.HandleTargetShot(this);
            ToppleTarget();
        }
    }
    
    public void Init(TargetTrigger trigger)
    {
        _targetTrigger = trigger;
    }

    public void Raise(float maxRaiseDelay)
    {
        float raiseTime = maxRaiseDelay * Random.value;

        Debug.Log("Raising Target " + gameObject.name);
        StartCoroutine(DelayTimer(raiseTime));
    }

    private IEnumerator DelayTimer(float delay)
    {
        yield return new WaitForSeconds(delay);
        RaiseTarget();
        routineStarted = false;
        isHit = false;
        isDown = false;
    }

    void RaiseTarget(bool doSound = true)
    {
        _animation.clip = targetUp;
        _animation.Play();

        if (doSound)
        {
            _audioSource.clip = upSound;
            audioSource.Play();
        }
    }

    void ToppleTarget(bool doSound = true)
    {
        _animation.clip = targetDown;
        _animation.Play();

        if (doSound)
        {
            _audioSource.clip = downSound;
            audioSource.Play();
        }

        isDown = true;
    }

    public void GotShot()
    {
        isHit = true;
    }
}