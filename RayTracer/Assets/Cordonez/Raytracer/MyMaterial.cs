namespace Cordonez.Raytracer
{
	using UnityEngine;

	public abstract class MyMaterial
	{
		public abstract bool Scatter(MyRay _ray, HitInfo _hitInfo, out Vector3 _attenuation, out MyRay _scattered);
	}
}