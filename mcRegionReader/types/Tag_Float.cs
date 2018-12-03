using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mcRegionReader.types
{
    class Tag_Float:Tag
    {
        public Tag_Float(string name)
        {
            this.setType(Type.TAG_Float);
            this.setName(name);
        }
        public Tag_Float(string name, float value)
        {
            this.setType(Type.TAG_Float);
            this.setName(name);
            this.setValue(value);
        }
        public float value = 0.0F;
        public override void setValue(object value)
        {
            this.value = (float)value;
        }
        public override object getValue()
        {
            return value;
        }
        public override bool compareTo(Tag tag)
        {
            if (getName() == tag.getName() && ((float)getValue()).CompareTo((float)tag.getValue())==0 && getType() == tag.getType())
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
