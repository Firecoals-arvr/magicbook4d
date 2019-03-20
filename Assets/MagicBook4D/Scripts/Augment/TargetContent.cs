using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Firecoals.Augmentation
{
    public class TargetContent
    {
        /// <summary>
        /// List of content under a image target
        /// There are 2 list: CLONE & UNIQUE
        /// </summary>
        public Dictionary<ContentType, List<string>> groupList { get; }

        private Transform parent;

        public TargetContent(Transform target)
        {
            groupList = new Dictionary<ContentType, List<string>>();
            this.parent = target;
        }
        /// <summary>
        /// UNIQUE = There is only one version of the content as the children
        /// CLONE = There is many version of the content as the children
        /// </summary>
        public enum ContentType
        {
            UNIQUE, CLONE
        }

        /// <summary>
        /// Clear all game objects which is child of the target
        /// </summary>
        public void ClearAll()
        {
            foreach (Transform child in parent)
            {
                GameObject.Destroy(child.gameObject);
            }
            groupList.Clear();
        }
        /// <summary>
        /// Create game object as child of target
        /// </summary>
        /// <param name="gameObject"></param>
        public GameObject Create(GameObject clone, ContentType type)
        {
            clone.transform.localPosition = parent.transform.position;
            clone.name = System.Guid.NewGuid().ToString();
            List<string> goNames;
            if (groupList.TryGetValue(type, out goNames))
            {
                goNames.Add(clone.name);
            }
            else { groupList.Add(type, new List<string> { clone.name }); }


            return clone;
        }
        /// <summary>
        /// Destroy all clone game objects
        /// </summary>
        public void ClearClone()
        {
            foreach (Transform child in parent)
            {
                foreach (string goName in groupList[ContentType.CLONE])
                {
                    if (child.gameObject.name == goName)
                        GameObject.Destroy(child.gameObject);
                }
            }
            groupList.Remove(ContentType.CLONE);
        }
        /// <summary>
        /// Destroy all unique game object
        /// </summary>
        public void ClearUnique()
        {
            foreach (Transform child in parent)
            {
                foreach (string goName in groupList[ContentType.UNIQUE])
                {
                    if (child.gameObject.name == goName)
                        GameObject.Destroy(child.gameObject);
                }
            }
            groupList.Remove(ContentType.UNIQUE);
        }
        /// <summary>
        /// Return true if game object name existed name in the UNIQUE List
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public bool IsChildExistsInUniqueList(string childName)
        {
            bool isChildAlive = false;
            foreach (string goName in groupList[ContentType.UNIQUE])
            {
                if (goName.Equals(childName))
                {
                    return true;
                }
            }
            return isChildAlive;
        }
        /// <summary>
        /// Return true if game object name existed name in the CLONE List
        /// </summary>
        /// <param name="childName"></param>
        /// <returns></returns>
        public bool IsChildExistsInCLoneList(string childName)
        {
            bool isChildAlive = false;
            foreach (string goName in groupList[ContentType.UNIQUE])
            {
                if (goName.Equals(childName))
                {
                    return true;
                }
            }
            return isChildAlive;
        }
        /// <summary>
        /// Unique list is null
        /// </summary>
        /// <returns></returns>
        public bool UniqueListIsNull()
        {
            return groupList[ContentType.UNIQUE].Count <= 0;

        }
        /// <summary>
        /// Clone list is null
        /// </summary>
        /// <returns></returns>
        public bool CloneListIsNull()
        {
            return groupList[ContentType.CLONE].Count <= 0;
        }
    }

}
