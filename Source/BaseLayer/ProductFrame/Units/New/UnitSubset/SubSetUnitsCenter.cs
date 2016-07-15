using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public class BxSubSetUnitsCenter : IBxUnitsCenter
    {
        List<IBxUnitCategory> _cates = null;
        IBxUnitsCenter _baseUnitsCenter;

        public BxSubSetUnitsCenter(IBxUnitsCenter baseUnitsCenter)
        {
            _baseUnitsCenter = baseUnitsCenter;
        }

        public void LoadUnitConfigFile(string sFilePath)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(sFilePath);
                XmlElement root = doc.DocumentElement;
                XmlNodeList cateNodes = root.SelectNodes("Category");

                List<IBxUnitCategory>  cates = new List<IBxUnitCategory>(cateNodes.Count);

                BxSubSetUnitCategory cate;
                IBxUnitCategory baseCate;
                string id,name;
                foreach (XmlElement one in cateNodes)
                {
                    id = one.GetAttribute("id");
                    name = one.GetAttribute("help");
                    if (string.IsNullOrWhiteSpace(name))
                        name = id;
                    baseCate = _baseUnitsCenter.Parse(id);
                    cate = new BxSubSetUnitCategory(baseCate,name);
                    cate.LoadUnitConfigNode(one);
                    cates.Add(cate);
                }
                cates.Sort((x, y) => string.Compare(x.Name, y.Name));
                _cates = cates;
            }
            catch (System.Exception) { }
        }

        #region IBxUnitsCenter 成员

        public IEnumerable<IBxUnitCategory> Categories{ get { return _cates; } }
        public IBxUnitCategory Parse(string categoryID)
        {
            IBxUnitCategory cate = _cates.Find(x => x.ID == categoryID); 
            if(cate == null)
                cate = _baseUnitsCenter.Parse(categoryID);
            return cate;
        }

        public IBxUnitCategory Find(string categoryCode)
        {
            IBxUnitCategory cate = _cates.Find(x => x.Code == categoryCode);
            if (cate == null)
                cate = _baseUnitsCenter.Find(categoryCode);

            return cate;
        }

        #endregion
    }



}
