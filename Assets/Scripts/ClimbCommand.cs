using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbCommand : MonoBehaviour
{
    [SerializeField]
    private Transform A;
    [SerializeField]
    private Transform B;

    [SerializeField]
    private Vector3 startClimbPos;
    [SerializeField]
    private Vector3 endClimbPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ClimbStart"))
        {
            var player = other.transform.parent.GetComponent<PlayerController>();

            if (player != null)
            {
                player.ClimbLadder(startClimbPos, this);
            }
        }
    }

    public Vector3 GetClimbPos()
    {
        return endClimbPos;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "ClimbObj")
        {
          //  transform.parent.position = Vector3.Lerp(A.transform.position, B.transform.position, 2f);
            Debug.Log("Hitting Climb Obj"); 
        }
    }

    public IEnumerator LerpPos(Vector3 targetPos, float dur)
    {
        float time = 0;
        Vector3 startPos = startClimbPos;
        targetPos = endClimbPos;
        while(time < dur)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, time);
            time += Time.deltaTime;
            yield return null; 
        }
        transform.position = targetPos; 
    }
}
