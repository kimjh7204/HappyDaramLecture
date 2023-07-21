using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _2_Game_Theory_ESS.Scripts
{
    public class ESSManager : MonoBehaviour
    {
        public GameObject blobDove;
        public GameObject blobHawk;
        public GameObject food;

        private List<Transform> allFood;

        public delegate void TickRate();
        public TickRate tickRate;
    
        public event TickRate FoodEatingEvent;
        public event TickRate FoodComsumeEvent;

        public const float width = 50f;
    
        public static ESSManager instance;
    
        private void Awake()
        {
            instance = this;
        
            allFood = new List<Transform>();
            StartCoroutine(Timer());
            StartCoroutine(FoodEatingTimer());
        }

        private IEnumerator Timer()
        {
            while (true)
            {
                //먹이 생성 코드
                var posX = Random.Range(-width, width);
                var posY = Random.Range(-width, width);

                var foodInstance = Instantiate(food, new Vector3(posX, 0f, posY), Quaternion.identity);
                allFood.Add(foodInstance.transform);

                if (tickRate != null)
                    tickRate();
            
                if(FoodComsumeEvent != null)
                    FoodComsumeEvent();
            
                yield return new WaitForSeconds(1f);
            }
        }

        private IEnumerator FoodEatingTimer()
        {
            while (true)
            {
                if (FoodEatingEvent != null)
                    FoodEatingEvent();
            
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
