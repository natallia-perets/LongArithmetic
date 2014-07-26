using System;
using System.Collections.Generic;
using System.Security.Cryptography;


namespace LongArithmetic
{
    class VeryBigInteger
    {

        static int power = 4;
        private List<int> digits;
        private Boolean sign = true;





        public VeryBigInteger(string number)
        {
            if (number.StartsWith("-"))
            {
                sign = false;
                number = number.Remove(0, 1);
            }

            digits = new List<int>();

            for (int i = number.Length - 1; i >= 0; i -= power)
            {
                int start = i - power + 1;

                if (start < 0)
                {
                    start = 0;
                }

                string dig = number.Substring(start, i - start + 1);
                digits.Add(Convert.ToInt32(dig));
            }

        }

        public VeryBigInteger(int size)
        {
            digits = new List<int>();
            sign = true;
            for (int i = 0; i < size; i++)
            {
                digits.Add(0);
            }
        }

        private static int SameLength(VeryBigInteger a, VeryBigInteger b)
        {
            if (a.digits.Count > b.digits.Count)
            {
                while (a.digits.Count != b.digits.Count)
                {
                    b.digits.Add(0);
                }
            }

            if (b.digits.Count > a.digits.Count)
            {
                while (a.digits.Count != b.digits.Count)
                {
                    a.digits.Add(0);
                }
            }
            return a.digits.Count;
        }

        private static void ClearNulls(VeryBigInteger a)
        {
            for (int i = a.digits.Count - 1; i >= 0; i--)
            {
                if (a.digits[i] != 0) break;
                a.digits.RemoveAt(i);
            }

        }

        public static VeryBigInteger operator +(VeryBigInteger a, VeryBigInteger b)
        {
            int maxListSize = SameLength(a, b);
            VeryBigInteger result = new VeryBigInteger(maxListSize + 1);

            if (a.sign == false && b.sign)
            {
                return b - a;
            }

            if (b.sign == false && a.sign)
            {
                return a + b;
            }

            if (a.sign == false && b.sign == false)
            {
                result.sign = false;
            }

            for (int i = 0; i < maxListSize; i++)
            {

                result.digits[i] += a.digits[i] + b.digits[i];


                if (result.digits[i] >= Math.Pow(10, power))
                {
                    result.digits[i] -= (int)Math.Pow(10, power);
                    result.digits[i + 1] += 1;

                }

            }

            ClearNulls(result);
            return result;
        }

        public static Boolean operator >(VeryBigInteger a, VeryBigInteger b)
        {
            if (a.digits.Count > b.digits.Count) return true;


            if (a.digits.Count < b.digits.Count) return false;

            if (a.digits[a.digits.Count - 1] > b.digits[b.digits.Count - 1])
            {
                return true;
            }

            return false;
        }


        public static Boolean operator ==(VeryBigInteger a, VeryBigInteger b)
        {
            if (!(a > b) && !(b > a)) return true;
            return false;
        }

        public static Boolean operator !=(VeryBigInteger a, VeryBigInteger b)
        {
            return !(a == b);
        }
        public static Boolean operator <(VeryBigInteger a, VeryBigInteger b)
        {
            if (a == b) return false;
            if (a > b) return false;
            return true;
        }
        public static VeryBigInteger operator -(VeryBigInteger a, VeryBigInteger b)
        {
            int maxListSize = SameLength(a, b);
            VeryBigInteger result = new VeryBigInteger(maxListSize);

            if (a.sign == false && b.sign)
            {
                VeryBigInteger temp = b;
                temp.sign = false;
                return a + temp;
            }

            if (b.sign == false && a.sign)
            {
                {
                    VeryBigInteger temp = b;
                    temp.sign = false;
                    return a + temp;
                    
                }
            }

            if (b.sign == false && a.sign == false)
            {
                {
                    VeryBigInteger temp1 = b;
                    temp1.sign = true;
                    VeryBigInteger temp2 = a;
                    temp2.sign = true;
                    return temp1-temp2;
                }
            }

            if (a < b)
            {
                VeryBigInteger temp;
                temp = a;
                a = b;
                b = temp;
                result.sign = false;

            }

            for (int i = 0; i < maxListSize; i++)
            {

                if (a.digits[i] < b.digits[i])
                {
                    for (int j = i + 1; j < maxListSize; j++)
                    {
                        if (a.digits[j] != 0)
                        {
                            a.digits[j] -= 1;
                            break;
                        }
                        a.digits[j] = 9999;
                    }
                    result.digits[i] = (int)Math.Pow(10, power) + a.digits[i] - b.digits[i];


                }

                else
                {
                    result.digits[i] = a.digits[i] - b.digits[i];
                }


            }

            for (int i = maxListSize - 1; i > 0; i--)
            {
                if (result.digits[i] == 0)
                {
                    result.digits.RemoveAt(i);
                }
            }

            ClearNulls(result);
            return result;
        }

        public static VeryBigInteger operator *(VeryBigInteger a, int coeff)
        {
            if (coeff > Math.Pow(10, power))
            {
                Console.WriteLine("Can not multiply");
                return a;
            }

            if (coeff == 0) return new VeryBigInteger(1);
            VeryBigInteger result = new VeryBigInteger(a.digits.Count + 1);

            for (int i = 0; i < a.digits.Count; i++)
            {
                result.digits[i] += a.digits[i] * coeff;

                if (result.digits[i].ToString().Length > power)
                {
                    result.digits[i + 1] = result.digits[i] / (int)Math.Pow(10, power);
                    result.digits[i] = result.digits[i] % (int)Math.Pow(10, power);
                }
            }


            ClearNulls(result);

            if (coeff < 0 || a.sign == false)
            {
                result.sign = false;
            }

            return result;

        }
        public static VeryBigInteger operator *(VeryBigInteger a, VeryBigInteger b)
        {
            VeryBigInteger result = new VeryBigInteger(a.digits.Count * b.digits.Count);
            VeryBigInteger temp = new VeryBigInteger(a.digits.Count);

            bool bsign = b.sign;
            bool asign = a.sign;

            a.sign = true;
            b.sign = true;

            if (a < b)
            {
                temp = a;
                a = b;
                b = temp;
            }

            for (int i = 0; i < b.digits.Count; i++)
            {
                temp = a * b.digits[i];

                for (int j = 0; j < i; j++)
                {
                    temp.digits.Insert(0, 0);
                }
                result += temp;
            }


            ClearNulls(result);

            if (bsign == false || asign == false)
            {
                result.sign = false;
            }

            a.sign = asign;
            b.sign = bsign;

            return result;

        }

        public override string ToString()
        {
            string result = "";

            if (sign == false)
            {
                result += "-";
            }

            for (int i = digits.Count - 1; i >= 0; i--)
            {

                if (digits[i].ToString().Length < 4 && i != (digits.Count - 1))
                {
                    string corrString = digits[i].ToString();
                    while (corrString.Length != 4)
                    {
                        corrString = String.Format("0{0}", corrString);
                    }
                    result += corrString;
                    continue;
                }

                result += digits[i].ToString();
            }

            return result;
        }
    }
}
