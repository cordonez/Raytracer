namespace Cordonez.Raytracer
{
	using UnityEngine;

	public class HitInfo
	{
		public float T;
		public Vector3 HitPoint;
		public Vector3 Normal;
		public MyMaterial Material;
	}
}