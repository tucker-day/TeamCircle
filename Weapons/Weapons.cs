using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;




namespace Assets.Scripts.Weapons
{
    internal class Weapons
    {
        private BoxCollider2D boxCollider2D;

        public GameObject Bow;
        public GameObject BowPrefab;

        public GameObject Sword;
        public GameObject SwordPrefab;

        public Animator Weaponanimator;

        public int EnemyHealth = 100; 


        public Weapons() {

            int WeaponDamage = 15;
            int WeaponSpeed = 20;
            int DuplicateWeaponDamage = 30;

           for(WeaponDamage = 0; WeaponDamage < 16; WeaponDamage++)
            {
              WeaponDamage= WeaponDamage + 1;
                WeaponSpeed = WeaponDamage + 2; 
            }

            for (WeaponSpeed = 0; WeaponSpeed < 21; WeaponSpeed++)
            {
                
                WeaponSpeed *= 2; 
                

            }

            for (DuplicateWeaponDamage = 0; DuplicateWeaponDamage < 31; DuplicateWeaponDamage++)
            {

                DuplicateWeaponDamage *= 2;

            }

            if (EnemyHealth <= 0)
            {
                EnemyHealth -= 0;

            }


        }








    }
}
