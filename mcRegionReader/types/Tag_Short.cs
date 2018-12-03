using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mcRegionReader.types
{
    class Tag_Short:Tag
    {
        public Tag_Short( string name)
        {
            this.setType(Type.TAG_Short);
            this.setName(name);
        }
        public Tag_Short( string name, short value)
        {
            this.setType(Type.TAG_Short);
            this.setName(name);
            this.setValue(value);
        }
        public short value = 0;
        public override void setValue(object value)
        {
            this.value = (short)value;
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
