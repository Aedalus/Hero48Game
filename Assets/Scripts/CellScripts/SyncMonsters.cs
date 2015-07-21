using UnityEngine;
using System.Collections;

public class SyncMonsters : MonoBehaviour {

    /*
     * This tries to make the monsters match the bobbing motion of the hero.
     * Not great code, but it seems to work most of the time
     */
    GameObject hero;
    GameObject heroSpriteHandler;

    Animator heroAnim;
    Animator thisAnim;

    int stateName;

	// Use this for initialization
	void Start ()
    {
        //Get objects
        hero = GameObject.FindGameObjectWithTag("Hero");
        if (hero == null)
            return;
        heroSpriteHandler = hero.transform.Find("SpriteHandler").gameObject;

        //Get animators
        heroAnim = heroSpriteHandler.GetComponent<Animator>();
        thisAnim = this.GetComponent<Animator>();
        sync();
	}

    public void sync()
    {   
        //Starts facing the right way
        if (heroSpriteHandler == null)
            return;
        this.transform.localScale = heroSpriteHandler.transform.localScale;

        
        //Sync to hero's time
        AnimatorStateInfo animInfo = heroAnim.GetCurrentAnimatorStateInfo(0);
        float heroTime = animInfo.normalizedTime;

        //Debug.Log("HeroTime: " + heroTime);
        int name = thisAnim.GetCurrentAnimatorStateInfo(0).fullPathHash;
        thisAnim.Play(name, 0, heroTime);  
    }
	
	// Update is called once per frame
	void Update () {
        if (thisAnim == null)
            return;
        int current = thisAnim.GetCurrentAnimatorStateInfo(0).fullPathHash;
        if (stateName != current)
        {
            stateName = current;
            sync();
        }
	}
}
