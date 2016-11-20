<h1><strong>C3D.NET</strong></h1>
<p>
<a href="https://github.com/mayswind/C3D.NET/blob/master/LICENSE" target="_blank"><img src="https://img.shields.io/github/license/mayswind/C3D.NET.svg?style=flat" title="License"/></a>
<a href="https://github.com/mayswind/C3D.NET/releases" target="_blank"><img src="https://img.shields.io/github/release/mayswind/C3D.NET.svg?style=flat" title="Lastest Release"/></a>
<a href="http://www.nuget.org/packages/C3D.NET" target="_blank"><img src="https://img.shields.io/nuget/v/C3D.NET.svg?style=flat" title="Nuget Package"/></a>
</p>
<h2><strong>Project Description</strong></h2>
<p>C3D.NET is a free class library written in C# for manipulating C3D file. This project supports .NET 2.0 and .NET 4.0, and provides a free Data Viewer based on this library.</p>
<p><img src="http://download-codeplex.sec.s-msft.com/Download?ProjectName=c3d&DownloadId=757078" alt="" width="640" /></p>
<p>The C3D format is a public domain, binary file format that has been used in Biomechanics, Animation and Gait Analysis laboratories to record synchronized 3D and analog data.</p>
<p>For more information on C3D file, please visit <a href="http://www.c3d.org" target="_blank"> http://www.c3d.org</a></p>
<h2><strong>Examle Usage</strong></h2>
<p>You can use these code to load a C3D file and output all points.</p>
<pre>C3DFile file = C3DFile.LoadFromFile("FILE PATH");
UInt16 firstFrameIndex = file.Header.FirstFrameIndex;
UInt16 pointCount = file.Parameters.GetParameterData&lt;UInt16&gt;("POINT:USED");

for (Int32 i = 0; i &lt; file.AllFrames.Count; i++)
{
    C3DFrame frame = file.AllFrames[i];

    for (Int32 j = 0; j &lt; pointCount; j++)
    {
        C3DPoint3DData point3D = frame.Point3Ds[j];

        Console.WriteLine("Frame {0} : X = {1}, Y = {2}, Z = {3}",
            firstFrameIndex + i, point3D.X, point3D.Y, point3D.Z);
    }
}</pre>
<p>Documentation:&nbsp;<a href="https://github.com/mayswind/C3D.NET/wiki/Documentation" target="_blank">https://github.com/mayswind/C3D.NET/wiki/Documentation</a></p>
<h2><strong>Installation</strong></h2>
<h3>Using Nuget</h3>
<pre>PM> Install-Package C3D.NET</pre>
<h3>Binary Library</h3>
<p>Latest Release:&nbsp;<a href="https://github.com/mayswind/C3D.NET/releases" target="_blank">https://github.com/mayswind/C3D.NET/releases</a></p>
<h2><strong>More</strong></h2>
<p>CodePlex Page:&nbsp;<a href="https://c3d.codeplex.com" target="_blank">https://c3d.codeplex.com</a><br />
NuGet:&nbsp;<a href="http://www.nuget.org/packages/C3D.NET" target="_blank">http://www.nuget.org/packages/C3D.NET/</a><br />
GitHub Page:&nbsp;<a href="https://github.com/mayswind/C3D.NET" target="_blank">https://github.com/mayswind/C3D.NET</a><br />
An article about C3D file type and this project (Simplified Chinese):&nbsp;<a href="http://www.cnblogs.com/mayswind/p/3369090.html" target="_blank">http://www.cnblogs.com/mayswind/p/3369090.html</a></p>
