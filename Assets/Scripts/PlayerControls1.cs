using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
 
    public CharacterController controller;
    public Animator anim;

    public AudioClip runningSound;
    private AudioSource audioSource;

    public float runningSpeed = 4.0f;
    public float rotationSpeed = 100.0f;
    public float jumpHeight = 6.0f;

    private float jumpInput;
    private float runInput;
    private float rotateInput;

    public Vector3 moveDir;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        runInput = Input.GetAxis("Vertical");
        rotateInput = Input.GetAxis("Horizontal");
    
        moveDir = new Vector3(0, jumpInput * jumpHeight, runInput * runningSpeed);

        moveDir = transform.TransformDirection(moveDir);

        controller.Move(moveDir * Time.deltaTime);
        
        transform.Rotate(0f, rotateInput * rotationSpeed * Time.deltaTime, 0f);
        
        Effects();
    }
   
    void CheckJump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            jumpInput = 1;

            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        if (controller.isGrounded)
        {
            jumpInput = 0;
        }
    }

    void Effects()
    {
        if (runInput != 0)
        {
            anim.SetBool("Run Forward", true);
            if (audioSource != null && !audioSource.isPlaying && controller.isGrounded)
            {
                audioSource.clip = runningSound;
                audioSource.Play();
            }
        } else {
            anim.SetBool("Run Forward", false);
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
        
        if (runInput != 0 && jumpInput == 0)
        
        CheckJump();
        
        if (jumpInput == 1)
        {
            // If true then set Boolean "Jump" parameter to true
            anim.SetBool("Jump", true);
        } else {
            // If false then set Boolean "Jump" parameter to false
            anim.SetBool("Jump", false);
        }
    }

}