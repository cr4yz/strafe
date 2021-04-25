using System;
using Sandbox;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strafe
{
 //   public static class TransformExtensions
	//{
	//	public static Vector3 Normalize( this Vector3 value )
	//	{
	//		float ls = value.x * value.x + value.y * value.y + value.z * value.z;
	//		float length = (float)System.Math.Sqrt( ls );
	//		return new Vector3( value.x / length, value.y / length, value.z / length );
	//	}

	//	public static Quaternion LookAt( Vector3 position, Vector3 point )
	//	{
	//		Vector3 PlanarUp = Vector3.Cross( (Vector3.Zero - position).Normalize(), (Vector3.Zero - position).Normalize() );
	//		return LookRotation( (Vector3.Zero - position).Normalize(), PlanarUp );
	//	}

	//	public static Quaternion LookRotation( Vector3 forward, Vector3 up )
	//	{
	//		forward.Normalize();

	//		Vector3 vector = forward.Normalize();
	//		Vector3 vector2 = Vector3.Cross( up, vector ).Normalize();
	//		Vector3 vector3 = Vector3.Cross( vector, vector2 );
	//		var m00 = vector2.x;
	//		var m01 = vector2.y;
	//		var m02 = vector2.z;
	//		var m10 = vector3.x;
	//		var m11 = vector3.y;
	//		var m12 = vector3.z;
	//		var m20 = vector.x;
	//		var m21 = vector.y;
	//		var m22 = vector.z;


	//		float num8 = (m00 + m11) + m22;
	//		var quaternion = new Quaternion();
	//		if ( num8 > 0f )
	//		{
	//			var num = (float)Math.Sqrt( num8 + 1f );
	//			quaternion.W = num * 0.5f;
	//			num = 0.5f / num;
	//			quaternion.X = (m12 - m21) * num;
	//			quaternion.Y = (m20 - m02) * num;
	//			quaternion.Z = (m01 - m10) * num;
	//			return quaternion;
	//		}
	//		if ( (m00 >= m11) && (m00 >= m22) )
	//		{
	//			var num7 = (float)Math.Sqrt( ((1f + m00) - m11) - m22 );
	//			var num4 = 0.5f / num7;
	//			quaternion.X = 0.5f * num7;
	//			quaternion.Y = (m01 + m10) * num4;
	//			quaternion.Z = (m02 + m20) * num4;
	//			quaternion.W = (m12 - m21) * num4;
	//			return quaternion;
	//		}
	//		if ( m11 > m22 )
	//		{
	//			var num6 = (float)Math.Sqrt( ((1f + m11) - m00) - m22 );
	//			var num3 = 0.5f / num6;
	//			quaternion.X = (m10 + m01) * num3;
	//			quaternion.Y = 0.5f * num6;
	//			quaternion.Z = (m21 + m12) * num3;
	//			quaternion.W = (m20 - m02) * num3;
	//			return quaternion;
	//		}
	//		var num5 = (float)Math.Sqrt( ((1f + m22) - m00) - m11 );
	//		var num2 = 0.5f / num5;
	//		quaternion.X = (m20 + m02) * num2;
	//		quaternion.Y = (m21 + m12) * num2;
	//		quaternion.Z = 0.5f * num5;
	//		quaternion.W = (m01 - m10) * num2;
	//		return quaternion;
	//	}
	//}
}
