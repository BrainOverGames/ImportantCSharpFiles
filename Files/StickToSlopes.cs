﻿using UnityEngine;

public class StickToSlopes
{
	/// <summary>
	/// If we're going down a slope, sticks the character to the slope to avoid bounces
	/// </summary>
	protected virtual void StickToSlope()
	{
		if ((_newPosition.y >= StickToSlopesOffsetY)
			|| (_newPosition.y <= -StickToSlopesOffsetY)
			|| (State.IsJumping)
			|| (!StickToSlopes)
			|| (!State.WasGroundedLastFrame)
			|| (_externalForce.y > 0)
			|| (_movingPlatform != null))
		{
			return;
		}

		float rayLength = 0;
		if (StickyRaycastLength == 0)
		{
			rayLength = _boundsWidth * Mathf.Abs(Mathf.Tan(Parameters.MaximumSlopeAngle));
			rayLength += _boundsHeight / 2 + RayOffset;
		}
		else
		{
			rayLength = StickyRaycastLength;
		}

		_stickRayCastOrigin.x = (_newPosition.x > 0) ? _boundsBottomLeftCorner.x : _boundsTopRightCorner.x;
		_stickRayCastOrigin.x += _newPosition.x;

		_stickRayCastOrigin.y = _boundsCenter.y + RayOffset;

		_stickRaycast = MMDebug.RayCast (_stickRayCastOrigin, -transform.up, rayLength, PlatformMask,Colors.LightBlue, Parameters.DrawRaycastsGizmos);	

		if (_stickRaycast)
		{
			if (_stickRaycast.collider == _ignoredCollider)
			{
				return;
			}

			_newPosition.y = -Mathf.Abs(_stickRaycast.point.y - _stickRayCastOrigin.y) 
				+ _boundsHeight / 2 
				+ RayOffset;

			State.IsCollidingBelow=true;
		}	
	}
}