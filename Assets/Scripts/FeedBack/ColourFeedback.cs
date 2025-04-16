using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class ColourFeedback : MonoBehaviour
    {
    
    [SerializeField] private Color[] colors; 

    [SerializeField] private KeyCode flashKey;

      [SerializeField] private Material flashMaterial;
    [SerializeField] private float duration;



    // The SpriteRenderer that should flash.
       private SpriteRenderer spriteRenderer;

    // The material that was in use, when the script started.
      private Material originalMaterial;

    // The currently running coroutine.
       private Coroutine flashRoutine = null;

     private void Start()
      {
        spriteRenderer= GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material; 
      }

    private void Update()
     {
            if (Input.GetKeyDown(flashKey))
             {
              Color randomColor = colors[Random.Range(0, colors.Length)];
              // flashMaterial.Flash(randomColor);    
             }


      }

    //gameobject.getComponent<ColorFeedback>().Flash(),


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

        
        yield return new WaitForSeconds(duration);

        
        spriteRenderer.material = originalMaterial;

        
        flashRoutine = null;
    }








}

