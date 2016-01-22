﻿using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	MazeGenerator gen;
	Maze m;

	int pX;
	int pY;

	void Start () 
	{
		gen = FindObjectOfType<MazeGenerator>();
	}

	public Vector2 pos()
	{
		return new Vector2(pX, pY);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m == null)
		{
			m = gen.currentMaze();
			RandomPlacement();
		}
		DoMovement();
	}

	public void RandomPlacement()
	{
		int x = 0;
		int y =0;
		while(m.GetCell(x,y).isWall)
		{
			x = Random.Range(1, (int)m.Dimensions().x);
			y = Random.Range(1, (int)m.Dimensions().y);
		}
		pX = x;
		pY = y;
		transform.position = new Vector3(x*1.5f, 0, y*1.5f);
		transform.localEulerAngles = new Vector3(0,Random.Range(0,4)*90, 0);
	}

	void DoMovement()
	{
		Vector2 fDir = new Vector2(0,1);
		float ang = transform.localEulerAngles.y;
		if(ang < 100 && ang > 80)
			fDir = new Vector2(1,0);
		else if(ang < 190 && ang > 170)
			fDir = new Vector2(0, -1);
		else if(ang < 280 && ang > 260)
			fDir = new Vector2(-1,0);

		if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			int nX = pX + (int)fDir.x;
			int nY = pY + (int)fDir.y;
			if(!m.GetCell(nX, nY).isWall)
			{
				pX = nX;
				pY = nY;
				transform.position = new Vector3(nX*1.5f, 0, nY*1.5f);
			}	
		}
		else if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			int nX = pX - (int)fDir.x;
			int nY = pY - (int)fDir.y;
			if(!m.GetCell(nX, nY).isWall)
			{
				pX = nX;
				pY = nY;
				transform.position = new Vector3(nX*1.5f, 0, nY*1.5f);
			}
		}
		else if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			transform.localEulerAngles += new Vector3(0,-90,0);
		}
		else if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			transform.localEulerAngles += new Vector3(0, 90, 0);
		}
	}
}
