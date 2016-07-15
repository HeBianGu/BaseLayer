#region Copyright 2001-2003 Christoph Daniel R�egg [Modified BSD License]
/*
Math.NET, a symbolic math library
Copyright (c) 2001-2003, Christoph Daniel Rueegg, http://cdrnet.net/. All rights reserved.

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice,
this list of conditions and the following disclaimer. 

2. Redistributions in binary form must reproduce the above copyright notice,
this list of conditions and the following disclaimer in the documentation
and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE
LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF
THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion

using System;
using cdrnet.Lib.MathLib.Core;
using cdrnet.Lib.MathLib.Exceptions;

namespace cdrnet.Lib.MathLib.Scalar
{
	[ParsingObject(ParsingObjectType.Function,250,Character="infinity")]
	public class ConstantInfinity: ScalarOperator
	{
		protected EmptyParameters parameters;
		public ConstantInfinity(Context context, EmptyParameters parameters): base(context)
		{
			this.parameters = parameters;
			Init(parameters);
		}
		public ConstantInfinity(Context context): base(context)
		{
			this.parameters = new EmptyParameters();
			Init(parameters);
		}
		public override double Calculate()
		{
			return double.PositiveInfinity;
		}
		public override bool IsConstant
		{
			get {return true;}
		}
		public override IScalarExpression Simplify()
		{
			return this;
		}
		public override IScalarExpression Differentiate(ScalarExpressionVariable var)
		{
			return ScalarExpressionValue.Zero(context);
		}
		public override IScalarExpression Integrate(ScalarExpressionVariable var)
		{
			return new ScalarMultiplication(context,this,var);
		}
	}
}
