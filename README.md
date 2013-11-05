<h2><strong>Project Description</strong></h2>
<p>C3D.NET is a free class library for manipulating C3D file. This project supports .NET 2.0 and .NET 4.0, and provides a free Data Viewer based on this library.</p>
<p><img src="http://download-codeplex.sec.s-msft.com/Download?ProjectName=c3d&DownloadId=746151" alt="" width="643" height="393"></p>
<p>The C3D format is a public domain, binary file format that has been used in Biomechanics, Animation and Gait Analysis laboratories to record synchronized 3D and analog data.</p>
<p>For more information on C3D file, please visit <a href="http://www.c3d.org" target="_blank">
http://www.c3d.org</a></p>
<h2><strong>Examle Usage</strong></h2>
<p>You can use these code to load a C3D file and output all points.</p>
<pre>C3DFile file = C3DFile.LoadFromFile(&quot;FILE PATH&quot;);
Int16 firstFrameIndex = file.Header.FirstFrameIndex;
Int16 pointCount = file.Parameters[&quot;POINT:USED&quot;].GetData&lt;Int16&gt;();

for (Int16 i = 0; i &lt; file.AllFrames.Count; i&#43;&#43;)
{
    for (Int16 j = 0; j &lt; pointCount; j&#43;&#43;)
    {
        Console.WriteLine(&quot;Frame {0} : X = {1}, Y = {2}, Z = {3}&quot;,
            firstFrameIndex &#43; i,
            file.AllFrames[i].Point3Ds[j].X,
            file.AllFrames[i].Point3Ds[j].Y ,
            file.AllFrames[i].Point3Ds[j].Z);
    }
}
</pre>
<h2><strong>More</strong></h2>
<p>Codeplex Page:&nbsp;<a href="https://c3d.codeplex.com" target="_blank">https://c3d.codeplex.com</a></p>
<p>Documentation:&nbsp;<a href="https://c3d.codeplex.com/documentation" target="_blank">https://c3d.codeplex.com/documentation</a></p>
<p>Github Page:&nbsp;<a href="https://github.com/mayswind/C3D.NET" target="_blank">https://github.com/mayswind/C3D.NET/</a></p>
<p>A Chinese article about C3D and this project:&nbsp;<a href="http://www.cnblogs.com/mayswind/p/3369090.html" target="_blank">http://www.cnblogs.com/mayswind/p/3369090.html</a></p>
