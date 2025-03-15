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

        public Animator animator;


        public Weapons() {

            int WeaponDamage = 15;
            int WeaponSpeed = 20;
            int DuplicateWeaponDamage = 30;

           /* for(WeaponDamage = 0; WeaponDamage < 16; WeaponDamage++)
            {
                cout << " The enemy lose health" << endl; 
            }

            for (DuplicateWeaponDamage = 0; DuplicateWeaponDamage < 21; DuplicateWeaponDamage++)
            {
                cout << "The damage will increase when a specific weapon is found multiple times" << endl;
                DuplicateWeaponDamage *= 2; 

            }*/


        }

       
       

        


    }
}
