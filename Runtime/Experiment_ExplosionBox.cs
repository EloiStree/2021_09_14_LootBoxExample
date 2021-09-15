using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Experiment_ExplosionBox : MonoBehaviour //> behaviour > component
{
    public bool m_useAutoStart;
    public float m_timeBeforeExplosion = 2f;
    public WallToExplode [] m_wallsToExplode;

    public UnityEvent m_toDoWhenBoum;

    [System.Serializable]
    public class WallToExplode {
        public GameObject m_wallToExplode;
        public Transform m_whereToCreateExplosion;
        public float m_explosionPower = 2;
        public float m_explosionRadius = 3;
        [Header("Dont touch")]
        public Rigidbody m_wallCreatedRigidBody;
    }


    void Start()
    {
        if(m_useAutoStart)
            Invoke("Boum", m_timeBeforeExplosion);
    }

   public  void Boum()
    {
        for (int i = 0; i < m_wallsToExplode.Length; i++)
        {

             m_wallsToExplode[i].m_wallCreatedRigidBody = m_wallsToExplode[i].m_wallToExplode.AddComponent<Rigidbody>();
              m_wallsToExplode[i].m_wallCreatedRigidBody.AddExplosionForce(
                m_wallsToExplode[i].m_explosionPower,
                m_wallsToExplode[i].m_whereToCreateExplosion.position,
                m_wallsToExplode[i].m_explosionRadius, 0, ForceMode.Impulse);
        }
        m_toDoWhenBoum.Invoke();
    }
}
