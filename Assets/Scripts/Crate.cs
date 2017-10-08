using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public Texture2D[] crateTextures;
    public CrateType type;
    public bool randomType;

    [Header("Sounds")]
    public AudioClip[] hitClips;

    private MeshRenderer mesh;
    private Rigidbody physics;

    void Awake()
    {
        physics = GetComponent<Rigidbody>();
    }

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

    void OnCollisionEnter(Collision other)
    {
        if(physics.velocity.magnitude > 0.05f)
        {
            AudioSource.PlayClipAtPoint(hitClips[Random.Range(0, hitClips.Length)], other.contacts[0].point);
        }
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
