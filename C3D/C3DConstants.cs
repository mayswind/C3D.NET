using System;

namespace C3D
{
    /// <summary>
    /// C3D常量类
    /// </summary>
    internal static class C3DConstants
    {
        #region 文件相关
        /// <summary>
        /// 默认参数部分开始的Section ID
        /// </summary>
        internal const Byte FILE_DEFAULT_FIRST_PARAM_SECTION = 0x02;

        /// <summary>
        /// 文档标识符
        /// </summary>
        internal const Byte FILE_SIGNATURE = 0x50;

        /// <summary>
        /// 文档Section大小(字节)
        /// </summary>
        internal const Int32 FILE_SECTION_SIZE = 0x200;

        /// <summary>
        /// CPU类型
        /// </summary>
        internal const C3DProcessorType FILE_DEFAULT_PROCESSOR_TYPE = C3DProcessorType.Intel;
        #endregion

        #region 文件头相关
        /// <summary>
        /// 关键值
        /// </summary>
        internal const Int16 FILEHEADER_KEY_VALUE = 0x3039;

        /// <summary>
        /// 最多事件数
        /// </summary>
        internal const Int16 FILEHEADER_MAX_EVENTS_COUNT = 0x12;
        #endregion

        #region 文件参数相关
        /// <summary>
        /// 第一块参数位置
        /// </summary>
        internal const Byte FILEPARAMETER_FIRST_PARAM_BLOCK = 0x01;

        /// <summary>
        /// 参数块标识符
        /// </summary>
        internal const Byte FILEPARAMETER_SIGNATURE = 0x50;
        #endregion

        #region 默认值相关
        internal const UInt16 DEFAULT_POINT_USED = 0;
        internal const Single DEFAULT_POINT_SCALE = -1.0F;
        internal const Single DEFAULT_POINT_RATE = 60.0F;
        internal const UInt16 DEFAULT_POINT_FIRST_FRAME = 1;
        internal const UInt16 DEFAULT_POINT_LAST_FRAME = 1;

        internal const UInt16 DEFAULT_ANALOG_USED = 0;
        internal const Single DEFAULT_ANALOG_RATE = 0;
        internal const Single DEFAULT_ANALOG_GEN_SCALE = 1.0F;
        #endregion
    }
}