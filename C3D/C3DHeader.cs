using System;
using System.Text;

using C3D.Number;

namespace C3D
{
    /// <summary>
    /// C3D文件头
    /// </summary>
    public sealed class C3DHeader
    {
        #region 常量
        /// <summary>
        /// 默认参数部分开始的Section ID
        /// </summary>
        private const Byte DEFAULT_FIRST_PARAM_SECTION = 0x02;

        /// <summary>
        /// 文件标识符
        /// </summary>
        internal const Byte SIGNATURE = 0x50;

        /// <summary>
        /// 关键值
        /// </summary>
        internal const Int16 KEY_VALUE = 0x3039;

        /// <summary>
        /// 最多事件数
        /// </summary>
        internal const Int16 MAX_EVENTS_COUNT = 0x12;
        #endregion

        #region 字段
        private Byte[] _data;
        #endregion

        #region 属性
        /// <summary>
        /// 设置文件标识符
        /// </summary>
        private Byte Signature
        {
            set { this._data[1] = value; }
        }
        
        /// <summary>
        /// 获取或设置3D坐标点个数
        /// </summary>
        public UInt16 PointCount
        {
            get { return (UInt16)this.GetInt16Record(2); }
            set { this.SetInt16Record(2, (Int16)value); }
        }

        /// <summary>
        /// 获取或设置模拟数据测量的个数
        /// </summary>
        public UInt16 AnalogMeasurementCount
        {
            get { return (UInt16)this.GetInt16Record(3); }
            set { this.SetInt16Record(3, (Int16)value); }
        }

        /// <summary>
        /// 获取或设置第一帧的序号(从1开始计数)
        /// </summary>
        public UInt16 FirstFrameIndex
        {
            get { return (UInt16)this.GetInt16Record(4); }
            set { this.SetInt16Record(4, (Int16)value); }
        }

        /// <summary>
        /// 获取或设置最后一帧的序号(从1开始计数)
        /// </summary>
        public UInt16 LastFrameIndex
        {
            get { return (UInt16)this.GetInt16Record(5); }
            set { this.SetInt16Record(5, (Int16)value); }
        }

        /// <summary>
        /// 获取或设置最大插值差距
        /// </summary>
        public Int16 MaxInterpolationGaps
        {
            get { return this.GetInt16Record(6); }
            set { this.SetInt16Record(6, value); }
        }

        /// <summary>
        /// 获取或设置比例因子(3D浮点坐标为负数)
        /// </summary>
        public Single ScaleFactor
        {
            get { return this.GetSingleRecord(7); }
            set { this.SetSingleRecord(7, value); }
        }

        /// <summary>
        /// 获取或设置每帧模拟样例个数
        /// </summary>
        public UInt16 AnalogSamplesPerFrame
        {
            get { return (UInt16)this.GetInt16Record(10); }
            set { this.SetInt16Record(10, (Int16)value); }
        }

        /// <summary>
        /// 获取或设置帧率
        /// </summary>
        public Single FrameRate
        {
            get { return this.GetSingleRecord(11); }
            set { this.SetSingleRecord(11, value); }
        }

        /// <summary>
        /// 获取或设置是否含有标签和范围
        /// </summary>
        public Boolean HasLableRangeData
        {
            get { return this.GetInt16Record(148) == C3DHeader.KEY_VALUE; }
            set { this.SetInt16Record(148, (value ? C3DHeader.KEY_VALUE : (Int16)0)); }
        }

        /// <summary>
        /// 获取或设置文档是否支持4字节事件标签
        /// </summary>
        public Boolean IsSupport4CharsLabel
        {
            get { return this.GetInt16Record(150) == C3DHeader.KEY_VALUE; }
            set { this.SetInt16Record(150, (value ? C3DHeader.KEY_VALUE : (Int16)0)); }
        }

        /// <summary>
        /// 获取事件数量
        /// </summary>
        public Int16 HeaderEventsCount
        {
            get { return this.GetInt16Record(151); }
            private set { this.SetInt16Record(151, value); }
        }

