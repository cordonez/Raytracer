namespace Cordonez.Raytracer
{
	using UnityEngine;

	public class _5a_SurfaceNormals : MonoBehaviour
	{
		public int Width;
		public int Height;
		public Material Material;

		private Texture2D m_texture2D;
		private readonly Vector3 m_bottomColor = Vector3.one;
		private readonly Vector3 m_topColor = new Vector3(0.5f, 0.7f, 1f);

		private void Start()
		{
			m_texture2D = new Texture2D(Width, Height);
			Material.mainTexture = m_texture2D;
			Draw();
		}

		private void Draw()
		{
			Vector3 lowerLeftCorner = new Vector3(-2f, -1f, -1f);
			Vector3 horizontal = new Vector3(4f, 0f, 0f);
			Vector3 vertical = new Vector3(0f, 2f, 0f);
			Vector3 origin = Vector3.zero;
			for (int i = 0; i < Width; i++)
			{
				for (int j = 0; j < Height; j++)
				{
					float u = (float) i / Width;
					float v = (float) j / Height;
					MyRay ray = new MyRay(origin, lowerLeftCorner + u * horizontal + v * vertical);
					Color color = GetColor(ray);
					m_texture2D.SetPixel(i, j, color);
				}
			}

			m_texture2D.Apply();
		}

		private Color GetBackgroundColor(MyRay _ray)
		{
			Vector3 unitDirection = _ray.Direction;
			float t = 0.5f * unitDirection.y + 1f;
			Vector3 c = (1f - t) * m_bottomColor + t * m_topColor;
			return new Color(c.x, c.y, c.z);
		}

		private float HitSphere(Vector3 _center, float _radius, MyRay _ray)
		{
			Vector3 oc = _ray.Origin - _center;
			float a = Vector3.Dot(_ray.Direction, _ray.Direction);
			float b = 2f * Vector3.Dot(oc, _ray.Direction);
			float c = Vector3.Dot(oc, oc) - _radius * _radius;
			float discriminant = b * b - 4 * a * c;
			return discriminant < 0 ? -1f : (-b - Mathf.Sqrt(discriminant)) / (2 * a);
		}

		private Color GetColor(MyRay _ray)
		{
			float t = HitSphere(new Vector3(0, 0, -1), 0.5f, _ray);
			if (t > 0)
			{
				Vector3 normal = _ray.PointAtT(t) - new Vector3(0, 0, -1);
				Vector3 normalAsRGB = 0.5f * (normal + Vector3.one);
				return new Color(normalAsRGB.x, normalAsRGB.y, normalAsRGB.z);
			}

			return GetBackgroundColor(_ray);
		}
	}
}