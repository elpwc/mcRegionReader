using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mcRegionReader.types
{
    class Tag_Long:Tag
    {
        public Tag_Long(string name)
        {
            this.setType(Type.TAG_Long);
            this.setName(name);
        }
        public Tag_Long(string name, long value)
        {
            this.setType(Type.TAG_Long);
            this.setName(name);
            this.setValue(value);
        }
        public long value = 0;
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
