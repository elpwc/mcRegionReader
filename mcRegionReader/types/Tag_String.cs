using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mcRegionReader.types
{
    class Tag_String:Tag
    {
        public Tag_String(string name)
        {
            this.setType(Type.TAG_String);
            this.setName(name);
        }
        public Tag_String(string name, string value)
        {
            this.setType(Type.TAG_String);
            this.setName(name);
            this.setValue(value);
        }
        public string value = string.Empty;
        public override void setValue(object value)
        {
            this.value = (string)value;
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
