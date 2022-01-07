using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticuleController : MonoBehaviour
{
    public static void PlayParticleEffect(string particlePrefabName, Transform transf)
	{
        ParticleSystem prefab = Resources.Load<ParticleSystem>("Particle/" + particlePrefabName);
        ParticleSystem ps = Instantiate(prefab, transf.position, new Quaternion());
        ps.transform.parent = transf;
        ps.transform.localScale = new Vector3(1, 1);
    }

}
