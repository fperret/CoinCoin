using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterManager : MonoBehaviour {

	public static WaterManager instance;

	public Material defaultMaterial;
	public GameObject waterTile;
	public Transform waterParent;
	public GameObject[,] waterTiles;
	public Transform[,] tilesTransforms;

	private MaterialPropertyBlock propertyBlockBlue;
	private MaterialPropertyBlock propertyBlockWhite;
	private MaterialPropertyBlock propertyBlockRed;


	public Renderer[,] tileRenderers;

	public bool[,]  tilesToUpdate;
	//public int[,] grid;
	public float[,] grid;
	//public int[,] nextGrid;
	public float[,] nextGrid;

	public const int sideSize = 81;
	const int arraySize = sideSize + 2;
	const int cellNb = sideSize * sideSize;


	const float waterFlow = 0.1f;
	const float waterHeight = 10;

	// If higher than 0.125 more than 100% of a cell would move from one update to another
	const float diffusionFactor = 0.12f;

	public void updateGrid(int x, int z, int val)
	{
		// check on x and z ?
		grid[x, z] += val;
		nextGrid[x, z ] += val;
		//if (grid[x, z] == 0)
		//	grid[x, z] = val;
	}

	IEnumerator updateWaves()
	{
		int loopCounter = 0;
		while (true)
		{
			//nextGrid = grid;
			for (int i = 2; i < sideSize; ++i)
			{
				for (int j = 2; j < sideSize; ++j)
				{
					if (tilesToUpdate[i, j] == false)
						continue;
					float heightDiff = grid[i - 1, j] - grid[i, j];

					if (heightDiff > 0 && (grid[i - 1, j] > waterHeight))
					{
							nextGrid[i, j] += waterFlow;
							nextGrid[i - 1, j] -= waterFlow;
					}
					
					heightDiff = grid[i - 1, j - 1] - grid[i, j];
					if (heightDiff > 0 && (grid[i - 1, j - 1] > waterHeight))
					{
						nextGrid[i, j] += waterFlow;
						nextGrid[i - 1, j - 1] -= waterFlow;
					}

					heightDiff = grid[i - 1, j + 1] - grid[i, j];
					if (heightDiff > 0 && (grid[i - 1, j + 1] > waterHeight))
					{
						nextGrid[i, j] += waterFlow;
						nextGrid[i - 1, j + 1] -= waterFlow;
					}

					heightDiff = grid[i, j - 1] - grid[i, j];
					if (heightDiff > 0 && (grid[i, j - 1] > waterHeight))
					{
						nextGrid[i, j] += waterFlow;
						nextGrid[i, j - 1] -= waterFlow;
					}

					heightDiff = grid[i, j + 1] - grid[i, j];
					if (heightDiff > 0 && (grid[i, j + 1] > waterHeight))
					{
						nextGrid[i, j] += waterFlow;
						nextGrid[i, j + 1] -= waterFlow;
					}

					heightDiff = grid[i + 1, j] - grid[i, j];
					if (heightDiff > 0 && (grid[i + 1, j] > waterHeight))
					{
						nextGrid[i, j] += waterFlow;
						nextGrid[i + 1, j] -= waterFlow;
					}

					heightDiff = grid[i + 1, j - 1] - grid[i, j];
					if (heightDiff > 0 && (grid[i + 1, j - 1] > waterHeight))
					{
						nextGrid[i, j] += waterFlow;
						nextGrid[i + 1, j - 1] -= waterFlow;
					}

					heightDiff = grid[i + 1, j + 1] - grid[i, j];
					if (heightDiff > 0 && (grid[i + 1, j + 1] > waterHeight))
					{
						nextGrid[i, j] += waterFlow;
						nextGrid[i + 1, j + 1] -= waterFlow;
					}
					//nextGrid[i, j] = grid[i, j];
					/*if (heightDiff > 0)
					{
						nextGrid[i, j] += waterFlow;
						nextGrid[i - 1, j] -= waterFlow;
						nextGrid[i, j] += (heightDiff * diffusionFactor);
						nextGrid[i - 1, j] -= (heightDiff * diffusionFactor);
					}
					
					heightDiff = grid[i - 1, j - 1] - grid[i, j];
					if (heightDiff > 0)
					{
						nextGrid[i, j] += (heightDiff * diffusionFactor);
						nextGrid[i - 1, j - 1] -= (heightDiff * diffusionFactor);
					}

					heightDiff = grid[i - 1, j + 1] - grid[i, j];
					if (heightDiff > 0)
					{
						nextGrid[i, j] += (heightDiff * diffusionFactor);
						nextGrid[i - 1, j + 1] -= (heightDiff * diffusionFactor);
					}

					heightDiff = grid[i, j - 1] - grid[i, j];
					if (heightDiff > 0)
					{
						nextGrid[i, j] += (heightDiff * diffusionFactor);
						nextGrid[i, j - 1] -= (heightDiff * diffusionFactor);
					}

					heightDiff = grid[i, j + 1] - grid[i, j];
					if (heightDiff > 0)
					{
						nextGrid[i, j] += (heightDiff * diffusionFactor);
						nextGrid[i, j + 1] -= (heightDiff * diffusionFactor);
					}

					heightDiff = grid[i + 1, j] - grid[i, j];
					if (heightDiff > 0)
					{
						nextGrid[i, j] += (heightDiff * diffusionFactor);
						nextGrid[i + 1, j] -= (heightDiff * diffusionFactor);
					}

					heightDiff = grid[i + 1, j - 1] - grid[i, j];
					if (heightDiff > 0)
					{
						nextGrid[i, j] += (heightDiff * diffusionFactor);
						nextGrid[i + 1, j - 1] -= (heightDiff * diffusionFactor);
					}

					heightDiff = grid[i + 1, j + 1] - grid[i, j];
					if (heightDiff > 0)
					{
						nextGrid[i, j] += (heightDiff * diffusionFactor);
						nextGrid[i + 1, j + 1] -= (heightDiff * diffusionFactor);
					}*/
				}
			}
			for (int i = 2; i < sideSize; ++i)
			{
				for (int j = 2; j < sideSize; ++j)
				{
					if (tilesToUpdate[i, j] == false && nextGrid[i, j] == -waterHeight)
					{
						tilesToUpdate[i, j] = true;
						nextGrid[i, j] = waterHeight;
					}
					else if (tilesToUpdate[i, j] == false)
					{
						nextGrid[i, j] = -waterHeight;
					}
					float heightDiff = grid[i, j] - nextGrid[i, j];
					//if (loopCounter == 10)
					//	tilesToUpdate[i, j] = true;
					
					/*if (Mathf.Abs(grid[i, j] - nextGrid[i, j]) > 0.1f)
					{
						tileRenderers[i, j].SetPropertyBlock(propertyBlockWhite);
					}
					else
						tileRenderers[i, j].SetPropertyBlock(propertyBlockBlue);*/
						if (tilesToUpdate[i, j])
							grid[i, j] = nextGrid[i, j];
					/*if (tilesToUpdate[i, j] == true)
						grid[i, j] = nextGrid[i, j];
					else
						nextGrid[i, j] = grid[i, j];*/
					//MaterialPropertyBlock testColor = new MaterialPropertyBlock();
					//testColor.SetColor("_Color", new Color(0, 0, grid[i, j]));
					//tileRenderers[i, j].SetPropertyBlock(testColor);
					
					tilesTransforms[i, j].localScale = new Vector3(1, grid[i, j] * 2, 1);

					if (heightDiff > 0)
					{
						tilesToUpdate[i, j] = false;
					}
				}
			}
			printGrid();
			
			loopCounter += 1;
			if (loopCounter > 10)
				loopCounter = 0;
			yield return new WaitForSeconds(0.03f);
			//while (!Input.GetKeyDown(KeyCode.Space))
			//	yield return null;
			yield return null;
		}
	}

	private void printGrid()
	{
		if (sideSize < 25)
		{
			string toPrint = "";
			for (int i = 1; i < sideSize; ++i)
			{
				string line = "";
				for (int j = 1; j < sideSize; ++j)
				{
					line += grid[i, j];
				}
				toPrint += line += "\n";
			}
			Debug.Log(toPrint);
			Debug.Log("--------------");
		}
	}

	private bool checkNearby(int i, int j)
	{
		if (grid[i - 1, j - 1] == 1
		|| grid[i -1, j] == 1
		|| grid[i - 1, j + 1] == 1
		|| grid[i, j - 1] == 1
		|| grid[i, j + 1] == 1
		|| grid[i + 1, j - 1] == 1
		|| grid[i + 1, j] == 1
		|| grid[i + 1, j + 1] == 1)
			return true;
		else
			return false;
	}

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		grid = new float[arraySize,arraySize];
		nextGrid = new float[arraySize,arraySize];
		waterTiles = new GameObject[arraySize,arraySize];
		tileRenderers = new Renderer[arraySize,arraySize];
		tilesTransforms = new Transform[arraySize,arraySize];
		tilesToUpdate = new bool[arraySize, arraySize];

		propertyBlockBlue = new MaterialPropertyBlock();
		propertyBlockBlue.SetColor("_Color", Color.blue);
		propertyBlockWhite = new MaterialPropertyBlock();
		propertyBlockWhite.SetColor("_Color", Color.white);
		propertyBlockRed = new MaterialPropertyBlock();
		propertyBlockRed.SetColor("_Color", Color.red);
		for (int i = 0; i < arraySize; ++i)
		{
			for (int j = 0; j < arraySize; ++j)
			{
				waterTiles[i, j] = Instantiate(waterTile, new Vector3(i, 0, j), Quaternion.identity);
				//waterTiles[i, j].transform.Rotate(new Vector3(90, 0, 0));
				//waterTiles[i, j].transform.parent = waterParent;
				
				tilesTransforms[i, j] = waterTiles[i, j].transform;

				tileRenderers[i, j] = waterTiles[i, j].GetComponent<Renderer>();
				tileRenderers[i, j].sharedMaterial = defaultMaterial;
				tileRenderers[i, j].SetPropertyBlock(propertyBlockBlue);
				
				grid[i, j] = 10;
				nextGrid[i, j] = 10;
				tilesToUpdate[i, j] = true;
			}
		}

		//grid[0,0] = 1;
		//grid[sideSize / 2, sideSize / 2] = 1;
		//grid[50, 50] = 250;
		//grid[41, 41] = 100;
		//nextGrid[41, 41] = 100;

		/*grid[40, 40] = 10000;
		grid[40, 41] = 10000;
		grid[40, 42] = 10000;
		grid[41, 40] = 10000;
		grid[41, 42] = 10000;
		grid[42, 40] = 10000;
		grid[42, 41] = 10000;
		grid[42, 42] = 10000;

		nextGrid[40, 40] = 10000;
		nextGrid[40, 41] = 10000;
		nextGrid[40, 42] = 10000;
		nextGrid[41, 40] = 10000;
		nextGrid[41, 42] = 10000;
		nextGrid[42, 40] = 10000;
		nextGrid[42, 41] = 10000;
		nextGrid[42, 42] = 10000;*/
		StartCoroutine("updateWaves");
	}
	
	// Update is called once per frame
	void Update () {

	}
		
}
