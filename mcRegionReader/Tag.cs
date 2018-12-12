using System;
using System.IO;

namespace mcRegionReader
{
    /// <summary>
    /// NBT标签的数据类型。
    /// </summary>
    public enum TagType
    {
        TAG_End = 0,
        TAG_Byte = 1,
        TAG_Short = 2,
        TAG_Int = 3,
        TAG_Long = 4,
        TAG_Float = 5,
        TAG_Double = 6,
        TAG_Byte_Array = 7,
        TAG_String = 8,
        TAG_List = 9,
        TAG_Compound = 10,
        TAG_Int_Array = 11,
        TAG_Long_Array = 12

    }
    /// <summary>
    /// 提供对NBT标签的读写方法。
    /// </summary>
    public class Tag
    {

        /// <summary>
        /// 取得对应id的NBT数据类型
        /// </summary>
        /// <param name="index">id</param>
        /// <returns>对应的NBT数据类型</returns>
        private static TagType GetTypeByIndex(int index)
        {
            return (TagType)index;
           // return (Type)Enum.GetValues(typeof(Type)).GetValue(index);
        }
        /// <summary>
        /// 此NBT标签的数据类型。
        /// </summary>
        public TagType type = TagType.TAG_End;
        /// <summary>
        /// 此NBT标签的名称。
        /// </summary>
        public string name = "";
        /// <summary>
        /// <para>此NBT标签的值</para>
        /// 若数据类型为<see cref="TagType.TAG_Compound"/>或<see cref="TagType.TAG_List"/>，则<see cref="value"/>以<see cref="Tag[]"/>数组形式储存。
        /// </summary>
        public object value = null;
        /// <summary>
        /// <para>若数据类型为<see cref="TagType.TAG_List"/>，则此项储存所有子项的统一的数据类型。</para>
        /// 否则，该项值为<see cref="TagType.TAG_End"/>。
        /// </summary>
        public TagType listType = TagType.TAG_End;

