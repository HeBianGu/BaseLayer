using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace OPT.PEOffice6.BaseLayer.Base
{
    public interface IBxElementCarrier
    {
        IBxUIConfigProvider UIConfigProvider { get; }
        Int32 ManageElement(IBxElementSite1 element);
        void RemoveElement(IBxElementSite1 element);
        IBxElementSite1 GetElement(Int32 id);
    }

    public interface IBxElementSite1 : IBxElement
    {
        IBxElementCarrier Carrier { get; }
        Int32 IDInCarrier { get; }
        //IBxCompound Container { get; }
    }

    public interface IBxElementSiteInit
    {
        void BindToParent(/*IBxCompound parent,*/ IBxElementCarrier carrier);
        void InitFieldInfo(FieldInfo info);
    }

    public enum BxElementSiteMode
    {
        Owner = 0,
        Refer = 1
    }

    public interface IBxElementSite : IBxElement
    {
        BxElementSiteMode SiteMode { get; }
        //IBxElementValue ReferTo { get; set; }
        void ReferTo(IBxElementValue val);
    }

    public interface IBxElementValue /*: IBxPersistStorageNode*/
    {
        void AddRefer(IBxElementSite site);
        void BreakRefer(IBxElementSite site);
        IBxElementEx Owner { get; }
        bool HasReferer { get; }
        IBxElementSite[] Referer { get; }
    }



    //public interface IBxSitePersistNode : IBxPersistNode
    //{
    //    void SaveStructure(IBxElementValue val);
    //    void OnValueRefered(IBxElementSite site, Int32 id);
    //}


}
