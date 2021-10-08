using System.Collections;
using System.Collections.Generic;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using Cinemachine;
using UnityEngine;

public class SkipCutScene : MonoBehaviour
{
    [SerializeField]
    public Animator aT;
    [SerializeField]
    private CinemachineVirtualCamera vC;
    public AnimationClip c;
    public GameObject avT;
    public PlayableDirector pD;
    private TimelineAsset tA;

    private void Start()
    {
        if(aT != null)
        {
            aT = GameObject.Find("CM vcam1").GetComponent<Animator>();
            return; 
        }

        c = GetComponent<AnimationClip>();
        pD = FindObjectOfType<PlayableDirector>().GetComponent<PlayableDirector>(); 
        tA = (TimelineAsset)pD.playableAsset; // cast Timeline Asset from Playable Director 
    }
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            {
              //  Debug.Log("Time");
                MuteTrack(0);
                if(avT != null)
                {
                    avT.gameObject.SetActive(true);
                }
                aT.playableGraph.Stop();
                aT.StopPlayback();
            }
        }
    }

    void MuteTrack(int trackI)
    {
        double t = pD.time;
        aT.enabled = false;

    }
}
