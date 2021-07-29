using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public static InputManager Instance { get { return _instance; } }

    [HideInInspector]
    public bool isPressingLeft;
    [HideInInspector]
    public bool isPressingRight;
    [HideInInspector]
    public bool isPressingForward;
    [HideInInspector]
    public bool isFiring;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    public void PressLeft() {isPressingLeft = true;}
    public void ReleaseLeft() {isPressingLeft = false;}
    public void PressRight() {isPressingRight = true;}
    public void ReleaseRight() {isPressingRight = false;}
    public void PressForward() {isPressingForward = true;}
    public void ReleaseForward() {isPressingForward = false;}
    public void PressFire() {isFiring = true;}
    public void ReleaseFire() {isFiring = false;}
}
