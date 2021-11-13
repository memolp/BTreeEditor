/*
 * User: 覃贵锋
 * Date: 2021/11/4
 * Time: 17:26
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using BTreeEditor.Data.Nodes;
using BTreeEditor.Data.Nodes.Branch;

namespace BTreeEditor.Data
{
	/// <summary>
	/// Description of BTreeWorkspaceUtils.
	/// </summary>
	public static class BTreeWorkspaceUtils
	{
		public static void Save(BTreeWorkspaceData wk)
		{
			IFormatter formater = new BinaryFormatter();
			using (FileStream fs = new FileStream(wk.WorkPath, FileMode.Create))
			{
				formater.Serialize(fs, wk);
			}
		}
		
		public static BTreeWorkspaceData Load(string path)
		{
			IFormatter formater = new BinaryFormatter();
			using (FileStream fs = new FileStream(path, FileMode.Open))
			{
				BTreeWorkspaceData wk = (BTreeWorkspaceData)formater.Deserialize(fs);
				wk.WorkPath = path;
				return wk;
			}
		}
		/// <summary>
		/// 递归写入全部节点的数据
		/// </summary>
		/// <param name="xw"></param>
		/// <param name="node"></param>
		private static void WriteBTreeNodeData(XmlTextWriter xw, BTreeNode node)
		{
			if(node == null) return;
			
			xw.WriteStartElement("node");
			xw.WriteAttributeString("class", node.ClassName);
			xw.WriteAttributeString("uid", node.NodeID);
			switch(node.NodeType)
			{
				case BTreeNodeType.ACTION_EMPTY:
				case BTreeNodeType.TRUE_CONDITION:
				case BTreeNodeType.FALSE_CONDITION:
					break;
				case BTreeNodeType.ACTION:
					xw.WriteStartElement("property");
					xw.WriteAttributeString("Method", ((ActionNode)node).Method);
					if(((ActionNode)node).Argument != null)
					{
						foreach (ParamData param in ((ActionNode)node).Argument.Arguments) 
						{
							xw.WriteStartElement("param");
							xw.WriteString((string)param.paramValue);
							xw.WriteEndElement();
						}
					}
					xw.WriteEndElement();
					foreach (BTreeNode element in node.Nodes) 
					{
						WriteBTreeNodeData(xw, element);
					}
					break;
				case BTreeNodeType.CONDITION:
					xw.WriteStartElement("property");
					xw.WriteAttributeString("Operator", ((ConditionNode)node).Method);
					if(((ConditionNode)node).Argument != null)
					{
						foreach (ParamData param in ((ConditionNode)node).Argument.Arguments) 
						{
							xw.WriteStartElement("param");
							xw.WriteString((string)param.paramValue);
							xw.WriteEndElement();
						}
					}
					xw.WriteEndElement();
					break;
				case BTreeNodeType.SEQUENCE:
				case BTreeNodeType.SELECTOR:
				case BTreeNodeType.AND_CONDITION:
				case BTreeNodeType.OR_CONDITION:
				case BTreeNodeType.PRE_CONDITION:
				case BTreeNodeType.BREAK_CONDITION:
					foreach (BTreeNode element in node.Nodes) 
					{
						WriteBTreeNodeData(xw, element);
					}
					break;
				case BTreeNodeType.IFELSE:
					WriteBTreeNodeData(xw, ((IfElseNode)node).ConditionNode.Nodes[0]);
					WriteBTreeNodeData(xw, ((IfElseNode)node).TrueNode.Nodes[0]);
					WriteBTreeNodeData(xw, ((IfElseNode)node).FalseNode.Nodes[0]);
					break;
				case BTreeNodeType.LOOP:
					xw.WriteStartElement("property");
					xw.WriteAttributeString("Count", ((LoopNode)node).LoopCount.ToString());
					xw.WriteEndElement();
					foreach (BTreeNode element in node.Nodes) 
					{
						WriteBTreeNodeData(xw, element);
					}
					break;
				case BTreeNodeType.REFER_BTREE:
					xw.WriteStartElement("property");
					xw.WriteAttributeString("file", ((ReferenceBTreeNode)node).btreeFile);
					xw.WriteEndElement();
					break;
			}
			xw.WriteEndElement();
		}
		/// <summary>
		/// 导出行为树数据xml格式的
		/// </summary>
		/// <param name="data"></param>
		/// <param name="export_file"></param>
		public static void ExportXMLBTreeData(BTreeVisualData data, string export_file)
		{
			using (FileStream fs = new FileStream(export_file, FileMode.Create))
			using (XmlTextWriter xw = new XmlTextWriter(fs, Encoding.UTF8))
			{
				xw.Formatting = Formatting.Indented;
				xw.WriteStartDocument();
				xw.WriteStartElement("behavior");
				xw.WriteAttributeString("name", Path.GetFileNameWithoutExtension(export_file));
				xw.WriteAttributeString("version", "1");
				
				RootNode node = data.RootNode;
				if(node.NodeCount > 0)
					WriteBTreeNodeData(xw, node.Nodes[0]);
				
				xw.WriteEndElement(); // behavior
				xw.WriteEndDocument();
			}
		}
		/// <summary>
		/// 保存行为数据
		/// </summary>
		/// <param name="data"></param>
		public static void SaveBTreeData(BTreeVisualData data)
		{
			IFormatter formater = new BinaryFormatter();
			using (FileStream fs = new FileStream(data.FileName, FileMode.Create))
			{
				formater.Serialize(fs, data);
			}
		}
		/// <summary>
		/// 加载行为树数据
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static BTreeVisualData LoadBTreeData(string path)
		{
			IFormatter formater = new BinaryFormatter();
			using (FileStream fs = new FileStream(path, FileMode.Open))
			{
				BTreeVisualData data = (BTreeVisualData)formater.Deserialize(fs);
				data.FileName = path;
				return data;
			}
		}
	}
}
