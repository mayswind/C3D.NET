# C3D.NET
[![License](https://img.shields.io/github/license/mayswind/C3D.NET.svg?style=flat)](https://github.com/mayswind/C3D.NET/blob/master/LICENSE)
[![Lastest Release](https://img.shields.io/github/release/mayswind/C3D.NET.svg?style=flat)](https://github.com/mayswind/C3D.NET/releases)
[![Nuget Package](https://img.shields.io/nuget/v/C3D.NET.svg?style=flat)](http://www.nuget.org/packages/C3D.NET)

## Project Description
C3D.NET is a free class library written in C# for manipulating C3D file. This project supports .NET 2.0 and .NET 4.0, and provides a free Data Viewer based on this library.

![C3D.NET DataViewer](http://download-codeplex.sec.s-msft.com/Download?ProjectName=c3d&DownloadId=757078)

The C3D format is a public domain, binary file format that has been used in Biomechanics, Animation and Gait Analysis laboratories to record synchronized 3D and analog data.

For more information on C3D file, please visit [http://www.c3d.org](http://www.c3d.org)

## Examle Usage

You can use these code to load a C3D file and output all points.

    C3DFile file = C3DFile.LoadFromFile("FILE PATH");
    UInt16 firstFrameIndex = file.Header.FirstFrameIndex;
    UInt16 pointCount = file.Parameters.GetParameterData<UInt16>("POINT:USED");
    
    for (Int32 i = 0; i < file.AllFrames.Count; i++)
    {
        C3DFrame frame = file.AllFrames[i];

        for (Int32 j = 0; j < pointCount; j++)
        {
            C3DPoint3DData point3D = frame.Point3Ds[j];

            Console.WriteLine("Frame {0} : X = {1}, Y = {2}, Z = {3}",
                firstFrameIndex + i, point3D.X, point3D.Y, point3D.Z);
        }
    }

Documentation: [https://github.com/mayswind/C3D.NET/wiki/Documentation](https://github.com/mayswind/C3D.NET/wiki/Documentation)

## Installation
### Using Nuget
    PM> Install-Package C3D.NET

### Binary Library
Latest Release: [https://github.com/mayswind/C3D.NET/releases](https://github.com/mayswind/C3D.NET/releases)

## More
CodePlex Page: [https://c3d.codeplex.com](https://c3d.codeplex.com)

NuGet: [http://www.nuget.org/packages/C3D.NET](http://www.nuget.org/packages/C3D.NET/)

GitHub Page [https://github.com/mayswind/C3D.NET](https://github.com/mayswind/C3D.NET)

An article about C3D file type and this project (Simplified Chinese): [http://www.cnblogs.com/mayswind/p/3369090.html](http://www.cnblogs.com/mayswind/p/3369090.html)
