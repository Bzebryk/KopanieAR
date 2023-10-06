using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Joystick movement_joystick;

    public float gravity = 16.8f;
    public float gravity_velocity = 0;
    public float kick_force = 10.0f;
    public Transform ground_check;

    public Camera cam;
    
    public float ground_distance = 0.1f;
    bool is_on_ground = false;
    

    public float speed = 1.0f;

    public LayerMask ground_mask;

    public CharacterController character_controller;

    float angle_turn_time = 0.1f;
    float turn_speed;
    float new_angle = 0;
    Vector3 direction;

    void Update()
    {
        gravity_velocity -= gravity * Time.deltaTime;
        is_on_ground = Physics.CheckSphere(ground_check.position, ground_distance, ground_mask);

        if (is_on_ground && gravity_velocity < 0) {
            gravity_velocity = -1.0f;
        }

        Vector3 cam_dir_forward = cam.transform.forward * movement_joystick.Vertical;
        Vector3 cam_dir_right = cam.transform.right * movement_joystick.Horizontal;

        direction = cam_dir_forward + cam_dir_right;
        direction.y = gravity_velocity;

        float move_factor = new Vector2(movement_joystick.Horizontal, movement_joystick.Vertical).normalized.magnitude;
        
        Vector3 velocity = direction * speed * move_factor;
        velocity.y = gravity_velocity;

        character_controller.Move(velocity * Time.deltaTime);
        
    }
    void FixedUpdate()
    {
        if (movement_joystick.Vertical != 0 || movement_joystick.Horizontal != 0)
        {
            new_angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        }
        transform.rotation = Quaternion.Euler(transform.rotation.x, new_angle, transform.rotation.z);
    }
    private void OnCollisionEnter(Collision collision){
        Rigidbody body = collision.collider.attachedRigidbody;

        if (body != null) {
            if (body.tag == "ball") {
                Vector3 kick_direction = (body.transform.position - transform.position).normalized;

                Vector3 kick_velocity = (kick_direction * kick_force);
                kick_velocity.y = body.velocity.y+(kick_force*0.2f);
                
                body.velocity = kick_velocity;
                
            }
        }
    }
}
