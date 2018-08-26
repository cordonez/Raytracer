namespace Cordonez.Raytracer
{
	using System.Collections.Generic;
	using UnityEngine;

	public class _5b_MultipleElements : MonoBehaviour
	{
		public int Width;
		public int Height;
		public Material Material;

		private Texture2D m_texture2D;
		private readonly Vector3 m_bottomColor = Vector3.one;
		private readonly Vector3 m_topColor = new Vector3(0.5f, 0.7f, 1f);

		private readonly List<IHitable> m_worldElements = new List<IHitable>();

		private void Start()
		{
			m_texture2D = new Texture2D(Width, Height);
			Material.mainTexture = m_texture2D;
			m_worldElements.Add(new Sphere(new Vector3(0, 0, -1), 0.5f));
			m_worldElements.Add(new Sphere(new Vector3(0, -100.5f, 1), 100));
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

		private Color GetColor(MyRay _ray)
		{
			HitInfo hitInfo = new HitInfo();
			if (m_worldElements.Hit(_ray, 0, float.MaxValue, hitInfo))
			{
				Vector3 normalAsRGB = 0.5f * (hitInfo.Normal + Vector3.one);
				return new Color(normalAsRGB.x, normalAsRGB.y, normalAsRGB.z);
			}

			return GetBackgroundColor(_ray);
		}
	}
}