using System;

namespace FMath
{
    public struct FPMatrix4x4 : IEquatable<FPMatrix4x4>
    {
        public static readonly FPMatrix4x4 zeroMatrix = new FPMatrix4x4(new FPVector4(0.0f, 0.0f, 0.0f, 0.0f),
            new FPVector4(0.0f, 0.0f, 0.0f, 0.0f), new FPVector4(0.0f, 0.0f, 0.0f, 0.0f),
            new FPVector4(0.0f, 0.0f, 0.0f, 0.0f));

        public static readonly FPMatrix4x4 identityMatrix = new FPMatrix4x4(new FPVector4(1f, 0.0f, 0.0f, 0.0f),
            new FPVector4(0.0f, 1f, 0.0f, 0.0f), new FPVector4(0.0f, 0.0f, 1f, 0.0f), new FPVector4(0.0f, 0.0f, 0.0f, 1f));

        private FPInt m00;
        private FPInt m10;
        private FPInt m20;
        private FPInt m30;
        private FPInt m01;
        private FPInt m11;
        private FPInt m21;
        private FPInt m31;
        private FPInt m02;
        private FPInt m12;
        private FPInt m22;
        private FPInt m32;
        private FPInt m03;
        private FPInt m13;
        private FPInt m23;
        private FPInt m33;

        public FPMatrix4x4(FPVector4 column0, FPVector4 column1, FPVector4 column2, FPVector4 column3)
        {
            m00 = column0.x;
            m01 = column1.x;
            m02 = column2.x;
            m03 = column3.x;
            m10 = column0.y;
            m11 = column1.y;
            m12 = column2.y;
            m13 = column3.y;
            m20 = column0.z;
            m21 = column1.z;
            m22 = column2.z;
            m23 = column3.z;
            m30 = column0.w;
            m31 = column1.w;
            m32 = column2.w;
            m33 = column3.w;
        }

        public FPMatrix4x4(FPInt a1, FPInt b1, FPInt c1, FPInt d1,
            FPInt a2, FPInt b2, FPInt c2, FPInt d2,
            FPInt a3, FPInt b3, FPInt c3, FPInt d3,
            FPInt a4, FPInt b4, FPInt c4, FPInt d4)
        {
            m00 = a1;
            m01 = b1;
            m02 = c1;
            m03 = d1;
            m10 = a2;
            m11 = b2;
            m12 = c2;
            m13 = d2;
            m20 = a3;
            m21 = b3;
            m22 = c3;
            m23 = d3;
            m30 = a4;
            m31 = b4;
            m32 = c4;
            m33 = d4;
        }

        public void Set(FPInt[,] arr)
        {
            if (arr.GetLength(0) != 4 || arr.GetLength(1) != 4) return;
            FPVector4[] vecs = new FPVector4[4];
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                vecs[i] = new FPVector4(arr[i, 0], arr[i, 1], arr[i, 2], arr[i, 3]);
            }

            Set(vecs[0], vecs[1], vecs[2], vecs[3]);
        }

        public void Set(FPVector4 column0, FPVector4 column1, FPVector4 column2, FPVector4 column3)
        {
            this = new FPMatrix4x4(column0, column1, column2, column3);
        }

        public FPInt this[int row, int column]
        {
            get => this[row + column * 4]; 
            set => this[row + column * 4] = value;
        }

