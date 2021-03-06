﻿// * **********************************************************************************************
// * Copyright (c) Edmondo Pentangelo. 
// *
// * This source code is subject to terms and conditions of the Microsoft Public License. 
// * A copy of the license can be found in the License.html file at the root of this distribution. 
// * By using this source code in any fashion, you are agreeing to be bound by the terms of the 
// * Microsoft Public License.
// *
// * You must not remove this notice, or any other, from this software.
// * **********************************************************************************************

namespace LambdaCalculus.Tests
module NumbersTests =

    open NUnit.Framework
    open LambdaCalculus.Numbers
    open LambdaCalculus.Interpreter
    open LambdaCalculus.AstToCode

    [<TestFixture>]
    type NumberTests = 
        new() = {}
        
        [<Test>]
        member o.Zero() = 
            "λf.λx.x" 
            |> interpret
            |> toNumber
            |> fun actual -> Assert.AreEqual(0, actual)
            
        [<Test>]
        member o.One() = 
            "λf.λx.(f x)" 
            |> interpret
            |> toNumber
            |> fun actual -> Assert.AreEqual(1, actual)
            
        [<Test>]
        member o.Two() = 
            "λf.λx.(f (f x))" 
            |> interpret
            |> toNumber
            |> fun actual -> Assert.AreEqual(2, actual)
            
        [<Test>]
        member o.Three() = 
            "λf.λx.(f (f (f x)))" 
            |> interpret
            |> toNumber
            |> fun actual -> Assert.AreEqual(3, actual)
            
        [<Test>]
        member o.Seven() = 
            "λf.λx.(f (f (f (f (f (f (f x)))))))" 
            |> interpret
            |> toNumber
            |> fun actual -> Assert.AreEqual(7, actual)
                    
        [<Test>]
        [<ExpectedException>]
        member o.NaN() = 
            "λx.x" 
            |> interpret
            |> toNumber
            |> ignore
            
        [<Test>]
        member o.From0() =
            0
            |> FromNumber
            |> toNumber
            |> fun actual -> Assert.AreEqual(0, actual)
            
        [<Test>]
        member o.From1() =
            1
            |> FromNumber
            |> toNumber
            |> fun actual -> Assert.AreEqual(1, actual)
            
        [<Test>]
        member o.From2() =
            2
            |> FromNumber
            |> toNumber
            |> fun actual -> Assert.AreEqual(2, actual)

        [<Test>]
        member o.From7() =
            7
            |> FromNumber
            |> toNumber
            |> fun actual -> Assert.AreEqual(7, actual)                        
        