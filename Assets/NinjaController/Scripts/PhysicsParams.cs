using UnityEngine;
using System.Collections;

namespace NinjaController {

  [System.Serializable]
  public class PhysicsParams {

    public float playerMass = 12.5f;
    public float gameGravity = -19.5f;
    public float jumpUpForce = 0.25f;
    public float jumpUpVel = 18.75f;
    [Tooltip("Additional gravity for jumping in case we're not holding down the jump button. Supposed to act as a penalty.")]
    public float jumpGravity = -22.5f;
    public float jumpWallVelVertical = 12;
    public float jumpWallVelHorizontal = 7.5f;
    [Tooltip("Maximum velocity sideways.")]
    public float inAirMaxVelHorizontal = 27.5f;
    [Tooltip("The force that acts when the players moves sideways in air.")]
    public float inAirMoveHorizontalForce = 12.5f;
    [Tooltip("The force that acts when the player is moving in one direction, but steering into the other.")]
    public float inAirMoveHorizontalForceReverse = 28.75f;
    public float onGroundMaxVelHorizontal = 25;
    public float onGroundMoveHorizontalForce = 20;
    public float onGroundMoveHorizontalForceReverse = 45;
    public float groundFriction = 35;
    [Tooltip("When velocity is lower than this value the player stops completely.")]
    public float groundFrictionEpsilon = 0.75f;
    public float wallFriction = 11.25f;
    public float wallFrictionStrongVelThreshold = -10;
    [Tooltip("Gets applied when moving down along the wall too fast.")]
    public float wallFrictionStrong = 22.5f;

    #region serialization version
    [HideInInspector]
    public int version = 1;
    #endregion
  }
}
