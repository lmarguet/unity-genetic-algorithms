using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopulationManager : MonoBehaviour
{
    public GameObject PersonPrefab;
    public int PopulationSize;
    public static float TimeElapsed = 0;

    private const float MIN_SCALE = .17f;
    private const float MAX_SCALE = .40f;
    
    private readonly List<GameObject> population = new List<GameObject>();
    private int trialTime = 10;
    private int generation = 1;
    private GUIStyle guiStyle;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < PopulationSize; i++)
        {
            var instanceGo = InstantiatePerson();

            var instanceDNA = instanceGo.GetComponent<DNA>();
            instanceDNA.R = Random.Range(0f, 1f);
            instanceDNA.G = Random.Range(0f, 1f);
            instanceDNA.B = Random.Range(0f, 1f);
            instanceDNA.Scale = Random.Range(MIN_SCALE, MAX_SCALE);

            population.Add(instanceGo);
        }
    }

    private GameObject InstantiatePerson()
    {
        var x = Random.Range(-9.5f, 9.5f);
        var y = Random.Range(-3.5f, 5.5f);
        var pos = new Vector3(x, y);
        
        return Instantiate(PersonPrefab, pos, Quaternion.identity);
    }

    void OnGUI()
    {
        guiStyle = new GUIStyle();
        guiStyle.fontSize = 50;
        guiStyle.normal.textColor = Color.white;
        
        GUI.Label(new Rect(10, 10, 100, 20), "Generation: " + generation, guiStyle);
        GUI.Label(new Rect(10, 65, 100, 20), "Trial time: " + (int)TimeElapsed, guiStyle);
    }

    // Update is called once per frame
    void Update()
    {
        TimeElapsed += Time.deltaTime;
        if (TimeElapsed > trialTime)
        {
            BreedNewPopulation();
            TimeElapsed = 0;
        }
    }

    private void BreedNewPopulation()
    {
        foreach (var person in population)
        {
            person.GetComponent<DNA>().Die();
        }
        
        var newPopulation = new List<GameObject>();
        var sortedList = population.OrderBy(x =>  x.GetComponent<DNA>().TimeToDie)
                                    .ToList();
        
        population.Clear();

        var startIndex = Mathf.RoundToInt(sortedList.Count / 2 - 1);
        for (int i = startIndex; i < sortedList.Count - 1; i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i]));
        }

        sortedList.ForEach(Destroy);

        generation++;
    }

    private GameObject Breed(GameObject a, GameObject b)
    {
        var offspring = InstantiatePerson();
        var dna1 = a.GetComponent<DNA>();
        var dna2 = b.GetComponent<DNA>();

        var instanceDNA = offspring.GetComponent<DNA>();
        instanceDNA.R = RandomChance(50) ? dna1.R : dna2.R;
        instanceDNA.G = RandomChance(50) ? dna1.G : dna2.G;
        instanceDNA.B = RandomChance(50) ? dna1.B : dna2.B;
        instanceDNA.Scale = RandomChance(50) ? dna1.Scale : dna2.Scale;
        return offspring;
    }

    private static bool RandomChance(int percentChance)
    {
        return Random.Range(0, 100) < percentChance;
    }
}

















