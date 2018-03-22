using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerDNA  {

	private List<int> genes;
	private int dnaLength;
	private int maxValues;

	public WalkerDNA(int l, int v)
	{
		genes = new List<int>();
		dnaLength = l;
		maxValues = v;
		
		SetRandom();
	}

	public void SetRandom()
	{
		genes.Clear();
		
		for (var i = 0; i < dnaLength; i++)
		{
			genes.Add(GetRandomGeneValue());	
		}
	}

	private int GetRandomGeneValue()
	{
		return Random.Range(0, maxValues);
	}

	public void SetInt(int pos, int value)
	{
		genes[pos] = value;
	}

	public void Combine(WalkerDNA dna1, WalkerDNA dna2)
	{
		for (var i = 0; i < dnaLength; i++)
		{
			genes[i] = i < dnaLength / 2 ? dna1.genes[i] : dna2.genes[i];
		}
	}

	public void Mutate()
	{
		genes[Random.Range(0, dnaLength)] = GetRandomGeneValue();
	}

	public int GetGene(int i)
	{
		return genes[i];
	}
}
