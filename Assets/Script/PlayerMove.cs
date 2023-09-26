using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour{
    [SerializeField] private float speed;
    public LayerMask wallLayer;
    public Transform wallCheckPoint;
    float speedMultiplier;
    float acceleration = 10;
    Rigidbody2D rb;
    bool btnPressed;
    bool isWallTouch;
    Vector2 relativeTransform;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start() {
        UpdateRelativeTransform();
    }
    private void FixedUpdate() {
        UpdateSpeedMultiplier();
        float targetSpeed = speed * speedMultiplier*relativeTransform.x;
        rb.velocity = new Vector2(targetSpeed,rb.velocity.y);
        isWallTouch = Physics2D.OverlapBox(wallCheckPoint.position,new Vector2(0.06f,0.8f),0,wallLayer);
        if(isWallTouch) Fiip();
    }
    
    public void Move(InputAction.CallbackContext value){
        if(value.started){
            btnPressed = true;
        }else if(value.canceled){
            btnPressed = false;
        }
        
    }
    void UpdateSpeedMultiplier(){
        if(btnPressed && speedMultiplier<1){
            speedMultiplier += Time.deltaTime*acceleration;
            if(speedMultiplier > 1) speedMultiplier = 1;
        }else if(!btnPressed && speedMultiplier>0){
            speedMultiplier -= Time.deltaTime*acceleration;
            if(speedMultiplier < 0) speedMultiplier = 0;
        }
    }
    public void Fiip(){
        transform.Rotate(0,180,0);
        UpdateRelativeTransform();
    }
    void UpdateRelativeTransform(){
        relativeTransform = transform.InverseTransformVector(Vector2.one);
    }

}
