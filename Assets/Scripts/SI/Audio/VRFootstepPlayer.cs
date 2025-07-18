using UnityEngine;
public class VRFootstepPlayer : MonoBehaviour
{
    public Transform player; 
    public AudioSource audioSource;
    public float movementThreshold = 0.01f;

    private Vector3 lastPosition;
    private bool wasMoving = false;

    void Start()
    {
        lastPosition = new Vector3(player.position.x, 0, player.position.z);
        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        Vector3 currentPosition = new Vector3(player.position.x, 0, player.position.z);
        float distanceMoved = Vector3.Distance(currentPosition, lastPosition);

        bool isMoving = distanceMoved > movementThreshold;

        if (isMoving && !wasMoving)
        {
            // Start or resume audio
            if (!audioSource.isPlaying)
            {
                if (audioSource.time <= 0f)
                {
                    audioSource.Play();
                }
                else
                {
                    audioSource.UnPause();
                }
            }
                
        }
        else if (!isMoving && wasMoving)
        {
            // Pause audio when movement stops
            audioSource.Pause();
        }

        if (isMoving)
        {
            lastPosition = currentPosition;
        }

        wasMoving = isMoving;
    }
}
