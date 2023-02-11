using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/PlayerData")]
public class PlayerData : ScriptableObject {
	public float Speed = 5;
	public float HitForce = 2;
	public float Gravity = Physics.gravity.y;
	public float TurnRate = 10;
	public float JumpHeight = 2;
}