        #region 内部属性
        /// <summary>
        /// 获取参数部分开始的Section ID(从1开始计数)
        /// </summary>
        internal Byte FirstParameterSectionID
        {
            get { return this._data[0]; }
            private set { this._data[0] = value; }
        }

        /// <summary>
        /// 获取或设置3D和模拟数据区第一个Section ID(从1开始计数)
        /// </summary>
        internal UInt16 FirstDataSectionID
        {
            get { return (UInt16)this.GetInt16Record(9); }
            set { this.SetInt16Record(9, (Int16)value); }
        }

        /// <summary>
        /// 获取或设置标签和范围开始的SectionID
        /// </summary>
        internal UInt16 FirstLabelRangeSectionID
        {
            get { return (UInt16)this.GetInt16Record(149); }
            set { this.SetInt16Record(149, (Int16)value); }
        }
        #endregion
        #endregion

        #region 构造方法
        /// <summary>
        /// 从已有的数据中初始化C3D文件头
        /// </summary>
        /// <param name="processorType">原处理器类型</param>
        /// <param name="rawData">原始头数据</param>
        internal C3DHeader(C3DProcessorType processorType, Byte[] rawData)
        {
            this._data = rawData;

            this.UpdateHeaderContent(processorType);
        }

        /// <summary>
        /// 初始化新的C3D文件头 
        /// </summary>
        internal C3DHeader()
        {
            this._data = new Byte[C3DFile.SECTION_SIZE];

            this.Signature = C3DHeader.SIGNATURE;
            this.FirstParameterSectionID = C3DHeader.DEFAULT_FIRST_PARAM_SECTION;
            this.PointCount = 0;
            this.AnalogMeasurementCount = 0;
            this.FirstFrameIndex = 1;
            this.LastFrameIndex = 1;
            this.ScaleFactor = -1.0F;
            this.AnalogSamplesPerFrame = 0;
            this.FrameRate = 60.0F;
            this.IsSupport4CharsLabel = true;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取所有事件列表
        /// </summary>
        /// <returns>所有事件列表</returns>
        public C3DHeaderEvent[] GetAllHeaderEvents()
        {
            Int16 count = this.HeaderEventsCount;

            if (count == 0)
            {
                return null;
            }
            else if (count > C3DHeader.MAX_EVENTS_COUNT)
            {
                count = C3DHeader.MAX_EVENTS_COUNT;
            }

            C3DHeaderEvent[] array = new C3DHeaderEvent[count];

            for (Int16 i = 0; i < count; i++)
            {
                array[i] = new C3DHeaderEvent(
                    Encoding.ASCII.GetString(this._data, 396 + i * 4, 4),
                    this.GetSingleRecord((Int16)(153 + i * 2)),
                    this._data[376 + i] == 1);
            }

            return array;
        }

        /// <summary>
        /// 设置事件列表
        /// </summary>
        /// <param name="events">事件列表</param>
        /// <exception cref="ArgumentOutOfRangeException">事件内容过多</exception>
        public void SetAllHeaderEvents(C3DHeaderEvent[] events)
        {
            Int16 count = (Int16)(events != null ? events.Length : 0);

            if (count > C3DHeader.MAX_EVENTS_COUNT)
            {
                throw new ArgumentOutOfRangeException("Header events is too much.");
            }

            for (Int16 i = 0; i < count; i++)
            {
                if (!String.IsNullOrEmpty(events[i].EventName) && events[i].EventName.Length > 4)
                {
                    throw new ArgumentOutOfRangeException("Event name is too long.");
                }
            }

            Byte[] empty = new Byte[4] { 0x20, 0x20, 0x20, 0x20 };
            for (Int16 i = 0; i < count; i++)
            {
                this.SetSingleRecord((Int16)(153 + i * 2), events[i].EventTime);
                this._data[376 + i] = (Byte)(events[i].IsDisplay ? 1 : 0);

                Array.Copy(empty, 0, this._data, 396 + i * 4, empty.Length);

                if (!String.IsNullOrEmpty(events[i].EventName))
                {
                    Byte[] nameData = Encoding.ASCII.GetBytes(events[i].EventName);
                    Array.Copy(nameData, 0, this._data, 396 + i * 4, nameData.Length);
                }
            }

            this.HeaderEventsCount = count;
        }
        #endregion

        #region 内部方法
        /// <summary>
        /// 获取C3D文件头原始数据
        /// </summary>
        /// <returns>C3D文件头原始数据</returns>
        internal Byte[] ToArray()
        {
            return this._data;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 获取16位整数记录
        /// </summary>
        /// <param name="index">记录索引(从1开始计数，2字节单位)</param>
        /// <returns>记录内容</returns>
        private Int16 GetInt16Record(Int16 index)
        {
            return C3DBitConverter.ToInt16(this._data, (index - 1) * 2);
        }

        /// <summary>
        /// 设置16位整数记录
        /// </summary>
        /// <param name="index">记录索引(从1开始计数，2字节单位)</param>
        /// <param name="value">记录内容</param>
        private void SetInt16Record(Int16 index, Int16 value)
        {
            Array.Copy(C3DBitConverter.GetBytes(value), 0, this._data, (index - 1) * 2, sizeof(Int16));
        }

        /// <summary>
        /// 获取32位单精度浮点数记录
        /// </summary>
        /// <param name="index">记录索引(从1开始计数，2字节单位)</param>
        /// <returns>记录内容</returns>
        private Single GetSingleRecord(Int16 index)
        {
            return C3DBitConverter.ToSingle(this._data, (index - 1) * 2);
        }

        /// <summary>
        /// 设置32位单精度浮点数记录
        /// </summary>
        /// <param name="index">记录索引(从1开始计数，2字节单位)</param>
        /// <param name="value">记录内容</param>
        private void SetSingleRecord(Int16 index, Single value)
        {
            Array.Copy(C3DBitConverter.GetBytes(value), 0, this._data, (index - 1) * 2, sizeof(Single));
        }

        #region 修正不同处理器的差异
        /// <summary>
        /// 更新16位整数记录
        /// </summary>
        /// <param name="oldType">原处理器类型</param>
        /// <param name="index">记录索引(从1开始计数，2字节单位)</param>
        private void UpdateInt16Record(C3DProcessorType oldType, Int16 index)
        {
            Int16 value = C3DBitConverter.ToInt16(oldType, this._data, (index - 1) * 2);
            this.SetInt16Record(index, value);
        }

        /// <summary>
        /// 更新32位单精度浮点数记录
        /// </summary>
        /// <param name="oldType">原处理器类型</param>
        /// <param name="index">记录索引(从1开始计数，2字节单位)</param>
        private void UpdateSingleRecord(C3DProcessorType oldType, Int16 index)
        {
            Single value = C3DBitConverter.ToSingle(oldType, this._data, (index - 1) * 2);
            this.SetSingleRecord(index, value);
        }

        /// <summary>
        /// 更新事件时间
        /// </summary>
        /// <param name="oldType">原处理器类型</param>
        private void UpdateHeaderEventTimes(C3DProcessorType oldType)
        {
            Int16 count = this.HeaderEventsCount;

            for (Int16 index = 153; index < 153 + count * 2; index += 2)
            {
                this.UpdateSingleRecord(oldType, index);
            }
        }

        /// <summary>
        /// 修正不同处理器文档格式问题
        /// </summary>
        /// <param name="processorType">原处理器类型</param>
        private void UpdateHeaderContent(C3DProcessorType processorType)
        {
            this.UpdateInt16Record(processorType, 2);
            this.UpdateInt16Record(processorType, 3);
            this.UpdateInt16Record(processorType, 4);
            this.UpdateInt16Record(processorType, 5);
            this.UpdateInt16Record(processorType, 6);
            this.UpdateSingleRecord(processorType, 7);
            this.UpdateInt16Record(processorType, 9);
            this.UpdateInt16Record(processorType, 10);
            this.UpdateSingleRecord(processorType, 11);
            this.UpdateInt16Record(processorType, 148);
            this.UpdateInt16Record(processorType, 149);
            this.UpdateInt16Record(processorType, 150);
            this.UpdateInt16Record(processorType, 151);
            this.UpdateHeaderEventTimes(processorType);
        }
        #endregion
        #endregion
    }
}