using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public Texture2D[] crateTextures;
    public CrateType type;
    public bool randomType;

    private MeshRenderer mesh;

	// Use this for initialization
	void Start ()
    {
        mesh = GetComponent<MeshRenderer>();

        if(randomType)
        {
            int numOfTypes = System.Enum.GetNames(typeof(CrateType)).Length;
            type = (CrateType)Random.Range(0, numOfTypes - 1);
        }

        mesh.material.mainTexture = GetCrateTexture(type);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public Texture2D GetCrateTexture(CrateType type)
    {
        switch(type)
        {
            case CrateType.Type_1:
                return crateTextures[0];
            case CrateType.Type_2:
                return crateTextures[1];
            case CrateType.Type_3:
                return crateTextures[2];
        }

        return null;
    }
}

public enum CrateType
{
    Type_1,
    Type_2,
    Type_3,
    None
}
