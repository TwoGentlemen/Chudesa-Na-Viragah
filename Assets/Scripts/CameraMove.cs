using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
	public float damping = 1.5f;
	public Vector2 offset = new Vector2(2f, 1f);
	public bool faceLeft;
	private Transform player;
	private int lastX;

	void Start()
	{
		offset = new Vector2(Mathf.Abs(offset.x), offset.y);
		FindPlayer(faceLeft);
	}

	public void FindPlayer(bool playerFaceLeft)
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		lastX = Mathf.RoundToInt(player.position.z);
	}

	void FixedUpdate()
	{
		if (player)
		{
			int currentZ = Mathf.RoundToInt(player.position.z);
			if (currentZ > lastX) faceLeft = false; 
			else if (currentZ < lastX) faceLeft = true;
			lastX = Mathf.RoundToInt(player.position.z);

			Vector3 target;

			if (faceLeft)
			{
				target = new Vector3(transform.position.x, player.position.y + offset.y, player.position.z - offset.x);
			}
			else
			{
				target = new Vector3(transform.position.x,player.position.y + offset.y, player.position.z + offset.x);
			}

			var dist = Vector3.Distance(new Vector3(0,0,transform.position.z), new Vector3(0,0,target.z));

			transform.position = Vector3.Lerp(transform.position, target, dist*damping * Time.deltaTime);
			
		}
	}
}
