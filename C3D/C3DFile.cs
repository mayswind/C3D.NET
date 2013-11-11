using System;
using System.Collections.Generic;
using System.IO;

namespace C3D
{
    /// <summary>
    /// C3D文件
    /// </summary>
    public sealed class C3DFile
    {
        #region 常量
        /// <summary>
        /// 文档Section大小(字节)
        /// </summary>
        internal const Int32 SECTION_SIZE = 0x200;

        /// <summary>
        /// CPU类型
        /// </summary>
        internal const C3DProcessorType DEFAULT_PROCESSOR_TYPE = C3DProcessorType.Intel;
        #endregion

        #region 字段
        private C3DProcessorType _processorType;
        private C3DHeader _header;
        private C3DParameterDictionary _parameterDictionary;
        private C3DFrameCollection _frameCollection;
        #endregion

        #region 属性
        /// <summary>
        /// 获取创建C3D文件的处理器类型
        /// </summary>
        public C3DProcessorType CreateProcessorType
        {
            get { return this._processorType; }
        }

        /// <summary>
        /// 获取C3D文件头
        /// </summary>
        public C3DHeader Header
        {
            get { return this._header; }
        }

        /// <summary>
        /// 获取或设置C3D文件所有参数
        /// </summary>
        public C3DParameterDictionary Parameters
        {
            get { return this._parameterDictionary; }
            set { this._parameterDictionary = value; }
        }

        /// <summary>
        /// 获取或设置C3D文件帧集合
        /// </summary>
        public C3DFrameCollection AllFrames
        {
            get { return this._frameCollection; }
            set { this._frameCollection = value; }
        }
        #endregion

        #region 构造方法
        private C3DFile()
        {
            this._processorType = C3DFile.DEFAULT_PROCESSOR_TYPE;
            this._header = new C3DHeader();
            this._parameterDictionary = new C3DParameterDictionary();
            this._frameCollection = new C3DFrameCollection();
        }

        private C3DFile(Stream stream)
        {
            C3DReader reader = new C3DReader(stream);

            this._processorType = reader.CreateProcessorType;
            this._header = reader.ReadHeader();
            this._parameterDictionary = reader.ReadParameters();
            this._frameCollection = new C3DFrameCollection();

            C3DParameterCache paramCache = C3DParameterCache.CreateCache(this);
            C3DFrame frame = null;
            while ((frame = reader.ReadNextFrame(paramCache)) != null)
            {
                this._frameCollection.Add(frame);
            }

            reader.Close();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 保存C3D文件到文件路径
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public void SaveTo(String filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                C3DWriter writer = new C3DWriter(fs);

                writer.WriteC3DFile(this);
            }
        }
        #endregion

        #region 静态方法
        /// <summary>
        /// 创建新的C3D文件
        /// </summary>
        /// <returns>C3D文件</returns>
        public static C3DFile Create()
        {
            return new C3DFile();
        }

        /// <summary>
        /// 从流中读取C3D文件
        /// </summary>
        /// <param name="stream">流</param>
        /// <returns>C3D文件</returns>
        public static C3DFile LoadFromStream(Stream stream)
        {
            return new C3DFile(stream);
        }

        /// <summary>
        /// 从文件路径中读取C3D文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>C3D文件</returns>
        public static C3DFile LoadFromFile(String filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return new C3DFile(fs);
            }
        }
        #endregion
    }
}