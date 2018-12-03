using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mcRegionReader.types
{
    class Tag_Double:Tag
    {
        public Tag_Double(string name)
        {
            this.setType(Type.TAG_Double);
            this.setName(name);
        }
        public Tag_Double(string name, double value)
        {
            this.setType(Type.TAG_Double);
            this.setName(name);
            this.setValue(value);
        }
        public double value = 0.0D;
        public override void setValue(object value)
        {
            this.value = (double)value;
        }
        public override object getValue()
        {
            return value;
        }
        public override bool compareTo(Tag tag)
        {
            if (getName() == tag.getName() && ((double)getValue()).CompareTo((double)tag.getValue()) == 0 && getType() == tag.getType())
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
