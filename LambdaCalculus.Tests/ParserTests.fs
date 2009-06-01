// * **********************************************************************************************
// * Copyright (c) Edmondo Pentangelo. 
// *
// * This source code is subject to terms and conditions of the Microsoft Public License. 
// * A copy of the license can be found in the License.html file at the root of this distribution. 
// * By using this source code in any fashion, you are agreeing to be bound by the terms of the 
// * Microsoft Public License.
// *
// * You must not remove this notice, or any other, from this software.
// * **********************************************************************************************

open NUnit.Framework
open Tokenizer
open Parser

let private x = Letter 'x'
let private y = Letter 'y'
let private z = Letter 'z'
let private λ = Symbol 'λ'

[<TestFixture>]
type ParserTests = 
    new() = {}
        
    [<Test>]
    member o.Test() = ()
    
    [<Test>]
    member o.Var() = Assert.AreEqual(Var x,  parse [x])
    
    [<Test>]
    member o.Lambda() = Assert.AreEqual(
                          Lambda(x, Var x), 
                          parse [λ;x;Symbol('.');x])

    [<Test>]
    member o.Application() = Assert.AreEqual(
                                Apply(Var x, Var y), 
                                parse [x;WhiteSpace;y])
                             Assert.AreEqual(
                                Apply(Apply(Var x, Var y), Var z), 
                                parse [x;WhiteSpace;y;WhiteSpace;z])
                             Assert.AreEqual(
                                Apply(Lambda(x, Var x), Lambda(y, Var y)), 
                                parse [Symbol('(');λ;x;Symbol('.');x;Symbol(')');WhiteSpace;Symbol('(');λ;y;Symbol('.');y;Symbol(')')])