        public Tag(TagType type, string name, Tag[] value) : this(type, name, (object)value)
        { }
        public Tag(string name, TagType listType) : this(TagType.TAG_List, name, listType)
        { }
        public Tag(TagType type, string name, object value)
        {
            switch (type)
            {
                case TagType.TAG_End:
                    if (value != null)
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case TagType.TAG_Byte:
                    if (!(value is byte))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case TagType.TAG_Short:
                    if (!(value is short))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case TagType.TAG_Int:
                    if (!(value is int))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case TagType.TAG_Long:
                    if (!(value is long))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case TagType.TAG_Float:
                    if (!(value is float))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case TagType.TAG_Double:
                    if (!(value is double))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case TagType.TAG_Byte_Array:
                    if (!(value is byte[]))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case TagType.TAG_String:
                    if (!(value is string))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case TagType.TAG_List:
                    if (value is TagType)
                    {
                        this.listType = (TagType)value;
                        value = new Tag[0];
                    }
                    else
                    {
                        if (!(value is Tag[]))
                        {
                            throw new Exception("IllegalArgument");
                        }
                        this.listType = ((Tag[])value)[0].GetTagType();
                    }
                    break;
                case TagType.TAG_Compound:
                    if (!(value is Tag[]))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case TagType.TAG_Int_Array:
                    if (!(value is int[]))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case TagType.TAG_Long_Array:
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

        public TagType GetTagType()
        {
            return type;
        }
        public string GetName()
        {
            return name;
        }
        public object GetValue()
        {
            return value;
        }

        public void SetValue(object newValue)
        {
            switch (type)
            {
                case TagType.TAG_End:
                    if (value != null)
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case TagType.TAG_Byte:
                    if (!(value is byte))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case TagType.TAG_Short:
                    if (!(value is short))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case TagType.TAG_Int:
                    if (!(value is int))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case TagType.TAG_Long:
                    if (!(value is long))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case TagType.TAG_Float:
                    if (!(value is float))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case TagType.TAG_Double:
                    if (!(value is double))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case TagType.TAG_Byte_Array:
                    if (!(value is byte[]))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case TagType.TAG_String:
                    if (!(value is string))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case TagType.TAG_List:
                    if (value is TagType)
                    {
                        this.listType = (TagType)value;
                        value = new Tag[0];
                    }
                    else
                    {
                        if (!(value is Tag[]))
                        {
                            throw new Exception("IllegalArgument");
                        }
                        this.listType = ((Tag[])value)[0].GetTagType();
                    }
                    break;
                case TagType.TAG_Compound:
                    if (!(value is Tag[]))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case TagType.TAG_Int_Array:
                    if (!(value is int[]))
                    {
                        throw new Exception("IllegalArgument");
                    }
                    break;
                case TagType.TAG_Long_Array:
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

        public TagType GetListTagType()
        {
            return listType;
        }

        public void AddTag(Tag tag)
        {
            if (type != TagType.TAG_List && type != TagType.TAG_Compound)
            {
                throw new Exception("RuntimeException");
            }
            Tag[] subtags = (Tag[])value;
            int index = subtags.Length;

            if (type == TagType.TAG_Compound) index--;
            InsertTag(tag, index);
        }
        public void InsertTag(Tag tag, int index)
        {
            if (type != TagType.TAG_List && type != TagType.TAG_Compound)
            {
                throw new Exception("RuntimeException");
            }

            Tag[] subtags = (Tag[])value;
            if (subtags.Length > 0)
            {
                if (type == TagType.TAG_List && tag.GetTagType() != GetListTagType())
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
        public Tag RemoveTag(int index)
        {
            if (type != TagType.TAG_List && type != TagType.TAG_Compound)
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
        public void RemoveSubTag(Tag tag)
        {
            if (type != TagType.TAG_List && type != TagType.TAG_Compound)
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
                    RemoveTag(i);
                    return;
                }
                else
                {
                    if (subtags[i].type == TagType.TAG_List || subtags[i].type == TagType.TAG_Compound)
                    {
                        subtags[i].RemoveSubTag(tag);
                    }
                }
            }
        }
        public Tag FindTagByName(string name)
        {
            return FindNextTagByName(name, null);
        }
        public Tag FindNextTagByName(string name, Tag found)
        {
            if (type != TagType.TAG_List && type != TagType.TAG_Compound)
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
                    Tag newFound = subtag.FindTagByName(name);
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

        public int GetSubtagsCount()
        {
            switch (type)
            {
                case TagType.TAG_Byte_Array:
                    return ((byte[])GetValue()).Length;
                case TagType.TAG_Compound:
                    return ((Tag[])GetValue()).Length;
                case TagType.TAG_Int_Array:
                    return ((int[])GetValue()).Length;
                case TagType.TAG_Long_Array:
                    return ((long[])GetValue()).Length;
                case TagType.TAG_List:
                    return ((Tag[])GetValue()).Length;
                default:
                    return 0;
            }
        }


        public static Tag ReadFrom(Stream st)
        {
            BinaryReader br = new BinaryReader(st);
            byte type = br.ReadByte();
            Tag tag = null;
            if (type == 0)
            {
                tag = new Tag(TagType.TAG_End, null, null);
            }
            else if (Enum.GetValues(typeof(TagType)).Length > type)
            {
                //string name = readUTF(br);
                tag = new Tag(GetTypeByIndex(type), BinaryJava.ReadUTF(br), ReadPayload(br, type));
            }
            br.Close();
            return tag;

        }

        private static object ReadPayload(BinaryReader br, byte type)
        {
            switch (type)
            {
                case 0:
                    return null;
                case 1:
                    return br.ReadByte();
                case 2:
                    return BinaryJava.ReadShort(br);
                case 3:
                    return BinaryJava.ReadInt(br);
                case 4:
                    return BinaryJava.ReadLong(br); ;
                case 5:
                    return BinaryJava.ReadFloat(br);
                case 6:
                    return BinaryJava.ReadDouble(br);
                case 7:
                    int length = BinaryJava.ReadInt(br);
                    return br.ReadBytes(length);
                case 8:
                    return BinaryJava.ReadUTF(br);
                case 9:
                    byte lt = br.ReadByte();
                    int ll = BinaryJava.ReadInt(br);
                    Tag[] lo = new Tag[ll];
                    for (int i = 0; i < ll; i++)
                    {
                        lo[i] = new Tag(GetTypeByIndex(lt), null, ReadPayload(br, lt));
                    }
                    if (lo.Length == 0)
                    {
                        return GetTypeByIndex(lt);
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
                            name = BinaryJava.ReadUTF(br);
                        }
                        Tag[] newTags = new Tag[tags.Length + 1];
                        Array.Copy(tags, 0, newTags, 0, tags.Length);
                        newTags[tags.Length] = new Tag(GetTypeByIndex(stt), name, ReadPayload(br, stt));
                        tags = newTags;
                    } while (stt != 0);
                    return tags;
                case 11:
                    int len = BinaryJava.ReadInt(br);
                    int[] ia = new int[len];
                    for (int i = 0; i < len; i++)
                    {
                        ia[i] = BinaryJava.ReadInt(br);
                    }
                    return ia;
                case 12:
                    int len_ = BinaryJava.ReadInt(br);
                    long[] ia_ = new long[len_];
                    for (int i = 0; i < len_; i++)
                    {
                        ia_[i] = BinaryJava.ReadLong(br);
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
        public void WriteTo(Stream st)
        {
            BinaryWriter bw = new BinaryWriter(st);
            bw.Write((byte)Convert.ToInt32(type));
            if (type!=TagType.TAG_End)
            {
                BinaryJava.WriteUTF(bw,name);
                WritePayload(bw);
            }
        }

        private void WritePayload(BinaryWriter bw)
        {
            switch (type)
            {
                case TagType.TAG_End:
                    break;
                case TagType.TAG_Byte:
                    bw.Write((byte)value);
                    break;
                case TagType.TAG_Short:
                    BinaryJava.WriteShort(bw,(short)value);
                    break;
                case TagType.TAG_Int:
                    BinaryJava.WriteInt(bw,(int)value);
                    break;
                case TagType.TAG_Long:
                    BinaryJava.WriteLong(bw, (long)value);
                    break;
                case TagType.TAG_Float:
                    BinaryJava.WriteFloat(bw, (float)value);
                    break;
                case TagType.TAG_Double:
                    BinaryJava.WriteDouble(bw,(double)value);
                    break;
                case TagType.TAG_Byte_Array:
                    byte[] ba = (byte[])value;
                    BinaryJava.WriteInt(bw,ba.Length);
                    bw.Write(ba);
                    break;
                case TagType.TAG_String:
                    BinaryJava.WriteUTF(bw, (string)value);
                    break;
                case TagType.TAG_List:
                    Tag[] list = (Tag[])value;
                    bw.Write((byte)Convert.ToInt32( GetListTagType()));
                    BinaryJava.WriteInt(bw, list.Length);
                    foreach (Tag tt in list)
                    {
                        tt.WritePayload(bw);
                    }
                    break;
                case TagType.TAG_Compound:
                    Tag[] subtags = (Tag[])value;
                    foreach (Tag st in subtags)
                    {
                        Tag subtag = st;
                        TagType type = subtag.GetTagType();
                        bw.Write((byte)Convert.ToInt32(type));
                        if (type!=TagType.TAG_End)
                        {
                            BinaryJava.WriteUTF(bw, (subtag.GetName()));
                            subtag.WritePayload(bw);
                        }
                    }
                    break;
                case TagType.TAG_Int_Array:
                    int[] ia = (int[])value;
                    BinaryJava.WriteInt(bw, ia.Length);
                    for (int i = 0; i < ia.Length; i++)
                    {
                        BinaryJava.WriteInt(bw, ia[i]);
                    }
                    break;
                case TagType.TAG_Long_Array:
                    long[] la = (long[])value;
                    BinaryJava.WriteInt(bw, la.Length);
                    for (int i = 0; i < la.Length; i++)
                    {
                        BinaryJava.WriteLong(bw, la[i]);
                    }
                    break;
                default:
                    break;
            }
        }




    }



}
