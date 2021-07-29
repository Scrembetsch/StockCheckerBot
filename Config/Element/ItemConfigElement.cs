using System;
using System.Configuration;

namespace StockCheckerBot.Config.Element
{
    public class ItemConfigElementCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ItemConfigElement();
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((ItemConfigElement)element).Alias;
        }

        public ItemConfigElement this[int index]
        {
            get
            {
                return (ItemConfigElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        new public ItemConfigElement this[string Name]
        {
            get
            {
                return (ItemConfigElement)BaseGet(Name);
            }
        }

        public int IndexOf(ItemConfigElement url)
        {
            return BaseIndexOf(url);
        }

        public void Add(ItemConfigElement url)
        {
            BaseAdd(url);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        public void Remove(ItemConfigElement url)
        {
            if (BaseIndexOf(url) >= 0)
            {
                BaseRemove(url.Alias);
            }
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Clear()
        {
            BaseClear();
        }
    }

    public class ItemConfigElement : ConfigurationElement
    {
        private const string ProductIdId = "ProductId";
        private const string AliasId = "Alias";

        [ConfigurationProperty(ProductIdId)]
        public string ProductId
        {
            get
            {
                return (string)this[ProductIdId];
            }
            set
            {
                this[ProductIdId] = value;
            }
        }

        [ConfigurationProperty(AliasId)]
        public string Alias
        {
            get
            {
                return (string)this[AliasId];
            }
            set
            {
                this[AliasId] = value;
            }
        }
    }
}