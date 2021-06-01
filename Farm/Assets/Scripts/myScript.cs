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
    public AudioSource walkSFX;
    bool walking = false;

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
        //print(joystick.Direction.x + " " + joystick.Direction.y);
        //print(button.Pressed);
        if ((joystick.Direction.y != 0 || joystick.Direction.y != 0) && gameObject.transform.position.y < 1f) walking = true; // better use != 0 here for both directions
        else walking = false;
        if (walking && !walkSFX.isPlaying) walkSFX.Play();
        if (!walking) walkSFX.Stop();
        
        fps.jumpAxis = button.Pressed;
        fps.mouseLook.lookAxis = field.TouchDist;
        
        if(shoot.Pressed)
        {
            
            shoot.Pressed = false;
            
            //print("shot");
            Shoot();
            //shot = true;
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
        /*
        if (hit.transform != null && hit.transform.tag != "terrain")
        {
            Destroy(hit.transform.gameObject);
            
        }
        */
        if(hit.transform != null && hit.transform.tag == "Enemy")
        {
            //print("shot");
            //print(GameObject.Find("Terrain").GetComponent<spawner>().counter);
            hit.transform.gameObject.GetComponent<enemyAI>().DestroyMe();
            GameObject.Find("Terrain").GetComponent<spawner>().counter--;
            GameObject.Find("Terrain").GetComponent<spawner>().killed++;
        }
        //Debug.DrawRay(ray.transform.position, ray.transform.forward * 25f, Color.red, 5);
        //line = (ray.transform.position,)
    }
}
