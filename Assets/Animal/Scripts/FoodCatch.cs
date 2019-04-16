using UnityEngine;
using System.Collections;
using Firecoals.Animal;

namespace Firecoals.Animal
{
    public class FoodCatch : MoveAndJump
    {

        public GameObject ImageTarget;

        //  public string animtrigger;
        public bool eatloop = false;
        public Vector3 foodpos;
        public GameObject ItemRender;
        public Vector3 inpos;
        public Transform feet;
        public bool caught = false;

        protected void Update()
        {
          //  Debug.Log("CanEat:" + CanEat);

            if (CanEat == false)
            {
                if (ItemRender != null)
                {
                    ItemRender.active = true;
                }

                transform.parent = Item.transform;
                if (CanMove == false)
                {
                    transform.localPosition = new Vector3(1, 1, 1);
                }
                else
                {

                    transform.localPosition = foodpos;
                }

                caught = false;
            }

            if (caught && this.transform.parent.tag == "Player")
            {
                // transform.localPosition = new Vector3(0f, -100f, 0f);
                StartCoroutine(fade());
            }

        }

        void OnTriggerEnter(Collider coll)
        {

            if (coll.gameObject.tag == "Player")
            {

                CanEat = true;
                Debug.Log("catched");
                transform.parent = coll.gameObject.transform;
                caught = true;
                feet = coll.transform;
                Debug.Log("catched1");
                transform.localPosition = inpos;
                if (Item != null)
                {
                    Item.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
                }
            }
        }

        IEnumerator fade()
        {
            caught = false;
            yield return new WaitForSeconds(0.5f);
            if (ItemRender != null)
            {
                ItemRender.active = false;
            }

        }

    }
}
