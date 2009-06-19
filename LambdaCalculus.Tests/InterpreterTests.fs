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

open NUnit.Framework
open Interpreter

[<TestFixture>]
type InterpreterTests =
    new() = {}

    [<Test>]
    member o.NoOp() = 
        Assert.AreEqual("(λy.y)", interpret "(λy.y)")
    
    [<Test>]
    member o.Identity() = 
        Assert.AreEqual("(λy.y)", interpret "(λx.x) (λy.y)")
        
    [<Test>]
    member o.Succ() = 
        Assert.AreEqual("(λn.(λf.(λx.(f ((n f) x)))))", interpret "(λn.λf.λx.(f (n f x)))")
        
    [<Test>]
    member o.SuccOne() = 
        Assert.AreEqual("(λf.(λx.(f (f x))))", interpret "(λn.λf.λx.(f (n f x))) (λf.λx.(f x))")

    [<Test>]
    member o.SuccTwo() = 
        Assert.AreEqual("(λf.(λx.(f (f (f x)))))", interpret "(λn.λf.λx.(f (n f x))) (λf.λx.(f (f x)))")           

    [<Test>]
    member o.SuccThree() = 
        Assert.AreEqual("(λf.(λx.(f (f (f (f x))))))", interpret "(λn.λf.λx.(f (n f x))) (λf.λx.(f (f (f x))))") 
 
    [<Test>] 
    member o.OnePlusZero() = 
        Assert.AreEqual("(λf.(λx.(f x)))", interpret "(λm.λn.λf.λx.((m f) (n f x))) (λf.λx.(f x)) (λf.λx.x)") 
                     
    [<Test>] 
    member o.OnePlusOne() = 
        Assert.AreEqual("(λf.(λx.(f (f x))))", interpret "(λm.λn.λf.λx.((m f) (n f x))) (λf.λx.(f x)) (λf.λx.(f x))") 
              
    [<Test>] 
    member o.TwoPlusOne() = 
        Assert.AreEqual("(λf.(λx.(f (f (f x)))))", interpret "(λm.λn.λf.λx.((m f) (n f x))) (λf.λx.(f (f x))) (λf.λx.(f x))")         
        
    [<Test>] 
    member o.ThreePlusTwo() = 
        Assert.AreEqual("(λf.(λx.(f (f (f (f (f x)))))))", interpret "(λm.λn.λf.λx.((m f) (n f x))) (λh.λx.(h (h x))) (λg.λx.(g (g (g x))))")  
        
    [<Test>]
    member o.TwoMultiplyByThree() =
        Assert.AreEqual("(λf.(λx.(f (f (f (f (f (f x))))))))", interpret "(λm.λn.λf.(n (m f))) (λh.λx.(h (h x))) (λg.λx.(g (g (g x))))")       