using System;
using System.Collections.Generic;

namespace mcRegionReader.types
{
	public class Tag_Int:Tag
	{
        public Tag_Int( string name)
        {
            this.setType(Type.TAG_Int);
            this.setName(name);
        }
        public Tag_Int(string name, int value)
        {
            this.setType(Type.TAG_Int);
            this.setName(name);
            this.setValue(value);
        }
        public int value = 0;
        public override void setValue(object value)
        {
            this.value = (int)value;
        }
        public override object getValue()
        {
            return value;
        }
        //public override int getValueLength()
        //{
        //    return (int)Math.Ceiling(Convert.ToString(value, 16).Length/2.0);
        //}
        public override bool compareTo(Tag tag)
        {
            if (getName() == tag.getName() && getValue() == tag.getValue() && getType() == tag.getType()) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}