using System;

namespace Utility
{
    /// <summary>
    /// 算法辅助类
    /// </summary>
    public class AlgorithmHelper
    {
        /// <summary>
        /// 算法实现：直接插入排序
        /// </summary>
        /// <param name="a"></param>
        public static void SimpleInsertSort(int[] a)
        {
            var len = a.Length;
            for (var i = 1; i < len; i++)
            {
                if (a[i] < a[i - 1])
                {
                    var j = i - 1;
                    var p = a[i];
                    a[i] = a[j];
                    while (j >= 0 && p < a[j])
                    {
                        a[j + 1] = a[j];
                        j--;
                    }
                    a[j + 1] = p;
                }
            }
            for (var i = 0; i < len; i++)
            {
                Console.Write(a[i] + ",");
            }
        }

        /// <summary>
        /// 求节点值在 [min,max] 之间的所有节点
        /// 从小到大顺序输出
        /// 二叉树特点： 左子树节点值《 父节点值《 右子树节点值
        /// </summary>
        /// <param name="root"></param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="count">无实际意义，用于记录循环次数</param>
        public static void PrintRange(BsTree root, int min, int max, ref int count)
        {
            if (root == null) return;
            if (min < root.Value && root.Value < max)
            {
                count++;
                PrintRange(root.LeftChrild, min, max, ref count);
                Console.WriteLine(root.Value);
                PrintRange(root.RightChrild, min, max, ref count);
            }
            if (root.Value <= min)
            {
                count++;
                if (root.Value == min)
                    Console.WriteLine(root.Value);
                PrintRange(root.RightChrild, min, max, ref count);
            }
            if (root.Value >= max)
            {
                count++;
                PrintRange(root.LeftChrild, min, max, ref count);
                if (root.Value == max)
                    Console.WriteLine(root.Value);
            }
        }
    }

    /// <summary>
    /// 二叉树
    /// </summary>
    public class BsTree
    {
        public BsTree LeftChrild;
        public BsTree RightChrild;
        public int Value;

        public BsTree(int v, BsTree left, BsTree right)
        {
            Value = v;
            LeftChrild = left;
            RightChrild = right;
        }
    }
}