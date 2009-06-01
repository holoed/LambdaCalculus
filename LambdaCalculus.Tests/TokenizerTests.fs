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

[<TestFixture>]
type TokenizerTests =
    new() = {}
    
    [<Test>]
    member x.Test() = Assert.Pass()

    [<Test>]
    member x.Letter() = Assert.AreEqual([Letter('x')], tokenize "x")

    [<Test>]
    member x.Symbol() = 
        Assert.AreEqual([Symbol('λ')], tokenize "λ")
        Assert.AreEqual([Symbol('.')], tokenize ".")
        Assert.AreEqual([Symbol('(')], tokenize "(")
        Assert.AreEqual([Symbol(')')], tokenize ")")
        Assert.AreEqual([WhiteSpace], tokenize " ")

    [<Test>]
    member x.Lambda() = 
        Assert.AreEqual(
            [Symbol('λ');Letter('x');Symbol('.');Letter('x')], 
            tokenize "λx.x")
        
    [<Test>]
    member x.Application() = 
        Assert.AreEqual(
            [Letter('x');WhiteSpace;Letter('y')], 
            tokenize "x y")
        Assert.AreEqual(
            [Symbol('λ');Letter('x');Symbol('.');Letter('x');WhiteSpace;Symbol('λ');Letter('y');Symbol('.');Letter('y')], 
            tokenize "λx.x λy.y")
        
