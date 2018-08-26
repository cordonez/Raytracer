namespace Cordonez.Raytracer
{
	using UnityEngine;

	public class Dielectric : MyMaterial
	{
		private readonly float m_refractiveIndex;

		public Dielectric(float _refractiveIndex)
		{
			m_refractiveIndex = _refractiveIndex;
		}

		public override bool Scatter(MyRay _ray, HitInfo _hitInfo, out Vector3 _attenuation, out MyRay _scattered)
		{
			Vector3 outwardNormal;
			Vector3 reflected = Reflect(_ray.Direction.normalized, _hitInfo.Normal);
			float ni_over_nt;
			_attenuation = new Vector3(1, 1, 1);
			Vector3 refracted = Vector3.zero;
			float reflectProb;
			float cosine;
			if (Vector3.Dot(_ray.Direction, _hitInfo.Normal) > 0)
			{
				outwardNormal = -_hitInfo.Normal;
				ni_over_nt = m_refractiveIndex;
				cosine = m_refractiveIndex * Vector3.Dot(_ray.Direction, _hitInfo.Normal) / _ray.Direction.magnitude;
			}
			else
			{
				outwardNormal = _hitInfo.Normal;
				ni_over_nt = 1 / m_refractiveIndex;
				cosine = -Vector3.Dot(_ray.Direction, _hitInfo.Normal) / _ray.Direction.magnitude;
			}

			if (Refract(_ray.Direction, outwardNormal, ni_over_nt, ref refracted))
			{
				reflectProb = Schlick(cosine, m_refractiveIndex);
			}
			else
			{
				_scattered = new MyRay(_hitInfo.HitPoint, reflected);
				return true;
			}

			if (Random.Range(0f, 1f) < reflectProb)
			{
				_scattered = new MyRay(_hitInfo.HitPoint, reflected);
			}
			else
			{
				_scattered = new MyRay(_hitInfo.HitPoint, refracted);
			}

			return true;
		}

		private float Schlick(float _cosine, float _refractiveIndex)
		{
			float r0 = (1 - m_refractiveIndex) / (1 + m_refractiveIndex);
			r0 = r0 * r0;
			return r0 + (1 - r0) * Mathf.Pow(1 - _cosine, 5);
		}

		private bool Refract(Vector3 _v, Vector3 _normal, float _ni_over_nt, ref Vector3 _refracted)
		{
			Vector3 uv = _v.normalized;
			float dt = Vector3.Dot(uv, _normal);
			float discriminant = 1 - _ni_over_nt * _ni_over_nt * (1 - dt * dt);
			if (discriminant > 0)
			{
				_refracted = _ni_over_nt * (uv - _normal * dt) - _normal * Mathf.Sqrt(discriminant);
				return true;
			}

			return false;
		}

		private Vector3 Reflect(Vector3 _v, Vector3 _n)
		{
			return _v - 2 * Vector3.Dot(_v, _n) * _n;
		}
	}
}