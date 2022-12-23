using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using Backtrack.Core;

public class ChoiceZone : MonoBehaviour
{
	Collider m_Collider;
	Vector3 m_Center;
    const string playerTag = "Player";
	[SerializeField] GameObject leftBranch;
    [SerializeField] GameObject rightBranch;

    // Use this for initialization
    void Start()
	{
		m_Collider = gameObject.GetComponent<Collider>();
		m_Center = m_Collider.bounds.center;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(playerTag))
        {
			// get collide point position
			Vector3 position = this.transform.position;
			Vector3 closestPoint = col.ClosestPoint(position);

			if(closestPoint.x < m_Center.x)
			{
				col.gameObject.transform.rotation = leftBranch.transform.rotation;
			}
			else
			{
				col.gameObject.transform.rotation = rightBranch.transform.rotation;
            }
        }
    }
}

