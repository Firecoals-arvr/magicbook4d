using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Animal
{
    /// <inheritdoc />
    /// <summary>
    /// Defined class for toy or food of animals
    /// </summary>
    public class Item : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.tag == "Player")
            {
                Debug.Log("Dog eating the bone");
            }
        }
    }

}
