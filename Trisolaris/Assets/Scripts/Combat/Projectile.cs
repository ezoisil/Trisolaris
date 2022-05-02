using System;
using System.Collections;
using System.Collections.Generic;
using Trisolaris.Core;
using UnityEngine;

namespace Trisolaris.Combat
{
    public class Projectile : MonoBehaviour
    {
    
        [SerializeField] float speed = 1f;

        private Health target = null;


        private void Awake()
        {
            
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
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * targetCapsule.height / 2;
            
        }

        public void SetTarget(Health target)
        {
            this.target = target;
        }
    }
}
