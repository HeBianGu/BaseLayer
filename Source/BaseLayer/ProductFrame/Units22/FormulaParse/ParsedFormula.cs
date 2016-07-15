using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    //public interface IBxUnitConvert : IBLPersistXmlNode
    //{
    //    double Calc(double var);
    //}
    //public interface IBxFormula : IBxUnitConvert
    //{
    //}


    class BUParsedItem : IBxUnitConvert
    {
        public IBxUnitConvert left = null;
        public IBxUnitConvert right = null;
        public IFEBinary mid = null;

        public BUParsedItem() { }
        public BUParsedItem(IBxUnitConvert leftParam, IFEBinary midOperater, IBxUnitConvert rightParam)
        {
            left = leftParam;
            mid = midOperater;
            right = rightParam;
        }

        #region IBxUnitConvert 成员
        public double Calc(double var)
        {
            return mid.Calculate(left.Calc(var), right.Calc(var));
        }
        //public void Save(XmlElement node, BCPersistHelp help)
        //{
        //    help.DTF.SaveTypeInfo(typeof(BUParsedItem), node);

        //    node.SetAttribute("_operator_", mid.ToString());
        //    XmlElement leftNode = node.EMAddChild("_left_");
        //    XmlElement rightNode = node.EMAddChild("_right_");
        //    left.Save(leftNode, help);
        //    right.Save(rightNode, help);
        //}
        //public void Load(XmlElement node, BCPersistHelp help)
        //{
        //    string sOperator = node.GetAttribute("_operator_");
        //    mid = BUOperators.Parse(sOperator);
        //    XmlElement leftNode = (XmlElement)node.SelectSingleNode("_left_");
        //    XmlElement rightNode = (XmlElement)node.SelectSingleNode("_right_");
        //    left = (IBxUnitConvert)help.DTF.CreateObject(leftNode);
        //    left.Load(leftNode, help);
        //    right = (IBxUnitConvert)help.DTF.CreateObject(rightNode);
        //    right.Load(rightNode, help);
        //}
        #endregion

        #region IBLPersistXmlNode 成员
        public void SaveXml(XmlElement node)
        {
            throw new NotImplementedException();
        }
        public void LoadXml(XmlElement node)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    class BUParsedNumber : IBxUnitConvert
    {
        private double _value;
        public double Value { get { return _value; } }
        public BUParsedNumber(double var) { _value = var; }

        #region IBxUnitConvert 成员
        public double Calc(double var) { return _value; }
        //public void Save(XmlElement node, BCPersistHelp help)
        //{
        //    help.DTF.SaveTypeInfo(typeof(BUParsedNumber), node);
        //    node.SetAttribute("_value_", _value.ToString());
        //}
        //public void Load(XmlElement node, BCPersistHelp help)
        //{
        //    string sVar = node.GetAttribute("_value_");
        //    _value = Convert.ToDouble(sVar);
        //}
        #endregion

        #region IBLPersistXmlNode 成员

        public void SaveXml(XmlElement node)
        {
            throw new NotImplementedException();
        }

        public void LoadXml(XmlElement node)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    class BUParsedVariant : IBxUnitConvert
    {
        #region IBxUnitConvert 成员
        public double Calc(double var) { return var; }
        //public void Save(XmlElement node, BCPersistHelp help)
        //{
        //    help.DTF.SaveTypeInfo(typeof(BUParsedVariant), node);
        //}
        //public void Load(XmlElement node, BCPersistHelp help)
        //{
        //}
        #endregion

        #region IBLPersistXmlNode 成员

        public void SaveXml(XmlElement node)
        {
            throw new NotImplementedException();
        }

        public void LoadXml(XmlElement node)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class ParsedFormula : IBxFormula
    {
        static public ParsedFormula s_sameUnitFormula;
        static ParsedFormula()
        {
            s_sameUnitFormula = new ParsedFormula(new BUParsedVariant());
        }

        static bool ContainsVariant(IBxUnitConvert item)
        {
            if (item == null)
                return false;
            if (item is BUParsedVariant)
                return true;
            if (item is BUParsedItem)
            {
                if (ContainsVariant(((BUParsedItem)item).left))
                    return true;
                return ContainsVariant(((BUParsedItem)item).right);
            }
            return false;
        }
        static IBxUnitConvert Contact(IBxUnitConvert left, IFEBinary mid, IBxUnitConvert right)
        {
            return new BUParsedItem(left, mid, right);
        }
        static IBxUnitConvert _Inverse(IBxUnitConvert Y, IBxUnitConvert X)
        {
            if (X is BUParsedVariant)
                return Y;

            if (!(X is BUParsedItem))
                throw new Exception("等式中无自变量!\n");

            BUParsedItem xItem = (BUParsedItem)X;
            //自变量包含在X的左侧
            if (ContainsVariant(xItem.left))
            {
                if (ContainsVariant(xItem.right))
                    throw new Exception("目前不能反转超过一个自变量的等式!\n");

                X = xItem.left;
                if (xItem.mid == BUOperators.s_binaryAdd)
                {
                    Y = Contact(Y, BUOperators.s_binarySubtract, xItem.right);
                }
                else if (xItem.mid == BUOperators.s_binarySubtract)
                {
                    Y = Contact(Y, BUOperators.s_binaryAdd, xItem.right);
                }
                else if (xItem.mid == BUOperators.s_binaryMultiply)
                {
                    Y = Contact(Y, BUOperators.s_binaryDivide, xItem.right);
                }
                else if (xItem.mid == BUOperators.s_binaryDivide)
                {
                    Y = Contact(Y, BUOperators.s_binaryMultiply, xItem.right);
                }
                else
                    throw new Exception();
            }
            else //自变量包含在X的右侧
            {
                if (!ContainsVariant(xItem.right))
                    throw new Exception("等式中无自变量!\n");

                X = xItem.right;
                if (xItem.mid == BUOperators.s_binaryAdd)
                {
                    Y = Contact(Y, BUOperators.s_binarySubtract, xItem.left);
                }
                else if (xItem.mid == BUOperators.s_binarySubtract)
                {
                    Y = Contact(xItem.left, BUOperators.s_binarySubtract, Y);
                }
                else if (xItem.mid == BUOperators.s_binaryMultiply)
                {
                    Y = Contact(Y, BUOperators.s_binaryDivide, xItem.left);
                }
                else if (xItem.mid == BUOperators.s_binaryDivide)
                {
                    Y = Contact(xItem.left, BUOperators.s_binaryDivide, Y);
                }
                else
                    throw new Exception();
            }

            return _Inverse(Y, X);
        }
        static IBxUnitConvert Inverse(IBxUnitConvert root)
        {
            BUParsedVariant var = new BUParsedVariant();
            return _Inverse(var, root);
        }
        public static ParsedFormula Inverse(ParsedFormula formula)
        {
            return new ParsedFormula(Inverse(formula.m_rootItem));
        }
        static ParsedFormula Parse(string formula, string variantName)
        {
            FormulaParser parser = new FormulaParser();
            parser.Formula = formula;
            parser.Variant = variantName;
            IBxUnitConvert root = parser.Parse();
            return (root == null) ? null : new ParsedFormula(root);
        }
        protected IBxUnitConvert m_rootItem;
        public ParsedFormula() { m_rootItem = null; }
        public ParsedFormula(IBxUnitConvert root) { m_rootItem = root; }

        public double Calc(double var) { return m_rootItem.Calc(var); }

        #region IBCPersistXml 成员

        //public void Save(XmlElement node, BCPersistHelp help)
        //{
        //    help.DTF.SaveTypeInfo(typeof(ParsedFormula), node);
        //    XmlElement rootItemNode = node.EMAddChild("_rootItem_");
        //    m_rootItem.Save(rootItemNode, help);
        //}

        //public void Load(XmlElement node, BCPersistHelp help)
        //{
        //    XmlElement rootItemNode = (XmlElement)node.SelectSingleNode("_rootItem_");
        //    m_rootItem = (IBxUnitConvert)help.DTF.CreateObject(rootItemNode);
        //    m_rootItem.Load(rootItemNode, help);
        //}

        #endregion

        #region IBLPersistXmlNode 成员

        public void SaveXml(XmlElement node)
        {
            throw new NotImplementedException();
        }

        public void LoadXml(XmlElement node)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    class CombinedFormula : IBxFormula
    {
        IBxFormula m_fml1 = null;
        IBxFormula m_fml2 = null;

        public CombinedFormula(){}
        public CombinedFormula(IBxFormula fml1,IBxFormula fml2)
        {
            m_fml1 = fml1;
            m_fml2 = fml2;
        }

        #region IBxUnitConvert 成员
        public double Calc(double var)
        {
            return m_fml1.Calc(m_fml2.Calc(var));
        }

        #endregion
        #region IBCPersistXml 成员
        //public void Save(XmlElement node, BCPersistHelp help)
        //{
        //    help.DTF.SaveTypeInfo(typeof(CombinedFormula), node);

        //    XmlElement node1 = node.EMAddChild("_formula1_");
        //    XmlElement node2 = node.EMAddChild("_formula2_");
        //    m_fml1.Save(node1, help);
        //    m_fml2.Save(node2, help);
        //}

        //public void Load(XmlElement node, BCPersistHelp help)
        //{
        //    XmlElement node1 = (XmlElement)node.SelectSingleNode("_formula1_");
        //    XmlElement node2 = (XmlElement)node.SelectSingleNode("_formula2_");
        //    m_fml1 = (IBxFormula)help.DTF.CreateObject(node1);
        //    m_fml1.Load(node1, help);
        //    m_fml2 = (IBxFormula)help.DTF.CreateObject(node2);
        //    m_fml2.Load(node1, help);
        //}
        #endregion

        #region IBLPersistXmlNode 成员

        public void SaveXml(XmlElement node)
        {
            throw new NotImplementedException();
        }

        public void LoadXml(XmlElement node)
        {
            throw new NotImplementedException();
        }

        #endregion
    }


}
