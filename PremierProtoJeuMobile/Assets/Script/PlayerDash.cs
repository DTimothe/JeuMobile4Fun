using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerDash : MonoBehaviour
{
    public Animator anim;
    public int cooldownDash;
    public float forceGravite;
    public Rigidbody body;
    Vector2 posDepart;
    Vector2 direction;
    float lengthSaut;
    bool directionChosen=false;


    void Update()
    {


        if (directionChosen == false)
        {
            recupVector();
        }
        else
        {
            coolDownDash();
        }
    }



    private void FixedUpdate()
    {
        Gravity();
        CheckSaut();
    }



    void Dash()
    {
        body.velocity = -direction * lengthSaut;
    }

    void recupVector()
    {
        if (Input.touchCount > 0)
        {
            /* Vector3 posDepart = Input.GetTouch(0).position;
             transform.Translate(Input.GetTouch(0).deltaPosition* Time.deltaTime);
             Debug.Log(Input.GetTouch(0).position);*/

            Touch touch = Input.GetTouch(0);


            switch (touch.phase)
            {
                case TouchPhase.Began:
                    {
                        posDepart = Input.GetTouch(0).position;
                        break;
                    }
                case TouchPhase.Moved:
                    {
                        lengthSaut = Vector2.Distance(touch.position, posDepart);
                        direction = touch.position - posDepart;
                        break;
                    }
                case TouchPhase.Ended:
                    {
                        anim.SetBool("dashing", true);
                        directionChosen = true;
                        print(direction.normalized);
                        break;
                    }
            }
        }

    }

    void Gravity()
    {
        body.velocity += new Vector3(0, forceGravite, 0);
        if (body.velocity.y < -10)
            body.velocity = new Vector3(body.velocity.x, -10, 0);
    }

    void CheckSaut()
    {
        if (body.velocity.y<=0)
        {
            anim.SetBool("dashing", false);
        }
    }

    IEnumerator coolDownDash()
    {
        yield return new WaitForSeconds(cooldownDash);
        directionChosen = true;
    }
}
