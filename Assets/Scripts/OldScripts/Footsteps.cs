using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    // 
    private EventInstance FootstepsSound;
    private EventInstance JumpSound;
    private EventInstance LandSound;

    public EventReference footstepsEvent;
    public EventReference jumpEvent;
    public EventReference landEvent;

    private float lastFootstepTime;
    private float distToGround;
    private bool isGrounded = true;
    private bool isJumping;

    private void Start()
    {
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(IsGrounded());
            PlayJump();
        }
    }

    private void FixedUpdate()
    {
        // Footsteps
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            if (IsGrounded() && Time.time - lastFootstepTime > 0.5f)
            {
                lastFootstepTime = Time.time;
                PlayFootsteps();
            }

        // Running
        if ((Input.GetKey(KeyCode.LeftShift) && Input.GetAxisRaw("Horizontal") != 0) ||
            (Input.GetKey(KeyCode.LeftShift) && Input.GetAxisRaw("Vertical") != 0))
            if (IsGrounded() && Time.time - lastFootstepTime > 0.25f)
            {
                lastFootstepTime = Time.time;
                PlayFootsteps();
            }
    }

    private void PlayFootsteps()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, distToGround + 0.5f))
        {
            if (hit.collider.CompareTag("Stone"))
            {
                FootstepsSound = RuntimeManager.CreateInstance(footstepsEvent);
                FootstepsSound.set3DAttributes(gameObject.transform.To3DAttributes());
                FootstepsSound.setParameterByNameWithLabel("Footsteps_surface", "Stone");
                FootstepsSound.start();
                FootstepsSound.release();
            }
            else if (hit.collider.CompareTag("Wood"))
            {
                FootstepsSound = RuntimeManager.CreateInstance(footstepsEvent);
                FootstepsSound.set3DAttributes(gameObject.transform.To3DAttributes());
                FootstepsSound.setParameterByNameWithLabel("Footsteps_surface", "Wood");
                FootstepsSound.start();
                FootstepsSound.release();
            }
            else if (hit.collider.CompareTag("Outside"))
            {
                FootstepsSound = RuntimeManager.CreateInstance(footstepsEvent);
                FootstepsSound.set3DAttributes(gameObject.transform.To3DAttributes());
                FootstepsSound.setParameterByNameWithLabel("Footsteps_surface", "Stone");
                FootstepsSound.start();
                FootstepsSound.release();
            }
            else if (hit.collider.CompareTag("Inside_stone"))
            {
                FootstepsSound = RuntimeManager.CreateInstance(footstepsEvent);
                FootstepsSound.set3DAttributes(gameObject.transform.To3DAttributes());
                FootstepsSound.setParameterByNameWithLabel("Footsteps_surface", "Stone");
                FootstepsSound.start();
                FootstepsSound.release();
            }
            else if (hit.collider.CompareTag("Inside_wood"))
            {
                FootstepsSound = RuntimeManager.CreateInstance(footstepsEvent);
                FootstepsSound.set3DAttributes(gameObject.transform.To3DAttributes());
                FootstepsSound.setParameterByNameWithLabel("Footsteps_surface", "Wood");
                FootstepsSound.start();
                FootstepsSound.release();
            }
            else
            {
                FootstepsSound = RuntimeManager.CreateInstance(footstepsEvent);
                FootstepsSound.set3DAttributes(gameObject.transform.To3DAttributes());
                FootstepsSound.setParameterByNameWithLabel("Footsteps_surface", "Stone");
                FootstepsSound.start();
                FootstepsSound.release();
            }
        }
    }

    private void PlayJump()
    {
        //if (!JumpSound.isValid())
        if (IsGrounded())
        {
            JumpSound = RuntimeManager.CreateInstance(jumpEvent); // "event:/Footsteps"

            if (IsGrounded())
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, Vector3.down, out hit, distToGround + 0.5f))
                {
                    //Debug.Log(hit.collider.tag);
                    if (hit.collider.CompareTag("Stone"))
                    {
                        JumpSound.setParameterByNameWithLabel("Footsteps_surface", "Stone");
                        JumpSound.start();
                    }
                    else if (hit.collider.CompareTag("Wood"))
                    {
                        JumpSound.setParameterByNameWithLabel("Footsteps_surface", "Wood");
                        JumpSound.start();
                    }
                    else if (hit.collider.CompareTag("Inside_stone"))
                    {
                        LandSound.setParameterByNameWithLabel("Footsteps_surface", "Stone");
                        JumpSound.start();
                    }
                    else if (hit.collider.CompareTag("Inside_wood"))
                    {
                        LandSound.setParameterByNameWithLabel("Footsteps_surface", "Wood");
                        JumpSound.start();
                    }
                    else if (hit.collider.CompareTag("Bed"))
                    {
                        JumpSound.setParameterByNameWithLabel("Footsteps_surface", "Bed");
                        JumpSound.start();
                    }
                    else
                    {
                        JumpSound.setParameterByNameWithLabel("Footsteps_surface", "Stone");
                        JumpSound.start();
                    }
                }
            }

            JumpSound.release();
            isGrounded = false;
            isJumping = true;
        }
    }

    // Play landing sound
    private void OnCollisionEnter(Collision col)
    {
        if (IsGrounded() && isGrounded == false) PlayLanding();
    }

    private void PlayLanding()
    {
        if (IsGrounded() && isGrounded == false)
            if (isJumping)
            {
                LandSound = RuntimeManager.CreateInstance(landEvent);
                RaycastHit hit;

                if (Physics.Raycast(transform.position, Vector3.down, out hit, distToGround + 0.5f))
                {
                    //Debug.Log("Hit object tag: " + hit.collider.tag);
                    if (hit.collider.CompareTag("Stone"))
                    {
                        LandSound.setParameterByNameWithLabel("Footsteps_surface", "Stone");
                        LandSound.start();
                    }
                    else if (hit.collider.CompareTag("Wood"))
                    {
                        LandSound.setParameterByNameWithLabel("Footsteps_surface", "Wood");
                        LandSound.start();
                    }
                    else if (hit.collider.CompareTag("Inside_stone"))
                    {
                        LandSound.setParameterByNameWithLabel("Footsteps_surface", "Stone");
                        LandSound.start();
                    }
                    else if (hit.collider.CompareTag("Inside_wood"))
                    {
                        LandSound.setParameterByNameWithLabel("Footsteps_surface", "Wood");
                        LandSound.start();
                    }
                    else if (hit.collider.CompareTag("Bed"))
                    {
                        LandSound.setParameterByNameWithLabel("Footsteps_surface", "Bed");
                        LandSound.start();
                    }
                    else
                    {
                        LandSound.setParameterByNameWithLabel("Footsteps_surface", "Stone");
                        LandSound.start();
                    }
                }

                LandSound.release();
                isGrounded = true;
                isJumping = false;
            }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.5f);
    }
}