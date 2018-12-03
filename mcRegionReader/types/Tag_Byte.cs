using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mcRegionReader.types
{
    public class Tag_Byte:Tag
    {
        public Tag_Byte(string name)
        {
            this.setType(Type.TAG_Byte);
            this.setName(name);
        }
        public Tag_Byte(string name, byte value)
        {
            this.setType(Type.TAG_Byte);
            this.setName(name);
            this.setValue(value);
        }
        public byte value = 0;
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
