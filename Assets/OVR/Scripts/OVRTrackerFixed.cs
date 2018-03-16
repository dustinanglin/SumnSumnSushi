/************************************************************************************

Copyright   :   Copyright 2014 Oculus VR, LLC. All Rights reserved.

Licensed under the Oculus VR Rift SDK License Version 3.3 (the "License");
you may not use the Oculus VR Rift SDK except in compliance with the License,
which is provided at the time of installation or download, or which
otherwise accompanies this software in either electronic or hard copy form.

You may obtain a copy of the License at

http://www.oculus.com/licenses/LICENSE-3.3

Unless required by applicable law or agreed to in writing, the Oculus VR SDK
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

************************************************************************************/

using System;
using System.Runtime.InteropServices;
using UnityEngine;
using VR = UnityEngine.VR;

/// <summary>
/// An infrared camera that tracks the position of a head-mounted display.
/// </summary>
public class OVRTrackerFixed
{
	/// <summary>
	/// The (symmetric) visible area in front of the sensor.
	/// </summary>
	public struct Frustum
	{
		/// <summary>
		/// The sensor's minimum supported distance to the HMD.
		/// </summary>
		public float nearZ;
		/// <summary>
		/// The sensor's maximum supported distance to the HMD.
		/// </summary>
		public float farZ;
		/// <summary>
		/// The sensor's horizontal and vertical fields of view in degrees.
		/// </summary>
		public Vector2 fov;
	}

	/// <summary>
	/// If true, a sensor is attached to the system.
	/// </summary>
	public bool isPresent
	{
		get {
			if (!OVRManagerFixed.isHmdPresent)
				return false;

			return OVRPluginFixed.positionSupported;
		}
	}

	/// <summary>
	/// If true, the sensor is actively tracking the HMD's position. Otherwise the HMD may be temporarily occluded, the system may not support position tracking, etc.
	/// </summary>
	public bool isPositionTracked
	{
		get {
			return OVRPluginFixed.positionTracked;
		}
	}

	/// <summary>
	/// If this is true and a sensor is available, the system will use position tracking when isPositionTracked is also true.
	/// </summary>
	public bool isEnabled
	{
		get {
			if (!OVRManagerFixed.isHmdPresent)
				return false;

			return OVRPluginFixed.position;
        }

		set {
			if (!OVRManagerFixed.isHmdPresent)
				return;

			OVRPluginFixed.position = value;
		}
	}

	/// <summary>
	/// Returns the number of sensors currently connected to the system.
	/// </summary>
	public int count
	{
		get {
			int count = 0;

			for (int i = 0; i < (int)OVRPluginFixed.Tracker.Count; ++i)
			{
				if (GetPresent(i))
					count++;
			}

			return count;
		}
	}

	/// <summary>
	/// Gets the sensor's viewing frustum.
	/// </summary>
	public Frustum GetFrustum(int tracker = 0)
	{
		if (!OVRManagerFixed.isHmdPresent)
			return new Frustum();


		return OVRPluginFixed.GetTrackerFrustum((OVRPluginFixed.Tracker)tracker).ToFrustum();
	}

	/// <summary>
	/// Gets the sensor's pose, relative to the head's pose at the time of the last pose recentering.
	/// </summary>
	public OVRPose GetPose(int tracker = 0)
	{
		if (!OVRManagerFixed.isHmdPresent)
			return OVRPose.identity;

		OVRPose p;
		switch (tracker)
		{
			case 0:
				p = OVRPluginFixed.GetNodePose(OVRPluginFixed.Node.TrackerZero, OVRPluginFixed.Step.Render).ToOVRPose();
				break;
			case 1:
				p = OVRPluginFixed.GetNodePose(OVRPluginFixed.Node.TrackerOne, OVRPluginFixed.Step.Render).ToOVRPose();
				break;
			case 2:
				p = OVRPluginFixed.GetNodePose(OVRPluginFixed.Node.TrackerTwo, OVRPluginFixed.Step.Render).ToOVRPose();
				break;
			case 3:
				p = OVRPluginFixed.GetNodePose(OVRPluginFixed.Node.TrackerThree, OVRPluginFixed.Step.Render).ToOVRPose();
				break;
			default:
				return OVRPose.identity;
		}
		
		return new OVRPose()
		{
			position = p.position,
			orientation = p.orientation * Quaternion.Euler(0, 180, 0)
		};
	}

	/// <summary>
	/// If true, the pose of the sensor is valid and is ready to be queried.
	/// </summary>
	public bool GetPoseValid(int tracker = 0)
	{
		if (!OVRManagerFixed.isHmdPresent)
			return false;

		switch (tracker)
		{
			case 0:
				return OVRPluginFixed.GetNodePositionTracked(OVRPluginFixed.Node.TrackerZero);
			case 1:
				return OVRPluginFixed.GetNodePositionTracked(OVRPluginFixed.Node.TrackerOne);
			case 2:
				return OVRPluginFixed.GetNodePositionTracked(OVRPluginFixed.Node.TrackerTwo);
			case 3:
				return OVRPluginFixed.GetNodePositionTracked(OVRPluginFixed.Node.TrackerThree);
			default:
				return false;
		}
	}

	public bool GetPresent(int tracker = 0)
	{
		if (!OVRManagerFixed.isHmdPresent)
			return false;

		switch (tracker)
		{
			case 0:
				return OVRPluginFixed.GetNodePresent(OVRPluginFixed.Node.TrackerZero);
			case 1:
				return OVRPluginFixed.GetNodePresent(OVRPluginFixed.Node.TrackerOne);
			case 2:
				return OVRPluginFixed.GetNodePresent(OVRPluginFixed.Node.TrackerTwo);
			case 3:
				return OVRPluginFixed.GetNodePresent(OVRPluginFixed.Node.TrackerThree);
			default:
				return false;
		}
	}
}
