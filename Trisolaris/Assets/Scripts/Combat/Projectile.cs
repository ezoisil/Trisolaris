using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trisolaris.Combat
{
    public class Projectile : MonoBehaviour
    {
        private Transform target = null;

        [SerializeField] float speed = 1f;

        private void Awake()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        void Start()
        {
       
        }

        void Update()
        {
            if (target == null) { Destroy(gameObject); }
            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null)
            {
                return target.position;
            }
            return target.position + Vector3.up * targetCapsule.height / 2;

        }
    }
}
