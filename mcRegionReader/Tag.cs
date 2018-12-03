using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace mcRegionReader
{
    /// <summary>
    /// 提供对NBT标签的读写方法。
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// NBT标签的数据类型。
        /// </summary>
        public enum Type
        {
            TAG_End=0,
            TAG_Byte=1,
            TAG_Short=2,
            TAG_Int=3,
            TAG_Long=4,
            TAG_Float=5,
            TAG_Double=6,
            TAG_Byte_Array=7,
            TAG_String=8,
            TAG_List=9,
            TAG_Compound=10,
            TAG_Int_Array=11,
            TAG_Long_Array=12

        }
        /// <summary>
        /// 取得对应id的NBT数据类型
        /// </summary>
        /// <param name="index">id</param>
        /// <returns>对应的NBT数据类型</returns>
        private static Type getTypeByIndex(int index)
        {
            return (Type)index;
           // return (Type)Enum.GetValues(typeof(Type)).GetValue(index);
        }
        /// <summary>
        /// 此NBT标签的数据类型。
        /// </summary>
        public Type type = Type.TAG_End;
        /// <summary>
        /// 此NBT标签的名称。
        /// </summary>
        public string name = "";
        /// <summary>
        /// <para>此NBT标签的值</para>
        /// 若数据类型为<see cref="Type.TAG_Compound"/>或<see cref="Type.TAG_List"/>，则<see cref="value"/>以<see cref="Tag[]"/>数组形式储存。
        /// </summary>
        public object value = null;
        /// <summary>
        /// <para>若数据类型为<see cref="Type.TAG_List"/>，则此项储存所有子项的统一的数据类型。</para>
        /// 否则，该项值为<see cref="Type.TAG_End"/>。
        /// </summary>
        public Type listType = Type.TAG_End;

        public Tag(Type type, string name, Tag[] value) : this(type, name, (object)value)
        { }
        public Tag(string name, Type listType) : this(Type.TAG_List, name, listType)
        { }
        public Tag(Type type, string name, object value)
        {
            switch (type)
            {
                case Type.TAG_End:
                    if (value != null)
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case Type.TAG_Byte:
                    if (!(value is byte))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case Type.TAG_Short:
                    if (!(value is short))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case Type.TAG_Int:
                    if (!(value is int))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case Type.TAG_Long:
                    if (!(value is long))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case Type.TAG_Float:
                    if (!(value is float))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case Type.TAG_Double:
                    if (!(value is double))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case Type.TAG_Byte_Array:
                    if (!(value is byte[]))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case Type.TAG_String:
                    if (!(value is string))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case Type.TAG_List:
                    if (value is Type)
                    {
                        this.listType = (Type)value;
                        value = new Tag[0];
                    }
                    else
                    {
                        if (!(value is Tag[]))
                        {
                            throw new Exception("IllegalArgument");
                        }
                        this.listType = ((Tag[])value)[0].getType();
                    }
                    break;
                case Type.TAG_Compound:
                    if (!(value is Tag[]))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case Type.TAG_Int_Array:
                    if (!(value is int[]))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case Type.TAG_Long_Array:
                    if (!(value is long[]))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                default:
                    throw new Exception("IllegalArgument");
            }
            this.type = type;
            this.name = name;
            this.value = value;
        }

        public Type getType()
        {
            return type;
        }
        public string getName()
        {
            return name;
        }
        public object getValue()
        {
            return value;
        }

        public void setValue(object newValue)
        {
            switch (type)
            {
                case Type.TAG_End:
                    if (value != null)
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case Type.TAG_Byte:
                    if (!(value is byte))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case Type.TAG_Short:
                    if (!(value is short))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case Type.TAG_Int:
                    if (!(value is int))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case Type.TAG_Long:
                    if (!(value is long))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case Type.TAG_Float:
                    if (!(value is float))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case Type.TAG_Double:
                    if (!(value is double))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case Type.TAG_Byte_Array:
                    if (!(value is byte[]))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case Type.TAG_String:
                    if (!(value is string))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case Type.TAG_List:
                    if (value is Type)
                    {
                        this.listType = (Type)value;
                        value = new Tag[0];
                    }
                    else
                    {
                        if (!(value is Tag[]))
                        {
                            throw new Exception("IllegalArgument");
                        }
                        this.listType = ((Tag[])value)[0].getType();
                    }
                    break;
                case Type.TAG_Compound:
                    if (!(value is Tag[]))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case Type.TAG_Int_Array:
                    if (!(value is int[]))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case Type.TAG_Long_Array:
                    if (!(value is long[]))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                default:
                    throw new Exception("IllegalArgument");
            }
            this.value = newValue;
        }

        public Type getListType()
        {
            return listType;
        }

        public void addTag(Tag tag)
        {
            if (type != Type.TAG_List && type != Type.TAG_Compound)
            {
                throw new Exception("RuntimeException");
            }
            Tag[] subtags = (Tag[])value;
            int index = subtags.Length;

            if (type == Type.TAG_Compound) index--;
            insertTag(tag, index);
        }
        public void insertTag(Tag tag, int index)
        {
            if (type != Type.TAG_List && type != Type.TAG_Compound)
            {
                throw new Exception("RuntimeException");
            }

            Tag[] subtags = (Tag[])value;
            if (subtags.Length > 0)
            {
                if (type == Type.TAG_List && tag.getType() != getListType())
                {
                    throw new Exception("IllegalArgument");
                }
            }
            if (index > subtags.Length)
            {
                throw new Exception("IndexOutOfBoundsException");
            }
            Tag[] newValue = new Tag[subtags.Length + 1];

            Array.Copy(subtags, 0, newValue, 0, index);
            newValue[index] = tag;
            Array.Copy(subtags, index, newValue, index + 1, subtags.Length - index);
            value = newValue;
        }
        public Tag removeTag(int index)
        {
            if (type != Type.TAG_List && type != Type.TAG_Compound)
            {
                throw new Exception("RuntimeException");
            }

            Tag[] subtags = (Tag[])value;
            Tag victim = subtags[index];
            Tag[] newValue = new Tag[subtags.Length - 1];
            Array.Copy(subtags, 0, newValue, 0, index);
            index++;
            Array.Copy(subtags, index, newValue, index - 1, subtags.Length - index);
            value = newValue;
            return victim;
        }
        public void removeSubTag(Tag tag)
        {
            if (type != Type.TAG_List && type != Type.TAG_Compound)
            {
                throw new Exception("RuntimeException");
            }
            if (tag == null)
            {
                return;
            }
            Tag[] subtags = (Tag[])value;
            for (int i = 0; i < subtags.Length; i++)
            {
                if (subtags[i] == tag)
                {
                    removeTag(i);
                    return;
                }
                else
                {
                    if (subtags[i].type == Type.TAG_List || subtags[i].type == Type.TAG_Compound)
                    {
                        subtags[i].removeSubTag(tag);
                    }
                }
            }
        }
        public Tag findTagByName(string name)
        {
            return findNextTagByName(name, null);
        }
        public Tag findNextTagByName(string name, Tag found)
        {
            if (type != Type.TAG_List && type != Type.TAG_Compound)
                return null;
            Tag[] subtags = (Tag[])value;
            foreach (Tag subtag in subtags)
            {
                if ((subtag.name == null && name == null) || (subtag.name != null && subtag.name.Equals(name)))
                {
                    return subtag;
                }
                else
                {
                    Tag newFound = subtag.findTagByName(name);
                    if (newFound != null)
                    {
                        if (newFound == found)
                        {
                            continue;
                        }
                        else
                        {
                            return newFound;
                        }
                    }
                }
            }
            return null;
        }

        public static Tag readFrom(Stream st)
        {
            BinaryReader br = new BinaryReader(st);
            byte type = br.ReadByte();
            Tag tag = null;
            if (type == 0)
            {
                tag = new Tag(Type.TAG_End, null, null);
            }
            else if (Enum.GetValues(typeof(Type)).Length > type)
            {
                //string name = readUTF(br);
                tag = new Tag(getTypeByIndex(type), BinaryJava.readUTF(br), readPayload(br, type));
            }
            br.Close();
            return tag;

        }

        private static object readPayload(BinaryReader br, byte type)
        {
            switch (type)
            {
                case 0:
                    return null;
                case 1:
                    return br.ReadByte();
                case 2:
                    return BinaryJava.readShort(br);
                case 3:
                    return BinaryJava.readInt(br);
                case 4:
                    return BinaryJava.readLong(br); ;
                case 5:
                    return BinaryJava.readFloat(br);
                case 6:
                    return BinaryJava.readDouble(br);
                case 7:
                    int length = BinaryJava.readInt(br);
                    return br.ReadBytes(length);
                case 8:
                    return BinaryJava.readUTF(br);
                case 9:
                    byte lt = br.ReadByte();
                    int ll = BinaryJava.readInt(br);
                    Tag[] lo = new Tag[ll];
                    for (int i = 0; i < ll; i++)
                    {
                        lo[i] = new Tag(getTypeByIndex(lt), null, readPayload(br, lt));
                    }
                    if (lo.Length == 0)
                    {
                        return getTypeByIndex(lt);
                    }
                    else
                    {
                        return lo;
                    }
                case 10:
                    byte stt;
                    Tag[] tags = new Tag[0];
                    do
                    {
                        stt = br.ReadByte();
                        string name = null;
                        if (stt != 0)
                        {
                            name = BinaryJava.readUTF(br);
                        }
                        Tag[] newTags = new Tag[tags.Length + 1];
                        Array.Copy(tags, 0, newTags, 0, tags.Length);
                        newTags[tags.Length] = new Tag(getTypeByIndex(stt), name, readPayload(br, stt));
                        tags = newTags;
                    } while (stt != 0);
                    return tags;
                case 11:
                    int len = BinaryJava.readInt(br);
                    int[] ia = new int[len];
                    for (int i = 0; i < len; i++)
                    {
                        ia[i] = BinaryJava.readInt(br);
                    }
                    return ia;
                case 12:
                    int len_ = BinaryJava.readInt(br);
                    long[] ia_ = new long[len_];
                    for (int i = 0; i < len_; i++)
                    {
                        ia_[i] = BinaryJava.readLong(br);
                    }
                    return ia_;
                default:
                    break;
            }
            return null;
        }

        /// <summary>
        /// 不要忘了Stream.Close(); !
        /// </summary>
        /// <param name="st"></param>
        public void writeTo(Stream st)
        {
            BinaryWriter bw = new BinaryWriter(st);
            bw.Write((byte)Convert.ToInt32(type));
            if (type!=Type.TAG_End)
            {
                BinaryJava.writeUTF(bw,name);
                writePayload(bw);
            }
        }

        private void writePayload(BinaryWriter bw)
        {
            switch (type)
            {
                case Type.TAG_End:
                    break;
                case Type.TAG_Byte:
                    bw.Write((byte)value);
                    break;
                case Type.TAG_Short:
                    BinaryJava.writeShort(bw,(short)value);
                    break;
                case Type.TAG_Int:
                    BinaryJava.writeInt(bw,(int)value);
                    break;
                case Type.TAG_Long:
                    BinaryJava.writeLong(bw, (long)value);
                    break;
                case Type.TAG_Float:
                    BinaryJava.writeFloat(bw, (float)value);
                    break;
                case Type.TAG_Double:
                    BinaryJava.writeDouble(bw,(double)value);
                    break;
                case Type.TAG_Byte_Array:
                    byte[] ba = (byte[])value;
                    BinaryJava.writeInt(bw,ba.Length);
                    bw.Write(ba);
                    break;
                case Type.TAG_String:
                    BinaryJava.writeUTF(bw, (string)value);
                    break;
                case Type.TAG_List:
                    Tag[] list = (Tag[])value;
                    bw.Write((byte)Convert.ToInt32( getListType()));
                    BinaryJava.writeInt(bw, list.Length);
                    foreach (Tag tt in list)
                    {
                        tt.writePayload(bw);
                    }
                    break;
                case Type.TAG_Compound:
                    Tag[] subtags = (Tag[])value;
                    foreach (Tag st in subtags)
                    {
                        Tag subtag = st;
                        Type type = subtag.getType();
                        bw.Write((byte)Convert.ToInt32(type));
                        if (type!=Type.TAG_End)
                        {
                            BinaryJava.writeUTF(bw, (subtag.getName()));
                            subtag.writePayload(bw);
                        }
                    }
                    break;
                case Type.TAG_Int_Array:
                    int[] ia = (int[])value;
                    BinaryJava.writeInt(bw, ia.Length);
                    for (int i = 0; i < ia.Length; i++)
                    {
                        BinaryJava.writeInt(bw, ia[i]);
                    }
                    break;
                case Type.TAG_Long_Array:
                    long[] la = (long[])value;
                    BinaryJava.writeInt(bw, la.Length);
                    for (int i = 0; i < la.Length; i++)
                    {
                        BinaryJava.writeLong(bw, la[i]);
                    }
                    break;
                default:
                    break;
            }
        }




    }



}
