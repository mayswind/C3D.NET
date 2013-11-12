<h2><strong>Project Description</strong></h2>
<p>C3D.NET is a free class library written in C# for manipulating C3D file. This project supports .NET 2.0 and .NET 4.0, and provides a free Data Viewer based on this library.</p>
<p><img src="http://download-codeplex.sec.s-msft.com/Download?ProjectName=c3d&DownloadId=746151" alt="" width="643" height="393" /></p>
<p>The C3D format is a public domain, binary file format that has been used in Biomechanics, Animation and Gait Analysis laboratories to record synchronized 3D and analog data.</p>
<p>For more information on C3D file, please visit <a href="http://www.c3d.org" target="_blank"> http://www.c3d.org</a></p>
<h2><strong>Examle Usage</strong></h2>
<p>You can use these code to load a C3D file and output all points.</p>
<pre>C3DFile file = C3DFile.LoadFromFile("FILE PATH");
Int16 firstFrameIndex = file.Header.FirstFrameIndex;
Int16 pointCount = file.Parameters["POINT:USED"].GetData&lt;Int16&gt;();

for (Int16 i = 0; i &lt; file.AllFrames.Count; i++)
{
    for (Int16 j = 0; j &lt; pointCount; j++)
    {
        Console.WriteLine("Frame {0} : X = {1}, Y = {2}, Z = {3}",
            firstFrameIndex + i,
            file.AllFrames[i].Point3Ds[j].X,
            file.AllFrames[i].Point3Ds[j].Y ,
            file.AllFrames[i].Point3Ds[j].Z);
    }
}
</pre>
<h2><strong>More</strong></h2>
<p>CodePlex Page:&nbsp;<a href="https://c3d.codeplex.com" target="_blank">https://c3d.codeplex.com</a><br />
GitHub Page:&nbsp;<a href="https://github.com/mayswind/C3D.NET" target="_blank">https://github.com/mayswind/C3D.NET</a><br />
A Chinese article about C3D and this project:&nbsp;<a href="http://www.cnblogs.com/mayswind/p/3369090.html" target="_blank">http://www.cnblogs.com/mayswind/p/3369090.html</a></p>
