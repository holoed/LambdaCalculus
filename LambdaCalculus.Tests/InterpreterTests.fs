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
open Interpreter

[<TestFixture>]
type InterpreterTests =
    new() = {}
    
    [<Test>]
    member o.Test() = ()
    
    [<Test>]
    member o.Identity() = 
        Assert.AreEqual(
            Closure(Letter('y'), Var(Letter('y')), []),
            interpret [] (Apply (Lambda(Letter 'x', Var( Letter 'x' )), Lambda(Letter 'y', Var( Letter 'y' ))))) 
        