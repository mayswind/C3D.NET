using System;

namespace C3D.DataViewer.Helper
{
    /// <summary>
    /// C3D参数辅助类
    /// </summary>
    internal static class C3DParameterHelper
    {
        /// <summary>
        /// 从参数数组中读取数组内容
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="parameter">C3D参数</param>
        /// <param name="size">数组大小</param>
        /// <returns>数组内容</returns>
        internal static T[] LoadFromParameterArray<T>(C3DParameter parameter, Int32 size)
        {
            Object raw = (parameter != null ? parameter.GetData() : null);
            T[] ret = null;

            if (raw != null && raw is T[])
            {
                ret = raw as T[];
            }
            else if (raw != null && raw is T && size > 0)
            {
                ret = new T[size];
                T unit = (T)raw;

                for (Int32 i = 0; i < ret.Length; i++)
                {
                    ret[i] = unit;
                }
            }

            return ret;
        }
    }
}