using System.Collections;
using System.Collections.Generic;
using UnityEngine;




    public class FeedbackScript : MonoBehaviour
    {
        
        [SerializeField] private Material flashMaterial;
        [SerializeField] private float duration;

        

        // The SpriteRenderer that should flash.
        private SpriteRenderer spriteRenderer;

        // The material that was in use, when the script started.
        private Material originalMaterial;

        // The currently running coroutine.
        private Coroutine flashRoutine;

      

        

        void Start()
        {
           
            spriteRenderer = GetComponent<SpriteRenderer>();

            
            originalMaterial = spriteRenderer.material;
        }

       

        public void Flash()
        {
            
            if (flashRoutine != null)
            {
               
                StopCoroutine(flashRoutine);
            }

           
            flashRoutine = StartCoroutine(FlashRoutine());
        }

        private IEnumerator FlashRoutine()
        {
           
            spriteRenderer.material = flashMaterial;

            // Pause the execution of this function for "duration" seconds.
            yield return new WaitForSeconds(duration);

            // After the pause, swap back to the original material.
            spriteRenderer.material = originalMaterial;

            // Set the routine to null, signaling that it's finished.
            flashRoutine = null;
        }

        
    }
