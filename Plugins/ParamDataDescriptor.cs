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
using System.Drawing.Design;
using BTreeEditor.Data;

namespace BTreeEditor.Plugins
{
	/// <summary>
	/// 参数数据的属性描述类
	/// </summary>
	public class ParamDataDescriptor : PropertyDescriptor
	{
		/// <summary>
		/// 当前的参数数据对象
		/// </summary>
		private readonly ParamData prop;
		/// <summary>
		/// 创建参数数据属性描述对象
		/// </summary>
		/// <param name="prop"></param>
        public ParamDataDescriptor(ParamData prop) : base(prop.paramDesc, null)
        {
            this.prop = prop;
        }
        /// <summary>
        /// 所属分类
        /// </summary>
        public override string Category { get { return "参数"; } }
        /// <summary>
        /// 属性描述
        /// </summary>
        public override string Description { get { return prop.toolTips; } }
        /// <summary>
        /// 属性名称
        /// </summary>
        public override string Name { get { return prop.paramDesc; } }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public override bool ShouldSerializeValue(object component) 
        {
        	return true;
        }
        public override void ResetValue(object component) 
        { 
        	this.prop.paramValue = null;
        }
        public override bool IsReadOnly { get { return false; } }
        public override Type PropertyType 
        { 
        	get
        	{
        		switch(prop.paramType)
        		{
        			case EnumParamType.INT:
        				return typeof(int);
        			case EnumParamType.FLOAT:
        				return typeof(float);
        			case EnumParamType.FILENAME:
        				return typeof(FileNameEditor);
        			case EnumParamType.PATH:
        				return typeof(FolderNameEditor);
        			default:
        				return typeof(string);
        		}
        	}
        }
        
		public override AttributeCollection Attributes {
			get 
			{
				if(prop.paramType == EnumParamType.FILENAME)
				{
					var a = new EditorAttribute(typeof(FileNameEditor), typeof(UITypeEditor));
					return AppendAttributeCollection(base.Attributes, a);
				}
				return base.Attributes;
			}
		}
        
        public override bool CanResetValue(object component) 
        { 
        	return true; 
        }
        public override Type ComponentType 
        { 
        	get 
        	{
        		
        		return typeof(ArgumentObject);
        	}
        	
        }
        public override void SetValue(object component, object value) 
        { 
        	this.prop.paramValue = value;
        }
        public override object GetValue(object component) 
        { 
        	return this.prop.paramValue;
        }
        
        static public AttributeCollection AppendAttributeCollection(AttributeCollection existing, params Attribute[] newAttrs)
        {
            return new AttributeCollection(AppendAttributes(existing, newAttrs));
        }

        static public Attribute[] AppendAttributes(AttributeCollection existing,  params Attribute[] newAttrs)
        {
            Attribute[] attributes;
            Attribute[] newArray = new Attribute[existing.Count + newAttrs.Length];
            int actualCount = existing.Count;
            existing.CopyTo(newArray, 0);

            for (int idx = 0; idx < newAttrs.Length; idx++)
            {
                if (newAttrs[idx] == null)
                {
                    throw new ArgumentNullException("newAttrs");
                }

                // Check if this attribute is already in the existing
                // array.  If it is, replace it.
                bool match = false;
                for (int existingIdx = 0; existingIdx < existing.Count; existingIdx++)
                {
                    if (newArray[existingIdx].TypeId.Equals(newAttrs[idx].TypeId))
                    {
                        match = true;
                        newArray[existingIdx] = newAttrs[idx];
                        break;
                    }
                }

                if (!match)
                {
                    newArray[actualCount++] = newAttrs[idx];
                }
            }

            // If some attributes were collapsed, create a new array.
            if (actualCount < newArray.Length)
            {
                attributes = new Attribute[actualCount];
                Array.Copy(newArray, 0, attributes, 0, actualCount);
            }
            else
            {
                attributes = newArray;
            }

            return attributes;
        }
	}
}