        public FPInt this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.m00;
                    case 1:
                        return this.m10;
                    case 2:
                        return this.m20;
                    case 3:
                        return this.m30;
                    case 4:
                        return this.m01;
                    case 5:
                        return this.m11;
                    case 6:
                        return this.m21;
                    case 7:
                        return this.m31;
                    case 8:
                        return this.m02;
                    case 9:
                        return this.m12;
                    case 10:
                        return this.m22;
                    case 11:
                        return this.m32;
                    case 12:
                        return this.m03;
                    case 13:
                        return this.m13;
                    case 14:
                        return this.m23;
                    case 15:
                        return this.m33;
                    default:
                        throw new IndexOutOfRangeException("Invalid matrix index!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.m00 = value;
                        break;
                    case 1:
                        this.m10 = value;
                        break;
                    case 2:
                        this.m20 = value;
                        break;
                    case 3:
                        this.m30 = value;
                        break;
                    case 4:
                        this.m01 = value;
                        break;
                    case 5:
                        this.m11 = value;
                        break;
                    case 6:
                        this.m21 = value;
                        break;
                    case 7:
                        this.m31 = value;
                        break;
                    case 8:
                        this.m02 = value;
                        break;
                    case 9:
                        this.m12 = value;
                        break;
                    case 10:
                        this.m22 = value;
                        break;
                    case 11:
                        this.m32 = value;
                        break;
                    case 12:
                        this.m03 = value;
                        break;
                    case 13:
                        this.m13 = value;
                        break;
                    case 14:
                        this.m23 = value;
                        break;
                    case 15:
                        this.m33 = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid matrix index!");
                }
            }
        }

        public bool Equals(FPMatrix4x4 other)
        {
            return GetColumn(0).Equals(other.GetColumn(0)) && this.GetColumn(1).Equals(other.GetColumn(1)) &&
                   this.GetColumn(2).Equals(other.GetColumn(2)) && this.GetColumn(3).Equals(other.GetColumn(3));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is FPMatrix4x4 other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = m00.GetHashCode();
                hashCode = hashCode ^ m10.GetHashCode();
                hashCode = hashCode ^ m20.GetHashCode();
                hashCode = hashCode ^ m30.GetHashCode();
                hashCode = hashCode ^ m01.GetHashCode();
                hashCode = hashCode ^ m11.GetHashCode();
                hashCode = hashCode ^ m21.GetHashCode();
                hashCode = hashCode ^ m31.GetHashCode();
                hashCode = hashCode ^ m02.GetHashCode();
                hashCode = hashCode ^ m12.GetHashCode();
                hashCode = hashCode ^ m22.GetHashCode();
                hashCode = hashCode ^ m32.GetHashCode();
                hashCode = hashCode ^ m03.GetHashCode();
                hashCode = hashCode ^ m13.GetHashCode();
                hashCode = hashCode ^ m23.GetHashCode();
                hashCode = hashCode ^ m33.GetHashCode();
                return hashCode;
            }
        }

        public FPVector4 GetColumn(int index)
        {
            switch (index)
            {
                case 0:
                    return new FPVector4(this.m00, this.m10, this.m20, this.m30);
                case 1:
                    return new FPVector4(this.m01, this.m11, this.m21, this.m31);
                case 2:
                    return new FPVector4(this.m02, this.m12, this.m22, this.m32);
                case 3:
                    return new FPVector4(this.m03, this.m13, this.m23, this.m33);
                default:
                    throw new IndexOutOfRangeException("Invalid column index!");
            }
        }

        public FPVector4 GetRow(int index)
        {
            switch (index)
            {
                case 0:
                    return new FPVector4(this.m00, this.m01, this.m02, this.m03);
                case 1:
                    return new FPVector4(this.m10, this.m11, this.m12, this.m13);
                case 2:
                    return new FPVector4(this.m20, this.m21, this.m22, this.m23);
                case 3:
                    return new FPVector4(this.m30, this.m31, this.m32, this.m33);
                default:
                    throw new IndexOutOfRangeException("Invalid row index!");
            }
        }

        public static FPMatrix4x4 TRS(FPVector3 pos, FPVector3 eulerAngles, FPVector3 scale)
        {
            FPMatrix4x4 mat = Translate(pos);
            mat = mat * Rotate(eulerAngles) * Scale(scale);
            return mat;
        }

        public static FPMatrix4x4 Rotate(FPVector3 euler)
        {
            return RotateY(euler.y) * RotateX(euler.x) * RotateZ(euler.z);
        }

        public static FPMatrix4x4 Translate(FPVector3 vector)
        {
            FPMatrix4x4 FPMatrix4x4 = identityMatrix;
            FPMatrix4x4.m00 = 1f;
            FPMatrix4x4.m01 = 0.0f;
            FPMatrix4x4.m02 = 0.0f;
            FPMatrix4x4.m03 = vector.x;
            FPMatrix4x4.m10 = 0.0f;
            FPMatrix4x4.m11 = 1f;
            FPMatrix4x4.m12 = 0.0f;
            FPMatrix4x4.m13 = vector.y;
            FPMatrix4x4.m20 = 0.0f;
            FPMatrix4x4.m21 = 0.0f;
            FPMatrix4x4.m22 = 1f;
            FPMatrix4x4.m23 = vector.z;
            FPMatrix4x4.m30 = 0.0f;
            FPMatrix4x4.m31 = 0.0f;
            FPMatrix4x4.m32 = 0.0f;
            FPMatrix4x4.m33 = 1f;
            return FPMatrix4x4;
        }

        public static FPMatrix4x4 RotateX(FPInt rad)
        {
            FPInt sin = FPMath.Sin(rad);
            FPInt cos = (FPInt) FPMath.Cos(rad);
            FPMatrix4x4 mat = identityMatrix;
            mat[1, 1] = cos;
            mat[1, 2] = -sin;
            mat[2, 1] = sin;
            mat[2, 2] = cos;
            return mat;
        }

        public static FPMatrix4x4 RotateY(FPInt rad)
        {
            FPInt sin = FPMath.Sin(rad);
            FPInt cos = (FPInt) FPMath.Cos(rad);
            FPMatrix4x4 mat = identityMatrix;
            mat[2, 2] = cos;
            mat[2, 0] = -sin;
            mat[0, 2] = sin;
            mat[0, 0] = cos;

            return mat;
        }
  

        public static FPMatrix4x4 RotateZ(FPInt rad)
        {
            FPInt sin = (FPInt) FPMath.Sin(rad);
            FPInt cos = (FPInt) FPMath.Cos(rad);
            FPMatrix4x4 mat = identityMatrix;
            mat[0, 0] = cos;
            mat[0, 1] = -sin;
            mat[1, 0] = sin;
            mat[1, 1] = cos;
            return mat;
        }

        /// <summary>
        /// 绕任意轴旋转
        /// </summary>
        /// <param name="axis">轴向量</param>
        /// <param name="angle">弧度</param>
        /// <returns></returns>
        public static FPMatrix4x4 ArbitraryAxis(FPVector4 axis, FPInt angle)
        {
            FPMatrix4x4 a = new FPMatrix4x4();
            //第一列
            a[0, 0] = (FPInt)(axis.x * axis.x * (1 - FPMath.Cos(angle)) + FPMath.Cos(angle));
            a[1, 0] = (FPInt)(axis.x * axis.y * (1 - FPMath.Cos(angle)) - axis.z * FPMath.Sin(angle));
            a[2, 0] = (FPInt)(axis.x * axis.z * (1 - FPMath.Cos(angle)) + axis.y * FPMath.Sin(angle));
            a[3, 0] = 0;
            //第二列
            a[0, 1] = (FPInt)(axis.x * axis.y * (1 - FPMath.Cos(angle)) + axis.z * FPMath.Sin(angle));
            a[1, 1] = (FPInt)(axis.y * axis.y * (1 - FPMath.Cos(angle)) + FPMath.Cos(angle));
            a[2, 1] = (FPInt)(axis.y * axis.z * (1 - FPMath.Cos(angle)) - axis.x * FPMath.Sin(angle));
            a[3, 1] = 0;
            //第三列
            a[0, 2] = (FPInt)(axis.x * axis.z * (1 - FPMath.Cos(angle)) - axis.y * FPMath.Sin(angle));
            a[1, 2] = (FPInt)(axis.y * axis.z * (1 - FPMath.Cos(angle)) + axis.x * FPMath.Sin(angle));
            a[2, 2] = (FPInt)(axis.z * axis.z * (1 - FPMath.Cos(angle)) + FPMath.Cos(angle));
            a[3, 2] = 0;
            //第四列
            a[0, 3] = 0;
            a[1, 3] = 0;
            a[2, 3] = 0;
            a[3, 3] = 1;
            return a;
        }

        public static FPMatrix4x4 Scale(FPVector3 scale)
        {
            FPMatrix4x4 mat = identityMatrix;
            mat[0, 0] = scale.x;
            mat[1, 1] = scale.y;
            mat[2, 2] = scale.z;
            return mat;
        }

        public static FPMatrix4x4 operator *(FPMatrix4x4 m1, FPMatrix4x4 m2)
        {
            FPMatrix4x4 FPMatrix4x4 = identityMatrix;

            FPMatrix4x4.m00 = (FPInt) (m1.m00 *  m2.m00 + m1.m01 *  m2.m10 +
                                     m1.m02 *  m2.m20 + m1.m03 *  m2.m30);
            FPMatrix4x4.m01 = (FPInt) (m1.m00 *  m2.m01 + m1.m01 *  m2.m11 +
                                     m1.m02 *  m2.m21 + m1.m03 *  m2.m31);
            FPMatrix4x4.m02 = (FPInt) (m1.m00 *  m2.m02 + m1.m01 *  m2.m12 +
                                     m1.m02 *  m2.m22 + m1.m03 *  m2.m32);
            FPMatrix4x4.m03 = (FPInt) (m1.m00 *  m2.m03 + m1.m01 *  m2.m13 +
                                     m1.m02 *  m2.m23 + m1.m03 *  m2.m33);
            FPMatrix4x4.m10 = (FPInt) (m1.m10 *  m2.m00 + m1.m11 *  m2.m10 +
                                     m1.m12 *  m2.m20 + m1.m13 *  m2.m30);
            FPMatrix4x4.m11 = (FPInt) (m1.m10 *  m2.m01 + m1.m11 *  m2.m11 +
                                     m1.m12 *  m2.m21 + m1.m13 *  m2.m31);
            FPMatrix4x4.m12 = (FPInt) (m1.m10 *  m2.m02 + m1.m11 *  m2.m12 +
                                     m1.m12 *  m2.m22 + m1.m13 *  m2.m32);
            FPMatrix4x4.m13 = (FPInt) (m1.m10 *  m2.m03 + m1.m11 *  m2.m13 +
                                     m1.m12 *  m2.m23 + m1.m13 *  m2.m33);
            FPMatrix4x4.m20 = (FPInt) (m1.m20 *  m2.m00 + m1.m21 *  m2.m10 +
                                     m1.m22 *  m2.m20 + m1.m23 *  m2.m30);
            FPMatrix4x4.m21 = (FPInt) (m1.m20 *  m2.m01 + m1.m21 *  m2.m11 +
                                     m1.m22 *  m2.m21 + m1.m23 *  m2.m31);
            FPMatrix4x4.m22 = (FPInt) (m1.m20 *  m2.m02 + m1.m21 *  m2.m12 +
                                     m1.m22 *  m2.m22 + m1.m23 *  m2.m32);
            FPMatrix4x4.m23 = (FPInt) (m1.m20 *  m2.m03 + m1.m21 *  m2.m13 +
                                     m1.m22 *  m2.m23 + m1.m23 *  m2.m33);
            FPMatrix4x4.m30 = (FPInt) (m1.m30 *  m2.m00 + m1.m31 *  m2.m10 +
                                     m1.m32 *  m2.m20 + m1.m33 *  m2.m30);
            FPMatrix4x4.m31 = (FPInt) (m1.m30 *  m2.m01 + m1.m31 *  m2.m11 +
                                     m1.m32 *  m2.m21 + m1.m33 *  m2.m31);
            FPMatrix4x4.m32 = (FPInt) (m1.m30 *  m2.m02 + m1.m31 *  m2.m12 +
                                     m1.m32 *  m2.m22 + m1.m33 *  m2.m32);
            FPMatrix4x4.m33 = (FPInt) (m1.m30 *  m2.m03 + m1.m31 *  m2.m13 +
                                     m1.m32 *  m2.m23 + m1.m33 *  m2.m33);
            return FPMatrix4x4;
        }

        public static FPVector4 operator *(FPMatrix4x4 m1, FPVector4 vector)
        {
            FPVector4 fpVector4 = FPVector4.zero;
            fpVector4.x = (FPInt) (m1.m00 *  vector.x + m1.m01 *  vector.y +
                                 m1.m02 *  vector.z + m1.m03 *  vector.w);
            fpVector4.y = (FPInt) (m1.m10 *  vector.x + m1.m11 *  vector.y +
                                 m1.m12 *  vector.z + m1.m13 *  vector.w);
            fpVector4.z = (FPInt) (m1.m20 *  vector.x + m1.m21 *  vector.y +
                                 m1.m22 *  vector.z + m1.m23 *  vector.w);
            fpVector4.w = (FPInt) (m1.m30 *  vector.x + m1.m31 *  vector.y +
                                 m1.m32 *  vector.z + m1.m33 *  vector.w);
            return fpVector4;
        }

        public static bool operator ==(FPMatrix4x4 m1, FPMatrix4x4 m2)
        {
            return m1.GetColumn(0) == m2.GetColumn(0) && m1.GetColumn(1) == m2.GetColumn(1) &&
                   m1.GetColumn(2) == m2.GetColumn(2) && m1.GetColumn(3) == m2.GetColumn(3);
        }

        public static bool operator !=(FPMatrix4x4 m1, FPMatrix4x4 m2)
        {
            return !(m1 == m2);
        }

        /// <summary>
        /// 求逆矩阵
        /// </summary>
        public FPMatrix4x4 Inverse()
        {
            Set(Inverse(ToArray()));
            return this;
        }

        private FPInt[,] Inverse(FPInt[,] arr)
        {
            FPInt det = GetDeterminant(arr);
            if (det.Equals(0))
            {
                Console.WriteLine("行列式为0，没有逆矩阵");
            }
            else
            {
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = i + 1; j < arr.GetLength(1); j++)
                    {
                        arr[j, i] /= det;
                    }
                }
            }

            return arr;
        }

        private FPInt[,] ToArray()
        {
            FPInt[,] result = new FPInt[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    result[i, j] = this[i, j];
                }
            }

            return result;
        }

        private FPInt GetDeterminant()
        {
            FPInt[,] arr = GetArr();
            return GetDeterminant(arr);
        }

        /// <summary>
        /// 转置矩阵
        /// </summary>
        public FPMatrix4x4 TransposeMatrix()
        {
            Set(TransposeMatrix(ToArray()));
            return this;
        }

        /// <summary>
        /// 转置矩阵
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        private FPInt[,] TransposeMatrix(FPInt[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = i + 1; j < arr.GetLength(1); j++)
                {
                    FPInt temp = arr[i, j];
                    arr[i, j] = arr[j, i];
                    arr[j, i] = temp;
                }
            }

            return arr;
        }

        #region 求行列式

        /// <summary>
        /// 求矩阵的行列式
        /// </summary>
        private static FPInt GetDeterminant(FPInt[,] arr)
        {
            if (arr.GetLength(0) <= 2) return GetSubArrDeterminant(arr, 0, 0);
            FPInt num = 0;
            for (int i = 0; i < arr.GetLength(1); i++)
            {
                num += GetSubArrDeterminant(arr, 0, i) * (FPInt) FPMath.Pow(-1, i);
            }

            return num;
        }

        /// <summary>
        /// 得到余子式的行列式
        /// </summary>
        private static FPInt GetSubArrDeterminant(FPInt[,] arr, int x, int y)
        {
            if (arr.GetLength(0) <= 2)
            {
                return arr[0, 0] * arr[1, 1] - arr[0, 1] * arr[1, 0];
            }

            return arr[x, y] * GetDeterminant(GetSubArr(arr, x, y));
        }
        /// <summary>
        /// 得到余子式
        /// </summary>
        private static FPInt[,] GetSubArr(FPInt[,] arr, int x, int y)
        {
            if (arr.GetLength(0) <= 1) return arr;
            FPInt[,] temp = new FPInt[arr.GetLength(0) - 1, arr.GetLength(1) - 1];
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                if (i == x) continue;
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (j == y) continue;
                    temp[i < x ? i : i - 1, j < y ? j : j - 1] = arr[i, j];
                }
            }

            return temp;
        }

        #endregion

        /// <summary>
        /// 求矩阵的伴随矩阵
        /// 求每个元素的代数余子式，组成一个矩阵
        /// </summary>
        private static FPInt[,] GetAdjointMatrix(FPInt[,] arr)
        {
            if (arr.GetLength(0) <= 2) return arr;
            FPInt[,] result = new FPInt[arr.GetLength(0), arr.GetLength(1)];
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    FPInt[,] temp = GetSubArr(arr, i, j);
                    result[i, j] = (FPInt) FPMath.Pow(-1, i + j) * GetDeterminant(temp);
                }
            }

            return result;
        }


        private FPInt[,] GetArr()
        {
            FPInt[,] arr = new FPInt[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    arr[i, j] = this[i, j];
                }
            }

            return arr;
        }
    }
}