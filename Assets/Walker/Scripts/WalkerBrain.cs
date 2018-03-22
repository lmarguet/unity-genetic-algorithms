using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class WalkerBrain : MonoBehaviour
{
    public int DNALength = 1;
    public float TimeAlive;
    public float DistanceTraveled;
    public WalkerDNA Dna;

    private ThirdPersonCharacter mcharacter;
    private Vector3 mMove;
    private bool mJump;
    private bool alive = true;

    void Start()
    {
        mcharacter = GetComponent<ThirdPersonCharacter>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "dead")
        {
            alive = false;
        }
    }

    public WalkerBrain Init()
    {
        // initalize DNA
        // 0 forward
        // 1 back
        // 2 left
        // 3 right
        // 4 jump
        // 5 crouch
        Dna = new WalkerDNA(DNALength, 6);
        alive = true;
        return this;
    }

    void FixedUpdate()
    {
        float h = 0;
        float v = 0;
        var crouch = false;

        var geneValue = Dna.GetGene(0);
        if (geneValue == 0)
            v = 1;
        else if (geneValue == 1)
        {
            v = -1;
        }
        else if (geneValue == 2)
        {
            h = 1;
        }
        else if (geneValue == 3)
        {
            h = -1;
        }
        else if (geneValue == 4)
        {
            mJump = true;
        }
        else if (geneValue == 5)
        {
            crouch = true;
        }

        mMove = v * Vector3.forward + h * Vector3.right;
        mcharacter.Move(mMove, crouch, mJump);
        
        mJump = false;

        if (alive)
        {
            TimeAlive += Time.deltaTime;
            DistanceTraveled += (h != 0 || v != 0) ? 1 : 0;
        }
    }
}