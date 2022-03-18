using System.Collections;
using System.Collections.Generic;
using Trisolaris.Core;
using Trisolaris.Movement;
using UnityEngine;

namespace Trisolaris.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks;
        [SerializeField] float weaponDamage = 5f;

        Health target;
        Mover mover;
        float timeSinceLastAttack;
        Animator animator;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;


            if (target == null) return;
            if (target.IsDead()) return;
            if (!GetIsInRange())
            {
                mover.MoveTo(target.transform.position);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {

            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                // This will tritgger the Hit() event.
                animator.SetTrigger("attack");
                timeSinceLastAttack = 0;
            }

        }
        // This is an animation event
        void Hit()
        {
            target.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
            Debug.Log("attack!");
        }

        public void Cancel()
        {
            target = null;
            animator.SetTrigger("stopAttacking");
        }


    }
}
