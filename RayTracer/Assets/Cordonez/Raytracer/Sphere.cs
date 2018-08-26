namespace Cordonez.Raytracer
{
	using UnityEngine;

	public class Sphere : IHitable
	{
		public Vector3 Center;
		public float Radius;
		public MyMaterial Material;

		public Sphere(Vector3 _center, float _radius)
		{
			Center = _center;
			Radius = _radius;
		}

		public Sphere(Vector3 _center, float _radius, MyMaterial _material)
		{
			Center = _center;
			Radius = _radius;
			Material = _material;
		}

		public bool Hit(MyRay _ray, float _tmin, float _tmax, HitInfo _hitInfo)
		{
			Vector3 oc = _ray.Origin - Center;
			float a = Vector3.Dot(_ray.Direction, _ray.Direction);
			float b = 2f * Vector3.Dot(oc, _ray.Direction);
			float c = Vector3.Dot(oc, oc) - Radius * Radius;
			float discriminant = b * b - 4 * a * c;
			if (discriminant > 0)
			{
				float t = (-b - Mathf.Sqrt(discriminant)) / (2 * a);
				if (t > _tmin && t < _tmax)
				{
					_hitInfo.T = t;
					_hitInfo.HitPoint = _ray.PointAtT(t);
					_hitInfo.Normal = (_hitInfo.HitPoint - Center) / Radius;
					_hitInfo.Material = Material;
					return true;
				}

				t = (-b + Mathf.Sqrt(discriminant)) / (2 * a);
				if (t > _tmin && t < _tmax)
				{
					_hitInfo.T = t;
					_hitInfo.HitPoint = _ray.PointAtT(t);
					_hitInfo.Normal = (_hitInfo.HitPoint - Center) / Radius;
					_hitInfo.Material = Material;
					return true;
				}
			}

			return false;
		}
	}
}