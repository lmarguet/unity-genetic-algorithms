using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility.Inspector;
using UnityStandardAssets.Characters.ThirdPerson;

public class BrainBehaviour : MonoBehaviour
{
    public float TimeAlive;
    public DNA Dna;
    public GameObject eyes;
    public float TimeWalking;
    public GameObject EthanPrefab;
    
    
    private bool alive = true;
    private bool seeGround = true;
    private int dnaLength = 2;
    private GameObject ethanInstance;


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "dead"){
            Debug.Log("DEAD");
            alive = false;
            TimeAlive = 0;
            TimeWalking = 0;
            Destroy(ethanInstance);
        }
    }

    void OnDestroy()
    {
        Destroy(ethanInstance);
    }

    public BrainBehaviour Init()
    {
        // 0  forward 1 left 2 right
        Dna = new DNA(dnaLength, 3);
        TimeAlive = 0;
        alive = true;

        ethanInstance = Instantiate(EthanPrefab, transform.position, transform.rotation);
        ethanInstance.GetComponent<AICharacterControl>().target = transform;
        
        return this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive) return;
//        Debug.DrawRay(
//            eyes.transform.position,
//            eyes.transform.forward * 10,
//            Color.red, 10);
        seeGround = false;

        RaycastHit hit;
        if (Physics.Raycast(eyes.transform.position, eyes.transform.forward * 10, out hit)){
            if (hit.collider.gameObject.tag == "platform"){
                seeGround = true;
            }
        }

        TimeAlive = PopulationManager.ElapsedTime;

        float turn = 0;
        float move = 0;

        var gene = seeGround ? Dna.GetGene(0) : Dna.GetGene(1);

        if (gene == 0){
            move = 1;
            TimeWalking++;
        }
        else if (gene == 1){ turn = -90; }
        else if (gene == 2){ turn = 90; }
        
        transform.Translate(0, 0, move * 0.1f);
        transform.Rotate(0, turn, 0); 
    }
}