using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mcRegionReader.types
{
    class Tag_List : Tag
    {
        public Tag_List(string name, Type listType)
        {
            this.setType(Type.TAG_List);
            this.setName(name);
            this.setListType(listType);
        }
        public Tag_List(string name, byte value, Type listType)
        {
            this.setType(Type.TAG_List);
            this.setName(name);
            this.setValue(value);
            this.setListType(listType);
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
