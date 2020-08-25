using System;
using System.Collections.Generic;

namespace SMSS.Function
{
    class Distance_Fomula
    {
        /// <summary>
        /// 欧氏距离
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static double Euclidean(List<double> l1, List<double> l2)
        {
            double distance = 0;
            for (int i = 0; i < l1.Count; i++)
            {
                distance += Math.Sqrt(Math.Pow(l1[i] - l2[i], 2));
            }
            return distance;
        }
        /// <summary>
        /// 卡方距离
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static double X2(List<double> l1, List<double> l2)
        {
            double distance = 0;
            for (int i = 0; i < l1.Count; i++)
            {
                if (l1[i] + l2[i] == 0)
                    distance += 0;
                else
                    distance += Math.Pow(l1[i] - l2[i], 2) / (l1[i] + l2[i]);
            }
            return distance;
        }
        /// <summary>
        /// 直方图相交距离
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static double Dxy(List<double> l1, List<double> l2)
        {
            double distance = 0;
            for (int i = 0; i < l1.Count; i++)
            {
                if (l1[i] < l2[i])
                    distance += l1[i];
                else
                    distance += l2[i];
            }
            return (1 - distance);
        }
    }
}
