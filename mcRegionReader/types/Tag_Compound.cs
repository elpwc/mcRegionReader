using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mcRegionReader.types
{
    class Tag_Compound : Tag
    {
        public Tag_Compound(string name)
        {
            this.setType(Type.TAG_Compound);
            this.setName(name);
        }
        public Tag_Compound(string name, byte value)
        {
            this.setType(Type.TAG_Compound);
            this.setName(name);
            this.setValue(value);
        }
        public byte value;
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
