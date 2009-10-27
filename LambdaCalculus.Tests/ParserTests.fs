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

namespace LambdaCalculus.Tests
module ParserTests = 

    open NUnit.Framework
    open LambdaCalculus.Tokenizer
    open LambdaCalculus.Ast
    open LambdaCalculus.Parser

    let private f = Letter 'f'
    let private x = Letter 'x'
    let private y = Letter 'y'
    let private z = Letter 'z'
    let private n = Letter 'n'
    let private λ = Symbol 'λ'


    let parse x = x |> tokenize |> parse

    [<TestFixture>]
    type ParserTests = 
        new() = {}
            
        [<Test>]
        member o.Test() = ()
        
        [<Test>]
        member o.Var() = Assert.AreEqual(Var x,  parse "x")
        
        [<Test>]
        member o.Lambda() = Assert.AreEqual(
                              Lambda(x, Var x), 
                              parse "λx.x")

        [<Test>]
        member o.Application() = Assert.AreEqual(
                                    Apply(Var x, Var y), 
                                    parse "x y")
                                 Assert.AreEqual(
                                    Apply(Apply(Var x, Var y), Var z), 
                                    parse "x y z")
                                    
        [<Test>]
        member o.ApplicationWithParenthesis() = 
                                 Assert.AreEqual(
                                    Apply(Var f, Apply(Var f, Var x)), 
                                    parse "f (f x)")

        [<Test>]
        member o.LambdaInsideApply() = 
                                 Assert.AreEqual(
                                    Apply(Lambda(x, Var x), Lambda(y, Var y)), 
                                    parse "(λx.x) (λy.y)")

        [<Test>]
        member o.ChurchZero() = 
                                 Assert.AreEqual(
                                    Lambda(f, Lambda(x, Var(x))), 
                                    parse "λf.λx.x")

        [<Test>]
        member o.ChurchOne() = 
                                 Assert.AreEqual(
                                    Lambda(f, Lambda(x, Apply(Var(f), Var(x)))), 
                                    parse "λf.λx.(f x)")
                                    
        [<Test>]
        member o.ChurchTwo() =   Assert.AreEqual(
                                    Lambda(f, Lambda(x, Apply(Var(f), Apply(Var(f), Var(x))))), 
                                    parse "λf.λx.(f (f x))")
                                    
        [<Test>]
        member o.Succ() =        Assert.AreEqual(
                                    Lambda(n, 
                                        Lambda(f, 
                                            Lambda(x, 
                                                Apply(Var(f), Apply(Apply(Var n, Var f), Var x))))), 
                                    parse "λn.λf.λx.(f (n f x))")
