using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class myScript : MonoBehaviour
{
    public FixedJoystick joystick;
    public FixedButton button;
    public FixedTouchField field;
    public FixedButton shoot;
    public Animator anim;
    public GameObject ray;
    public AudioSource shootSFX;
    bool shot = false;
    //public LineRenderer line;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //print(anim.GetBool("shoot"));
        var fps = GetComponent<RigidbodyFirstPersonController>();
        
        fps.runAxis = joystick.Direction;
        fps.jumpAxis = button.Pressed;
        fps.mouseLook.lookAxis = field.TouchDist;
        
        if(shoot.Pressed)
        {
            
            shoot.Pressed = false;
            
            //print("shot");
            Shoot();
            shot = true;
            //Invoke("ResetShot", 0.5f);
        }
    }

    void Shoot()
    {
        shootSFX.Play();
        anim.SetTrigger("shoot");
        //shot = false;
        RaycastHit hit;
        Physics.Raycast(ray.transform.position, ray.transform.forward, out hit, 25f);
        if (hit.transform != null)
        {
            Debug.Log("You Hit: " + hit.transform.gameObject.name);
        }
        Debug.DrawRay(ray.transform.position, ray.transform.forward * 25f, Color.red, 5);
        //line = (ray.transform.position,)
    }
}
