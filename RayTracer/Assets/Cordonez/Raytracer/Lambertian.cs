namespace Cordonez.Raytracer
{
	using UnityEngine;

	public class Lambertian : MyMaterial
	{
		public Vector3 Albedo;

		public Lambertian(Vector3 _albedo)
		{
			Albedo = _albedo;
		}

		public override bool Scatter(MyRay _ray, HitInfo _hitInfo, out Vector3 _attenuation, out MyRay _scattered)
		{
			Vector3 target = _hitInfo.HitPoint + _hitInfo.Normal + _8_Metal.RandomPointInUnitSphere();
			_scattered = new MyRay(_hitInfo.HitPoint, target - _hitInfo.HitPoint);
			_attenuation = Albedo;
			return true;
		}
	}
}