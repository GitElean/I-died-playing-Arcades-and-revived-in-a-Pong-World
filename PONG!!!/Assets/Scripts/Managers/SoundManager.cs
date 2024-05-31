using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip paddleHitSound;
    public AudioClip parrySound;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPaddleHitSound()
    {
        if (paddleHitSound != null)
        {
            audioSource.PlayOneShot(paddleHitSound);
        }
        else
        {
            Debug.LogError("Paddle hit sound is not assigned in the SoundManager.");
        }
    }

    public void PlayParrySound()
    {
        if (parrySound != null)
        {
            audioSource.PlayOneShot(parrySound);
        }
        else
        {
            Debug.LogError("Parry sound is not assigned in the SoundManager.");
        }
    }
}
