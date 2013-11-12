using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace C3D.DataViewer
{
    internal static class TreeListConverter
    {
        internal static TreeNode GetOverviewNode(C3DFile file)
        {
            TreeNode overviewNode = new TreeNode("Overview") { Tag = "OVERVIEW" };

            return overviewNode;
        }

        internal static TreeNode GetHeaderNode(C3DFile file)
        {
            TreeNode headerNode = new TreeNode("Header") { Tag = "HEADER" };
            TreeNode headerEventNode = new TreeNode("Events") { Tag = "EVENTS" };

            headerNode.Nodes.Add(headerEventNode);

            return headerNode;
        }

        internal static TreeNode GetParametersNode(C3DFile file)
        {
            TreeNode treeNode = new TreeNode("Parameters") { Tag = "PARAMETERS" };

            foreach (C3DParameterGroup group in file.Parameters)
            {
                TreeNode treeGroup = new TreeNode(group.Name) { Tag = "PARAMETERS_GROUP|" + group.Name };

                foreach (C3DParameter param in group)
                {
                    TreeNode treeParam = new TreeNode(param.Name) { Tag = "PARAMETERS_ITEM|" + group.Name + ":" + param.Name };
                    treeGroup.Nodes.Add(treeParam);
                }

                treeNode.Nodes.Add(treeGroup);
            }

            return treeNode;
        }

        internal static TreeNode Get3DDataNode(C3DFile file)
        {
            TreeNode treeNode = new TreeNode("3D Data") { Tag = "3D" };
            String[] labels = (file.Parameters.ContainsParameter("POINT", "LABELS") ? file.Parameters["POINT", "LABELS"].GetData<String[]>() : null);

            if (file.AllFrames.Count > 0)
            {
                for (Int32 i = 0; i < file.AllFrames[0].Point3Ds.Length; i++)
                {
                    String name = (labels != null && labels.Length > i ? labels[i].TrimEnd() : String.Format("POINT {0}", i.ToString()));
                    TreeNode treePoint = new TreeNode(String.Format("{0}<{1}>", (i + 1).ToString(), name)) { Tag = "3D|" + i.ToString() };
                    treeNode.Nodes.Add(treePoint);
                }
            }

            return treeNode;
        }

        internal static TreeNode GetAnalogDataNode(C3DFile file)
        {
            TreeNode treeNode = new TreeNode("Analog Data") { Tag = "ANALOG" };
            String[] labels = (file.Parameters.ContainsParameter("ANALOG", "LABELS") ? file.Parameters["ANALOG", "LABELS"].GetData<String[]>() : null);

            if (file.AllFrames.Count > 0)
            {
                for (Int32 i = 0; i < file.AllFrames[0].AnalogSamples.Length; i++)
                {
                    String name = (labels != null && labels.Length > i ? labels[i].TrimEnd() : String.Format("CHANNEL {0}", i.ToString()));
                    TreeNode treePoint = new TreeNode(String.Format("{0}<{1}>", (i + 1).ToString(), name)) { Tag = "ANALOG|" + i.ToString() };
                    treeNode.Nodes.Add(treePoint);
                }
            }

            return treeNode;
        }
    }
}