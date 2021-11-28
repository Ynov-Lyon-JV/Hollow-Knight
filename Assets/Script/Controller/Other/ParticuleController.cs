using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticuleController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static void playParticleEffect(string particlePrefabName, Transform transf)
	{
        var prefab = Resources.Load<ParticleSystem>("Particles/" + particlePrefabName);
        ParticleSystem ps = Instantiate(prefab, transf.position, new Quaternion());
        ps.transform.parent = transf;
        ps.transform.localScale = new Vector3(1, 1);

    }
}
