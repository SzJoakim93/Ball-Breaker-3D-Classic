using System;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	int life = 1;
	public int Life
	{
		get { return life; }
	}
	bool isBonus;
	public bool IsBonus
	{
		get { return isBonus; }
	}
	bool isMovable;
	public bool IsMovable
	{
		get { return isMovable; }
	}

	float gravity_range;

	float startPoint;
	float endPoint;
	float moveSpeed;

	List<int> childrenBlocks;

	void Start()
	{

	}

	void Update()
	{
		if (transform.position.x < startPoint && moveSpeed < 0
			|| transform.position.x > endPoint && moveSpeed > 0)
			moveSpeed *= -1;
		
		if (isMovable)
		{
			transform.position = new Vector3(
					transform.position.x + moveSpeed * Time.deltaTime,
					transform.position.y,
					transform.position.z);
		}
			

		if (isBonus)
			transform.Rotate(0.0f, -15.0f * Time.deltaTime, 0.0f);

		if (gravity_range > 0.0f)
		{
			transform.Translate(0.0f, -0.2f * Time.deltaTime, 0.0f);
			gravity_range -= 0.1f * Time.deltaTime;
		}
	}

	public void hit()
	{
		if (life != 0 && !isMovable)
		{
			if (life > 0)
				life--;
			else
				life = 0;

			if (life == 0)
			{
				gameObject.SetActive(false);
				foreach(var i in childrenBlocks)
					Global.BlockList[i].addGravityRange(transform.localScale.y/100.0f);
			}
		}
    
	}

	public void initProp(int life)
	{
		childrenBlocks = new List<int>();
		this.isBonus = life == -1;
		this.isMovable = life == -2;
		this.life = (life > 0 ? life : 1);
	}

	public void initMovePositions(float start, float end, float speed)
	{
		this.startPoint = start;
		this.endPoint = end;
		this.moveSpeed = speed;
	}

	public void AddChild(int x)
	{
		this.childrenBlocks.Add(x);
	}

	void addGravityRange(float range)
	{
		gravity_range = range;
	}

}