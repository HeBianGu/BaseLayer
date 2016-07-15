using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPT.Product.Base
{
    enum EFormulaElementType
    {
        invalid = -1,
        number = 0, // 数字
        variant,//自变量
        unary, // unary operator 一元算符
        binary, // binary operator  2元算符
        temary,  // temary operator  3元算符
        leftBrace,   //左括号 
        rightBrace  // 
    }

    interface IFormulaElement
    {
        EFormulaElementType ElementType { get; }
        bool CanFollowedBy(EFormulaElementType follow);
    }

    interface IFEOperator : IFormulaElement
    {   
        double Calculate(params double[] input);
        //IFEOperator Clone();
    }

    interface IFEUnary : IFEOperator
    {
        double Calculate(double param);
    }

    enum EFEOperatorGrade
    {
        grade0, // +,-,self,end,
        grade1, // *,/,
        grade2,  // ^,
        grade3,  // 
        grade4,  // (,
        gradeMax  // ^,
    }

    interface IFEBinary : IFEOperator
    {
        double Calculate(double param1, double param2);
        EFEOperatorGrade Grade { get; }
    }


}
