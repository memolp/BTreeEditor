/*
 * User: 覃贵锋
 * Date: 2021/11/5
 * Time: 10:59
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using BTreeEditor.Data;

namespace BTreeEditor.Plugins
{
	/// <summary>
	/// ArgumentObjectConverter 参数转换对象
	/// </summary>
	public class ArgumentObjectConverter : ExpandableObjectConverter
	{
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            var stdProps = base.GetProperties(context, value, attributes);
            ArgumentObject obj = value as ArgumentObject;
            List<ParamData> customProps = obj == null ? null : obj.Arguments;
            PropertyDescriptor[] props = new PropertyDescriptor[stdProps.Count + (customProps == null ? 0 : customProps.Count)];
            stdProps.CopyTo(props, 0);
            if (customProps != null)
            {
                int index = stdProps.Count;
                foreach (ParamData prop in customProps)
                {
                    props[index++] = new ParamDataDescriptor(prop);
                }
            }
         
            PropertyDescriptorCollection res = new PropertyDescriptorCollection(props);
            return res;
        }
	}
	
	
}
