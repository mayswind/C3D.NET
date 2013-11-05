using System;
using System.Collections.Generic;

namespace C3D
{
    /// <summary>
    /// C3D帧数据集合
    /// </summary>
    public sealed class C3DFrameCollection : List<C3DFrame>
    {
        #region 方法
        /// <summary>
        /// 根据帧索引和点索引获取3D坐标点
        /// </summary>
        /// <param name="frameIndex">帧索引</param>
        /// <param name="pointIndex">点索引</param>\
        /// <exception cref="ArgumentOutOfRangeException">帧索引或点索引超出范围</exception>
        /// <returns>3D坐标点</returns>
        public C3DPoint3DData Get3DPoint(Int32 frameIndex, Int32 pointIndex)
        {
            if (frameIndex < 0 || frameIndex >= this.Count)
            {
                throw new ArgumentOutOfRangeException("Frame index is INVALID!");
            }

            C3DFrame frame = this[frameIndex];

            if (pointIndex < 0 || pointIndex >= frame.Point3Ds.Length)
            {
                throw new ArgumentOutOfRangeException("Point index is INVALID!");
            }

            return frame.Point3Ds[pointIndex];
        }

        /// <summary>
        /// 根据帧索引和点索引获取模拟采样
        /// </summary>
        /// <param name="frameIndex">帧索引</param>
        /// <param name="sampleIndex">采样索引</param>\
        /// <exception cref="ArgumentOutOfRangeException">帧索引或采样索引超出范围</exception>
        /// <returns>模拟采样</returns>
        public C3DAnalogSamples GetAnalogSample(Int32 frameIndex, Int32 sampleIndex)
        {
            if (frameIndex < 0 || frameIndex >= this.Count)
            {
                throw new ArgumentOutOfRangeException("Frame index is INVALID!");
            }

            C3DFrame frame = this[frameIndex];

            if (sampleIndex < 0 || sampleIndex >= frame.AnalogSamples.Length)
            {
                throw new ArgumentOutOfRangeException("Sample index is INVALID!");
            }

            return frame.AnalogSamples[sampleIndex];
        }
        #endregion
    }
}