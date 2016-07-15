using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public interface IBxModifyManage
    {
        void ResetModifyFlag();
    }

    public interface IBxElementCarrier :IBxModifyInfo
    {
        IBxStaticUIConfigProvider SCICProvider { get; }
        //IBxModifyInfo ModifyInfo { get; }
        // Int32 ManageElement(IBxElementSite element);
        //void RemoveElement(IBxElementSite element);
        //IBxElementSite GetElement(Int32 id);

        void AddModified(IBxModifyManage one);
        void RemoveModified(IBxModifyManage one);
    }

    public interface IBxInitCarrier
    {
        void ResetCarrier(IBxElementCarrier carrier);
    }

    public interface IBxElementSiteEx : IBxElementSite
    {
        //IBxElementCarrier Carrier { get; }
        //Int32 IDInCarrier { get; }
    }

    public interface IBxElementEx : IBxElement
    {
        //IBxElementCarrier Carrier { get; }
    }

    //public enum BxElementSiteMode
    //{
    //    Owner = 0,
    //    Refer = 1
    //}

    public interface IBxElementReferer : IBxElementSiteEx
    {
        void ReferTo(IBxReferableElement val);
    }

    public interface IBxReferableElement : IBxElementEx
    {
        void AddRefer(IBxElementReferer referer);
        void BreakRefer(IBxElementReferer referer);
        bool HasReferer { get; }
        IBxElementReferer[] Referers { get; }
        IBxElementSiteEx Owner { get; }
    }

    //public interface IBxElementSiteInit
    //{
    //    void InitCarrier(/*IBxCompound parent,*/ IBxElementCarrier carrier);
    //    void InitContainer(IBxCompound container);
    //    void InitFieldInfo(IBxCompound container, FieldInfo info);
    //    void InitStaticUIConfig(BxXmlUIItem staticItem);
    //}


    public class BxUIConfigID
    {
        string _id;
        public string FileID
        {
            get
            {
                int index = string.IsNullOrEmpty(_id) ? -1 : _id.IndexOf(',');
                if (index < 0)
                    return null;

                return _id.Substring(0, index);
            }
        }
        public string ItemID
        {
            get
            {
                int index = string.IsNullOrEmpty(_id) ? -1 : _id.IndexOf(',');
                if (index < 0)
                    return null;

                return _id.Substring(index + 1, _id.Length - index - 1);
            }
        }

        public BxUIConfigID() { _id = null; }
        public BxUIConfigID(string id) { _id = id; }
        public BxUIConfigID(string fileID, string itemID) { _id = fileID + itemID; }
        public BxUIConfigID(string fileID, int itemID) { _id = fileID + itemID.ToString(); }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }
        public override string ToString()
        {
            return _id.ToString();
        }

        public static void Split(string id, out string fileID, out string itemID)
        {
            int index = string.IsNullOrEmpty(id) ? -1 : id.IndexOf(',');
            if (index < 0)
            {
                fileID = null;
                itemID = null;
            }
            else
            {
                fileID = id.Substring(0, index);
                itemID = id.Substring(index + 1, id.Length - index - 1);
            }
        }

        public static void Split(string id, out string fileID, out int itemID)
        {
            int index = string.IsNullOrEmpty(id) ? -1 : id.IndexOf(',');
            if (index < 0)
            {
                fileID = null;
                itemID = -1;
            }
            else
            {
                fileID = id.Substring(0, index);
                itemID = int.Parse(id.Substring(index + 1, id.Length - index - 1));
            }
        }

        public static string GetFileID(string id)
        {
            int index = string.IsNullOrEmpty(id) ? -1 : id.IndexOf(',');
            if (index < 0)
                return null;
            return id.Substring(0, index);
        }

        public static int GetItemIDInt(string id)
        {
            int index = string.IsNullOrEmpty(id) ? -1 : id.IndexOf(',');
            if (index < 0)
                return -1;
            return int.Parse(id.Substring(index + 1, id.Length - index - 1));
        }

        public static string GetItemID(string id)
        {
            int index = string.IsNullOrEmpty(id) ? -1 : id.IndexOf(',');
            if (index < 0)
                return null;
            return id.Substring(index + 1, id.Length - index - 1);
        }

        public static string Combine(string fileID, string itemID)
        {
            return fileID + "," + itemID;
        }
    }


    public interface IBxStaticUIConfigProvider
    {
        BxXmlUIItem GetStaticUIConfig(string id);
    }

    public interface IBxStaticUIConfigPregnant
    {
        string ConfigID { get; }
        void InitSUICProvider(IBxStaticUIConfigProvider suicProvider);
        BxXmlUIItem StaticUIConfig { get; }
    }

    public interface IBxElementSiteInit
    {
        void InitContainer(IBxCompound container);
        //void InitSUICProvider(IBxStaticUIConfigProvider suicProvider);
        void ResetCarrier(IBxElementCarrier carrier);
        void InitSUICPregnant(IBxStaticUIConfigPregnant suicPregnant);
        //void InitUIConfigProvider(IBxUIConfigProvider provider);
        //void InitStaticUIConfigID(string configFileID, Int32 configItemID);
    }

    public interface IBxElementInit
    {
        //void InitSUICProvider(IBxStaticUIConfigProvider suicProvider);
        void ResetCarrier(IBxElementCarrier carrier);

        //void InitCarrier(/*IBxCompound parent,*/ IBxElementCarrier carrier);
        // void InitSite(IBxElementSiteEx site);
        //void OnInit();
    }

    public interface IBxElementInit_ForOldCode
    {
        void OldInit();
    }

    //public interface IBxSitePersistNode : IBxPersistNode
    //{
    //    void SaveStructure(IBxElementValue val);
    //    void OnValueRefered(IBxElementSite site, Int32 id);
    //}

    public interface IBxElementSiteVertionType
    {
        string VertionType { get; set; }
        string Version { get; set; }
    }
}
