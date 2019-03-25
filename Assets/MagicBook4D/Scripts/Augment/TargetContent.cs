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

        public Transform parent;

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
            Unique, Clone
        }

        /// <summary>
        /// Clear all game objects which is child of the target
        /// </summary>
        public void ClearAll()
        {
            groupList.Clear();
        }

        /// <summary>
        /// Create game object as child of target
        /// </summary>
        /// <param name="clone"></param>
        /// <param name="type"></param>
        public GameObject Create(GameObject clone, ContentType type)
        {
            clone.transform.localPosition = parent.transform.position;
            clone.name = System.Guid.NewGuid().ToString();
            if (groupList.TryGetValue(type, out var goNames))
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
            groupList.Remove(ContentType.Clone);
        }
        /// <summary>
        /// Destroy all unique game object
        /// </summary>
        public void ClearUnique()
        {
            groupList.Remove(ContentType.Unique);
        }
        /// <summary>
        /// Return true if game object name existed name in the UNIQUE List
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public bool IsChildExistsInUniqueList(string childName)
        {
            const bool isChildAlive = false;
            foreach (var goName in groupList[ContentType.Unique])
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
            const bool isChildAlive = false;
            foreach (var goName in groupList[ContentType.Unique])
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
            return groupList[ContentType.Unique] == null;

        }
        /// <summary>
        /// Clone list is null
        /// </summary>
        /// <returns></returns>
        public bool CloneListIsNull()
        {
            return groupList[ContentType.Clone] == null;
        }
    }

}
