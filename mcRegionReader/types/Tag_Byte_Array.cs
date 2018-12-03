using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mcRegionReader.types
{
    class Tag_Byte_Array:Tag
    {
        public Tag_Byte_Array(string name)
        {
            this.setType(Type.TAG_Byte_Array);
            this.setName(name);
            this.setListType(Type.TAG_Byte);
        }
        public Tag_Byte_Array(string name, byte value)
        {
            this.setType(Type.TAG_Byte_Array);
            this.setName(name);
            this.setValue(value);
            this.setListType(Type.TAG_Byte);
        }
        public byte value ;
        public override void setValue(object value)
        {
            this.value = (byte)value;
        }
        public override object getValue()
        {
            return value;
        }
        public override bool compareTo(Tag tag)
        {
            if (getName() == tag.getName() && compareToList(tag.getTags()) && getType() == tag.getType() && getListType() == tag.getListType()) 
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
