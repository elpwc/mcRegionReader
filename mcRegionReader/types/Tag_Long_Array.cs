using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mcRegionReader.types
{
    class Tag_Long_Array : Tag
    {
        public Tag_Long_Array(string name)
        {
            this.setType(Type.TAG_Long_Array);
            this.setName(name);
            this.setListType(Type.TAG_Long);
        }
        public Tag_Long_Array(string name, byte value)
        {
            this.setType(Type.TAG_Long_Array);
            this.setName(name);
            this.setValue(value);
            this.setListType(Type.TAG_Int);
        }
        public  long value;
        public override void setValue(object value)
        {
            this.value = (long)value;
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
