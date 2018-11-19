using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calc
{
	//   ██████╗ ██████╗ ███╗   ██╗███████╗████████╗ █████╗ ███╗   ██╗████████╗███████╗
	//  ██╔════╝██╔═══██╗████╗  ██║██╔════╝╚══██╔══╝██╔══██╗████╗  ██║╚══██╔══╝██╔════╝██╗
	//  ██║     ██║   ██║██╔██╗ ██║███████╗   ██║   ███████║██╔██╗ ██║   ██║   ███████╗╚═╝
	//  ██║     ██║   ██║██║╚██╗██║╚════██║   ██║   ██╔══██║██║╚██╗██║   ██║   ╚════██║██╗
	//  ╚██████╗╚██████╔╝██║ ╚████║███████║   ██║   ██║  ██║██║ ╚████║   ██║   ███████║╚═╝
	//   ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═══╝   ╚═╝   ╚══════╝

	// Returns Gravitational Constant. Newtonian Exponent is e-11.
	public static float G()
	{
		return (6.673889f * Mathf.Exp(-11));
	}

	// Returns a Float or Double precision approximation of the value of Phi.
	public static float Phi ()
	{
		return (1.618034f);
	}
		
	public static double DoublePhi ()
	{
		return(1.6180339887499d);
	}

	// Returns a Float or Double precision approximation of the value of Pi.
	public static float Pi ()
	{
		return(3.141593f);
	}

	public static double DoublePi ()
	{
		return(3.14159265358979d);
	}

	// Returns a Float that is in the Golden Ratio (A or B) with an input value (B or A).
	public static float GoldenRatioA(float b = 1f)
	{
		return (b * Phi());
	}

	public static float GoldenRatioB(float a = 1f)
	{
		return (a * (Phi() - 1));
	}

	// Returns a Float that is a subsection (A or B) in the Golden Ratio with the sum (A+B).
	public static float GoldenSectionA(float ab = 1f)
	{
		return (ab * (Phi() - 1));
	}

	public static float GoldenSectionB(float ab = 1f)
	{
		return (ab - (ab * (Phi() - 1)));
	}

	//   ██████╗ ███████╗ ██████╗ ███╗   ███╗███████╗████████╗██████╗ ██╗   ██╗
	//  ██╔════╝ ██╔════╝██╔═══██╗████╗ ████║██╔════╝╚══██╔══╝██╔══██╗╚██╗ ██╔╝██╗
	//  ██║  ███╗█████╗  ██║   ██║██╔████╔██║█████╗     ██║   ██████╔╝ ╚████╔╝ ╚═╝
	//  ██║   ██║██╔══╝  ██║   ██║██║╚██╔╝██║██╔══╝     ██║   ██╔══██╗  ╚██╔╝  ██╗
	//  ╚██████╔╝███████╗╚██████╔╝██║ ╚═╝ ██║███████╗   ██║   ██║  ██║   ██║   ╚═╝
	//   ╚═════╝ ╚══════╝ ╚═════╝ ╚═╝     ╚═╝╚══════╝   ╚═╝   ╚═╝  ╚═╝   ╚═╝

	public static Vector3 SurfaceNormal (Vector3[] triangle)
	{
		return (Vector3.Cross(triangle[1] - triangle[0], triangle[2] - triangle[0]).normalized);
	}

	public static Vector3 SurfaceNormal (Vector3 vertA, Vector3 vertB, Vector3 vertC)
	{
		Vector3[] triangle = new [] { vertA, vertB, vertC };
		
		return (SurfaceNormal(triangle));
	}

	public static Vector3 NearestVertex (Vector3[] vertices)
	{
		return (NearestVertex (vertices, Vector3.zero));
	}

	public static Vector3 NearestVertex (Vector3[] vertices, Vector3 point)
	{
		float shortestDist = Mathf.Infinity;
		Vector3 nearestVert = Vector3.zero;

		// scan all vertices to find nearest
		foreach (Vector3 vertex in vertices)
		{
			Vector3 delta = point - vertex;
			float distance = delta.sqrMagnitude;
			if (distance < shortestDist)
			{
				shortestDist = distance;
				nearestVert = vertex;
			}
		}

		return (nearestVert);
	}

	public static int NearestVertID (Vector3[] vertices)
	{
		return (NearestVertID (vertices, Vector3.zero));
	}

	public static int NearestVertID (Vector3[] vertices, Vector3 point)
	{
		float shortestDist = Mathf.Infinity;
		int nearestVertID = 0;

		// scan all vertices to find nearest
		for (int i = 0; i < vertices.Length; i++)
		{
			Vector3 delta = point - vertices[i];
			float distance = delta.sqrMagnitude;
			if (distance < shortestDist)
			{
				shortestDist = distance;
				nearestVertID = i;
			}
		}

		return (nearestVertID);
	}

		// Returns the average Vector3 or Vector2 of a set of Vectors.
	public static Vector3 Midpoint (List<Vector3> points)
	{
		return (Midpoint (points.ToArray ()));
	}
	public static Vector3 Midpoint (Vector3 pointA, Vector3 pointB)
	{
		return (Midpoint (new Vector3[2] {pointA, pointB}));
	}
	public static Vector3 Midpoint (Vector3[] points)
	{
		Vector3 midpoint = Vector3.zero;
		int pointCount = points.Length;

		foreach (Vector3 point in points)
		{
			midpoint += point;
		}

		midpoint /= pointCount;
		return (midpoint);
	}
	public static Vector2 Midpoint (List<Vector2> points)
	{
		return (Midpoint (points.ToArray ()));
	}
	public static Vector2 Midpoint (Vector2 pointA, Vector2 pointB)
	{
		return (Midpoint (new Vector2[2] {pointA, pointB}));
	}
	public static Vector2 Midpoint (Vector2[] points)
	{
		Vector2 midpoint = Vector2.zero;
		int pointCount = points.Length;

		foreach (Vector2 point in points)
		{
			midpoint += point;
		}

		midpoint /= pointCount;
		return (midpoint);
	}

	// Returns float representing the angle of a slope between two points.
	public static float SlopeAngle(Vector2 pointA, Vector2 pointB, Vector2 up)
	{
		return (90 - Vector2.Angle(pointB-pointA, up));
	}
	public static float SlopeAngle(Vector3 pointA, Vector3 pointB, Vector3 up)
	{
		return (90 - Vector3.Angle(pointB-pointA, up));
	}

	// Returns float equal to shortest distance from testPoint to an infinite line crossing pointA and pointB.
	public static float DistanceToLine(List<Vector2> points, Vector2 testPoint)
	{
		return (DistanceToLine(points[0], points[1], testPoint));
	}
	public static float DistanceToLine(Vector2[] points, Vector2 testPoint)
	{
		return (DistanceToLine(points[0], points[1], testPoint));
	}
	public static float DistanceToLine(Vector2 pointA, Vector2 pointB, Vector2 testPoint)
	{
		return ((pointB.x - pointA.x) * (testPoint.y - pointA.y) - (pointB.y - pointA.y) * (testPoint.x - pointA.x));
	}

	// Returns float equal to shortest distance from testPoint to an infinite plane crossing pointA, pointB, and pointC.
	public static float DistanceToPlane (List<Vector3> points, Vector3 testPoint)
	{
		return (DistanceToPlane (points[0], points[1], points[2], testPoint));
	}
	public static float DistanceToPlane (Vector3[] points, Vector3 testPoint)
	{
		return (DistanceToPlane (points[0], points[1], points[2], testPoint));
	}
	public static float DistanceToPlane (Vector3 pointA, Vector3 pointB, Vector3 pointC, Vector3 testPoint)
	{
		Plane tri = new Plane (pointA, pointB, pointC);
		float distance = tri.GetDistanceToPoint(testPoint);
		return distance;
	}

	// Returns a float representing the latitude of a position relative to origin.
	//      lat=atan2(z,sqrt(x*x+y*y))
	//      float latitude = (float)Mathf.Acos(position.y / sphereRadius);
	//      Mathf.Atan2(position.y, Mathf.Sqrt(position.x*position.x + position.z*position.z));
	public static float GetLatitude (Vector3 position, float sphereRadius)
	{
		float latitude = Mathf.Rad2Deg * Mathf.Atan2(position.y, Mathf.Sqrt(position.x*position.x + position.z*position.z));
		return latitude;
	}

	// Returns a float representing the latitude of a position relative to origin.
	//      float longitude = (float)Mathf.Atan(position.x / position.z);
	//      lng=atan2(y,x)
	public static float GetLongitude (Vector3 position, float sphereRadius)
	{
		return GetLongitude (position);
	}
	public static float GetLongitude (Vector3 position)
	{
		float longitude = Mathf.Rad2Deg * Mathf.Atan2(position.x, -position.z);
		return longitude;
	}

	// ███╗   ███╗ █████╗ ████████╗██╗  ██╗   
	// ████╗ ████║██╔══██╗╚══██╔══╝██║  ██║██╗
	// ██╔████╔██║███████║   ██║   ███████║╚═╝
	// ██║╚██╔╝██║██╔══██║   ██║   ██╔══██║██╗
	// ██║ ╚═╝ ██║██║  ██║   ██║   ██║  ██║╚═╝
	// ╚═╝     ╚═╝╚═╝  ╚═╝   ╚═╝   ╚═╝  ╚═╝ 

	public static float RoundToMultiple (float value, float multiple)
	{
		float result = value + multiple/2;
		result -= result % multiple;

		Debug.Log("Rounding Value: " + value + " to Multiple: " + multiple + " (Result: " + result + ")");
		return (result);
		//return (((value + multiple/2) / multiple) * multiple);
	}

	public static bool isEven (int value)
	{
		return (value % 2 == 0);
	}

	public static bool isOdd (int value)
	{
		return !isEven(value);
	}

	public static bool NearEqual (float valueA, float valueB, int decimalPrecision = 4)
	{
		if (decimalPrecision == 0)
		{
			return (valueA == valueB);
		}

		// Negative decimalPrecision rounds to nearest 10(-1), 100(-2), 1000(-3), etc.
		if (decimalPrecision < 0)
		{
			decimalPrecision = Mathf.Abs(decimalPrecision);
			valueA = Mathf.Round(valueA / (Mathf.Pow(10, decimalPrecision)));
			valueB = Mathf.Round(valueB / (Mathf.Pow(10, decimalPrecision)));

			decimalPrecision = 0;
		}

		return (System.Math.Round (valueA, decimalPrecision) == System.Math.Round (valueB, decimalPrecision));
	}

	public static float Wrap (float value, float min, float max)
	{
		value -= min;
		max -= min;

		return (((max + value % max) % max) + min);
	}

	public static float Wrap (float value, float max)
	{
		float min = 0;
		return (Wrap(value, min, max));
	}

	public static int Wrap (int value, int min, int max)
	{
		value -= min;
		max -= min;

		return (((max + value % max) % max) + min);
	}

	public static int Wrap (int value, int max)
	{
		int min = 0;
		return (Wrap(value, min, max));
	}

	// Returns highest value within an array
	public static float Highest (float[] values)
	{
		float highest = Mathf.NegativeInfinity;

		for (int i = 0; i < values.Length; i++)
		{
			if (values[i] > highest)
			{
				highest = values[i];
			}
		}

		return (highest);
	}

	public static int Highest (int[] values)
	{
		int highest = int.MinValue;

		for (int i = 0; i < values.Length; i++)
		{
			if (values[i] > highest)
			{
				highest = values[i];
			}
		}

		return (highest);
	}

	// Returns lowest value within an array
	public static float Lowest (float[] values)
	{
		float lowest = Mathf.Infinity;

		for (int i = 0; i < values.Length; i++)
		{
			if (values[i] < lowest)
			{
				lowest = values[i];
			}
		}

		return (lowest);
	}

	public static int Lowest (int[] values)
	{
		int lowest = int.MaxValue;

		for (int i = 0; i < values.Length; i++)
		{
			if (values[i] < lowest)
			{
				lowest = values[i];
			}
		}

		return (lowest);
	}

	public static float Average (float[] values)
	{
		float average = 0;

		for (int i = 0; i < values.Length; i++)
		{
			average += values[i];
		}

		average /= values.Length;

		return (average);
	}

	public static float Average (float valueA, float valueB)
	{
		float[] values = new [] {valueA, valueB};

		return (Average(values));
	}

	public static float Average (float valueA, float valueB, float valueC)
	{
		float[] values = new [] {valueA, valueB, valueC};

		return (Average(values));
	}

	public static float Average (float valueA, float valueB, float valueC, float valueD)
	{
		float[] values = new [] {valueA, valueB, valueC, valueD};

		return (Average(values));
	}

	public static float Average (int[] values)
	{
		float[] floatValues = new float[values.Length];

		for (int i = 0; i < values.Length; i++)
		{
			floatValues[i] = (float) values[i];
		}

		return (Average(floatValues));
	}

	// Returns an array of IDs of all highest values within an array
	public static int[] HighestIDs (float[] values)
	{
		float highest = Mathf.NegativeInfinity;
		List<int> highestIDs = new List<int>();

		for (int i = 0; i < values.Length; i++)
		{
			if (values[i] > highest)
			{
				highestIDs.Clear();
				highestIDs.Add(i);
			}
			else if (values[i] == highest)
			{
				highestIDs.Add(i);
			}
		}

		return (highestIDs.ToArray());
	}

	public static int[] HighestIDs (int[] values)
	{
		int highest = int.MinValue;
		List<int> highestIDs = new List<int>();

		for (int i = 0; i < values.Length; i++)
		{
			if (values[i] > highest)
			{
				highestIDs.Clear();
				highestIDs.Add(i);
			}
			else if (values[i] == highest)
			{
				highestIDs.Add(i);
			}
		}

		return (highestIDs.ToArray());
	}

	// Returns an array of IDs of all lowest values within an array
	public static int[] LowestIDs (float[] values)
	{
		float lowest = Mathf.Infinity;
		List<int> lowestIDs = new List<int>();

		for (int i = 0; i < values.Length; i++)
		{
			if (values[i] < lowest)
			{
				lowestIDs.Clear();
				lowestIDs.Add(i);
			}
			else if (values[i] == lowest)
			{
				lowestIDs.Add(i);
			}
		}

		return (lowestIDs.ToArray());
	}

	public static int[] LowestIDs (int[] values)
	{
		int lowest = int.MaxValue;
		List<int> lowestIDs = new List<int>();

		for (int i = 0; i < values.Length; i++)
		{
			if (values[i] < lowest)
			{
				lowestIDs.Clear();
				lowestIDs.Add(i);
			}
			else if (values[i] == lowest)
			{
				lowestIDs.Add(i);
			}
		}

		return (lowestIDs.ToArray());
	}

	//  ██████╗ ██╗  ██╗██╗   ██╗███████╗██╗ ██████╗███████╗
	//  ██╔══██╗██║  ██║╚██╗ ██╔╝██╔════╝██║██╔════╝██╔════╝██╗
	//  ██████╔╝███████║ ╚████╔╝ ███████╗██║██║     ███████╗╚═╝
	//  ██╔═══╝ ██╔══██║  ╚██╔╝  ╚════██║██║██║     ╚════██║██╗
	//  ██║     ██║  ██║   ██║   ███████║██║╚██████╗███████║╚═╝
	//  ╚═╝     ╚═╝  ╚═╝   ╚═╝   ╚══════╝╚═╝ ╚═════╝╚══════╝

	// Returns a velocity Float that is the magnitude of the Gravity Force Vector exerted on a given Mass. (Force = GravitationalConstant * (Mass1 * Mass2)/Distance Squared.)
	public static float Magnet (float power, float distance)
	{
		return (power / (distance * distance));
	}

	// ██████╗  █████╗ ███╗   ██╗██████╗  ██████╗ ███╗   ███╗   
	// ██╔══██╗██╔══██╗████╗  ██║██╔══██╗██╔═══██╗████╗ ████║██╗
	// ██████╔╝███████║██╔██╗ ██║██║  ██║██║   ██║██╔████╔██║╚═╝
	// ██╔══██╗██╔══██║██║╚██╗██║██║  ██║██║   ██║██║╚██╔╝██║██╗
	// ██║  ██║██║  ██║██║ ╚████║██████╔╝╚██████╔╝██║ ╚═╝ ██║╚═╝
	// ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝╚═════╝  ╚═════╝ ╚═╝     ╚═╝  

	public static float PerlinNoise (float xPos, float yPos, float amplitude = 10.0f, float frequency = 100.0f)
	{
		xPos = xPos * frequency + 0.0001f;
		yPos = yPos * frequency + 0.0001f;
		
		float result = Mathf.PerlinNoise (xPos, yPos) * amplitude;

		return(result);
	}

	// Returns an integer, representing the result of rolling a number of dice.
	public static int DiceRoll(int rolls = 1, int sides = 6, int rollBonus = 0, int totalBonus = 0)
	{
		int total = 0;
		for (int r = 0; r < rolls; r++)
		{
			total += (Random.Range (0, sides) + 1 + rollBonus);
		}
		return (total + totalBonus);
	}

	// Returns true if a random value up to outOf (exclusive) is within odds.
	public static bool Probability(int odds = 1, int outOf = 2)
	{
		return (Random.Range(0, outOf) < odds);
	}

	// TODO: Jitter vector by angle. Optional X/Y/Z dampening multiplier/locks/constraints as float
	public static Vector3 VectorScatter(Vector3 vector, float angle)
	{
		return (vector);
	}	

	//  ██╗   ██╗████████╗██╗██╗     ██╗████████╗██╗   ██╗   
	//  ██║   ██║╚══██╔══╝██║██║     ██║╚══██╔══╝╚██╗ ██╔╝██╗
	//  ██║   ██║   ██║   ██║██║     ██║   ██║    ╚████╔╝ ╚═╝
	//  ██║   ██║   ██║   ██║██║     ██║   ██║     ╚██╔╝  ██╗
	//  ╚██████╔╝   ██║   ██║███████╗██║   ██║      ██║   ╚═╝
	//   ╚═════╝    ╚═╝   ╚═╝╚══════╝╚═╝   ╚═╝      ╚═╝

	// Returns Ray drawn between Camera and Mouse Position.
	public static Ray MouseRay ()
	{
		return (Camera.main.ScreenPointToRay(Input.mousePosition));
	}

	// Returns Ray drawn between Camera and a Screen Position (Width and Height in pixels, or Vector 3)
	public static Ray ScreenRay (Vector3 position)
	{
		return (Camera.main.ScreenPointToRay(position));
	}

	public static Ray ScreenRay (Vector2 position)
	{
		return (ScreenRay(new Vector3(position.x, position.y, 0f)));
	}

	public static Ray ScreenRay (float width, float height)
	{
		return (ScreenRay(new Vector3(width, height, 0f)));
	}

	// Returns Ray drawn between Camera and a Screen Position (Width and Height in float percent of screen space, 0.0 to 1.0)
	public static Ray ScreenRayRelative (float x, float y)
	{
		return (ScreenRay(new Vector3(Screen.width*x, Screen.height*y, 0f)));
	}

	// Returns Float or Double Precision magnitude of the Gravity Force Vector exerted on a given Mass. (Force = GravitationalConstant * (Mass1 * Mass2)/Distance Squared.)
	public static Color ColorCycle (int colorID)
	{
		colorID = Wrap(colorID, 8);
		Color colorValue = Color.grey;

		switch (colorID) 
		{
		case 0:
			colorValue = Color.cyan;
			break;
		case 1:
			colorValue = Color.magenta;
			break;
		case 2:
			colorValue = Color.yellow;
			break;
		case 3:
			colorValue = Color.white;
			break;
		case 4:
			colorValue = Color.red;
			break;
		case 5:
			colorValue = Color.green;
			break;
		case 6:
			colorValue = Color.blue;
			break;
		case 7:
			colorValue = Color.black;
			break;
		}

		return (colorValue);
	}

}